using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Security.Claims;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using System.IO;
using static System.Net.WebRequestMethods;
using iTextSharp.text;
using System.Windows;
using System.Security.Policy;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;
using Org.BouncyCastle.Pqc.Crypto.Lms;

namespace ProyectoLastLink.Pages
{
    public partial class Verifica_PreBatch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //  Gvbind();

            if (!IsPostBack)
            {
                //CargarDatos();
            }
        }






        //public void enviar_datos_correo_estado(string Codigo_Pedido, string Fecha, string Estado2, string Direccion, string Comuna, string Telefono, string Correo, string proveedor, string comprador)
        //{
        //    string cadena2 = obtenerDatos_articulos_pedido(Codigo_Pedido);

        //    string cadena = "<p style'font-family: tahoma;font-size: 12px;'>Estimado/a  <strong>" + comprador + "</strong> </p>";
        //    cadena = cadena + "			 <p style='font - family: tahoma; font - size: 12px; '>Mediante el presente correo, informamos a usted que su Pedido esta siendo Armado para Pronto Despacho</p>";
        //    cadena = cadena + "            <p style='font - family: tahoma; font - size: 12px; '>A continuación encontrará el detalle del Pedido</p>";
        //    cadena = cadena + "              <table width='750' border='0' cellpadding='2' cellspacing='1' bgcolor='#e5e5e5'>";
        //    cadena = cadena + "             <thead>";
        //    cadena = cadena + "             		<tr bgcolor='FFFFF'>";
        //    cadena = cadena + "             			<td style=font-family: tahoma;font-size: 12px;' colspan='2'><strong>Información del Pedido<strong></td>";
        //    cadena = cadena + "              		</tr>";
        //    cadena = cadena + "             	</thead>";
        //    cadena = cadena + "              	<tbody>";
        //    cadena = cadena + "              		<tr bgcolor='FFFFF' > ";
        //    cadena = cadena + "              			<td width='200' style='font - family: tahoma; font - size: 12px; '>Fecha:</td>";
        //    cadena = cadena + "              			<td width='550' style='font - family: tahoma; font - size: 12px; color:#5F9EA0'>" + Fecha + "</td>";
        //    cadena = cadena + "              		</tr>";
        //    cadena = cadena + "             		<tr bgcolor='#FFFFF'>";
        //    cadena = cadena + "             			<td width='200' style='font-family: tahoma;font-size: 12px;'>Codigo Pedido:</td>";
        //    cadena = cadena + "              			<td width='550' style='font-family: tahoma;font-size: 12px;color:#5F9EA0'>" + Codigo_Pedido + "</td>";
        //    cadena = cadena + "              		</tr>";
        //    cadena = cadena + "			 		<tr bgcolor='#FFFFF'>";
        //    cadena = cadena + "              			<td width='200' style='font-family: tahoma;font-size: 12px;'>Proveedor:</td>";
        //    cadena = cadena + "             			<td width='550' style='font-family: tahoma;font-size: 12px;color:#5F9EA0'>" + proveedor + "</td>";
        //    cadena = cadena + "              		</tr>";
        //    cadena = cadena + "			 		<tr bgcolor='#FFFFF'>";
        //    cadena = cadena + "              			<td width='200' style='font-family: tahoma;font-size: 12px;'>Estado del Pedido</td>";
        //    cadena = cadena + "              			<td width='550' style='font-family: tahoma;font-size: 12px;color:#5F9EA0'>" + Estado2 + "</td>";
        //    cadena = cadena + "              		</tr>";
        //    cadena = cadena + "			 		<tr bgcolor='#FFFFF'>";
        //    cadena = cadena + "              			<td width='200' style='font-family: tahoma;font-size: 12px;'>Dirección Comprador</td>";
        //    cadena = cadena + "              			<td width='550' style='font-family: tahoma;font-size: 12px;color:#5F9EA0'>" + Direccion + "</td>";
        //    cadena = cadena + "             		</tr>";
        //    cadena = cadena + "			 		<tr bgcolor='#FFFFF'>";
        //    cadena = cadena + "              			<td width='200' style='font-family: tahoma;font-size: 12px;'>Comuna Comprador</td>";
        //    cadena = cadena + "              			<td width='550' style='font-family: tahoma;font-size: 12px;color:#5F9EA0'>" + Comuna + "</td></tr><tr bgcolor='#FFFFF'>";
        //    cadena = cadena + "             			<td width='200' style='font-family: tahoma;font-size: 12px;'>Telefono Comprador</td>";
        //    cadena = cadena + "              			<td width='550' style='font-family: tahoma;font-size: 12px;color:#5F9EA0'>" + Telefono + "</td></tr>";
        //    cadena = cadena + "			 		<tr bgcolor='#FFFFF'>";
        //    cadena = cadena + "              			<td width='200' style='font-family: tahoma;font-size: 12px;'>Correo Comprador</td>";
        //    cadena = cadena + "              			<td width='550' style='font - family: tahoma; font - size: 12px; color:#5F9EA0'>" + Correo + "</td>";
        //    cadena = cadena + "              		</tr>";
        //    cadena = cadena + "                 	</tbody>";
        //    cadena = cadena + "             </table>";

        //    string cadena_final = cadena + cadena2;




        //    var email = new MimeMessage();
        //    email.From.Add(new MailboxAddress("Pedido Nro. : " + Codigo_Pedido + " En Proceso de Armado", "servicioalcliente@lastlink.cl"));
        //    email.To.Add(new MailboxAddress("Lastlink", Correo));
        //    email.Subject = "No Responder: Pedido En Proceso de Armado";
        //    email.Body = new TextPart(MimeKit.Text.TextFormat.Html)

        //    {
        //        //Text = "<p style'font-family: tahoma;font-size: 12px;'>Estimado/a  <strong>" + comprador + "</strong> </p><p style='font-family: tahoma;font-size: 12px;'>Mediante el presente correo, informamos a usted que su Pedido esta siendo Armado para Pronto Despacho</p><p style='font-family: tahoma;font-size: 12px;'>A continuación encontrará el detalle del Pedido</p><table width='750' border='0' cellpadding='2' cellspacing='1' bgcolor='#e5e5e5'><thead><tr bgcolor='#FFFFF'><td style='font - family: tahoma; font - size: 12px; ' colspan='2'><strong>Información del Pedido<strong></td></tr></thead><tbody><tr bgcolor='#FFFFF'><td width='200' style='font-family: tahoma;font-size: 12px;'>Fecha:</td><td width='550' style='font-family: tahoma;font-size: 12px;color:#5F9EA0'>" + Fecha + "</td></tr><tr bgcolor='#FFFFF'><td width='200' style='font-family: tahoma;font-size: 12px;'>Codigo Pedido:</td><td width='550' style='font-family: tahoma;font-size: 12px;color:#5F9EA0'>" + Codigo_Pedido + "</td></tr><tr bgcolor='#FFFFF'><td width='200' style='font-family: tahoma;font-size: 12px;'>Proveedor:</td><td width='550' style='font-family: tahoma;font-size: 12px;color:#5F9EA0'>" + proveedor + "</td></tr><tr bgcolor='#FFFFF'><td width='200' style='font-family: tahoma;font-size: 12px;'>Estado del Pedido</td><td width='550' style='font-family: tahoma;font-size: 12px;color:#5F9EA0'>" + Estado2 + "</td></tr><tr bgcolor='#FFFFF'><td width='200' style='font-family: tahoma;font-size: 12px;'>Dirección Comprador</td><td width='550' style='font-family: tahoma;font-size: 12px;color:#5F9EA0'>" + Direccion + "</td></tr><tr bgcolor='#FFFFF'><td width='200' style='font-family: tahoma;font-size: 12px;'>Comuna Comprador</td><td width='550' style='font-family: tahoma;font-size: 12px;color:#5F9EA0'>" + Comuna + "</td></tr><tr bgcolor='#FFFFF'><td width='200' style='font-family: tahoma;font-size: 12px;'>Telefono Comprador</td><td width='550' style='font-family: tahoma;font-size: 12px;color:#5F9EA0'>" + Telefono + "</td></tr><tr bgcolor='#FFFFF'><td width='200' style='font-family: tahoma;font-size: 12px;'>Correo Comprador</td><td width='550' style='font-family: tahoma;font-size: 12px;color:#5F9EA0'>" + Correo + "</td></tr></tbody></table><br><img src='https://www.lastlink.cl/wp-content/uploads/2023/07/LOGO1.png' style='width:40px; height:25px;'/> LastLink.cl - Todos los derechos reservados.</p>"
        //        Text = cadena_final
        //    };
        //    using (var smtp = new SmtpClient())
        //    {
        //        smtp.Connect("lastlink.cl", 465, true);
        //        smtp.Authenticate("cla93309", "wQERgzdBpdhHvJKEVhRa");
        //        smtp.Send(email);
        //        smtp.Disconnect(true);

        //    }

        //}

        //protected string obtenerDatos_articulos_pedido(string id_plat)
        //{




        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
        //    SqlCommand cmd = new SqlCommand("obtenerDatos_articulos_pedido_correo", con);
        //    cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //    cmd.Parameters.Add("@id_plat", SqlDbType.VarChar).Value = id_plat;

        //    con.Open();
        //    SqlDataReader dr = cmd.ExecuteReader();
        //    string Producto;
        //    string Cantidad;
        //    string Imagen;
        //    string cadena = "<table width='750' border='0' cellpadding='2' cellspacing='1' bgcolor='#e5e5e5'>";
        //    cadena = cadena + "<thead>";
        //    cadena = cadena + "<tr bgcolor='FFFFF'>";
        //    cadena = cadena + "<td style=font-family: tahoma;font-size: 12px;' colspan='2'><strong>Información de Productos del Pedido<strong></td>";
        //    cadena = cadena + "</tr>";
        //    cadena = cadena + "	</thead>";

        //    cadena = cadena + "<tbody>";
        //    cadena = cadena + "<tr bgcolor='#FFFFF'>";
        //    cadena = cadena + "<td width='200' style='font-family: tahoma;font-size: 12px;'>Nombre Producto</td>";
        //    cadena = cadena + "<td width='200' style='font-family: tahoma;font-size: 12px;'>Cantidad</td>";
        //    cadena = cadena + "<td width='200' style='font-family: tahoma;font-size: 12px;'>Imagen</td>";
        //    cadena = cadena + "</tr>";

        //    while (dr.Read())
        //    {
        //        Producto = dr["Producto"].ToString();
        //        Cantidad = dr["Cantidad"].ToString();
        //        Imagen = dr["Imagen"].ToString();
        //        cadena = cadena + " <tr bgcolor='#FFFFF'>";
        //        cadena = cadena + " <td width='550' style='font-family: tahoma;font-size: 12px;color:#5F9EA0''>" + Producto + "</td>";
        //        cadena = cadena + " <td width='200' style='font-family: tahoma;font-size: 12px;color:#5F9EA0''>" + Cantidad + "</td>";
        //        cadena = cadena + " <td width='550' style='font-family: tahoma;font-size: 12px;color:#5F9EA0''><img src='" + Imagen + "' style = 'width:80px; height:40px;'></td>";
        //        cadena = cadena + " </tr>";

        //    }

        //    cadena = cadena + " </tbody>";
        //    cadena = cadena + " </table>";
        //    cadena = cadena + "LastLink.cl - Todos los derechos reservados.</ p >";


        //    return cadena;
        //}

        //public void saca_datos_Correo(string idPlat)
        //{
        //    int Estado = 2;
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
        //    SqlCommand cmd = new SqlCommand("Listar_Ordenes_correo", con);
        //    cmd.Parameters.Add("@idPlat", SqlDbType.VarChar).Value = idPlat;
        //    cmd.Parameters.Add("@Estado", SqlDbType.VarChar).Value = Estado;
        //    cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //    con.Open();
        //    SqlDataReader dr = cmd.ExecuteReader();
        //    if (dr.Read())
        //    {
        //        string Fecha = dr["Fecha_Venta"].ToString();
        //        string Codigo_Pedido = dr["Id_Plat"].ToString();
        //        string Estado2 = dr["Estado"].ToString();
        //        string Direccion = dr["Direccion"].ToString();
        //        string Comuna = dr["Comuna"].ToString();
        //        string Telefono = dr["Telefono"].ToString();
        //        string Correo = dr["Correo_electronico"].ToString();
        //        string proveedor = dr["Proveedor"].ToString();
        //        string comprador = dr["comprador"].ToString();

        //        enviar_datos_correo_estado(Codigo_Pedido, Fecha, Estado2, Direccion, Comuna, Telefono, Correo, proveedor, comprador);
        //    }
        //    con.Close();
        //}
        private void CargarDatos()
        {
            try
            {
                string buscar_fecha = BuscarNombre_calendario.Text.Trim();
                string buscar2_fecha = BuscarNombre2_calendario.Text.Trim();
                var fecha1 = DateTime.Now;
                var fecha2 = DateTime.Now;
                DateTime dt, dt2;
                var date1 = DateTime.TryParse(buscar_fecha, out dt);
                var date2 = DateTime.TryParse(buscar2_fecha, out dt2);
                if (date1 && date2)
                {
                    fecha1 = dt;
                    fecha2 = dt2;
                }
                //////////////////

                /// buscar por nombre
                string buscar = BuscarNombre.Text.Trim();
                DataTable datatable = new DataTable();
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
                if (buscar == "" && buscar_fecha == "" && buscar2_fecha == "")
                {
                    Session["cadena"] = "Listar_Ordenes_batch";
                }
                if (buscar != "" && buscar_fecha == "" && buscar2_fecha == "")
                {
                    Session["cadena"] = "Listar_Ordenes_buscar_Proveedor_Batch";
                }
                if (buscar == "" && buscar_fecha != "" && buscar2_fecha != "")
                {
                    Session["cadena"] = "Listar_Ordenes_buscar_Batch";
                }
                string cadena = Convert.ToString(Session["cadena"]);
                //SqlDataAdapter da = new SqlDataAdapter("sp_datos_Movimiento", con);
                SqlCommand cmd = new SqlCommand(cadena, con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (buscar != "" && buscar_fecha == "" && buscar2_fecha == "")
                {
                    cmd.Parameters.AddWithValue("@Busqueda", buscar);
                }
                if (buscar == "" && buscar_fecha != "" && buscar2_fecha != "")
                {
                    cmd.Parameters.AddWithValue("@Busqueda", fecha1);
                    cmd.Parameters.AddWithValue("@Busqueda2", fecha2);
                }
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

        protected void Button5_Click(object sender, EventArgs e)
        {
            CargarDatos();

        }


        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            CargarDatos();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            CargarDatos();

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
            BuscarNombre.Text = "";

        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            //BuscarNombre por estado
            Panel3.Visible = true;
            Panel_Fecha.Visible = false;
            BuscarNombre_calendario.Text = "";
            BuscarNombre2_calendario.Text = "";

        }


        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            BuscarNombre_calendario.Text = Calendar1.SelectedDate.ToString();

        }
        protected void Calendar2_SelectionChanged(object sender, EventArgs e)
        {
            BuscarNombre2_calendario.Text = Calendar2.SelectedDate.ToString();

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


        public int Productos_en_bodega(string seller, string id_plat)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
            SqlCommand cmd = new SqlCommand("ver_existencia_bodega_bach", con);
            cmd.Parameters.Add("@id_plat", SqlDbType.VarChar).Value = id_plat;
            cmd.Parameters.Add("@seller", SqlDbType.VarChar).Value = seller;

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            //string Id_producto;
            int valor;
            if (dr.Read())
            {
                //   valor = Convert.ToInt32(dr["valor"]);
                Session["Id_producto"] = dr["valor"].ToString();


            }
            con.Close();
            valor = Convert.ToInt32(Session["Id_producto"]);
            return valor;
        }

        public int Productos_en_bodega_sin_SKU(string seller, string id_plat)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
            SqlCommand cmd = new SqlCommand("ver_existencia_sku_Migracion_Bach", con);
            cmd.Parameters.Add("@id_plat", SqlDbType.VarChar).Value = id_plat;
            cmd.Parameters.Add("@seller", SqlDbType.VarChar).Value = seller;

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            //string Id_producto;
            int valor;
            if (dr.Read())
            {
                //   valor = Convert.ToInt32(dr["valor"]);
                Session["Sin_sku"] = dr["valor"].ToString();

            }
            con.Close();
            valor = Convert.ToInt32(Session["Sin_sku"]);
            return valor;
        }

        public int Productos_sin_Stock_bach(string seller, string id_plat)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
            SqlCommand cmd = new SqlCommand("ver_existencia_Stock_bodega_bach", con);
            cmd.Parameters.Add("@id_plat", SqlDbType.VarChar).Value = id_plat;
            cmd.Parameters.Add("@seller", SqlDbType.VarChar).Value = seller;

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            //string Id_producto;
            int valor;
            if (dr.Read())
            {
                //   valor = Convert.ToInt32(dr["valor"]);
                Session["Stock_inicial"] = dr["Stock_inicial"].ToString();

            }
            con.Close();
            valor = Convert.ToInt32(Session["Stock_inicial"]);
            return valor;
        }

        protected void CambiarEstado_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                CheckBox chkdelete = (CheckBox)GridView1.Rows[i].Cells[0].FindControl("ChkEmpty");
                if (chkdelete.Checked == true)
                {
                    int id = Convert.ToInt32(GridView1.Rows[i].Cells[1].Text);
                    string sellers = (GridView1.Rows[i].Cells[4].Text);
                    string idplat = (GridView1.Rows[i].Cells[3].Text);

                    int sin_sku;

                    sin_sku = Productos_en_bodega_sin_SKU(sellers, idplat);
                    if (sin_sku == 0)
                    {



                        int valor;
                        valor = Productos_en_bodega(sellers, idplat);
                        if (valor == 1)
                        {
                            int sin_stock;
                            sin_stock = Productos_sin_Stock_bach(sellers, idplat);

                            if (sin_stock == 0)
                            {

                                string Observaciones = "Sin Errores en Bodega para Pedido" + idplat;

                                insertar_error_validacion(Observaciones);


                                //  cargar_estado_log(idplat, Estado);
                                //aqui se llema a correo
                                //saca_datos_Correo(idplat);
                            }
                            else
                            {
                             //   ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "Swal.fire({icon: 'error',title: 'Error..',  text: 'No hay Stock en Bodega para Pedido," + idplat + " En Seller:" + sellers + " favor verifique en Bodega e Ingresar Stock!'})", true);
                                string Observaciones = " No hay Stock en Bodega para Pedido" + idplat + " En Seller:" + sellers + " favor verifique en Bodega e Ingresar Stock!";

                                insertar_error_validacion(Observaciones);
                            }

                        }
                        else
                        {
                          //  ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "Swal.fire({icon: 'error',title: 'Error..',  text: 'Hay SKU inexistentes en Bodega para Pedido," + idplat + " En Seller:" + sellers + " favor verifique en Bodega de Seller intentelo nuevamente!'})", true);
                            string Observaciones = "Hay SKU inexistentes en Bodega para Pedido," + idplat + " En Seller:" + sellers + " favor verifique en Bodega de Seller";

                              insertar_error_validacion(Observaciones);


                        }
                    }
                    else
                    {
                        //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "Swal.fire({icon: 'error',title: 'Error..',  text: 'Hay SKU inexistentes Al momento de Migración para Pedido," + idplat + " En Seller:" + sellers + " favor Informar a Seller que debe Ingresar SKU en su Plataforma de Ventas, SOLUCIÓN: Actualice el SKU en Opción Lista de Pedidos->Detalles-->Editar'})", true);
                        string Observaciones = "Hay SKU inexistentes Al momento de Migración para Pedido," + idplat + " En Seller:" + sellers + " favor Informar a Seller que debe Ingresar SKU en su Plataforma de Ventas, SOLUCIÓN: Actualice el SKU en Opción Lista de Pedidos->Detalles-->Editar";
                        insertar_error_validacion(Observaciones);

                    }
                }
            }
            CargarDatos();
        }


        private void insertar_error_validacion(string Observaciones)
        {
                try
                {

                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
                    SqlCommand cmd = new SqlCommand("sp_registrar_Errores_PreBatch", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Observaciones", System.Data.SqlDbType.VarChar).Value = Observaciones;
                     con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                }
                catch (Exception)
                {

                    throw;
                }
        }


        //private void cargar_estado_log(string idplat, int Estado)
        //{
        //    try
        //    {

        //        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
        //        SqlCommand cmd = new SqlCommand("sp_registrar_log_estado", con);
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.Parameters.Add("@Id_Plat", System.Data.SqlDbType.VarChar).Value = idplat;
        //        cmd.Parameters.Add("@Estado", SqlDbType.Int).Value = Estado;
        //        con.Open();
        //        cmd.ExecuteNonQuery();

        //        con.Close();

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }


        //}

        //private void Lista_Bodega_PreArmado(int id)
        //{
        //    SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());

        //    SqlCommand cmd2 = new SqlCommand("sp_registrar_Lista_Batch", con2);
        //    cmd2.CommandType = System.Data.CommandType.StoredProcedure;
        //    cmd2.Parameters.Add("@idCabeceraPedido", System.Data.SqlDbType.Int).Value = id;
        //    con2.Open();
        //    cmd2.ExecuteNonQuery();
        //    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "Swal.fire({icon: 'success',title: 'Ok..',  text: 'Creación Batch Realizada!'})", true);

        //    con2.Close();
        //}
    }
}