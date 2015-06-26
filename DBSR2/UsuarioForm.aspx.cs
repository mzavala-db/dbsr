using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace DBSR2
{
    public partial class UsuarioForm : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CancelarButton.Click += new EventHandler(CancelarButton_Click);
            GrabarButton.Click += new EventHandler(GrabarButton_Click);

            if (!IsPostBack)
            {
                if (Request["Id"] != null)
                {
                    string userName = Request["Id"];
                    ViewState["Id"] = userName;

                    NombreLiteral.Text = userName;

                    var roles = Roles.GetAllRoles();

                    foreach (string rol in roles)
                    {
                        RolesList.Items.Add(new ListItem { Text = rol, Selected = Roles.IsUserInRole(userName, rol) });
                    }
                }
            }
        }

        void GrabarButton_Click(object sender, EventArgs e)
        {
            var userName = ViewState["Id"].ToString();

            foreach (ListItem item in RolesList.Items)
            {
                var asignado = item.Selected;
                var rol = item.Text;
                var usuarioEnRol = Roles.IsUserInRole(userName, rol);

                if (usuarioEnRol && !asignado)
                {
                    Roles.RemoveUserFromRole(userName, rol);
                }
                else if (!usuarioEnRol && asignado)
                {
                    Roles.AddUserToRole(userName, rol);
                }
            }
            GoBack();
        }

        void CancelarButton_Click(object sender, EventArgs e)
        {
            GoBack();
        }
    }
}