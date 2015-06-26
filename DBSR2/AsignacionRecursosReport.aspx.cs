using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DBSR2
{
    public partial class AsignacionRecursosReport : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var fechaHoy = DateTime.Now;
                FechaTextBox.Text = fechaHoy.ToString("dd/MM/yyyy");
            }

            CancelarButton.Click += new EventHandler(CancelarButton_Click);
            GenerarButton.Click += new EventHandler(GenerarButton_Click);
        }

        void GenerarButton_Click(object sender, EventArgs e)
        {
            var fecha = Convert.ToDateTime(FechaTextBox.Text);

            var dtReporte = new DataTable();
            dtReporte.Columns.Add(new DataColumn("Recurso", typeof(string)));

            dtReporte.PrimaryKey = new DataColumn[] { dtReporte.Columns["Recurso"] };

            var recursosAsignados = new HashSet<string>(from r in DbsrContext.Recurso select r.Nombre);

            // buscar los recursos asignados para esta fecha
            var asignados = from rp in DbsrContext.RecursoProyecto
                            join p in DbsrContext.Proyecto on rp.IdProyecto equals p.IdProyecto
                            join r in DbsrContext.Recurso on rp.IdRecurso equals r.IdRecurso
                            where ((rp.FechaDesde ?? DateTime.MinValue) <= fecha) && (fecha <= (rp.FechaHasta ?? DateTime.MaxValue))
                            orderby p.Nombre, r.Nombre
                            select new { Recurso = r.Nombre, Proyecto = p.Nombre, Horas = rp.Horas };

            var proyectos = new HashSet<string>();

            // armar el dataset
            foreach (var asignado in asignados)
            {
                var nombreProyecto = asignado.Proyecto;
                if (!proyectos.Contains(nombreProyecto))
                {
                    dtReporte.Columns.Add(new DataColumn(nombreProyecto, typeof(int)));
                    proyectos.Add(nombreProyecto);
                }

                DataRow row = dtReporte.Rows.Find(new object[] { asignado.Recurso });

                if (row == null)
                {
                    row = dtReporte.NewRow();
                    row["Recurso"] = asignado.Recurso;
                    dtReporte.Rows.Add(row);
                }
                row[nombreProyecto] = asignado.Horas;
                recursosAsignados.Remove(asignado.Recurso);
            }

            // agregar recursos sin asignar
            foreach (var recurso in recursosAsignados)
            {
                var row = dtReporte.NewRow();
                row["Recurso"] = "* " + recurso;
                dtReporte.Rows.Add(row);
            }

            if (dtReporte.Rows.Count == 0)
            {
                MostrarPopup("No se encontraron datos para la fecha indicada.");
            }
            else
            {
                var subtitulos = new List<string>
                {
                    "Fecha: " + fecha.ToString("dd/MM/yyyy"),
                    "Fecha de generación: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")
                };

                var xlsx = new ExcelExporter(dtReporte, "Reporte de asignación de recursos", subtitulos);
                string fileName = String.Format(@"AsignacionRecursos_{0}.xlsx", DateTime.Now.ToString("yyyyMMddhhmmssffff"));
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