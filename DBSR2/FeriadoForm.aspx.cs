using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DBSR2
{
    public partial class FeriadoForm : BasePage
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
                    int idFeriado = Convert.ToInt32(Request["Id"]);
                    ViewState["Id"] = idFeriado;

                    var feriado = DbsrContext.Feriado.Single(f => f.IdFeriado == idFeriado);

                    FechaTextBox.Text = feriado.Fecha.ToString("dd/MM/yyyy");
                    NombreTextBox.Text = feriado.Nombre;
                }
            }
        }

        void EliminarButton_Click(object sender, EventArgs e)
        {
            int idFeriado = Convert.ToInt32(ViewState["Id"]);

            Feriado feriado = DbsrContext.Feriado.Single(f => f.IdFeriado == idFeriado);

            DbsrContext.Feriado.Remove(feriado);
            DbsrContext.SaveChanges();

            Response.Redirect("FeriadoList.aspx");
        }

        void GrabarButton_Click(object sender, EventArgs e)
        {
            Feriado feriado;

            if (ViewState["Id"] == null)
            {
                feriado = new Feriado {
                    Fecha = Convert.ToDateTime(FechaTextBox.Text),
                    Nombre = NombreTextBox.Text
                };
                DbsrContext.Feriado.Add(feriado);
            }
            else
            {
                int idFeriado = Convert.ToInt32(ViewState["Id"]);

                feriado = DbsrContext.Feriado.Single(f => f.IdFeriado == idFeriado);

                feriado.Fecha = Convert.ToDateTime(FechaTextBox.Text);
                feriado.Nombre = NombreTextBox.Text;
            }
            DbsrContext.SaveChanges();

            Response.Redirect("FeriadoList.aspx");
        }

        void CancelarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("FeriadoList.aspx");
        }
    }
}