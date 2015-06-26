using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DBSR2
{
    public partial class CuentaFacturacionForm : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CancelarButton.Click += new EventHandler(CancelarButton_Click);
            GrabarButton.Click += new EventHandler(GrabarButton_Click);
            EliminarButton.Click += new EventHandler(EliminarButton_Click);

            if (!IsPostBack)
            {
                if (Request["Id"] != null)
                {
                    int idCuentaFacturacion = Convert.ToInt32(Request["Id"]);
                    ViewState["Id"] = idCuentaFacturacion;

                    var cuentaFacturacion = DbsrContext.CuentaFacturacion.Single(cf => cf.IdCuentaFacturacion == idCuentaFacturacion);

                    NombreTextBox.Text = cuentaFacturacion.Nombre;
                }
            }
        }

        void EliminarButton_Click(object sender, EventArgs e)
        {
            int idCuentaFacturacion = Convert.ToInt32(ViewState["Id"]);

            CuentaFacturacion cuentaFacturacion = DbsrContext.CuentaFacturacion.Single(cf => cf.IdCuentaFacturacion == idCuentaFacturacion);

            DbsrContext.CuentaFacturacion.Remove(cuentaFacturacion);
            DbsrContext.SaveChanges();

            Response.Redirect("CuentaFacturacionList.aspx");
        }

        void GrabarButton_Click(object sender, EventArgs e)
        {
            CuentaFacturacion cuentaFacturacion;

            if (ViewState["Id"] == null)
            {
                cuentaFacturacion = new CuentaFacturacion { Nombre = NombreTextBox.Text };
                DbsrContext.CuentaFacturacion.Add(cuentaFacturacion);
            }
            else
            {
                int idCuentaFacturacion = Convert.ToInt32(ViewState["Id"]);

                cuentaFacturacion = DbsrContext.CuentaFacturacion.Single(cf => cf.IdCuentaFacturacion == idCuentaFacturacion);

                cuentaFacturacion.Nombre = NombreTextBox.Text;
            }
            DbsrContext.SaveChanges();

            Response.Redirect("CuentaFacturacionList.aspx");
        }

        void CancelarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("CuentaFacturacionList.aspx");
        }
 
    }
}