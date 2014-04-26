using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KISproject.Code.Kinoprocat;
using Controllers;
using Microsoft.Ajax.Utilities;
using MKB.TimePicker;

namespace KISproject.Kinoprocat
{
    public partial class MoviesPage : System.Web.UI.Page
    {
        private MovieController mController;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Если пользователь не имеет должности (роли) кинопрокатчика
            // то перенаправляем его на страницу выход.
            if (!User.IsInRole("Kinoprocat"))
            {
                // Запретить доступ к этой странице.
                // В замен переадресовать на страницу входа.
                Response.Redirect("~/Login.aspx");
            }

            mController = new MovieController();
        }

        // Обработчик события смены выбранного индека у элемента, т.е смена элемента.
        // Получает выделенный объект в таблице GridView.
        protected void GridViewMovies_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Делает кнопки доступным для пользователя.
            EnabledButtons(true);

            // Это работает за счет метода расширения, который определенн
            // в статическом классе ExtMeths.
            ExtMovie selectedMovie = GridViewMovies.SelectedRow.ConvertToExtMovie();

            // Сохранить выделенный объект в состоянии представления.
            ViewState["SelectedMovie"] = selectedMovie;
        }

        // Обработчик события кнопки "Добавить"
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            // Получить значение isAddingItem из состоянии представления.
            ViewState["isAddingItem"] = true;

            // Остановить AJAX обновления.
            ProductTimer.Enabled = false;

            // Очистить контролы TextBox от ранее введенной информации.
            WriteDataToControls();

            // Отобразить popup окно.
            ModalPopupWindow.Show();
        }

        // Обработчик события кнопки "Изменить".
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            // Извлекаем выделенный объект из состоянии представления.
            ExtMovie selectedMovie = (ExtMovie)ViewState["SelectedMovie"];
            ViewState["isAddingItem"] = false;

            // Остановить AJAX обновления.
            ProductTimer.Enabled = false;

            // Записать значения из выбранной строки (т.е объекта)
            // на формы.
            WriteDataToControls(selectedMovie);

            // Отобразить pop-up окно.
            ModalPopupWindow.Show();
        }
        
        // Обработчик события кнопки "Удалить".
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            // Заблокировать доступ к кнопкам "Удалить" и "Изменить".
            EnabledButtons(false);

            // Из выделенной строки получаем значения
            ExtMovie selectedMovie = (ExtMovie)ViewState["SelectedMovie"];

            string message = mController.removeMovie(selectedMovie);
            if (message != "success")
            {
                ShowPopUpMsg(message);
                EnabledButtons(true);
            }
        }

        protected void ProductTimer_Tick(object sender, EventArgs e)
        {
            // Осуществить привязку данных.
            GridViewMovies.DataBind();
        }

        // Обработчик события кнопки "Подтвердить".
        protected void btnOk_Click(object sender, EventArgs e)
        {
            // Получить значение isAddingItem из состоянии представления.
            bool isAddingItem = (bool)ViewState["isAddingItem"];

            // Нажата кнопка "Добавить"?
            if (isAddingItem)
            {
                // Извлечь данные с контролов
                ExtMovie extMovie = BuildExtMovieFromControls();

                int result = mController.addMovie(extMovie);

                // Произошел сбой?
                if (result == -1)
                {
                    ShowPopUpMsg("Ошибка соединения или обращения к БД!");
                }
            }
            else // следовательно нажата кнопка "Изменить".
            {
                ExtMovie selectedMovie = (ExtMovie)ViewState["SelectedMovie"];

                ExtMovie newMovie = BuildExtMovieFromControls();
                newMovie.Movie_id = selectedMovie.Movie_id;

                bool result = mController.updateMovie(newMovie);

                if (!result)
                {
                    ShowPopUpMsg("Ошибка соединения или обращения к БД!");
                }
            }

            // Запускаем таймер
            ProductTimer.Enabled = true;
        }

        // Обработчик события  кнопки "Отмена" в pop-up окне.
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            // Возобновить AJAX обновления.
            ProductTimer.Enabled = true;
        }

        private void ShowPopUpMsg(string msg)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("alert('");
            sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"));
            sb.Append("');");
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showalert", sb.ToString(), true);
        }

        // Создает объект типа ExtMovie на основе данных с контролов.
        private ExtMovie BuildExtMovieFromControls()
        {
            // Получаем длительность фильма.
            TimeSpan duration = new TimeSpan(TimeSelectorDuration.Hour,
                    TimeSelectorDuration.Minute, TimeSelectorDuration.Second);
            // Получаем дату выхода фильма.
            DateTime rDate = DateTime.Parse(txtBoxReleaseDate.Text);

            // Добавляем новый фильм
            Movie movie = new Movie(txtBoxTitle.Text, rDate.ToString("yyyy-MM-dd"),
                txtBoxGenre.Text, duration, txtBoxActors.Text, txtBoxAge.Text,
                txtBoxCountry.Text, txtBoxDirector.Text, DDListDisributor.SelectedValue);

            /*
             * DDListDisributor.SelectedValue или DDListDisributor.SelectedItem.Value 
             * - это id дистрибьютора.
             * DDListDisributor.SelectedItem.Text - это имя дистрибьютора.
             */
            Distributor distributor = new Distributor(DDListDisributor.SelectedItem.Text);

            ExtMovie tempMovie = new ExtMovie(movie, distributor);

            return tempMovie;
        }

        // Пишет данные полученные с параметра в контролы (панели PanelAddEditMovie).
        private void WriteDataToControls(ExtMovie extMovie)
        {
            txtBoxTitle.Text = extMovie.Movie.Title;
            txtBoxGenre.Text = extMovie.Movie.Genre;

            DateTime dTime = DateTime.Parse(extMovie.Movie.ReleaseDate);
            txtBoxReleaseDate.Text = dTime.ToString("dd.MM.yyyy");

            TimeSelectorDuration.SetTime(extMovie.Movie.Duration.Hours,
               extMovie.Movie.Duration.Minutes, extMovie.Movie.Duration.Seconds,
               TimeSelector.AmPmSpec.AM);

            txtBoxActors.Text = extMovie.Movie.Actors;
            txtBoxAge.Text = extMovie.Movie.Age;
            txtBoxCountry.Text = extMovie.Movie.Country;
            txtBoxDirector.Text = extMovie.Movie.Director;
            DDListDisributor.SelectedIndex = DDListDisributor.Items.IndexOf(
                DDListDisributor.Items.FindByValue(extMovie.Movie.Distributors_id));
        }

        // Очищает данные с контролов.
        private void WriteDataToControls()
        {
            txtBoxTitle.Text = "";
            txtBoxReleaseDate.Text = "";
            txtBoxGenre.Text = "";

            TimeSelectorDuration.SetTime(1, 0, 0,TimeSelector.AmPmSpec.AM);

            txtBoxActors.Text = "";
            txtBoxAge.Text = "";
            txtBoxCountry.Text = "";
            txtBoxDirector.Text = "";
            DDListDisributor.SelectedIndex = 0;
        }

        private void EnabledButtons(bool enabled)
        {
            btnEdit.Enabled = enabled;
            btnDelete.Enabled = enabled;
        }
    }
}