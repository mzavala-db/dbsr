using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DBSR2
{
    public partial class LicenciasReport : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CancelarButton.Click += new EventHandler(CancelarButton_Click);
            GenerarButton.Click += new EventHandler(GenerarButton_Click);
        }

        void GenerarButton_Click(object sender, EventArgs e)
        {
            var fechaDesde = Convert.ToDateTime(FechaDesdeTextBox.Text);
            var fechaHasta = Convert.ToDateTime(FechaHastaTextBox.Text);

            var dtReporte = new DataTable();
            dtReporte.Columns.Add(new DataColumn("Recurso", typeof(string)));
            dtReporte.Columns.Add(new DataColumn("FechaDesde", typeof(string)));
            dtReporte.Columns.Add(new DataColumn("FechaHasta", typeof(string)));
            dtReporte.Columns.Add(new DataColumn("MotivoLicencia", typeof(string)));
            dtReporte.Columns.Add(new DataColumn("Dias", typeof(int)));

            var licencias = from rl in DbsrContext.RecursoLicencia
                            where (rl.FechaDesde >= fechaDesde && rl.FechaHasta <= fechaHasta)
                                || (rl.FechaDesde <= fechaDesde && rl.FechaHasta >= fechaDesde && rl.FechaHasta <= fechaHasta)
                                || (rl.FechaDesde >= fechaDesde && rl.FechaDesde <= fechaHasta && rl.FechaHasta >= fechaHasta)
                                || (rl.FechaDesde <= fechaDesde && rl.FechaHasta >= fechaHasta)
                            orderby rl.Recurso.Nombre, rl.FechaDesde, rl.FechaHasta
                            select new { Recurso = rl.Recurso.Nombre, rl.FechaDesde, rl.FechaHasta, MotivoLicencia = rl.MotivoLicencia.Descripcion };

            foreach (var l in licencias)
            {
                dtReporte.Rows.Add(l.Recurso, l.FechaDesde.ToString("dd/MM/yyyy"), l.FechaHasta.ToString("dd/MM/yyyy"), l.MotivoLicencia,
                    (l.FechaHasta - l.FechaDesde).Days + 1);
            }

            if (dtReporte.Rows.Count == 0)
            {
                MostrarPopup("No se encontraron licencias para la fecha indicada.");
            }
            else
            {
                var subtitulos = new List<string>
                {
                    "Fecha desde: " + fechaDesde.ToString("dd/MM/yyyy"),
                    "Fecha hasta: " + fechaHasta.ToString("dd/MM/yyyy"),
                    "Fecha de generación: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")
                };
                var xlsx = new ExcelExporter(dtReporte, "Reporte de licencias", subtitulos);
                string fileName = String.Format(@"Licencias_{0}.xlsx", DateTime.Now.ToString("yyyyMMddhhmmssffff"));
                xlsx.Export(fileName);
                DescargarArchivo(fileName);
            }
        }

        void CancelarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}