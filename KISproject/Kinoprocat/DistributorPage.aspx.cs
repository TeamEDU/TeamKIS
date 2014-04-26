using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using KISproject.Code.Kinoprocat;
using Controllers;

namespace KISproject.Kinoprocat
{
    public partial class DistributorPage : System.Web.UI.Page
    {
        private DistributorController dController;

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

            dController = new DistributorController();
        }

        // Обработчик события смены выбранного индека у элемента, т.е смена элемента.
        // Получает выделенный объект в таблице GridView.
        protected void GridViewDistribution_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Делает кнопки доступным для пользователя.
            EnabledButtons(true);

            // Это работает за счет метода расширения, который определенн
            // в статическом классе ExtMeths
            ExtDistributor selectedDistributor = GridViewDistributors.SelectedRow.ConvertToExtDistributor();

            // Сохранить выделенный объект в состоянии представления (Request)
            ViewState["SelectedDistributor"] = selectedDistributor;
        }

        // Обработчик события кнопки "Подтвердить".
        protected void btnOk_Click(object sender, EventArgs e)
        {
            // Получить значение isAddingItem из состоянии представления.
            bool isAddingItem = (bool)ViewState["isAddingItem"];

            // Нажата кнопка "Добавить"?
            if (isAddingItem)
            {
                // Добавляем нового дистрибьютора
                ExtDistributor extDistributor = new ExtDistributor(
                    new Distributor(txtBoxName.Text),
                    new Contact(txtBoxPhone.Text, txtBoxEmail.Text,
                    txtBoxAddress.Text));

                int result = dController.addDistributor(extDistributor);

                // Произошел сбой?
                if (result == -1)
                {
                    ShowPopUpMsg("Ошибка соединения или обращения к БД!");
                }
            }
            else // следовательно нажата кнопка "Изменить".
            {
                ExtDistributor selectedDistributor = (ExtDistributor)ViewState["SelectedDistributor"];

                // Извлекаем новые данные из форм.
                selectedDistributor.Distributor.Name = txtBoxName.Text;
                selectedDistributor.Contact.Phone = txtBoxPhone.Text;
                selectedDistributor.Contact.Email = txtBoxEmail.Text;
                selectedDistributor.Contact.Address = txtBoxAddress.Text;
            
                // Изменяем текущего дистрибьютора
                bool result = dController.updateDistributor(selectedDistributor);

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
            // Извлекаем выделенный объект из состоянии представления
            ExtDistributor selectedDistributor = (ExtDistributor)ViewState["SelectedDistributor"];
            ViewState["isAddingItem"] = false;

            // Остановить AJAX обновления.
            ProductTimer.Enabled = false;

            // Записать значения из выбранной строки (т.е объекта)
            // на формы.
            WriteDataToControls(selectedDistributor);

            // Отобразить pop-up окно.
            ModalPopupWindow.Show();
        }

        // Обработчик события кнопки "Удалить".
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            // Заблокировать доступ к кнопкам "Удалить" и "Изменить".
            EnabledButtons(false);

            // Из выделенной строки получаем значения
            ExtDistributor selectedDistributor = (ExtDistributor)ViewState["SelectedDistributor"];
            
            string message = dController.removeDistributor(selectedDistributor);
            if (message != "success")
            {
                ShowPopUpMsg(message);
                EnabledButtons(true);
            }
        }

        protected void ProductTimer_Tick(object sender, EventArgs e)
        {
            // Осуществить привязку данных.
            GridViewDistributors.DataBind();
        }

        // Управляет доступам к кнопкам.
        private void EnabledButtons(bool enabled)
        {
            btnEdit.Enabled = enabled;
            btnDelete.Enabled = enabled;
        }

        private void ShowPopUpMsg(string msg)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("alert('");
            sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"));
            sb.Append("');");
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "showalert", sb.ToString(), true);
        }

        // Пишет данные полученные с параметра в контролы 
        // (на формы TextBox панели PanelAddEditDistributor).
        private void WriteDataToControls(ExtDistributor selectedDistributor)
        {
            txtBoxName.Text = selectedDistributor.Distributor.Name;
            txtBoxPhone.Text = selectedDistributor.Contact.Phone;
            txtBoxEmail.Text = selectedDistributor.Contact.Email;
            txtBoxAddress.Text = selectedDistributor.Contact.Address;
        }
        
        // Очищает данные с контролов.
        private void WriteDataToControls()
        {
            txtBoxName.Text = "";
            txtBoxPhone.Text = "";
            txtBoxEmail.Text = "";
            txtBoxAddress.Text = "";
        }
    }
}