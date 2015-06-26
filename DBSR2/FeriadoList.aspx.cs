using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DBSR2
{
    public partial class FeriadoList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FeriadosGridView.RowDataBound += new GridViewRowEventHandler(FeriadosGridView_RowDataBound);
            FeriadosGridView.PageIndexChanging += new GridViewPageEventHandler(FeriadosGridView_PageIndexChanging);
            NuevoButton.Click += new EventHandler(NuevoButton_Click);
            VolverButton.Click += new EventHandler(VolverButton_Click);

            BindGrid();
        }

        protected void BindGrid()
        {
            var feriados = from f in DbsrContext.Feriado
                           orderby f.Fecha
                           select f;

            FeriadosGridView.DataSource = feriados;
            FeriadosGridView.DataBind();
        }

        void FeriadosGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            FeriadosGridView.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        void FeriadosGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var hl = e.Row.FindControl("EditarLink") as HyperLink;
                hl.NavigateUrl = String.Format("FeriadoForm.aspx?Id={0}", DataBinder.Eval(e.Row.DataItem, "IdFeriado"));
            }
        }

        void VolverButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        void NuevoButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("FeriadoForm.aspx");
        }
    }
}