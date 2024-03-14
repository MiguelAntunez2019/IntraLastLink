using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Ordenes;
using System.Windows.Markup;

namespace ProyectoLastLink.Pages
{
    public partial class MigracionVentas : System.Web.UI.Page
    {
       
            protected void Page_Load(object sender, EventArgs e)
            {
              
            }

        public class Resultado
        { 
            public string Contador { get; set; }
        }

        [WebMethod(EnableSession = true)]
        [AllowAnonymous]
        public static async Task<Resultado> EjecutarPrograma(string name)
        {
            try
            {
                Program programa = new Program();
                await programa.EjecutaPrograma();
                int cuentaOrdenes = CuentaOrdenes();
                MigracionVentas mg = new MigracionVentas();
                mg.migrar_data();
             
               

                if (cuentaOrdenes.ToString() != null)
                {
                    DateTime hora_ejecucion = DateTime.Now;
                    TimeZoneInfo zonaHorariaChile = TimeZoneInfo.FindSystemTimeZoneById("Pacific SA Standard Time");
                    DateTime hora_ejecucion_chilena = TimeZoneInfo.ConvertTime(hora_ejecucion, zonaHorariaChile);

                    string hora_ejecucion_formateada = hora_ejecucion_chilena.ToString("yyyy-MM-dd HH:mm:ss");

                    string ejecutamigracion = "Se ejecuta migracion a las " + hora_ejecucion_formateada + " de forma manual";
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
                return new Resultado { Contador = cuentaOrdenes.ToString() };
            }
         

       
            catch (Exception ex)
            {
                return new Resultado { Contador = "Ocurrió un error." };
            }
           
        }

        public  void migrar_data()
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
                SqlCommand cmd = new SqlCommand("sp_datos_extrae_dato_cargar", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                string Id_plat;
                int id_seller;
                string comuna;
                while (dr.Read())
                {
                    Id_plat = dr["id_Plat"].ToString();
                    id_seller = Convert.ToInt32(dr["id_seller"]);
                    comuna = dr["comuna"].ToString();

                    insertar_venta_tabla_final(Id_plat, id_seller, comuna);
                }
            }
            catch
            {
               // Response.Write("<script>alert('No se pudo acceder a la base de datos')</script>");
                // ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "Swal.fire({icon: 'error',title: 'OOPS..',  text: 'No se pudo acceder a la base de datos!'})", true);
                throw;
            }
        }
        public static void insertar_venta_tabla_final(string Id_plat, int id_seller, string comuna)
        {
            try
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
                SqlCommand cmd = new SqlCommand("SP_cargar_Migracion", con);
                cmd.Parameters.Add("@Id_plat", System.Data.SqlDbType.VarChar).Value = Id_plat;
                cmd.Parameters.Add("@id_seller", SqlDbType.Int).Value = id_seller;
                cmd.Parameters.Add("@comuna", System.Data.SqlDbType.VarChar).Value = comuna;

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                //aqui va el open de conexion en caso que mande error
                SqlDataReader dr = cmd.ExecuteReader();
                con.Close();
                eliminar_duplicados_ventas();
            }
            catch
            {
             //   ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "Swal.fire({icon: 'error',title: 'OOPS..',  text: 'No se pudo acceder a la base de datos!'})", true);
                throw;
            }

        }
        protected static void eliminar_duplicados_ventas()
        {

            try
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
                SqlCommand cmd = new SqlCommand("sp_eliminar_duplicados_Pedido", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                //aqui va el open de conexion en caso que mande error
                SqlDataReader dr = cmd.ExecuteReader();
                con.Close();
            }
            catch
            {
             //   ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "Swal.fire({icon: 'error',title: 'OOPS..',  text: 'No se pudo acceder a la base de datos!'})", true);
                throw;
            }

        }

        public static int CuentaOrdenes()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
            con.Open();

            SqlCommand cmd = new SqlCommand("ContarOrdenesDeCompra", con);
            cmd.CommandType = CommandType.StoredProcedure;

           
            int contador = (int)cmd.ExecuteScalar();

            con.Close();

         
            return contador;
        }

        public static int Cuenta()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
            con.Open();

            SqlCommand cmd = new SqlCommand("ContarOrdenesDeCompra", con);
            cmd.CommandType = CommandType.StoredProcedure;

        
            int contador = (int)cmd.ExecuteScalar();

            con.Close();

            return contador;
        }

        [WebMethod]
            protected void btnGetData_Click(object sender, EventArgs e)
            {
                
            }
        
    }
}