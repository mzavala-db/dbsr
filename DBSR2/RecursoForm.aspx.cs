using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DBSR2
{
    public partial class RecursoForm : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            CancelarButton.Click += new EventHandler(CancelarButton_Click);
            GrabarButton.Click += new EventHandler(GrabarButton_Click);
            EliminarButton.Click += new EventHandler(EliminarButton_Click);

            if (!IsPostBack)
            {
                TipoRecursoDropDown.DataSource = from tp in DbsrContext.TipoRecurso
                                                 orderby tp.Descripcion
                                                 select tp;
                TipoRecursoDropDown.DataBind();

                if (Request["Id"] != null)
                {
                    int idRecurso = Convert.ToInt32(Request["Id"]);
                    ViewState["Id"] = idRecurso;

                    var recurso = DbsrContext.Recurso.Single(r => r.IdRecurso == idRecurso);

                    NombreTextBox.Text = recurso.Nombre;
                    TipoRecursoDropDown.SelectedValue = recurso.IdTipoRecurso.ToString();
                    ActivoCheckBox.Checked = recurso.Activo;
                }
            }

        }

        void EliminarButton_Click(object sender, EventArgs e)
        {
            int idRecurso = Convert.ToInt32(ViewState["Id"]);

            Recurso recurso = DbsrContext.Recurso.Single(r => r.IdRecurso == idRecurso);

            DbsrContext.Recurso.Remove(recurso);
            DbsrContext.SaveChanges();

            Response.Redirect("RecursoList.aspx");
        }

        void GrabarButton_Click(object sender, EventArgs e)
        {
            Recurso recurso;

            if (ViewState["Id"] == null)
            {
                recurso = new Recurso
                {
                    Nombre = NombreTextBox.Text,
                    IdTipoRecurso = Convert.ToByte(TipoRecursoDropDown.SelectedValue),
                    Activo = ActivoCheckBox.Checked
                };
                DbsrContext.Recurso.Add(recurso);
            }
            else
            {
                int idRecurso = Convert.ToInt32(ViewState["Id"]);

                recurso = DbsrContext.Recurso.Single(r => r.IdRecurso == idRecurso);

                recurso.Nombre = NombreTextBox.Text;
                recurso.IdTipoRecurso = Convert.ToByte(TipoRecursoDropDown.SelectedValue);
                recurso.Activo = ActivoCheckBox.Checked;
            }
            DbsrContext.SaveChanges();

            Response.Redirect("RecursoList.aspx");
        }

        void CancelarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("RecursoList.aspx");
        }
    }
}