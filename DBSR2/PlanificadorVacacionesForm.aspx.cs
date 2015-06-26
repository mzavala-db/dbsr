using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace DBSR2
{
    public partial class PlanificadorVacacionesForm : BasePage
    {
        private Dictionary<int, string> _feriados;

        protected void Page_Load(object sender, EventArgs e)
        {
            VacacionesCalendar.DayRender += new DayRenderEventHandler(VacacionesCalendar_DayRender);
            VacacionesCalendar.VisibleMonthChanged += new MonthChangedEventHandler(VacacionesCalendar_VisibleMonthChanged);
            VolverButton.Click += new EventHandler(VolverButton_Click);

            if (!IsPostBack)
            {
                VacacionesCalendar.VisibleDate = DateTime.Now;
                RefreshCalendar();
            }
        }

        void VacacionesCalendar_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            RefreshCalendar();
        }

        void RefreshCalendar()
        {
            // buscar feriados del mes actual
            var feriado = from f in DbsrContext.Feriado
                          where f.Fecha.Month == VacacionesCalendar.VisibleDate.Month
                            && f.Fecha.Year == VacacionesCalendar.VisibleDate.Year
                          select f;

            _feriados = new Dictionary<int, string>();

            foreach (Feriado f in feriado)
            {
                _feriados.Add(f.Fecha.Day, f.Nombre);
            }
        }

        void VacacionesCalendar_DayRender(object sender, DayRenderEventArgs e)
        {
            // buscar vacaciones
            var recursoLicencia = from rl in DbsrContext.RecursoLicencia
                                  where rl.FechaDesde <= e.Day.Date && e.Day.Date <= rl.FechaHasta
                                  orderby rl.Recurso.IdTipoRecurso, rl.Recurso.Nombre
                                  select rl;

            if (e.Day.Date.Month == VacacionesCalendar.VisibleDate.Month && (e.Day.Date.DayOfWeek == DayOfWeek.Saturday || e.Day.Date.DayOfWeek == DayOfWeek.Sunday))
            {
                e.Cell.BackColor = Color.LightBlue;
            }

            string feriado;
            if (e.Day.Date.Month == VacacionesCalendar.VisibleDate.Month && _feriados.TryGetValue(e.Day.Date.Day, out feriado))
            {
                e.Cell.BackColor = Color.LightCoral;
                e.Cell.Controls.Add(new LiteralControl("<br /><strong>" + feriado + "</strong>"));
            }

            if (e.Day.Date == DateTime.Now.Date)
            {
                e.Cell.BackColor = Color.LightGreen;
            }

            foreach (RecursoLicencia rl in recursoLicencia)
            {
                string msg = "<br/>" + rl.Recurso.Nombre;

                if (rl.Recurso.IdTipoRecurso == 2)
                {
                    // buscar equipo
                    var pls = from rp in DbsrContext.RecursoProyecto
                              where rp.IdRecurso == rl.IdRecurso && (((rp.FechaDesde ?? DateTime.MinValue) <= e.Day.Date) && (e.Day.Date <= (rp.FechaHasta ?? DateTime.MaxValue)))
                              select rp.Proyecto.Recurso;

                    if (pls != null)
                    {
                        var nombrePls = new List<string>();
                        foreach (Recurso pl in pls)
                        {
                            string nombre = "";
                            foreach (string s in pl.Nombre.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                            {
                                nombre += s[0].ToString();
                            }
                            nombrePls.Add(nombre.ToUpper());
                        }
                        msg += " (" + String.Join(",", nombrePls.ToArray()) + ")";
                    }
                }
                e.Cell.Controls.Add(new Label() { Text = msg, ToolTip = rl.MotivoLicencia.Descripcion + (!String.IsNullOrEmpty(rl.Observaciones) ? " - " + rl.Observaciones : "" ) } );
            }
        }

        void VolverButton_Click(object sender, EventArgs e)
        {
            GoBack();
        }
    }
}