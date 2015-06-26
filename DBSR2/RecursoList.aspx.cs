using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DBSR2
{
    public partial class RecursoList : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            RecursosGridView.RowDataBound += new GridViewRowEventHandler(RecursosGridView_RowDataBound);
            RecursosGridView.PageIndexChanging += new GridViewPageEventHandler(RecursosGridView_PageIndexChanging);
            NuevoButton.Click += new EventHandler(NuevoButton_Click);
            VolverButton.Click += new EventHandler(VolverButton_Click);

            if (!IsPostBack)
            {
                Session["FiltroActivo"] = Session["FiltroActivo"] ?? "1";

                Master.FindControl("FiltrosPanel").Visible = true;

                BindGrid();
            }
        }

        void RecursosGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            RecursosGridView.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        private void BindGrid()
        {
            int filtroActivo = Convert.ToInt32(Session["FiltroActivo"]);

            var recursos = from r in DbsrContext.Recurso
                           orderby r.Nombre
                           where (filtroActivo == -1 || (filtroActivo == 0 && !r.Activo) || (filtroActivo == 1 && r.Activo))
                           select r;

            RecursosGridView.DataSource = recursos;
            RecursosGridView.DataBind();
        }

        void RecursosGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ((HyperLink) e.Row.FindControl("EditarLink")).NavigateUrl = String.Format("RecursoForm.aspx?Id={0}", DataBinder.Eval(e.Row.DataItem, "IdRecurso"));
                ((HyperLink) e.Row.FindControl("LicenciasLink")).NavigateUrl = String.Format("RecursoLicenciaList.aspx?IdRecurso={0}", DataBinder.Eval(e.Row.DataItem, "IdRecurso"));
            }
        }

        protected void ActivoDropDownChanged(object sender, EventArgs e)
        {
            DropDownList activoDropDown = (DropDownList)sender;
            Session["FiltroActivo"] = activoDropDown.SelectedValue;
            BindGrid();
        }

        void VolverButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        void NuevoButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("RecursoForm.aspx");
        }
 
    }
}