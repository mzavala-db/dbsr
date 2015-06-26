using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DBSR2
{
    public partial class ProyectoForm : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CancelarButton.Click += new EventHandler(CancelarButton_Click);
            GrabarButton.Click += new EventHandler(GrabarButton_Click);
            EliminarButton.Click += new EventHandler(EliminarButton_Click);

            if (!IsPostBack)
            {
                ClienteDropDown.DataSource = from c in DbsrContext.Cliente
                                             orderby c.Nombre
                                             select c;
                ClienteDropDown.DataBind();

                CuentaFacturacionDropDown.DataSource = from cf in DbsrContext.CuentaFacturacion
                                                       orderby cf.Nombre
                                                       select cf;
                CuentaFacturacionDropDown.DataBind();

                ModalidadDropDown.DataSource = from m in DbsrContext.ModalidadProyecto
                                               orderby m.IdModalidadProyecto
                                               select m;
                ModalidadDropDown.DataBind();

                ProjectLeaderDropDown.DataSource = from r in DbsrContext.Recurso
                                                   where r.IdTipoRecurso == 1
                                                   select r;
                ProjectLeaderDropDown.DataBind();

                if (Request["Id"] != null)
                {
                    int idProyecto = Convert.ToInt32(Request["Id"]);
                    ViewState["Id"] = idProyecto;

                    var proyecto = DbsrContext.Proyecto.Single(p => p.IdProyecto == idProyecto);

                    NombreTextBox.Text = proyecto.Nombre;
                    ClienteDropDown.SelectedValue = proyecto.IdCliente.ToString();
                    CuentaFacturacionDropDown.SelectedValue = proyecto.IdCuentaFacturacion.ToString();
                    ModalidadDropDown.SelectedValue = proyecto.IdModalidadProyecto.ToString();
                    ProjectLeaderDropDown.SelectedValue = proyecto.IdRecursoPL.ToString();
                    if (proyecto.FechaFinEstimada != null)
                        FechaFinEstimadaTextBox.Text = proyecto.FechaFinEstimada.Value.ToString("dd/MM/yyyy");
                    if (proyecto.FechaFinReal != null)
                        FechaFinRealTextBox.Text = proyecto.FechaFinReal.Value.ToString("dd/MM/yyyy");
                    if (proyecto.FechaInicioEstimada != null)
                        FechaInicioEstimadaTextBox.Text = proyecto.FechaInicioEstimada.Value.ToString("dd/MM/yyyy");
                    if (proyecto.FechaInicioReal != null)
                        FechaInicioRealTextBox.Text = proyecto.FechaInicioReal.Value.ToString("dd/MM/yyyy");
                    ActivoCheckBox.Checked = proyecto.Activo;
                }
                else
                {
                    ActivoCheckBox.Checked = true;
                }
            }

        }

        void EliminarButton_Click(object sender, EventArgs e)
        {
            int idProyecto = Convert.ToInt32(ViewState["Id"]);

            Proyecto proyecto = DbsrContext.Proyecto.Single(p => p.IdProyecto == idProyecto);

            DbsrContext.Proyecto.Remove(proyecto);
            DbsrContext.SaveChanges();

            GoBack();
        }

        void GrabarButton_Click(object sender, EventArgs e)
        {
            Proyecto proyecto;

            if (ViewState["Id"] == null)
            {
                proyecto = new Proyecto {
                    Nombre = NombreTextBox.Text,
                    IdCliente = Convert.ToInt32(ClienteDropDown.SelectedValue),
                    IdCuentaFacturacion = Convert.ToInt32(CuentaFacturacionDropDown.SelectedValue),
                    IdModalidadProyecto = Convert.ToByte(ModalidadDropDown.SelectedValue),
                    IdRecursoPL = Convert.ToInt32(ProjectLeaderDropDown.SelectedValue),
                    FechaInicioEstimada = (FechaInicioEstimadaTextBox.Text != "") ? (DateTime?)Convert.ToDateTime(FechaInicioEstimadaTextBox.Text) : null,
                    FechaInicioReal = (FechaInicioRealTextBox.Text != "") ? (DateTime?)Convert.ToDateTime(FechaInicioRealTextBox.Text) : null,
                    FechaFinEstimada = (FechaFinEstimadaTextBox.Text != "") ? (DateTime?)Convert.ToDateTime(FechaFinEstimadaTextBox.Text) : null,
                    FechaFinReal = (FechaFinRealTextBox.Text != "") ? (DateTime?)Convert.ToDateTime(FechaFinRealTextBox.Text) : null,
                    Activo = ActivoCheckBox.Checked
                };
                DbsrContext.Proyecto.Add(proyecto);
            }
            else
            {
                int idProyecto = Convert.ToInt32(ViewState["Id"]);

                proyecto = DbsrContext.Proyecto.Single(p => p.IdProyecto == idProyecto);

                proyecto.Nombre = NombreTextBox.Text;
                proyecto.IdCliente = Convert.ToInt32(ClienteDropDown.SelectedValue);
                proyecto.IdCuentaFacturacion = Convert.ToInt32(CuentaFacturacionDropDown.SelectedValue);
                proyecto.IdModalidadProyecto = Convert.ToByte(ModalidadDropDown.SelectedValue);
                proyecto.IdRecursoPL = Convert.ToInt32(ProjectLeaderDropDown.SelectedValue);
                proyecto.FechaInicioEstimada = (FechaInicioEstimadaTextBox.Text != "") ? (DateTime?)Convert.ToDateTime(FechaInicioEstimadaTextBox.Text) : null;
                proyecto.FechaInicioReal = (FechaInicioRealTextBox.Text != "") ? (DateTime?)Convert.ToDateTime(FechaInicioRealTextBox.Text) : null;
                proyecto.FechaFinEstimada = (FechaFinEstimadaTextBox.Text != "") ? (DateTime?)Convert.ToDateTime(FechaFinEstimadaTextBox.Text) : null;
                proyecto.FechaFinReal = (FechaFinRealTextBox.Text != "") ? (DateTime?)Convert.ToDateTime(FechaFinRealTextBox.Text) : null;
                proyecto.Activo = ActivoCheckBox.Checked;
            }
            DbsrContext.SaveChanges();

            GoBack();
        }

        void CancelarButton_Click(object sender, EventArgs e)
        {
            GoBack();
        }
    }
}