using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using KISproject.Code.Kinoprocat;

// Статический класс, в котором определены методы расширения.
// Это необходимо для расширения функционала объектов стандартных типов.
// Т.е преобразует выделенную строку данных типа GridViewRow в тип ExtDistributor.
static class ExtMeths
{
    public static ExtDistributor ConvertTo(this GridViewRow row)
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
}