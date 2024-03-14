using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Drawing.Printing;
using iTextSharp.text.pdf.codec.wmf;
using QRCoder;
using ZXing;
using ZXing.Common;
using System.Drawing;
using System.IO;
using System.Security.Policy;
using System.Security.Cryptography.Xml;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit;


namespace ProyectoLastLink.Pages
{
    public partial class Pedidos_seller : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //  Gvbind();
            //aca debo migrar la info de las tablas del demonio de pedido y sus
            //productos



            if (!IsPostBack)
            {
                CargarDatos();
                CargarDatos2();

            }

        }




        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView11.EditIndex = e.NewEditIndex;
            // CargarDatos_movimiento(Convert.ToString(Session["ID_PLAT_MOV"]));
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["conexion"].ToString();

            int id = Convert.ToInt32(GridView11.DataKeys[e.RowIndex].Value.ToString());
            string Sku_ped = ((TextBox)GridView11.Rows[e.RowIndex].Cells[4].Controls[0]).Text;




            using (SqlConnection con = new SqlConnection(cs))

            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update Producto_pedido set Sku_ped='" + Sku_ped + "' where id_prod_ped='" + id + "' ", con);
                int t = cmd.ExecuteNonQuery();
                if (t > 0)
                {
                    //  Response.Write("<script>alert('Informacion Actualizada')</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "Swal.fire({icon: 'success',title: 'Ok..',  text: 'Sku Actualizado!'})", true);

                    GridView11.EditIndex = -1;
                    CargarDatos_movimiento(Convert.ToString(Session["ID_PLAT_MOV"]));

                }
            }
        }
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView11.EditIndex = -1;
            CargarDatos_movimiento(Convert.ToString(Session["ID_PLAT_MOV"]));
        }


        protected void Button6_Click(object sender, EventArgs e)
        {


            GridView2.Visible = true;
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/ms-excel";
            Response.AddHeader("content-disposition", "attachment; filename = Migrar_Seller_Pedidos.xls");
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GridView2.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.End();
        }
        public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
        {

        }
        private void CargarDatos2()
        {
            try
            {
                //buscar por fecha 
                string usuario = Convert.ToString(Session["usuario"]);
                string Mes = Convert.ToString(Session["Mes"]);
                string fecha_inicio = Convert.ToString(Session["fecha_inicio"]);
                string fecha_fin = Convert.ToString(Session["fecha_fin"]);

                if (Session["Id_rol"].ToString() == "2") {
                

                            if (fecha_inicio == "" && fecha_fin == "")
                            {
                                Session["cadena_busqueda"] = "Listar_Ordenes_seller_propios";
                            }
                            else
                            {
                                Session["cadena_busqueda"] = "Listar_Ordenes_seller_propios_rango";
                            }

                }
                if (Session["Id_rol"].ToString() == "1")
                {

                    if (fecha_inicio == "" && fecha_fin == "")
                    {
                        Session["cadena_busqueda"] = "Listar_Ordenes_seller_propios_todos";
                    }
                    else
                    {
                        Session["cadena_busqueda"] = "Listar_Ordenes_seller_propios_rango_todos";
                    }

                }


                string consulta = Session["cadena_busqueda"].ToString();

                DataTable datatable = new DataTable();
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());

                //SqlDataAdapter da = new SqlDataAdapter("sp_datos_Movimiento", con);
                SqlCommand cmd = new SqlCommand(consulta, con);
                cmd.Parameters.AddWithValue("@usuario", usuario);
                cmd.Parameters.AddWithValue("@Mes", Mes);
                cmd.Parameters.AddWithValue("@fecha_inicio", fecha_inicio);
                cmd.Parameters.AddWithValue("@fecha_fin", fecha_fin);



                if (Session["consulta_seller"].ToString() == "1")
                {
                    cmd.Parameters.AddWithValue("@Estado", 11);
                }
                if (Session["consulta_seller"].ToString() == "2")
                {
                    cmd.Parameters.AddWithValue("@Estado", 9);
                }

                if (Session["consulta_seller"].ToString() == "3")
                {
                    cmd.Parameters.AddWithValue("@Estado", 8);
                }
                if (Session["consulta_seller"].ToString() == "4")
                {
                    cmd.Parameters.AddWithValue("@Estado", 1);
                }
                if (Session["consulta_seller"].ToString() == "10")
                {
                    cmd.Parameters.AddWithValue("@Estado", 10);
                }

                if (Session["consulta_seller"].ToString() == "5")
                {
                    cmd.Parameters.AddWithValue("@Estado", 3);
                }

                if (Session["consulta_seller"].ToString() == "6")
                {
                    cmd.Parameters.AddWithValue("@Estado", 999);
                }




                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                con.Open();
                adapter.Fill(datatable);
                con.Close();
                GridView2.DataSource = datatable;
                GridView2.DataBind();
                            }
            catch
            {
                throw;

            }

        }


        private void CargarDatos()
        {
            try
            {
                //buscar por fecha 
                string usuario = Convert.ToString(Session["usuario"]);
                string Mes = Convert.ToString(Session["Mes"]);
                string fecha_inicio = Convert.ToString(Session["fecha_inicio"]);
                string fecha_fin = Convert.ToString(Session["fecha_fin"]);



                if (Session["Id_rol"].ToString() == "2")
                { 
                
                    if (fecha_inicio == "" && fecha_fin == "")
                        {
                            Session["cadena_busqueda"] = "Listar_Ordenes_seller_propios";
                        }
                        else
                        {
                            Session["cadena_busqueda"] = "Listar_Ordenes_seller_propios_rango";
                        }

                }
               
                if (Session["Id_rol"].ToString() == "1")
                {

                    if (fecha_inicio == "" && fecha_fin == "")
                    {
                        Session["cadena_busqueda"] = "Listar_Ordenes_seller_propios_todos";
                    }
                    else
                    {
                        Session["cadena_busqueda"] = "Listar_Ordenes_seller_propios_rango_todos";
                    }

                }




                string consulta = Session["cadena_busqueda"].ToString();
                DataTable datatable = new DataTable();
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
                     
                SqlCommand cmd = new SqlCommand(consulta, con);
                cmd.Parameters.AddWithValue("@usuario", usuario);
                cmd.Parameters.AddWithValue("@Mes", Mes);
                cmd.Parameters.AddWithValue("@fecha_inicio", fecha_inicio);
                cmd.Parameters.AddWithValue("@fecha_fin", fecha_fin);



                if (Session["consulta_seller"].ToString() == "1")
                 {
                    cmd.Parameters.AddWithValue("@Estado", 11);
                }
                if (Session["consulta_seller"].ToString() == "2")
                {
                    cmd.Parameters.AddWithValue("@Estado", 9);
                }

                if (Session["consulta_seller"].ToString() == "3")
                {
                    cmd.Parameters.AddWithValue("@Estado", 8);
                }
                if (Session["consulta_seller"].ToString() == "4")
                {
                    cmd.Parameters.AddWithValue("@Estado",1);
                }

                if (Session["consulta_seller"].ToString() == "10")
                {
                    cmd.Parameters.AddWithValue("@Estado", 10);
                }




                if (Session["consulta_seller"].ToString() == "5")
                {
                    cmd.Parameters.AddWithValue("@Estado", 3);
                }

                if (Session["consulta_seller"].ToString() == "6")
                {
                    cmd.Parameters.AddWithValue("@Estado",999);
                }




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
            CargarDatos();
        }

        protected void GridView11_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView11.PageIndex = e.NewPageIndex;
        }

      
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            string id = GridView1.SelectedRow.Cells[0].Text;
            string Id_Plat = GridView1.SelectedRow.Cells[1].Text;
            string Estado = GridView1.SelectedRow.Cells[6].Text;

            if (Estado== "Rechazado")
             {
                Panel1.Visible = true;
                //Motivo_Activacion.Visible= true;
            }
            else
            {
                Panel1.Visible= false;
                //Motivo_Activacion.Visible = false;
            }

            Session["ID_PLAT_MOV"] = GridView1.SelectedRow.Cells[1].Text;
            CargarDatos_movimiento(Id_Plat);



           // Origen.Text = GridView1.SelectedRow.Cells[1].Text;
            Id_plat.Text = GridView1.SelectedRow.Cells[1].Text;
          //  Proveedor.Text = GridView1.SelectedRow.Cells[2].Text;
            Nombre_Comprador.Text = GridView1.SelectedRow.Cells[2].Text;
            Direccion.Text = GridView1.SelectedRow.Cells[3].Text;
            //Comuna.Text = GridView1.SelectedRow.Cells[6].Text;
            //Telefono.Text = GridView1.SelectedRow.Cells[7].Text;
           // Correo_electronico.Text = GridView1.SelectedRow.Cells[7].Text;
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
             

       
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["conexion"].ToString();

            int ide = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update Cabecera_Pedido set Estado=10  where id='" + ide + "' ", con);
                int t = cmd.ExecuteNonQuery();
                if (t > 0)
                {

                    //Response.Write("<script>alert('Informacion Eliminada')</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "Swal.fire({icon: 'info',title: 'Ok..',  text: 'Pedido Cancelado!'})", true);

                    GridView1.EditIndex = -1;
                    CargarDatos();
                }




            }

        }

        private void cargar_estado_log(string idplat, int Estado, string Comentario)
        {
            try
            {
                string usuario = Convert.ToString(Session["usuario"]);

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
                SqlCommand cmd = new SqlCommand("sp_registrar_log_estado_eliminar", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Plat", System.Data.SqlDbType.VarChar).Value = idplat;
                cmd.Parameters.Add("@Estado", SqlDbType.Int).Value = Estado;
                cmd.Parameters.Add("@usuario", System.Data.SqlDbType.VarChar).Value = usuario;
                cmd.Parameters.Add("@Comentario", System.Data.SqlDbType.VarChar).Value = Comentario;


                con.Open();
                cmd.ExecuteNonQuery();

                con.Close();

            }
            catch (Exception)
            {

                throw;
            }


        }
        protected void Activar_Click(object sender, EventArgs e)
        {
            try
            {

                string usuario = Convert.ToString(Session["usuario"]);
                string idplat = Id_plat.Text;
                int Estado = 1;
                string Comentario = Motivo_Activacion.Text;
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
                SqlCommand cmd = new SqlCommand("sp_cambiar_estado", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Plat", System.Data.SqlDbType.VarChar).Value = idplat;
                cmd.Parameters.Add("@Estado", System.Data.SqlDbType.Int).Value = Estado;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                cargar_estado_log(idplat, Estado, Comentario);
                saca_datos_Correo(idplat);

                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "Swal.fire({icon: 'success',title: 'Ok..',  text: 'Pedido Dejado Ingresado Para Seguir Con Proceso!'})", true);
                CargarDatos();

            }
            catch (Exception)
            {

                throw;
            }
        }



        public void saca_datos_Correo(string idPlat)
        {
            int Estado = 1;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
            SqlCommand cmd = new SqlCommand("Listar_correo_seller_2", con);
            cmd.Parameters.Add("@Id_Plat", System.Data.SqlDbType.VarChar).Value = idPlat;
            cmd.Parameters.Add("@Estado", System.Data.SqlDbType.Int).Value = Estado;


            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string Id_Plat = dr["Id_Plat"].ToString();
                string Correo = dr["correo"].ToString();
                string Comentario = dr["Comentario"].ToString();
                string Fecha = dr["fecha"].ToString();

                enviar_datos_correo_estado(Id_Plat, Fecha, Correo, Comentario);
            }
            con.Close();
        }
        public void enviar_datos_correo_estado(string Codigo_Pedido, string Fecha, string Correo, string Comentario)
        {
            //  string cadena2 = obtenerDatos_articulos_pedido(Codigo_Pedido);

            string cadena = "<p style'font-family: tahoma;font-size: 12px;'>Estimado/a  </p>";
            cadena = cadena + "			 <p style='font - family: tahoma; font - size: 12px; '>Mediante el presente correo, informamos a usted que un Pedido Ha sido Activado</p>";
            cadena = cadena + "            <p style='font - family: tahoma; font - size: 12px; '>A continuación encontrará el detalle del Pedido</p>";
            cadena = cadena + "              <table width='750' border='0' cellpadding='2' cellspacing='1' bgcolor='#e5e5e5'>";
            cadena = cadena + "             <thead>";
            cadena = cadena + "             		<tr bgcolor='FFFFF'>";
            cadena = cadena + "             			<td style=font-family: tahoma;font-size: 12px;' colspan='2'><strong>Información del Pedido<strong></td>";
            cadena = cadena + "              		</tr>";
            cadena = cadena + "             	</thead>";
            cadena = cadena + "              	<tbody>";
            cadena = cadena + "              		<tr bgcolor='FFFFF' > ";
            cadena = cadena + "              			<td width='200' style='font - family: tahoma; font - size: 12px; '>Fecha Anulacion:</td>";
            cadena = cadena + "              			<td width='550' style='font - family: tahoma; font - size: 12px; color:#5F9EA0'>" + Fecha + "</td>";
            cadena = cadena + "              		</tr>";
            cadena = cadena + "             		<tr bgcolor='#FFFFF'>";
            cadena = cadena + "             			<td width='200' style='font-family: tahoma;font-size: 12px;'>Codigo Pedido:</td>";
            cadena = cadena + "              			<td width='550' style='font-family: tahoma;font-size: 12px;color:#5F9EA0'>" + Codigo_Pedido + "</td>";
            cadena = cadena + "              		</tr>";
            cadena = cadena + "			 		<tr bgcolor='#FFFFF'>";
            cadena = cadena + "              			<td width='200' style='font-family: tahoma;font-size: 12px;'>Comentario:</td>";
            cadena = cadena + "             			<td width='550' style='font-family: tahoma;font-size: 12px;color:#5F9EA0'>" + Comentario + "</td>";
            cadena = cadena + "              		</tr>";
            cadena = cadena + "                 	</tbody>";
            cadena = cadena + "             </table>";

            string cadena_final = cadena; //+ cadena2;

            Correo = "cmujica@lastlink.cl";


            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Pedido Nro. : " + Codigo_Pedido + " Cancelado", "servicioalcliente@lastlink.cl"));
            email.To.Add(new MailboxAddress("Lastlink", Correo));
            email.Subject = "No Responder: Correo Automatico";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)

            {
                //Text = "<p style'font-family: tahoma;font-size: 12px;'>Estimado/a  <strong>" + comprador + "</strong> </p><p style='font-family: tahoma;font-size: 12px;'>Mediante el presente correo, informamos a usted que su Pedido esta siendo Armado para Pronto Despacho</p><p style='font-family: tahoma;font-size: 12px;'>A continuación encontrará el detalle del Pedido</p><table width='750' border='0' cellpadding='2' cellspacing='1' bgcolor='#e5e5e5'><thead><tr bgcolor='#FFFFF'><td style='font - family: tahoma; font - size: 12px; ' colspan='2'><strong>Información del Pedido<strong></td></tr></thead><tbody><tr bgcolor='#FFFFF'><td width='200' style='font-family: tahoma;font-size: 12px;'>Fecha:</td><td width='550' style='font-family: tahoma;font-size: 12px;color:#5F9EA0'>" + Fecha + "</td></tr><tr bgcolor='#FFFFF'><td width='200' style='font-family: tahoma;font-size: 12px;'>Codigo Pedido:</td><td width='550' style='font-family: tahoma;font-size: 12px;color:#5F9EA0'>" + Codigo_Pedido + "</td></tr><tr bgcolor='#FFFFF'><td width='200' style='font-family: tahoma;font-size: 12px;'>Proveedor:</td><td width='550' style='font-family: tahoma;font-size: 12px;color:#5F9EA0'>" + proveedor + "</td></tr><tr bgcolor='#FFFFF'><td width='200' style='font-family: tahoma;font-size: 12px;'>Estado del Pedido</td><td width='550' style='font-family: tahoma;font-size: 12px;color:#5F9EA0'>" + Estado2 + "</td></tr><tr bgcolor='#FFFFF'><td width='200' style='font-family: tahoma;font-size: 12px;'>Dirección Comprador</td><td width='550' style='font-family: tahoma;font-size: 12px;color:#5F9EA0'>" + Direccion + "</td></tr><tr bgcolor='#FFFFF'><td width='200' style='font-family: tahoma;font-size: 12px;'>Comuna Comprador</td><td width='550' style='font-family: tahoma;font-size: 12px;color:#5F9EA0'>" + Comuna + "</td></tr><tr bgcolor='#FFFFF'><td width='200' style='font-family: tahoma;font-size: 12px;'>Telefono Comprador</td><td width='550' style='font-family: tahoma;font-size: 12px;color:#5F9EA0'>" + Telefono + "</td></tr><tr bgcolor='#FFFFF'><td width='200' style='font-family: tahoma;font-size: 12px;'>Correo Comprador</td><td width='550' style='font-family: tahoma;font-size: 12px;color:#5F9EA0'>" + Correo + "</td></tr></tbody></table><br><img src='https://www.lastlink.cl/wp-content/uploads/2023/07/LOGO1.png' style='width:40px; height:25px;'/> LastLink.cl - Todos los derechos reservados.</p>"
                Text = cadena_final
            };
            using (var smtp = new SmtpClient())
            {
                smtp.Connect("lastlink.cl", 465, true);
                smtp.Authenticate("cla93309", "wQERgzdBpdhHvJKEVhRa");
                smtp.Send(email);
                smtp.Disconnect(true);

            }

        }



    }
}