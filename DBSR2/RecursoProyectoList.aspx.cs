using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DBSR2
{
    public partial class RecursoProyectoList : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            NuevoButton.Click += new EventHandler(NuevoButton_Click);
            VolverButton.Click += new EventHandler(VolverButton_Click);
            RecursoProyectoGridView.RowDataBound += new GridViewRowEventHandler(RecursoProyectoGridView_RowDataBound);
            RecursoProyectoGridView.PageIndexChanging += new GridViewPageEventHandler(RecursoProyectoGridView_PageIndexChanging);
            RecursoProyectoGridView.Sorting += new GridViewSortEventHandler(RecursoProyectoGridView_Sorting);

            if (!IsPostBack)
            {
                Session["FiltroProyecto"] = Request["IdProyecto"] ?? Session["FiltroProyecto"] ?? "-1";
                Session["FiltroRecurso"] = Session["FiltroRecurso"] ?? "-1";
                ViewState["SortDirection"] = SortDirection.Ascending;
                ViewState["SortExpression"] = "Recurso.Nombre";

                RecursoDropDown.DataSource = from r in DbsrContext.Recurso
                                             orderby r.Nombre
                                             select r;
                RecursoDropDown.DataBind();
                RecursoDropDown.SelectedValue = Session["FiltroRecurso"].ToString();

                ProyectoDropDown.DataSource = from p in DbsrContext.Proyecto
                                              orderby p.Nombre
                                              select p;
                ProyectoDropDown.DataBind();
                ProyectoDropDown.SelectedValue = Session["FiltroProyecto"].ToString();

                Master.FindControl("FiltrosPanel").Visible = true;

                BindGrid();
            }
        }

        void RecursoProyectoGridView_Sorting(object sender, GridViewSortEventArgs e)
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

        void RecursoProyectoGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            RecursoProyectoGridView.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        void RecursoProyectoGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var hl = (HyperLink) e.Row.FindControl("EditarLink");
                hl.NavigateUrl = String.Format("RecursoProyectoForm.aspx?Id={0}", DataBinder.Eval(e.Row.DataItem, "IdRecursoProyecto"));
            }
        }

        private void BindGrid()
        {
            int filtroRecurso = Convert.ToInt32(Session["FiltroRecurso"]);
            int filtroProyecto = Convert.ToInt32(Session["FiltroProyecto"]);

            var recursosProyectos = from rp in DbsrContext.RecursoProyecto
                                    orderby rp.Recurso.Nombre, rp.Proyecto.Nombre, rp.FechaDesde
                                    where (filtroRecurso == -1 || rp.IdRecurso == filtroRecurso) &&
                                        (filtroProyecto == -1 || rp.IdProyecto == filtroProyecto)
                                    select rp;

            recursosProyectos = recursosProyectos.OrderBy(SortExpression);

            RecursoProyectoGridView.DataSource = recursosProyectos;
            RecursoProyectoGridView.DataBind();

        }

        protected void RecursoDropDownChanged(object sender, EventArgs e)
        {
            DropDownList recursoDropDown = (DropDownList) sender;
            Session["FiltroRecurso"] = recursoDropDown.SelectedValue;
            this.BindGrid();
        }

        protected void ProyectoDropDownChanged(object sender, EventArgs e)
        {
            DropDownList proyectoDropDown = (DropDownList)sender;
            Session["FiltroProyecto"] = proyectoDropDown.SelectedValue;
            this.BindGrid();
        }

        void VolverButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        void NuevoButton_Click(object sender, EventArgs e)
        {
            if (Session["FiltroProyecto"].ToString() != "-")
            {
                Response.Redirect("RecursoProyectoForm.aspx?IdProyecto=" + Session["FiltroProyecto"]);
            }
            else
                Response.Redirect("RecursoProyectoForm.aspx");
        }
    }
}