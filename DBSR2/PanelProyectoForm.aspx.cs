using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DBSR2
{
    public partial class PanelProyectoForm : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ProyectoDropDown.SelectedIndexChanged += new EventHandler(ProyectoDropDown_SelectedIndexChanged);
            EditarProyectoButton.Click += new EventHandler(EditarProyectoButton_Click);
            ProximoProyectoButton.Click += new EventHandler(ProximoProyectoButton_Click);
            VolverButton.Click += new EventHandler(VolverButton_Click);

            ReporteEstadoGridView.RowCommand += new GridViewCommandEventHandler(ReporteEstadoGridView_RowCommand);
            ReporteEstadoGridView.PageIndexChanging += new GridViewPageEventHandler(ReporteEstadoGridView_PageIndexChanging);

            AgregarReporteEstadoButton.Click += new EventHandler(AgregarReporteEstadoButton_Click);
            GrabarReporteEstadoButton.Click += new EventHandler(GrabarReporteEstadoButton_Click);

            HitoGridView.RowCommand += new GridViewCommandEventHandler(HitoGridView_RowCommand);
            HitoGridView.RowDataBound += new GridViewRowEventHandler(HitoGridView_RowDataBound);
            HitoGridView.PageIndexChanging += new GridViewPageEventHandler(HitoGridView_PageIndexChanging);
            AgregarHitoButton.Click += new EventHandler(AgregarHitoButton_Click);
            GrabarHitoButton.Click += new EventHandler(GrabarHitoButton_Click);

            if (!IsPostBack)
            {
                ProyectoDropDown.DataSource = from p in DbsrContext.Proyecto
                                              where p.Activo
                                              orderby p.Nombre
                                              select p;
                ProyectoDropDown.DataBind();

                if (Request["IdProyecto"] != null)
                {
                    ProyectoDropDown.SelectedValue = Request["IdProyecto"];
                    ProyectoDropDown_SelectedIndexChanged(ProyectoDropDown, null);
                }
            }
        }

        #region Recursos
        void ActualizarRecursoGrid()
        {


            int idProyecto = Convert.ToInt32(ProyectoDropDown.SelectedValue);
            int filtroVigente = Convert.ToInt32(Session["FiltroVigente"]);
            var fecha = DateTime.Now;

            var recursos = from rp in DbsrContext.RecursoProyecto
                           join rl1 in DbsrContext.RecursoLicencia.Where(rl1 => rl1.FechaDesde >= fecha) on rp.IdRecurso equals rl1.IdRecurso into RecursoLicenciaNulls
                           from rl2 in RecursoLicenciaNulls.DefaultIfEmpty()
                           where rp.IdProyecto == idProyecto
                                && (filtroVigente == -1 
                                    || (filtroVigente == 0 && !((rp.FechaDesde ?? DateTime.MinValue) <= fecha) && (fecha <= (rp.FechaHasta ?? DateTime.MaxValue))) 
                                    || (filtroVigente == 1 && ((rp.FechaDesde ?? DateTime.MinValue) <= fecha) && (fecha <= (rp.FechaHasta ?? DateTime.MaxValue))))
                           orderby rp.Recurso.IdTipoRecurso, rp.Recurso.Nombre, rl2.FechaDesde
                           select new { FechaDesde = rp.FechaDesde, FechaHasta = rp.FechaHasta, Recurso = rp.Recurso.Nombre,
                               Horas = rp.Horas, HorasFacturacion = rp.HorasFacturacion, Vigente = ((rp.FechaDesde ?? DateTime.MinValue) <= fecha) && (fecha <= (rp.FechaHasta ?? DateTime.MaxValue)),
                               FechaProximaLicencia = (DateTime?) rl2.FechaDesde, MotivoLicencia = rl2.MotivoLicencia.Descripcion };

            RecursoGridView.DataSource = recursos;
            RecursoGridView.DataBind();

            ((DropDownList)RecursoGridView.HeaderRow.FindControl("ProyectoVigenteDropDown")).SelectedValue = filtroVigente.ToString();
        }
        #endregion

        #region Reportes de estado
        void ReporteEstadoGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var idReporteEstado = Convert.ToInt32(e.CommandArgument);

            var reporteEstado = DbsrContext.ReporteEstado.Single(re => re.IdReporteEstado == idReporteEstado);

            switch (e.CommandName)
            {
                case "Editar":
                    ViewState["IdReporteEstado"] = idReporteEstado;
                    ReporteEstadoDescripcionTextBox.Text = reporteEstado.Descripcion;
                    ReporteEstadoModalPopupExtender.Show();
                    break;

                case "Eliminar":
                    DbsrContext.ReporteEstado.Remove(reporteEstado);
                    DbsrContext.SaveChanges();
                    ActualizarReporteEstadoGrid();
                    break;
            }
        }

        void ReporteEstadoGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ReporteEstadoGridView.PageIndex = e.NewPageIndex;
            ActualizarReporteEstadoGrid();
        }

        void AgregarReporteEstadoButton_Click(object sender, EventArgs e)
        {
            ReporteEstadoDescripcionTextBox.Text = "";
            ReporteEstadoModalPopupExtender.Show();
        }

        void GrabarReporteEstadoButton_Click(object sender, EventArgs e)
        {
            if (ViewState["IdReporteEstado"] != null)
            {
                var idReporteEstado = Convert.ToInt32(ViewState["IdReporteEstado"]);
                var reporteEstado = DbsrContext.ReporteEstado.Single(re => re.IdReporteEstado == idReporteEstado);

                reporteEstado.Descripcion = ReporteEstadoDescripcionTextBox.Text;
                ViewState["IdReporteEstado"] = null;
            }
            else
            {
                var reporteEstado = new ReporteEstado
                {
                    Fecha = DateTime.Now,
                    Descripcion = ReporteEstadoDescripcionTextBox.Text,
                    IdProyecto = Convert.ToInt32(ProyectoDropDown.SelectedValue)
                };
                DbsrContext.ReporteEstado.Add(reporteEstado);
            }
            DbsrContext.SaveChanges();
            ReporteEstadoDescripcionTextBox.Text = "";
            ReporteEstadoModalPopupExtender.Hide();
            ActualizarReporteEstadoGrid();
        }

        void ActualizarReporteEstadoGrid()
        {
            var idProyecto = Convert.ToInt32(ProyectoDropDown.SelectedValue);

            var reportes = from re in DbsrContext.ReporteEstado
                           where re.IdProyecto == idProyecto
                           orderby re.Fecha descending
                           select re;

            ReporteEstadoGridView.DataSource = reportes;
            ReporteEstadoGridView.DataBind();
        }
        #endregion

        #region Hitos
        void HitoGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var idHito = Convert.ToInt32(e.CommandArgument);

            var hito = DbsrContext.Hito.Single(h => h.IdHito == idHito);

            switch (e.CommandName)
            {
                case "Editar":
                    ViewState["IdHito"] = idHito;
                    HitoFechaEstimadaTextBox.Text = hito.FechaEstimada.ToString("dd/MM/yyyy");
                    HitoFechaCumplimientoTextBox.Text = hito.FechaCumplimiento.HasValue ? hito.FechaCumplimiento.Value.ToString("dd/MM/yyyy") : "";
                    HitoDescripcionTextBox.Text = hito.Descripcion;
                    HitoModalPopupExtender.Show();
                    break;

                case "Eliminar":
                    DbsrContext.Hito.Remove(hito);
                    DbsrContext.SaveChanges();
                    ActualizarHitoGrid();
                    break;
            }

        }

        void HitoGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.Cells[2].Text == "&nbsp;")
            {
                if (Convert.ToDateTime(e.Row.Cells[1].Text) < DateTime.Now.Date)
                {
                    e.Row.Cells[1].BackColor = Color.Red;
                    e.Row.Cells[1].ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[1].Font.Bold = true;
                }
                else if (Convert.ToDateTime(e.Row.Cells[1].Text) == DateTime.Now.Date)
                {
                    e.Row.Cells[1].BackColor = Color.Orange;
                    e.Row.Cells[1].ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[1].Font.Bold = true;
                }
            }
        }

        void HitoGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            HitoGridView.PageIndex = e.NewPageIndex;
            ActualizarHitoGrid();
        }

        void AgregarHitoButton_Click(object sender, EventArgs e)
        {
            HitoFechaEstimadaTextBox.Text = "";
            HitoFechaCumplimientoTextBox.Text = "";
            HitoDescripcionTextBox.Text = "";
            HitoModalPopupExtender.Show();
        }

        void GrabarHitoButton_Click(object sender, EventArgs e)
        {
            if (ViewState["IdHito"] != null)
            {
                var idHito = Convert.ToInt32(ViewState["IdHito"]);
                var hito = DbsrContext.Hito.Single(h => h.IdHito == idHito);
                hito.FechaEstimada = Convert.ToDateTime(HitoFechaEstimadaTextBox.Text);
                hito.FechaCumplimiento = (HitoFechaCumplimientoTextBox.Text != "") ? (DateTime?) Convert.ToDateTime(HitoFechaCumplimientoTextBox.Text) : null;
                hito.Descripcion = HitoDescripcionTextBox.Text;
                ViewState["IdHito"] = null;
            }
            else
            {
                var hito = new Hito
                {
                    FechaAlta = DateTime.Now,
                    FechaEstimada = Convert.ToDateTime(HitoFechaEstimadaTextBox.Text),
                    FechaCumplimiento = (HitoFechaCumplimientoTextBox.Text != "") ? (DateTime?) Convert.ToDateTime(HitoFechaEstimadaTextBox.Text) : null,
                    Descripcion = HitoDescripcionTextBox.Text,
                    IdProyecto = Convert.ToInt32(ProyectoDropDown.SelectedValue),
                    IdTaskAsana = null
                };
                DbsrContext.Hito.Add(hito);
            }
            DbsrContext.SaveChanges();
            HitoFechaEstimadaTextBox.Text = "";
            HitoFechaCumplimientoTextBox.Text = "";
            HitoDescripcionTextBox.Text = "";
            HitoModalPopupExtender.Hide();
            ActualizarHitoGrid();
        }

        void ActualizarHitoGrid()
        {
            var idProyecto = Convert.ToInt32(ProyectoDropDown.SelectedValue);

            var hitos = from h in DbsrContext.Hito
                        where h.IdProyecto == idProyecto
                        orderby h.FechaAlta descending
                        select h;

            HitoGridView.DataSource = hitos;
            HitoGridView.DataBind();
        }
        #endregion


        protected void ProyectoVigenteDropDownChanged(object sender, EventArgs e)
        {
            DropDownList vigenteDropDown = (DropDownList)sender;
            Session["FiltroVigente"] = vigenteDropDown.SelectedValue;
            ActualizarRecursoGrid();
        }

        void ProyectoDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            var idProyecto = Convert.ToInt32(((DropDownList)sender).SelectedValue);
            var proyecto = DbsrContext.Proyecto.Single(p => p.IdProyecto == idProyecto);

            ProjectLeaderLabel.Text = proyecto.Recurso.Nombre;
            ClienteLabel.Text = proyecto.Cliente.Nombre;
            CuentaFacturacionLabel.Text = proyecto.CuentaFacturacion.Nombre;
            ModalidadLabel.Text = proyecto.ModalidadProyecto.Nombre;

            FechaFinEstimadaLabel.Text = (proyecto.FechaFinEstimada.HasValue) ? proyecto.FechaFinEstimada.Value.ToString("dd/MM/yyyy") : "Sin datos";
            FechaFinRealLabel.Text = (proyecto.FechaFinReal.HasValue) ? proyecto.FechaFinReal.Value.ToString("dd/MM/yyyy") : "Sin datos";
            FechaInicioEstimadaLabel.Text = (proyecto.FechaInicioEstimada.HasValue) ? proyecto.FechaInicioEstimada.Value.ToString("dd/MM/yyyy") : "Sin datos";
            FechaInicioRealLabel.Text = (proyecto.FechaInicioReal.HasValue) ? proyecto.FechaInicioReal.Value.ToString("dd/MM/yyyy") : "Sin datos";

            ActivoLabel.Text = proyecto.Activo ? "Sí" : "No";

            Session["FiltroVigente"] = "1";
            ProyectoPanel.Visible = true;
            RecursosUpdatePanel.Visible = true;
            ReporteEstadoUpdatePanel.Visible = true;
            HitoUpdatePanel.Visible = true;

            ActualizarRecursoGrid();
            ActualizarReporteEstadoGrid();
            ActualizarHitoGrid();
        }

        void EditarProyectoButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(String.Format("ProyectoForm.aspx?Id={0}", ProyectoDropDown.SelectedValue));
        }

        void ProximoProyectoButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(String.Format("PanelProyectoForm.aspx?IdProyecto={0}", ProyectoDropDown.Items[ProyectoDropDown.SelectedIndex+1].Value));
        }

        void VolverButton_Click(object sender, EventArgs e)
        {
            GoBack();
        }
    }
}