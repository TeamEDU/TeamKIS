﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KISproject.Administrator
{
    public partial class AddRole : System.Web.UI.Page
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

        protected void btnAddRole_Click(object sender, EventArgs e)
        {
            if (FindRole(txtBoxNewRoleName.Text))
            {
                AddRoleAction.Text = "Такая роль уже существует";
            }
            else
            {
                Roles.CreateRole(txtBoxNewRoleName.Text);
            }
        }

        // Ищет указанную роль (FindRole) по источнику данных (БД).
        // игнорирует регистр.
        private bool FindRole(string nameRole)
        {
            foreach (string item in Roles.GetAllRoles())
            {
                if (String.Compare(nameRole, item, true) == 0)
                    return true;
            }

            return false;
        }
    }
}