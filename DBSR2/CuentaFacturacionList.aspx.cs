using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DBSR2
{
    public partial class CuentaFacturacionList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CuentasFacturacionGridView.RowDataBound += new GridViewRowEventHandler(CuentasFacturacion_RowDataBound);
            CuentasFacturacionGridView.PageIndexChanging += new GridViewPageEventHandler(CuentasFacturacionGridView_PageIndexChanging);
            NuevoButton.Click += new EventHandler(NuevoButton_Click);
            VolverButton.Click += new EventHandler(VolverButton_Click);

            BindGrid();
        }

        protected void BindGrid()
        {
            var cuentasFacturacion = from cf in DbsrContext.CuentaFacturacion
                                     orderby cf.Nombre
                                     select cf;

            CuentasFacturacionGridView.DataSource = cuentasFacturacion;
            CuentasFacturacionGridView.DataBind();
        }

        void CuentasFacturacionGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CuentasFacturacionGridView.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        void CuentasFacturacion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var hl = e.Row.FindControl("EditarLink") as HyperLink;
                hl.NavigateUrl = String.Format("CuentaFacturacionForm.aspx?Id={0}", DataBinder.Eval(e.Row.DataItem, "IdCuentaFacturacion"));
            }
        }

        void VolverButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        void NuevoButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("CuentaFacturacionForm.aspx");
        }
    }
}