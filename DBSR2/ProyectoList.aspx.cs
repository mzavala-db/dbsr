using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DBSR2
{
    public partial class ProyectoList : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            ProyectosGridView.RowDataBound += new GridViewRowEventHandler(ProyectosGridView_RowDataBound);
            ProyectosGridView.PageIndexChanging += new GridViewPageEventHandler(ProyectosGridView_PageIndexChanging);
            ProyectosGridView.Sorting += new GridViewSortEventHandler(ProyectosGridView_Sorting);

            NuevoButton.Click += new EventHandler(NuevoButton_Click);
            VolverButton.Click += new EventHandler(VolverButton_Click);

            if (!IsPostBack)
            {
                Session["FiltroActivo"] = Session["FiltroActivo"] ?? "1";
                Session["FiltroCliente"] = Session["FiltroCliente"] ?? "-1";
                Session["FiltroProjectLeader"] = Session["FiltroProjectLeader"] ?? "-1";
                ViewState["SortDirection"] = SortDirection.Ascending;
                ViewState["SortExpression"] = "Nombre";

                Master.FindControl("FiltrosPanel").Visible = true;

                ProjectLeaderDropDown.DataSource = (from r in DbsrContext.Recurso
                                                   where r.IdTipoRecurso == 1
                                                   orderby r.Nombre
                                                   select r).ToList<Recurso>();
                ProjectLeaderDropDown.DataBind();
                ProjectLeaderDropDown.SelectedValue = Session["FiltroProjectLeader"].ToString();

                ClienteDropDown.DataSource = (from c in DbsrContext.Cliente
                                             orderby c.Nombre
                                             select c).ToList<Cliente>();
                ClienteDropDown.DataBind();
                ClienteDropDown.SelectedValue = Session["FiltroCliente"].ToString();

                BindGrid();
            }
        }

        void ProyectosGridView_Sorting(object sender, GridViewSortEventArgs e)
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

        void ProyectosGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ProyectosGridView.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        private void BindGrid()
        {
            int filtroActivo = Convert.ToInt32(Session["FiltroActivo"]);
            int filtroCliente = Convert.ToInt32(Session["FiltroCliente"]);
            int filtroProjectLeader = Convert.ToInt32(Session["FiltroProjectLeader"]);

            var proyectos = from p in DbsrContext.Proyecto
                            where (filtroActivo == -1 || (filtroActivo == 0 && !p.Activo) || (filtroActivo == 1 && p.Activo))
                                && (filtroCliente == -1 || p.IdCliente == filtroCliente)
                                && (filtroProjectLeader == -1 || p.IdRecursoPL == filtroProjectLeader)
                            select p;

            proyectos = proyectos.OrderBy(SortExpression);

            ProyectosGridView.DataSource = proyectos.ToList<Proyecto>();
            ProyectosGridView.DataBind();
        }

        protected void ActivoDropDownChanged(object sender, EventArgs e)
        {
            DropDownList activoDropDown = (DropDownList)sender;
            Session["FiltroActivo"] = activoDropDown.SelectedValue;
            this.BindGrid();
        }

        protected void ClienteDropDownChanged(object sender, EventArgs e)
        {
            DropDownList clienteDropDown = (DropDownList)sender;
            Session["FiltroCliente"] = clienteDropDown.SelectedValue;
            this.BindGrid();
        }

        protected void ProjectLeaderDropDownChanged(object sender, EventArgs e)
        {
            DropDownList projectLeaderDropDown = (DropDownList)sender;
            Session["FiltroProjectLeader"] = projectLeaderDropDown.SelectedValue;
            this.BindGrid();
        }

        void ProyectosGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var idProyecto = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "IdProyecto"));

                var hl = e.Row.FindControl("EditarLink") as HyperLink;
                hl.NavigateUrl = String.Format("ProyectoForm.aspx?Id={0}", idProyecto);
                
                var hl2 = e.Row.FindControl("RecursosLink") as HyperLink;

                if (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "Activo")))
                    hl2.NavigateUrl = String.Format("RecursoProyectoList.aspx?IdProyecto={0}", idProyecto);
                else
                    hl2.Visible = false;

                var hl3 = e.Row.FindControl("PanelLink") as HyperLink;
                hl3.NavigateUrl = String.Format("PanelProyectoForm.aspx?IdProyecto={0}", idProyecto);
            }
        }

        void VolverButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        void NuevoButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProyectoForm.aspx");
        }
    }
}