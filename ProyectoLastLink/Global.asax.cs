using ProyectoLastLink.Pages;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

namespace ProyectoLastLink
{
    public class Global : HttpApplication
    {

        private static Timer _Timer;
        void Application_Start(object sender, EventArgs e)
        {
   
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            System.Web.Routing.RouteTable.Routes.MapPageRoute("Default", "", "~/Pages/Index.aspx");
            EjecutaTareaProgramada();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Credentials", "true");

            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                HttpContext.Current.Response.StatusCode = 200;
                HttpContext.Current.Response.End();
            }

         
        }

        private static void EjecutaTareaProgramada()
        {
            try
            {
                _Timer = new Timer(EjecutarTareaProgramada, null, TimeSpan.Zero, TimeSpan.FromMinutes(10));
            } catch(Exception ex) {
                ex.ToString();
            }

        }

        private async static void EjecutarTareaProgramada(object state)
        {
            try
            {
                MigracionVentas mg = new MigracionVentas();
                string nombre = "";
                await Task.Run(() => MigracionVentas.EjecutarPrograma(nombre));
                
            }
            catch (Exception ex)
            {
                string ejecutamigracion = ex.ToString();
                var conexion = ConfigurationManager.ConnectionStrings["conexion"].ToString();

                SqlConnection con = new SqlConnection(conexion);
                con.Open();
                if (con.State == ConnectionState.Open)
                {

                    SqlCommand com = new SqlCommand("insert into Log_EjecucionMigracion (informacion) values('" + ejecutamigracion + "')", con);
                    com.ExecuteNonQuery();
                }
                else
                {
                    con.Open();
                }
            }
        }


    }
}