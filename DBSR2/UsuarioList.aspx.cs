using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace DBSR2
{
//    [RequireRole("Admin")]
    public partial class UsuarioList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UsuariosGridView.RowDataBound += new GridViewRowEventHandler(UsuariosGridView_RowDataBound);
            UsuariosGridView.PageIndexChanging += new GridViewPageEventHandler(UsuariosGridView_PageIndexChanging);
            VolverButton.Click += new EventHandler(VolverButton_Click);

            BindGrid();
        }

        void UsuariosGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            UsuariosGridView.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void BindGrid()
        {
            var usuarios = Membership.GetAllUsers();

            UsuariosGridView.DataSource = usuarios;
            UsuariosGridView.DataBind();
        }

        void UsuariosGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var hl = e.Row.FindControl("EditarLink") as HyperLink;
                hl.NavigateUrl = String.Format("UsuarioForm.aspx?Id={0}", DataBinder.Eval(e.Row.DataItem, "UserName"));
            }
        }

        void VolverButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}