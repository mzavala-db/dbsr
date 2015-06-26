using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


//using Google;
//using Google.Apis.Auth.OAuth2;
//using Google.Apis.Calendar.v3;
//using Google.Apis.Calendar.v3.Data;
//using Google.Apis.Services;

namespace DBSR2
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //UserCredential credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
            //    new ClientSecrets
            //    {
            //        ClientId = "814884687579-t8miqn6ibrop48fvi79ss3vu84s2ofro.apps.googleusercontent.com",
            //        ClientSecret = "PEaUaUThQoNrKEOr5Y7mrL0d",
            //    },

            //    new[] { CalendarService.Scope.Calendar },
            //    "user", System.Threading.CancellationToken.None).Result;

            //// Create the service.
            //var service = new CalendarService(new BaseClientService.Initializer()
            //{
            //    HttpClientInitializer = credential,
            //    ApplicationName = "DBSR2",
            //});

            //var ev = new Event();
            //ev.Summary = "Vacaciones de alguien";
            //ev.Start = new EventDateTime() { Date = "2014-10-01" };
            //ev.End = new EventDateTime() { Date = "2014-10-15" };


            //service.Events.Insert(ev, "Vacaciones Digbang");

            /*
            var hito = new Hito
            {
                FechaAlta = DateTime.Now,
                FechaEstimada = Convert.ToDateTime("15/07/2014"),
                FechaCumplimiento = null,
                Descripcion = "Prueba de alta de tarea"
            };

            long idTaskAsana = AsanaService.AddTask(hito);
            hito.IdTaskAsana = (idTaskAsana != 0) ? (long?)idTaskAsana : null;
            */
            Response.Redirect("ProyectoList.aspx");
        }
    }
}