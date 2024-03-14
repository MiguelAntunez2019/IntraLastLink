using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text.pdf.codec.wmf;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using System.Net;
using Newtonsoft.Json;
using System.Windows.Markup;
using ZXing;
using AplicationGoogle;

namespace ProyectoLastLink.Pages
{
    public partial class Planificador : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //  Gvbind();
            if (!IsPostBack)
            {
                CargarDatos();
            }
        }

        private void CargarDatos()
        {
            try
            {
                DataTable datatable = new DataTable();
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());

                //SqlDataAdapter da = new SqlDataAdapter("sp_datos_Movimiento", con);
                SqlCommand cmd = new SqlCommand("Listar_Ordenes_despacho", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                con.Open();
                adapter.Fill(datatable);
                con.Close();
                GridView1.DataSource = datatable;
                GridView1.DataBind();
                if (datatable.Rows.Count == 0)
                {

                    GridView1.Visible = false;
                }
                else
                {

                    GridView1.Visible = true;
                }



            }
            catch
            {
                throw;

            }

        }



        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            CargarDatos();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string buscar = BuscarNombre.Text.Trim();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
            SqlCommand cmd = new SqlCommand("Listar_Ordenes_buscar_Proveedor_Planificacion", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Busqueda", buscar);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable datatable = new DataTable();
            con.Open();
            adapter.Fill(datatable);
            con.Close();
            GridView1.DataSource = datatable;
            GridView1.DataBind();

            if (datatable.Rows.Count == 0)
            {

                GridView1.Visible = false;
            }
            else
            {

                GridView1.Visible = true;
            }




        }

        protected void GridView11_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView11.PageIndex = e.NewPageIndex;
        }


        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            string id = GridView1.SelectedRow.Cells[1].Text;
            string Id_Plat = GridView1.SelectedRow.Cells[3].Text;
            CargarDatos_movimiento(Id_Plat);



            Origen.Text = GridView1.SelectedRow.Cells[2].Text;
            Id_plat.Text = GridView1.SelectedRow.Cells[3].Text;
            Proveedor.Text = GridView1.SelectedRow.Cells[4].Text;
            Nombre_Comprador.Text = GridView1.SelectedRow.Cells[5].Text;
            Direccion.Text = GridView1.SelectedRow.Cells[6].Text;
            //Comuna.Text = GridView1.SelectedRow.Cells[6].Text;
            // Telefono.Text = GridView1.SelectedRow.Cells[7].Text;
            Correo_electronico.Text = GridView1.SelectedRow.Cells[8].Text;
            //Fecha_Venta.Text = GridView1.SelectedRow.Cells[7].Text;

            ModalPopupExtender1.Show();
        }


        private void CargarDatos_movimiento(string Id_Plat)
        {
            try
            {

                DataTable datatable = new DataTable();
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());

                //SqlDataAdapter da = new SqlDataAdapter("sp_datos_Movimiento", con);
                SqlCommand cmd = new SqlCommand("sp_datos_Detalle_Pedidopopup", con);
                cmd.Parameters.AddWithValue("@Id_Plat", Id_Plat);

                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                con.Open();
                adapter.Fill(datatable);
                con.Close();
                GridView11.DataSource = datatable;
                GridView11.DataBind();
                if (datatable.Rows.Count == 0)
                {

                    GridView11.Visible = false;
                }
                else
                {

                    GridView11.Visible = true;
                }



            }
            catch
            {
                throw;

            }

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            //Panel3 es seller
            //Panel_Fecha
            //buscar por fecha
            Panel3.Visible = false;
            Panel_Fecha.Visible = true;

        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            //BuscarNombre por estado
            Panel3.Visible = true;
            Panel_Fecha.Visible = false;

        }


        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            BuscarNombre_calendario.Text = Calendar1.SelectedDate.ToString();

        }
        protected void Calendar2_SelectionChanged(object sender, EventArgs e)
        {
            BuscarNombre2_calendario.Text = Calendar2.SelectedDate.ToString();

        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            //BuscarNombre2.Text = Calendar1.SelectedDate.ToString();
            string buscar = BuscarNombre_calendario.Text.Trim();
            string buscar2 = BuscarNombre2_calendario.Text.Trim();
            var fecha1 = DateTime.Now;
            var fecha2 = DateTime.Now;
            DateTime dt, dt2;
            var date1 = DateTime.TryParse(buscar, out dt);
            var date2 = DateTime.TryParse(buscar2, out dt2);

            if (date1 && date2)
            {
                fecha1 = dt;
                fecha2 = dt2;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
            SqlCommand cmd = new SqlCommand("Listar_Ordenes_buscar_Planificado", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Busqueda", fecha1);
            cmd.Parameters.AddWithValue("@Busqueda2", fecha2);


            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable datatable = new DataTable();
            con.Open();
            adapter.Fill(datatable);
            con.Close();
            GridView1.DataSource = datatable;
            GridView1.DataBind();

            if (datatable.Rows.Count == 0)
            {

                GridView1.Visible = false;
            }
            else
            {

                GridView1.Visible = true;
            }

        }

        protected void ChkEmpty_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkstatus = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chkstatus.NamingContainer;

        }

        protected void ChkHeader_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkHeader = (CheckBox)GridView1.HeaderRow.FindControl("ChkHeader");
            foreach (GridViewRow row in GridView1.Rows)
            {
                CheckBox chkrow = (CheckBox)row.FindControl("ChkEmpty");
                if (chkHeader.Checked == true)
                {
                    chkrow.Checked = true;
                }
                else
                {
                    chkrow.Checked = false;
                }

            }
        }

        protected void CambiarEstado_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                CheckBox chkdelete = (CheckBox)GridView1.Rows[i].Cells[0].FindControl("ChkEmpty");
                if (chkdelete.Checked == true)
                {
                    int id = Convert.ToInt32(GridView1.Rows[i].Cells[1].Text);
                    string idplat = (GridView1.Rows[i].Cells[3].Text);
                    Lista_Bodega_PreArmado(id);
                    SqlConnection cone = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);
                    cone.Open();
                    int Estado = 6;
                    SqlCommand cmd = new SqlCommand("update Cabecera_Pedido set Estado='" + Estado + "' where id= '" + id + "' ", cone);

                    // cmd.ExecuteNonQuery();
                    int t = cmd.ExecuteNonQuery();

                    if (t > 0)
                    {

                        GridView1.EditIndex = -1;

                    }
                    cone.Close();
                    cargar_estado_log(idplat, Estado);



                }
            }
            CargarDatos();
        }

        private void cargar_estado_log(string idplat, int Estado)
        {
            try
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
                SqlCommand cmd = new SqlCommand("sp_registrar_log_estado", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Plat", System.Data.SqlDbType.VarChar).Value = idplat;
                cmd.Parameters.Add("@Estado", SqlDbType.Int).Value = Estado;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception)
            {

                throw;
            }


        }

        private void Lista_Bodega_PreArmado(int id)
        {
          string url_map = "https://maps.googleapis.com/maps/api/geocode/json?address=direccion&key=AIzaSyD4sH_gxiyWTb6oWiIg8-xFYPvp4gFdQi0";
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
                SqlCommand cmd = new SqlCommand("sp_registrar_Lista_Planificacion", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@idCabeceraPedido", SqlDbType.Int).Value = id;
                cmd.Parameters.Add("@url_map", System.Data.SqlDbType.VarChar).Value = url_map;
                con.Open();
                int t = cmd.ExecuteNonQuery();
                if (t > 0)
                {
                       ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "Swal.fire({icon: 'success',title: 'Ok..',  text: 'Ingreso Realizado Correctamente!'})", true);
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "Swal.fire({icon: 'error',title: 'Ops..',  text: 'Hubo Un Error al Momento de Generar Carga!'})", true);
                }
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
                 ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "Swal.fire({icon: 'error',title: 'Error..',  text: 'Registro sin Dirección!'})", true);
                throw;
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
    }
}