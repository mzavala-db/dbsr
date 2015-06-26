using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DBSR2
{
    public partial class ClienteList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientesGridView.RowDataBound += new GridViewRowEventHandler(ClientesGridView_RowDataBound);
            ClientesGridView.PageIndexChanging += new GridViewPageEventHandler(ClientesGridView_PageIndexChanging);
            ClientesGridView.Sorting += new GridViewSortEventHandler(ClientesGridView_Sorting);
            NuevoButton.Click += new EventHandler(NuevoButton_Click);
            VolverButton.Click += new EventHandler(VolverButton_Click);

            if (!IsPostBack)
            {
                ViewState["SortDirection"] = SortDirection.Ascending;
                ViewState["SortExpression"] = "Nombre";
            }

            BindGrid();

        }

        void ClientesGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            var sortDirection = (SortDirection)ViewState["SortDirection"];

            if (sortDirection == SortDirection.Ascending)
                sortDirection = SortDirection.Descending;
            else
                sortDirection = SortDirection.Ascending;

            ViewState["SortExpression"] = e.SortExpression;
            ViewState["SortDirection"] = sortDirection;

            BindGrid();
        }

        void ClientesGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ClientesGridView.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void BindGrid()
        {
            var clientes = (from c in DbsrContext.Cliente
                           select c).OrderBy(SortExpression);

            ClientesGridView.DataSource = clientes;
            ClientesGridView.DataBind();
        }

        void ClientesGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var hl = e.Row.FindControl("EditarLink") as HyperLink;
                hl.NavigateUrl = String.Format("ClienteForm.aspx?Id={0}", DataBinder.Eval(e.Row.DataItem, "IdCliente"));
            }
        }

        void VolverButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        void NuevoButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClienteForm.aspx");
        }
    }
}