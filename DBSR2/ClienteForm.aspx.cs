using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DBSR2
{
    public partial class ClienteForm : BasePage
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
                    int idCliente = Convert.ToInt32(Request["Id"]);
                    ViewState["Id"] = idCliente;

                    var cliente = DbsrContext.Cliente.Single(c => c.IdCliente == idCliente);

                    NombreTextBox.Text = cliente.Nombre;
                }
            }
        }

        void EliminarButton_Click(object sender, EventArgs e)
        {
            int idCliente = Convert.ToInt32(ViewState["Id"]);

            Cliente cliente = DbsrContext.Cliente.Single(c => c.IdCliente == idCliente);

            DbsrContext.Cliente.Remove(cliente);
            DbsrContext.SaveChanges();

            Response.Redirect("ClienteList.aspx");
        }

        void GrabarButton_Click(object sender, EventArgs e)
        {

            Cliente cliente;

            if (ViewState["Id"] == null)
            {
                cliente = new Cliente { Nombre = NombreTextBox.Text };
                DbsrContext.Cliente.Add(cliente);
            }
            else
            {
                int idCliente = Convert.ToInt32(ViewState["Id"]);

                cliente = DbsrContext.Cliente.Single(c => c.IdCliente == idCliente);

                cliente.Nombre = NombreTextBox.Text;
            }
            DbsrContext.SaveChanges();

            Response.Redirect("ClienteList.aspx");
        }

        void CancelarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClienteList.aspx");
        }
    }
}