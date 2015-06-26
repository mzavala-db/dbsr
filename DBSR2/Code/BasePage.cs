using System;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DBSR2
{
    public class BasePage : System.Web.UI.Page
    {
        public DBSREntities2 DbsrContext;

        public BasePage()
        {
            this.Load += new EventHandler(BasePage_Load);
        }
        
        protected override void OnInit(EventArgs e)
        {
            DbsrContext = new DBSREntities2();
        }

        protected void BasePage_Load(object sender, EventArgs e)
        {
            // validar roles
            foreach (RequireRoleAttribute att in Attribute.GetCustomAttributes(this.GetType(), typeof(RequireRoleAttribute)))
            {
                if (!User.IsInRole(att.Role))
                {
                    FormsAuthentication.SignOut();
                    Response.Redirect("~/Login.aspx");
                }
            }

            if (!IsPostBack)
            {
                if (Request.UrlReferrer != null)
                    ViewState["UrlReferrer"] = Request.UrlReferrer.ToString();
            }
        }

        public void GoBack()
        {
            if (ViewState["UrlReferrer"] != null)
                Response.Redirect(ViewState["UrlReferrer"].ToString());
        }

        public void MostrarPopup(string msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "errorMessage", String.Format("alert('{0}');", msg), true);
        }

        public void DescargarArchivo(string fileName)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "downloadFile", String.Format("window.location='{0}/{1}';", "files", fileName), true);
        }

        public string SortExpression
        {
            get
            {
                var sortDirection = ViewState["SortDirection"];
                var sortExpression = ViewState["SortExpression"];

                if (sortDirection != null)
                {
                    return sortExpression + " " + ((SortDirection)sortDirection).ToString().ToLower();
                }
                else
                    return null;
            }
        }

    }
}