using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DBSR2
{
    public partial class HorasVendidasReport : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var fechaHoy = DateTime.Now;
                FechaHastaTextBox.Text = fechaHoy.ToString("dd/MM/yyyy");
                FechaDesdeTextBox.Text = new DateTime(fechaHoy.Year, fechaHoy.Month, 1).ToString("dd/MM/yyyy");
            }

            CancelarButton.Click += new EventHandler(CancelarButton_Click);
            GenerarButton.Click += new EventHandler(GenerarButton_Click);
        }

        void GenerarButton_Click(object sender, EventArgs e)
        {
            var fechaDesde = Convert.ToDateTime(FechaDesdeTextBox.Text);
            var fechaHasta = Convert.ToDateTime(FechaHastaTextBox.Text);

            var dtReporte = new DataTable();
            dtReporte.Columns.Add(new DataColumn("Recurso", typeof(string)));
            dtReporte.Columns.Add(new DataColumn("Cliente", typeof(string)));
            dtReporte.Columns.Add(new DataColumn("Proyecto", typeof(string)));
            dtReporte.Columns.Add(new DataColumn("TipoRecurso", typeof(string)));
            dtReporte.Columns.Add(new DataColumn("CuentaFacturacion", typeof(string)));
            dtReporte.Columns.Add(new DataColumn("Horas", typeof(int)));

            dtReporte.PrimaryKey = new DataColumn[] { dtReporte.Columns["Recurso"],
                dtReporte.Columns["Cliente"],
                dtReporte.Columns["Proyecto"]
            };

            var fecha = fechaDesde;

            // calcular la cantidad de días hábiles en el período
            var feriados = new HashSet<DateTime>(from f in DbsrContext.Feriado
                                                 where fechaDesde <= f.Fecha && f.Fecha <= fechaHasta
                                                 select f.Fecha);
            int diasHabiles = 0;

            while (fecha <= fechaHasta)
            {
                if (!feriados.Contains(fecha) && fecha.DayOfWeek != DayOfWeek.Saturday && fecha.DayOfWeek != DayOfWeek.Sunday)
                {
                    diasHabiles++;

                    // buscar los recursos asignados para esta fecha
                    var asignados = from rp in DbsrContext.RecursoProyecto
                                    where ((rp.FechaDesde ?? DateTime.MinValue) <= fecha) && (fecha <= (rp.FechaHasta ?? DateTime.MaxValue))
                                    select new { Recurso = rp.Recurso.Nombre, Cliente = rp.Proyecto.Cliente.Nombre, Proyecto = rp.Proyecto.Nombre,
                                        TipoRecurso = rp.Recurso.TipoRecurso.Descripcion, CuentaFacturacion = rp.Proyecto.CuentaFacturacion.Nombre, HorasFacturacion = rp.HorasFacturacion };

                    // acumular en el dataset
                    foreach (var asignado in asignados)
                    {
                        var key = new object[] { asignado.Recurso, asignado.Cliente, asignado.Proyecto };
                        DataRow row = dtReporte.Rows.Find(key);
                        if (row != null)
                        {
                            row["Horas"] = Convert.ToInt32(row["Horas"]) + asignado.HorasFacturacion;
                        }
                        else
                        {
                            row = dtReporte.NewRow();
                            row["Recurso"] = asignado.Recurso;
                            row["Cliente"] = asignado.Cliente;
                            row["Proyecto"] = asignado.Proyecto;
                            row["TipoRecurso"] = asignado.TipoRecurso;
                            row["CuentaFacturacion"] = asignado.CuentaFacturacion;
                            row["Horas"] = asignado.HorasFacturacion;
                            dtReporte.Rows.Add(row);
                        }
                    }
                }

                fecha = fecha.AddDays(1);
            }

            if (dtReporte.Rows.Count == 0)
            {
                MostrarPopup("No se encontraron recursos asignados a proyectos para la fecha indicada.");
            }
            else
            {
                var subtitulos = new List<string>
                {
                    "Fecha desde: " + fechaDesde.ToString("dd/MM/yyyy"),
                    "Fecha hasta: " + fechaHasta.ToString("dd/MM/yyyy"),
                    "Días hábiles: " + diasHabiles,
                    "Fecha de generación: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")
                };
                var xlsx = new ExcelExporter(dtReporte, "Reporte de horas vendidas", subtitulos);
                string fileName = String.Format(@"HorasVendidas_{0}.xlsx", DateTime.Now.ToString("yyyyMMddhhmmssffff"));
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