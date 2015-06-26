using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace DBSR2
{
    public partial class Login_original : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IngresarButton.Click += new EventHandler(IngresarButton_Click);
            Master.FindControl("Menu1").Visible = false;
            //Roles.RemoveUserFromRole("mzavala", "usuario");
        }

        void IngresarButton_Click(object sender, EventArgs e)
        {
            var usuario = UsuarioTextBox.Text;
            var password = PasswordTextBox.Text;

            if (Membership.ValidateUser(usuario, password))
            {
                if (Request.QueryString["ReturnUrl"] != null)
                {
                    FormsAuthentication.RedirectFromLoginPage(usuario, false);
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(usuario, false);
                    Response.Redirect("Default.aspx");
                }
            }
            else
            {
                MostrarPopup("No se puede autenticar el usuario");
            }
        }


    }
}