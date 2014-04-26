using System;
using System.Globalization;
using System.Web.UI.WebControls;
using KISproject.Code.Kinoprocat;

// Статический класс, в котором определены методы расширения.
// Это необходимо для расширения функционала объектов стандартных типов.
// Т.е преобразует выделенную строку данных типа GridViewRow в тип ExtDistributor.
static class ExtMeths
{
    public static ExtDistributor ConvertToExtDistributor(this GridViewRow row)
    {
        ExtDistributor extDistributor = new ExtDistributor();

        // [0] - это select в DataGrid
        extDistributor.Distributor_id = Convert.ToInt32(row.Cells[1].Text);
        extDistributor.Distributor.Contacts_id = Convert.ToInt32(row.Cells[2].Text);
        extDistributor.Distributor.Name = row.Cells[3].Text;
        extDistributor.Contact.Phone = row.Cells[4].Text;
        extDistributor.Contact.Email = row.Cells[5].Text;
        extDistributor.Contact.Address = row.Cells[6].Text;

        return extDistributor;
    }

    public static ExtMovie ConvertToExtMovie(this GridViewRow row)
    {
        ExtMovie extMovie = new ExtMovie();

        // [0] - это select в DataGrid
        extMovie.Movie_id = Convert.ToInt32(row.Cells[1].Text);
        extMovie.Movie.Distributors_id = row.Cells[2].Text;
        extMovie.Movie.Title = row.Cells[3].Text;
        
        DateTime rDate = DateTime.Parse(row.Cells[4].Text);
        extMovie.Movie.ReleaseDate = rDate.ToString("yyyy-MM-dd");

        extMovie.Movie.Genre = row.Cells[5].Text;

        TimeSpan duration;
        TimeSpan.TryParse(row.Cells[6].Text, out duration);
        extMovie.Movie.Duration = duration;

        extMovie.Movie.Actors = row.Cells[7].Text;
        extMovie.Movie.Age = row.Cells[8].Text;
        extMovie.Movie.Country = row.Cells[9].Text;
        extMovie.Movie.Director = row.Cells[10].Text;
        extMovie.Distributor.Name = row.Cells[11].Text;

        return extMovie;
    }
}