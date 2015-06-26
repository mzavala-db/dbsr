using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DBSR2
{
    public partial class RecursoProyectoForm : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            CancelarButton.Click += new EventHandler(CancelarButton_Click);
            GrabarButton.Click += new EventHandler(GrabarButton_Click);
            EliminarButton.Click += new EventHandler(EliminarButton_Click);

            if (!IsPostBack)
            {
                RecursoDropDown.DataSource = from r in DbsrContext.Recurso
                                             orderby r.Nombre
                                             select r;
                RecursoDropDown.DataBind();

                ProyectoDropDown.DataSource = from p in DbsrContext.Proyecto
                                              where p.Activo
                                              orderby p.Nombre
                                              select p;
                ProyectoDropDown.DataBind();

                if (Request["Id"] != null)
                {
                    int idRecursoProyecto = Convert.ToInt32(Request["Id"]);
                    ViewState["Id"] = idRecursoProyecto;

                    var recursoProyecto = DbsrContext.RecursoProyecto.Single(rp => rp.IdRecursoProyecto == idRecursoProyecto);

                    RecursoDropDown.SelectedValue = recursoProyecto.IdRecurso.ToString();
                    ProyectoDropDown.SelectedValue = recursoProyecto.IdProyecto.ToString();
                    if (recursoProyecto.FechaDesde.HasValue)
                        FechaDesdeTextBox.Text = recursoProyecto.FechaDesde.Value.ToString("dd/MM/yyyy");
                    if (recursoProyecto.FechaHasta.HasValue)
                        FechaHastaTextBox.Text = recursoProyecto.FechaHasta.Value.ToString("dd/MM/yyyy");
                    HorasTextBox.Text = recursoProyecto.Horas.ToString();
                    HorasFacturacionTextBox.Text = recursoProyecto.HorasFacturacion.ToString();
                    ObservacionesTextBox.Text = recursoProyecto.Observaciones;
                }

                if (Request["IdProyecto"] != null)
                {
                    Session["FiltroProyecto"] = Request["IdProyecto"];
                    ProyectoDropDown.SelectedValue = Request["IdProyecto"].ToString();
                }

                SetFocus(RecursoDropDown);
            }
        }

        void EliminarButton_Click(object sender, EventArgs e)
        {
            int idRecursoProyecto = Convert.ToInt32(ViewState["Id"]);

            RecursoProyecto recursoProyecto = DbsrContext.RecursoProyecto.Single(rp => rp.IdRecursoProyecto == idRecursoProyecto);

            DbsrContext.RecursoProyecto.Remove(recursoProyecto);
            DbsrContext.SaveChanges();

            if (Session["FiltroProyecto"] != null)
                Response.Redirect("RecursoProyectoList.aspx?IdProyecto=" + Session["FiltroProyecto"]);
            else
                Response.Redirect("RecursoProyectoList.aspx");
        }

        void GrabarButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                RecursoProyecto recursoProyecto;

                if (ViewState["Id"] == null)
                {
                    recursoProyecto = new RecursoProyecto
                    {
                        IdRecurso = Convert.ToInt32(RecursoDropDown.SelectedValue),
                        IdProyecto = Convert.ToInt32(ProyectoDropDown.SelectedValue),
                        Horas = Convert.ToByte(HorasTextBox.Text),
                        HorasFacturacion = Convert.ToByte(HorasFacturacionTextBox.Text),
                        Observaciones = ObservacionesTextBox.Text
                    };
                    if (FechaDesdeTextBox.Text != "")
                        recursoProyecto.FechaDesde = Convert.ToDateTime(FechaDesdeTextBox.Text);
                    if (FechaHastaTextBox.Text != "")
                        recursoProyecto.FechaHasta = Convert.ToDateTime(FechaHastaTextBox.Text);

                    DbsrContext.RecursoProyecto.Add(recursoProyecto);
                }
                else
                {
                    int idRecursoProyecto = Convert.ToInt32(ViewState["Id"]);

                    recursoProyecto = DbsrContext.RecursoProyecto.Single(rp => rp.IdRecursoProyecto == idRecursoProyecto);

                    recursoProyecto.IdRecurso = Convert.ToInt32(RecursoDropDown.SelectedValue);
                    recursoProyecto.IdProyecto = Convert.ToInt32(ProyectoDropDown.SelectedValue);
                    if (FechaDesdeTextBox.Text != "")
                        recursoProyecto.FechaDesde = Convert.ToDateTime(FechaDesdeTextBox.Text);
                    else
                        recursoProyecto.FechaDesde = null;
                    if (FechaHastaTextBox.Text != "")
                        recursoProyecto.FechaHasta = Convert.ToDateTime(FechaHastaTextBox.Text);
                    else
                        recursoProyecto.FechaHasta = null;
                    recursoProyecto.Horas = Convert.ToByte(HorasTextBox.Text);
                    recursoProyecto.HorasFacturacion = Convert.ToByte(HorasFacturacionTextBox.Text);
                    recursoProyecto.Observaciones = ObservacionesTextBox.Text;
                }
                DbsrContext.SaveChanges();

                if (Session["FiltroProyecto"] != null)
                    Response.Redirect("RecursoProyectoList.aspx?IdProyecto=" + Session["FiltroProyecto"]);
                else
                    Response.Redirect("RecursoProyectoList.aspx");
            }
        }

        void CancelarButton_Click(object sender, EventArgs e)
        {
            if (Session["FiltroProyecto"] != null)
                Response.Redirect("RecursoProyectoList.aspx?IdProyecto=" + Session["FiltroProyecto"]);
            else
                Response.Redirect("RecursoProyectoList.aspx");
        }
    }
}