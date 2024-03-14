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
using Microsoft.AspNetCore.Http;
using static QRCoder.PayloadGenerator;
using DocumentFormat.OpenXml.Office2010.Excel;
using System.Windows.Markup;
using DocumentFormat.OpenXml.Spreadsheet;
using MimeKit;

using MailKit.Net.Smtp;
using MailKit;
namespace ProyectoLastLink.Pages
{
    public partial class ListaPedidos : System.Web.UI.Page
    {
        //private string imagePath = @"C:\Users\ivan.carreno\Desktop\Lastlink\Etiquetas\Etiquetas\Imagen\primerapartelogo.png";
        protected string pedido_imprimir
        {
            get
            {
                if (ViewState["Pedido_Imprimir"] == null)
                {
                   
                    return "";
                }
                return (string)ViewState["Pedido_Imprimir"];
            }
            set
            {
                ViewState["Pedido_Imprimir"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //  Gvbind();
            //aca debo migrar la info de las tablas del demonio de pedido y sus
            //productos
            string usuariovalida = Convert.ToString(Session["usuario"]);
            if (usuariovalida == "")
            {
                Response.Redirect("finsesion.aspx");
            }
            if (IsPostBack && pedido_imprimir!="")
            {

                btnImprimir_Click(sender, e);
            }

            if (!IsPostBack)
            {
                CargarDatos();

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


        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            string usuariovalida = Convert.ToString(Session["usuario"]);
            if (usuariovalida == "")
            {
                Response.Redirect("finsesion.aspx");
            }
            string cs = ConfigurationManager.ConnectionStrings["conexion"].ToString();
            pedido_imprimir = Convert.ToString(Session["ID_PLAT_MOV"]);
            string pedido_1 = pedido_imprimir.ToString();
            string pedido = String.Empty;
            if (pedido_1.Length <= 5)
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
               
                }

            }
            else { 
            pedido= Convert.ToString(Session["ID_PLAT_MOV"]); 
            }
         
          
            Imprimir_Click(sender, e, pedido);
        }



    
        protected void Imprimir_Click(object sender, EventArgs e, string pedido_imprimir)
        {
       
            int ancho = 500;
            int alto = 500;
      
            string url = ResolveUrl("~/ImprimePdf.aspx?Cantidad_Etiquetas=" + 1 + "&Pedido=" + pedido_imprimir);

           
            ScriptManager.RegisterStartupScript(this, GetType(), "AbrirVentana", $"window.open('{url}', '_blank', 'width={ancho}, height={alto}');", true);
        }







        private void CargarDatos()
        {
            string usuariovalida = Convert.ToString(Session["usuario"]);
            if (usuariovalida == "")
            {
                Response.Redirect("finsesion.aspx");
            }
            try
            {
                //buscar por fecha 

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
                //
                DataTable datatable = new DataTable();
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
               
               
                if (buscar == "" && buscar_fecha=="" && buscar2_fecha=="")
                {
                    Session["cadena"] = "Listar_Ordenes";
                }
                if (buscar != "" && buscar_fecha == "" && buscar2_fecha == "")
                {
                    Session["cadena"] = "Listar_Ordenes_buscar_Proveedor";
                }
               
                if (buscar == "" && buscar_fecha != "" && buscar2_fecha != "")
                {
                    Session["cadena"] = "Listar_Ordenes_buscar";
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
                

                GridView2.DataSource = datatable;
                GridView2.DataBind();

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

        protected void Button6_Click(object sender, EventArgs e)
        {

            
            GridView2.Visible = true;
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/ms-excel";
            Response.AddHeader("content-disposition", "attachment; filename = Migrar_Pedidos.xls");
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




        protected void Button5_Click(object sender, EventArgs e)
        {
            string usuariovalida = Convert.ToString(Session["usuario"]);
            if (usuariovalida == "")
            {
                Response.Redirect("finsesion.aspx");
            }
            CargarDatos();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            CargarDatos();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string usuariovalida = Convert.ToString(Session["usuario"]);
            if (usuariovalida == "")
            {
                Response.Redirect("finsesion.aspx");
            }
            CargarDatos();
        }

        protected void GridView11_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView11.PageIndex = e.NewPageIndex;
        }

        public void validadExistencia_Para_imprimir(string Id_Plat)
        {
            string id_plat=Id_Plat.ToString();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
            SqlCommand cmd = new SqlCommand("SP_vailidar_impresion", con);
            cmd.Parameters.Add("@Id_plat", System.Data.SqlDbType.VarChar).Value = Convert.ToString(id_plat);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                
                int id_impresion = Convert.ToInt32(dr["id_impresion"]);
                ImprimirButon.Visible = true;

            }
            else
            {

                ImprimirButon.Visible = false;
            } 


            //aqui va el open de conexion en caso que mande error
            
            con.Close();
            
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            string id = GridView1.SelectedRow.Cells[0].Text;
            string Id_Plat= GridView1.SelectedRow.Cells[2].Text;
            validadExistencia_Para_imprimir(Id_Plat);

            Session["ID_PLAT_MOV"]= GridView1.SelectedRow.Cells[2].Text;
            CargarDatos_movimiento(Id_Plat);



             Origen.Text = GridView1.SelectedRow.Cells[1].Text;
             Id_plat.Text = GridView1.SelectedRow.Cells[2].Text;
             Proveedor.Text = GridView1.SelectedRow.Cells[3].Text;
            Nombre_Comprador.Text = GridView1.SelectedRow.Cells[4].Text;
            Direccion.Text = GridView1.SelectedRow.Cells[5].Text;
            //Comuna.Text = GridView1.SelectedRow.Cells[6].Text;
            //Telefono.Text = GridView1.SelectedRow.Cells[7].Text;
            Correo_electronico.Text = GridView1.SelectedRow.Cells[7].Text;
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
            string usuariovalida = Convert.ToString(Session["usuario"]);
            if (usuariovalida == "")
            {
                Response.Redirect("finsesion.aspx");
            }
            //Panel3 es seller
            //Panel_Fecha
            //buscar por fecha
            Panel3.Visible = false;
            Panel_Fecha.Visible = true;
            BuscarNombre.Text = "";


        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            string usuariovalida = Convert.ToString(Session["usuario"]);
            if (usuariovalida == "")
            {
                Response.Redirect("finsesion.aspx");
            }
            //BuscarNombre por estado
            Panel3.Visible = true;
            Panel_Fecha.Visible = false;
            BuscarNombre_calendario.Text="";
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

        protected void Cancelar_pedido_Click(object sender, EventArgs e)
        {
            BuscarNombre_calendario.Text = "";
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
             int ide = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
            Id_plat2.Text = ide.ToString();
            ModalPopupExtender2.Show();

        }


        protected void Button7_Click(object sender, EventArgs e)
        {
            try
            {
                string usuariovalida = Convert.ToString(Session["usuario"]);
                int Id = Convert.ToInt32(Id_plat2.Text);
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
                SqlCommand cmd = new SqlCommand("sp_registrar_motivo_rechazo_aprobado", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Motivo", System.Data.SqlDbType.VarChar).Value = Motivo_rechazo.Text;
                cmd.Parameters.Add("@usuario", System.Data.SqlDbType.VarChar).Value = usuariovalida;
                cmd.Parameters.Add("@Estado", System.Data.SqlDbType.Int).Value = 10;
                cmd.Parameters.Add("@idCabecera", System.Data.SqlDbType.Int).Value = Id;

                con.Open(); 
                cmd.ExecuteNonQuery();
                con.Close();
                saca_datos_Correo(Id);
                GridView1.EditIndex = -1;
                CargarDatos();
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "Swal.fire({icon: 'success',title: 'Ok..',  text: 'Rechazo Ingresado Correctamente!'})", true);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public void saca_datos_Correo(int idPlat)
        {
            int Estado = 10;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
            SqlCommand cmd = new SqlCommand("Listar_correo_seller", con);
            cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = idPlat;
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
            cadena = cadena + "			 <p style='font - family: tahoma; font - size: 12px; '>Mediante el presente correo, informamos a usted que un Pedido Ha sido Cancelado</p>";
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
        protected string obtenerDatos_articulos_pedido(string id_plat)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
            SqlCommand cmd = new SqlCommand("obtenerDatos_articulos_pedido_correo", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@id_plat", SqlDbType.VarChar).Value = id_plat;

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            string Producto;
            string Cantidad;
            string Imagen;
            string cadena = "<table width='750' border='0' cellpadding='2' cellspacing='1' bgcolor='#e5e5e5'>";
            cadena = cadena + "<thead>";
            cadena = cadena + "<tr bgcolor='FFFFF'>";
            cadena = cadena + "<td style=font-family: tahoma;font-size: 12px;' colspan='2'><strong>Información de Productos del Pedido<strong></td>";
            cadena = cadena + "</tr>";
            cadena = cadena + "	</thead>";

            cadena = cadena + "<tbody>";
            cadena = cadena + "<tr bgcolor='#FFFFF'>";
            cadena = cadena + "<td width='200' style='font-family: tahoma;font-size: 12px;'>Nombre Producto</td>";
            cadena = cadena + "<td width='200' style='font-family: tahoma;font-size: 12px;'>Cantidad</td>";
            cadena = cadena + "<td width='200' style='font-family: tahoma;font-size: 12px;'>Imagen</td>";
            cadena = cadena + "</tr>";

            while (dr.Read())
            {
                Producto = dr["Producto"].ToString();
                Cantidad = dr["Cantidad"].ToString();
                Imagen = dr["Imagen"].ToString();
                cadena = cadena + " <tr bgcolor='#FFFFF'>";
                cadena = cadena + " <td width='550' style='font-family: tahoma;font-size: 12px;color:#5F9EA0''>" + Producto + "</td>";
                cadena = cadena + " <td width='200' style='font-family: tahoma;font-size: 12px;color:#5F9EA0''>" + Cantidad + "</td>";
                cadena = cadena + " <td width='550' style='font-family: tahoma;font-size: 12px;color:#5F9EA0''><img src='" + Imagen + "' style = 'width:80px; height:40px;'></td>";
                cadena = cadena + " </tr>";

            }

            cadena = cadena + " </tbody>";
            cadena = cadena + " </table>";
            cadena = cadena + "LastLink.cl - Todos los derechos reservados.</ p >";


            return cadena;
        }







    }
}