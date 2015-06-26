using System;
using System.Web.Security;

namespace DBSR2
{
    public partial class Login : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.FindControl("Menu1").Visible = false;
            Login1.LoggedIn += new EventHandler(Login1_LoggedIn);

            if (!IsPostBack)
            {
                if (Request["UsuarioNoHabilitado"] != null)
                {
                    MostrarPopup("El usuario no se encuentra habilitado para operar el sistema.");
                }
            }
        }

        void Login1_LoggedIn(object sender, EventArgs e)
        {
            // chequear que el usuario tenga al menos un rol asignado en el sistema
            var roles = Roles.GetRolesForUser(Login1.UserName);

            if (roles.Length == 0)
            {
                FormsAuthentication.SignOut();
                Response.Redirect("~/Login.aspx?UsuarioNoHabilitado=1");
            }
        }
     }
}