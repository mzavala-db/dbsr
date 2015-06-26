using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DBSR2
{
    public partial class RecursoLicenciaList : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            NuevoButton.Click += new EventHandler(NuevoButton_Click);
            VolverButton.Click += new EventHandler(VolverButton_Click);
            RecursoLicenciaGridView.RowDataBound += new GridViewRowEventHandler(RecursoLicenciaGridView_RowDataBound);
            RecursoLicenciaGridView.PageIndexChanging += new GridViewPageEventHandler(RecursoLicenciaGridView_PageIndexChanging);
            RecursoLicenciaGridView.Sorting += new GridViewSortEventHandler(RecursoLicenciaGridView_Sorting);

            if (!IsPostBack)
            {
                Session["FiltroRecurso"] = Request["IdRecurso"] ?? Session["FiltroRecurso"] ?? "-1";
                ViewState["SortDirection"] = SortDirection.Ascending;
                ViewState["SortExpression"] = "Recurso.Nombre";

                RecursoDropDown.DataSource = from r in DbsrContext.Recurso
                                             orderby r.Nombre
                                             select r;
                RecursoDropDown.DataBind();
                RecursoDropDown.SelectedValue = Session["FiltroRecurso"].ToString();

                Master.FindControl("FiltrosPanel").Visible = true;

                BindGrid();
            }
        }

        void RecursoLicenciaGridView_Sorting(object sender, GridViewSortEventArgs e)
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

        void RecursoLicenciaGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            RecursoLicenciaGridView.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        void RecursoLicenciaGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var hl = (HyperLink) e.Row.FindControl("EditarLink");
                hl.NavigateUrl = String.Format("RecursoLicenciaForm.aspx?Id={0}", DataBinder.Eval(e.Row.DataItem, "IdRecursoLicencia"));
            }
        }

        private void BindGrid()
        {
            int filtroRecurso = Convert.ToInt32(Session["FiltroRecurso"]);

            var recursosLicencia = from rl in DbsrContext.RecursoLicencia
                                   where (filtroRecurso == -1 || rl.IdRecurso == filtroRecurso)
                                   select rl;

            recursosLicencia = recursosLicencia.OrderBy(SortExpression);

            RecursoLicenciaGridView.DataSource = recursosLicencia;
            RecursoLicenciaGridView.DataBind();

        }

        protected void RecursoDropDownChanged(object sender, EventArgs e)
        {
            DropDownList recursoDropDown = (DropDownList) sender;
            Session["FiltroRecurso"] = recursoDropDown.SelectedValue;
            this.BindGrid();
        }

        void VolverButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("RecursoList.aspx");
        }

        void NuevoButton_Click(object sender, EventArgs e)
        {
            if (Session["FiltroRecurso"].ToString() != "-")
            {
                Response.Redirect("RecursoLicenciaForm.aspx?IdRecurso=" + Session["FiltroRecurso"]);
            }
            else
                Response.Redirect("RecursoLicenciaForm.aspx");
        }
    }
}