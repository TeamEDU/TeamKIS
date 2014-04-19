using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KISproject
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Login1_LoggedIn(object sender, EventArgs e)
        {
            // Перенаправляет пользователя на страницу, 
            // в зависимости от его роли (группы).
            if (Roles.IsUserInRole(Login1.UserName, "Admin"))
            {
                Response.Redirect("Administrator\\AdminMain.aspx");
            }

            if (Roles.IsUserInRole(Login1.UserName, "Kinoprocat"))
            {
                Response.Redirect("Kinoprocat\\DistributorPage.aspx");
            }

        }
    }
}