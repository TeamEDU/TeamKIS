using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KISproject.Administrator
{
    public partial class CreateUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.IsInRole("Admin"))
            {
                // Запретить доступ к этой странице.
                // В замен переадресовать на страницу входа.
                Response.Redirect("Login.aspx");
            }
        }

        //protected void BuildRoleList(object sender, EventArgs e)
        //{
        //    ListAllRoles.DataSource = Roles.GetAllRoles();
        //    ListAllRoles.DataBind();
        //}

        //protected void AssignUserToRoles(object sender, EventArgs e)
        //{
        //    foreach (ListItem item in ListAllRoles.Items)
        //    {
        //        if (item.Selected)
        //        {
        //            Roles.AddUserToRole(CreateUserWizard1.UserName, item.Text);
        //        }
        //    }
        //}

    }
}