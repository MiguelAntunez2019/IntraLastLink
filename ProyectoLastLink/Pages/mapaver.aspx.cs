using ProyectoLastLink.clases;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Principal;
using System.Data.SqlTypes;
using iTextSharp.text.pdf.codec.wmf;
using System.Configuration.Provider;
using Newtonsoft.Json;
using System.Net;

namespace ProyectoLastLink.Pages
{
    public partial class mapaver : System.Web.UI.Page
    {
        DataTable Tabla;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                Gvbind();
                vermapa();
            }
        }

        public void vermapa()
        {
            SqlConnection conexionSQL = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select (Id_Plat+'-'+Direccion +','+Comuna) as info,(Latitud +',' + Longitud) as posicion from Lista_Planificacion where  estado=1";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conexionSQL;
            conexionSQL.Open();
            Tabla = new DataTable();
            Tabla.Load(cmd.ExecuteReader());
            Repeater1.DataSource = Tabla;
            Repeater1.DataBind();
            conexionSQL.Close();
        }
        protected void Gvbind()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
            SqlCommand cmd = new SqlCommand("sp_datos_Info_mapa", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows == true)
            {

                GridView1.DataSource = dr;
                GridView1.DataBind();

            }


        }
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            Gvbind();
        }
        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["conexion"].ToString();
            int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
            string Pedido = ((TextBox)GridView1.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
            String Direccion = ((TextBox)GridView1.Rows[e.RowIndex].Cells[2].Controls[0]).Text;
            string Comuna = ((TextBox)GridView1.Rows[e.RowIndex].Cells[3].Controls[0]).Text;
            // string Latitud = ((TextBox)GridView1.Rows[e.RowIndex].Cells[4].Controls[0]).Text;
            // string Longitud = ((TextBox)GridView1.Rows[e.RowIndex].Cells[5].Controls[0]).Text;

           string url_map = "https://maps.googleapis.com/maps/api/geocode/json?address=direccion&key=AIzaSyD4sH_gxiyWTb6oWiIg8-xFYPvp4gFdQi0";



            using (SqlConnection con = new SqlConnection(cs))

            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update Lista_Planificacion set Url_Mapa='" + url_map + "',Direccion='" + Direccion + "',Latitud='0',Longitud='0',  Comuna='" + Comuna + "'  where id_Lista_planificacion='" + id + "' ", con);
                int t = cmd.ExecuteNonQuery();
                if (t > 0)
                {
                    Lista_Bodega_PreArmado(id);
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "Swal.fire({icon: 'success',title: 'Ok..',  text: 'Información Actualizada!'})", true);

                    GridView1.EditIndex = -1;
                    Gvbind();
                    vermapa();
                }
            }
        }

        private void Lista_Bodega_PreArmado(int id)
        {
            //string url_map = "https://maps.googleapis.com/maps/api/geocode/json?address=direccion&key=AIzaSyD4sH_gxiyWTb6oWiIg8-xFYPvp4gFdQi0";
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
                SqlCommand cmd = new SqlCommand("sp_actualiza_Lista_Planificacion", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@idCabeceraPedido", SqlDbType.Int).Value = id;
               // cmd.Parameters.Add("@url_map", System.Data.SqlDbType.VarChar).Value = url_map;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                con.Close();
                //una vez insertado el registro de sacar la url de la BD y leer
                //la url con jason 
                sacar_productos_url_paraMapa();
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void sacar_productos_url_paraMapa()
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
                SqlCommand cmd = new SqlCommand("sp_datos_extraer_urlMapas", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                int id_Lista_planificacion;
                string Url_mapa;
                while (dr.Read())
                {
                    id_Lista_planificacion = Convert.ToInt32(dr["id_Lista_planificacion"]);
                    Url_mapa = dr["Url_Mapa"].ToString();
                    sacar_lon_lat(id_Lista_planificacion, Url_mapa);
                }

                con.Close();
            }
            catch (Exception)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "Swal.fire({icon: 'error',title: 'Error..',  text: 'Registro sin Dirección o Con Signos Extraños!'})", true);
                
            }

        }

                    private void sacar_lon_lat(int id_Lista, string Url)
                    {
                        //string url = @"https://maps.googleapis.com/maps/api/geocode/json?address=Suecia,2045,Santiago,Providencia,Chile&key=AIzaSyD4sH_gxiyWTb6oWiIg8-xFYPvp4gFdQi0";
                        string json;
                        using (var client = new WebClient())
                        {
                            json = client.DownloadString(Url);
                        }
                        AplicationGoogle.Result coord = JsonConvert.DeserializeObject<AplicationGoogle.Result>(json);
                        var cordenada = coord;
                        var latitud = coord.results.Select(x => x.geometry.location.lat).First();
                        var longitud = coord.results.Select(x => x.geometry.location.lng).First();

                        // var coordenadas = "Latitud:" + latitud + "Longitud:" + longitud;
                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
                        SqlCommand cmd = new SqlCommand("sp_ingresar_Lat_long", con);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_Lista", System.Data.SqlDbType.VarChar).Value = id_Lista;
                        cmd.Parameters.Add("@latitud", SqlDbType.VarChar).Value = latitud;
                        cmd.Parameters.Add("@longitud", SqlDbType.VarChar).Value = longitud;

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();


                    }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            Gvbind();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("poligono.aspx");
        }
    }
}