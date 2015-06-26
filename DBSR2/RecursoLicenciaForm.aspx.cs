using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DBSR2
{
    public partial class RecursoLicenciaForm : BasePage
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

                MotivoLicenciaDropDown.DataSource = from ml in DbsrContext.MotivoLicencia
                                                    orderby ml.Descripcion
                                                    select ml;
                MotivoLicenciaDropDown.DataBind();

                if (Request["Id"] != null)
                {
                    int idRecursoLicencia = Convert.ToInt32(Request["Id"]);
                    ViewState["Id"] = idRecursoLicencia;

                    var recursoLicencia = DbsrContext.RecursoLicencia.Single(rl => rl.IdRecursoLicencia == idRecursoLicencia);

                    RecursoDropDown.SelectedValue = recursoLicencia.IdRecurso.ToString();
                    MotivoLicenciaDropDown.SelectedValue = recursoLicencia.IdMotivoLicencia.ToString();
                    FechaDesdeTextBox.Text = recursoLicencia.FechaDesde.ToString("dd/MM/yyyy");
                    FechaHastaTextBox.Text = recursoLicencia.FechaHasta.ToString("dd/MM/yyyy");
                    ObservacionesTextBox.Text = recursoLicencia.Observaciones;
                }

                if (Request["IdRecurso"] != null)
                {
                    Session["FiltroRecurso"] = Request["IdRecurso"];
                    RecursoDropDown.SelectedValue = Request["IdRecurso"].ToString();
                }

                SetFocus(RecursoDropDown);
            }
        }

        void EliminarButton_Click(object sender, EventArgs e)
        {
            int idRecursoLicencia = Convert.ToInt32(ViewState["Id"]);

            RecursoLicencia recursoLicencia = DbsrContext.RecursoLicencia.Single(rl => rl.IdRecursoLicencia == idRecursoLicencia);

            DbsrContext.RecursoLicencia.Remove(recursoLicencia);
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
                RecursoLicencia recursoLicencia;

                if (ViewState["Id"] == null)
                {
                    recursoLicencia = new RecursoLicencia
                    {
                        IdRecurso = Convert.ToInt32(RecursoDropDown.SelectedValue),
                        IdMotivoLicencia = Convert.ToByte(MotivoLicenciaDropDown.SelectedValue),
                        FechaDesde = Convert.ToDateTime(FechaDesdeTextBox.Text),
                        FechaHasta = Convert.ToDateTime(FechaHastaTextBox.Text),
                        Observaciones = ObservacionesTextBox.Text
                    };

                    DbsrContext.RecursoLicencia.Add(recursoLicencia);
                }
                else
                {
                    int idRecursoLicencia = Convert.ToInt32(ViewState["Id"]);

                    recursoLicencia = DbsrContext.RecursoLicencia.Single(rl => rl.IdRecursoLicencia == idRecursoLicencia);

                    recursoLicencia.IdRecurso = Convert.ToInt32(RecursoDropDown.SelectedValue);
                    recursoLicencia.IdMotivoLicencia = Convert.ToByte(MotivoLicenciaDropDown.SelectedValue);
                    recursoLicencia.FechaDesde = Convert.ToDateTime(FechaDesdeTextBox.Text);
                    recursoLicencia.FechaHasta = Convert.ToDateTime(FechaHastaTextBox.Text);
                    recursoLicencia.Observaciones = ObservacionesTextBox.Text;
                }
                DbsrContext.SaveChanges();

                if (Session["FiltroRecurso"] != null)
                    Response.Redirect("RecursoLicenciaList.aspx?IdRecurso=" + Session["FiltroRecurso"]);
                else
                    Response.Redirect("RecursoLicenciaList.aspx");
            }
        }

        void CancelarButton_Click(object sender, EventArgs e)
        {
            if (Session["FiltroRecurso"] != null)
                Response.Redirect("RecursoLicenciaList.aspx?IdRecurso=" + Session["FiltroRecurso"]);
            else
                Response.Redirect("RecursoLicenciaList.aspx");
        }
    }
}