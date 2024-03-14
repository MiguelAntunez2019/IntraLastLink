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
using QRCoder;
using ZXing;
using ZXing.Common;
using System.Drawing.Printing;
using System.Drawing;
using System.IO;
using System.Windows.Controls;

using System.Linq.Expressions;
using System.Web.Services;

using iTextSharp;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Font = System.Drawing.Font;
using DocumentFormat.OpenXml.Bibliography;
using Image = iTextSharp.text.Image;
using DocumentFormat.OpenXml.Wordprocessing;
using Paragraph = iTextSharp.text.Paragraph;
using Rectangle = iTextSharp.text.Rectangle;
using PageSize = iTextSharp.text.PageSize;
using DocumentFormat.OpenXml.Drawing.Charts;
namespace ProyectoLastLink.Pages
{
    
     public partial class Armado_pedido : System.Web.UI.Page
    {
        private string imagePath = @"C:\Users\ivan.carreno\Desktop\Lastlink\Etiquetas\Etiquetas\Imagen\primerapartelogo.png";
        //private string imagePath = "~/imagenes/primerapartelogo.png";
        //private string imagePath = @"..\Lastlink\Etiquetas\Etiquetas\Imagen\primerapartelogo.png";

        combos com = new combos();
        DataSet ds = new DataSet();
        protected int cantidad_imprimir
        {
            get
            {
                if (ViewState["Cantidad_Imprimir"] == null)
                {
                    // Valor inicial cuando no se ha establecido en ViewState
                    return 0;
                }
                return (int)ViewState["Cantidad_Imprimir"];
            }
            set
            {
                ViewState["Cantidad_Imprimir"] = value;
            }
        }

        protected string pedido_imprimir
        {
            get
            {
                if (ViewState["Pedido_Imprimir"] == null)
                {
                    // Valor inicial cuando no se ha establecido en ViewState
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
            fnllenaSelect();
         
        }

        private bool ObtenerCondicion(int i )
        {
            // Implementa lógica para determinar si se debe abrir el popup
            // Puedes basarte en algún estado, variable, o cualquier otra condición específica de tu aplicación
            return true; // Cambia esto según tus requisitos
        }
        private void fnllenaSelect()
        {
            if (IsPostBack == false)
            {
                try
                {

                    ds = com.getComboPedido();
                    Pedido.DataSource = ds;
                    Pedido.DataValueField = "Nombre_pedido";
                    Pedido.DataTextField = "Id_Plat";
                    Pedido.DataSource = ds;
                    Pedido.DataBind();
                    Pedido.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccione Pedido --", "0"));
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }


        void Limpiar()
        {
            producto_lectura.Text = "";

        }



        protected void verificar_Click(object sender, EventArgs e)
        {
            try
            {
                //aca debo verificar si el totoal de productos Lista_Bodega_Batch es igual al cantidad pistoleado
                //si l bvalor de la diferencia es 0 entonces esta listo el package y se debe acrtivar el boton de cambio
                //de estado a ARMADO y dejar el estado de Lista_bodega_batch 2 que es cerrado 

                string CodigoBarra = producto_lectura.Text.Trim();
                string seller = Session["id_seller"].ToString();

                Ver_stock_producto(CodigoBarra, seller);

                producto_lectura.Focus();


                // procedimeitno para validar existencias por CodigoBarra y Seller



                //aqui se debe verificar si el pistoleo que realiza esta dentro de la tabla
                //Lista_Bodega_Batch si esta ejecuta lo de abajo sino esta
                //mandar mensaje que ese Producto no esta dentro del Pedido
                //sp sp_datos_validar_pistoleo_Lista_Bodega_Batch
                //-------------

                SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
                SqlCommand cmd2 = new SqlCommand("sp_datos_validar_pistoleo_Lista_Bodega_Batch", con2);
                cmd2.Parameters.Add("@Codigobarra", SqlDbType.VarChar).Value = producto_lectura.Text.Trim();
                cmd2.Parameters.Add("@id_Plat", SqlDbType.VarChar).Value = Pedido.SelectedValue;

                cmd2.CommandType = System.Data.CommandType.StoredProcedure;
                con2.Open();
                SqlDataReader dr2 = cmd2.ExecuteReader();
                if (dr2.Read())
                {
                    Insertar_pistoleo_lista_Bach(CodigoBarra);
                    verificar_total_Lista_Bodega_Batch();
                    Gvbind();
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
                    SqlCommand cmd = new SqlCommand("sp_datos_extraer_productos", con);
                    cmd.Parameters.Add("@Codigobarra", SqlDbType.VarChar).Value = producto_lectura.Text.Trim();
                    cmd.Parameters.Add("@id_seller", SqlDbType.Int).Value = Convert.ToString(Session["id_seller"]);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    //string Id_producto=;

                    if (dr.Read())
                    {
                        Session["Id_producto"] = dr["Id_producto"].ToString();
                        Session["sumar_a_stock"] = dr["Stock_Inicial"].ToString();
                        insertar_movimiento();
                        sumar_stock_inicial();
                    }
                    con.Close();


                }
                else
                {
                    producto_lectura.Focus();
                    producto_lectura.Text = "";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "Swal.fire({icon: 'error',title: 'Error..',  text: 'Codigo No Corresponde a Pedido Actual!'})", true);

                }

                con2.Close();






            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void Ver_stock_producto(string CodigoBarra, string seller)

        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
                SqlCommand cmd = new SqlCommand("EXtraer_stock_seller_codigoBarra", con);
                cmd.Parameters.Add("@CodigoBarra", System.Data.SqlDbType.VarChar).Value = CodigoBarra;
                cmd.Parameters.Add("@Seller", System.Data.SqlDbType.Int).Value = Convert.ToInt32(seller);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                int resultado;
                if (dr.Read())
                {
                    resultado = Convert.ToInt32(dr["resultado"].ToString());

                    if (resultado == 1)
                    {

                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "Swal.fire({icon: 'info',title: 'Opss',  html: 'El Stock esta por debajo de lo Minimo, de igual manera se puede Seguir. Favor Informar a Seller'})", true);
                    }

                }


                con.Close();
            }
            catch (Exception)
            {
                throw;
            }

        }

        protected void verificar_total_Lista_Bodega_Batch()
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
                SqlCommand cmd = new SqlCommand("sp_datos_total_validacion", con);
                cmd.Parameters.Add("@id_pedido", System.Data.SqlDbType.VarChar).Value = Convert.ToString(Session["id_pedido"]);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                int resultado;
                if (dr.Read())
                {
                    resultado = Convert.ToInt32(dr["resultado"].ToString());



                    if (resultado == 0)
                    {
                        // Armado.Visible = true;
                        Panel3.Visible = true;
                        cantidad.Focus();
                        //aca se activa boton para cambiar estados de las tablas
                        //y se deja el boyon visible
                    }



                }




                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }


        protected void Insertar_pistoleo_lista_Bach(string CodigoBarra)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
                SqlCommand cmd = new SqlCommand("sp_datos_Crear_Batch_Bodega", con);
                cmd.Parameters.Add("@Codigobarra", SqlDbType.VarChar).Value = CodigoBarra;
                cmd.Parameters.Add("@id_pedido", System.Data.SqlDbType.VarChar).Value = Convert.ToString(Session["id_pedido"]);


                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                int id_Lista_Bodega_Batch;
                int cantidad;
                if (dr.Read())
                {
                    id_Lista_Bodega_Batch = Convert.ToInt32(dr["id_Lista_Bodega_Batch"].ToString());
                    cantidad = Convert.ToInt32(dr["Cantidad_pistoleo"].ToString()) + 1;

                    // Session["id_Lista_Bodega_Batch"] = dr["id_Lista_Bodega_Batch"].ToString();
                    // Session["cantidad"] = dr["cantidad_ped"].ToString();


                    //Session["sumar_a_stock"] = dr["Stock_Inicial"].ToString();

                    Insertar_Cantidad_Pistoleo(id_Lista_Bodega_Batch, cantidad);
                }
                else
                {
                    //    Response.Write("<script>alert('Seller No Posee este Producto')</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "Swal.fire({icon: 'error',title: 'Error..',  text: 'Seller No Posee este Producto!'})", true);

                }
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }


        protected void Insertar_Cantidad_Pistoleo(int id_Lista_Bodega_Batch, int cantidad)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
                SqlCommand cmd = new SqlCommand("sp_datos_ActualizarCant_Batch_Bodega", con);
                cmd.Parameters.Add("@id_producto", SqlDbType.Int).Value = id_Lista_Bodega_Batch;
                cmd.Parameters.Add("@cantidad", SqlDbType.Int).Value = cantidad;

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                //aqui va el open de conexion en caso que mande error
                SqlDataReader dr = cmd.ExecuteReader();
                con.Close();




            }
            catch (Exception)
            {
                throw;
            }
        }








        protected void sumar_stock_inicial()
        {
            try
            {
                int suma = Convert.ToInt32(Session["sumar_a_stock"]) - 1;
                string producto = Session["Id_producto"].ToString();
                string seller = Session["id_seller"].ToString();


                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
                SqlCommand cmd = new SqlCommand("sp_suma_ingreso_unidad", con);
                cmd.Parameters.Add("@id_producto", SqlDbType.Int).Value = Convert.ToString(Session["Id_producto"]);
                cmd.Parameters.Add("@cantidad", SqlDbType.Int).Value = suma;
                cmd.Parameters.Add("@id_seller", SqlDbType.Int).Value = Convert.ToString(Session["id_seller"]);



                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                //aqui va el open de conexion en caso que mande error
                SqlDataReader dr = cmd.ExecuteReader();
                con.Close();

            }
            catch (Exception)
            {
                throw;
            }


        }

        protected void insertar_movimiento()


        {
            SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
            SqlCommand cmd2 = new SqlCommand("sp_registrar_movimientos", con2);
            string Comentario = "Venta";
            cmd2.Parameters.Add("@id_producto", SqlDbType.Int).Value = Convert.ToString(Session["Id_producto"]);
            cmd2.Parameters.Add("@id_seller", SqlDbType.Int).Value = Convert.ToString(Session["id_seller"]);
            cmd2.Parameters.Add("@cantidad", SqlDbType.Int).Value = 1;
            cmd2.Parameters.Add("@tipo_movimiento", SqlDbType.Int).Value = 2; //Salida
            cmd2.Parameters.Add("@usuario", System.Data.SqlDbType.VarChar).Value = Session["usuario"].ToString();
            cmd2.Parameters.Add("@comentario", System.Data.SqlDbType.VarChar).Value = Comentario;


            cmd2.CommandType = System.Data.CommandType.StoredProcedure;
            con2.Open();

            //aqui va el open de conexion en caso que mande error
            SqlDataReader dr = cmd2.ExecuteReader();

            Limpiar();
            con2.Close();
            sumar_stock_inicial();
            Gvbind();
        }

        protected void Gvbind()
        {
            string variable = Session["id_pedido"].ToString();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
            SqlCommand cmd = new SqlCommand("sp_Armar_Pedido", con);
            cmd.Parameters.Add("@Pedido", System.Data.SqlDbType.VarChar).Value = Session["id_pedido"].ToString();


            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();


            GridView1.DataSource = dr;
            GridView1.DataBind();




        }

        protected void Seleccionar_Seller_Click(object sender, EventArgs e)
        {
            producto_lectura.Focus();
            string id_pedido;
            id_pedido = Pedido.SelectedValue;
            Session["id_pedido"] = id_pedido;
            Panel1.Visible = true;
            Sacar_Seller(id_pedido);
            Gvbind();
        }



        protected void Sacar_Seller(string id_pedido)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
            SqlCommand cmd = new SqlCommand("sp_datos_Crear_Batch_Bodega_seller", con);
            cmd.Parameters.Add("@id_pedido", System.Data.SqlDbType.VarChar).Value = Convert.ToString(Session["id_pedido"]);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {

                Session["id_seller"] = dr["id_seller"].ToString();
                validacion_sku();

            }
        }

        public void validacion_sku()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
            SqlCommand cmd = new SqlCommand("sp_datos_Crear_validar_sku", con);
            cmd.Parameters.Add("@id_pedido", System.Data.SqlDbType.VarChar).Value = Convert.ToString(Session["id_pedido"]);
            cmd.Parameters.Add("@id_seller", System.Data.SqlDbType.VarChar).Value = Convert.ToString(Session["id_seller"]);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            int contador_sin_sku;
            if (dr.Read())
            {
                contador_sin_sku = Convert.ToInt32(dr["contador_sin_sku"]);

                if (contador_sin_sku > 0)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "Swal.fire({icon: 'info',title: 'Opss',  html: 'No se Puede Generar Pedido, falta SKU en Producto/s! Proveedor Externo Verificar en:</br> <strong>Menu Pedidos-->Lista de Pedidos</strong>'})", true);
                    Otro.Visible = true;
                    Panel1.Visible = false;
                    // Response.End();
                }

            }

        }

        protected void Registrar_Click1(object sender, EventArgs e)
        {
            string pedidomandar = "";
            try
            {
                string id_pedido = Pedido.SelectedValue;
                pedidomandar = id_pedido;
                SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
                SqlCommand cmd2 = new SqlCommand("sp_datos_Actualizar_cabeceraylistaBach", con2);
                cmd2.Parameters.Add("@id_pedido", System.Data.SqlDbType.VarChar).Value = id_pedido;
                cmd2.CommandType = System.Data.CommandType.StoredProcedure;
                con2.Open();
                SqlDataReader dr2 = cmd2.ExecuteReader();
                con2.Close();

                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "Swal.fire({icon: 'success',title: 'Ok..',  text: 'Empaque Realizado Correctamente!'})", true);

                Armado.Visible = false;
                Otro.Visible = true;
                Etiquetar.Visible = true;
                //cargar_estado_log(id_pedido);

              
                string conexion = ConfigurationManager.ConnectionStrings["conexion"].ToString();

                using (SqlConnection con = new SqlConnection(conexion))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("datos_bultos_etiquetas", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@id_pedido", id_pedido);
                    SqlDataReader dataReader = com.ExecuteReader();
                    if (dataReader.Read())
                    {
                        cantidad_imprimir = (int)dataReader["cantidad_package"];
                        pedido_imprimir = id_pedido;
                        con.Close();
                    }
                }

              ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "Swal.fire({icon: 'success',title: 'Ok..',  text: 'Empaque Realizado Correctamente!'})", true);


            }
            catch (Exception ex) { }
          
   }


        protected void Redirige(HttpContext e , int i , string pedido)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "popup", "function abrirPopup() { window.open('/TuPaginaPopup.aspx', 'PopupWindow', 'width=800, height=600'); }", false);
        }



        private void cargar_estado_log(string id_pedido)
        {
            try
            {
                int Estado = 3;
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
                SqlCommand cmd = new SqlCommand("sp_registrar_log_estado", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Plat", System.Data.SqlDbType.VarChar).Value = id_pedido;
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

        protected void Otro_Click(object sender, EventArgs e)
        { }

        protected void Ingreso_Impresion_Click(object sender, EventArgs e)
        {
            Armado.Visible = true;
            string id_pedido = Pedido.SelectedValue;
            // string cantidad = cantidad.Text();
            try
            {



                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
                SqlCommand cmd = new SqlCommand("sp_registrar_Impresion_Etiqueta", con);
                cmd.Parameters.Add("@cantidad", SqlDbType.Int).Value = cantidad.Text;
                cmd.Parameters.Add("@Id_Plat", System.Data.SqlDbType.VarChar).Value = id_pedido;

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                //aqui va el open de conexion en caso que mande error
                SqlDataReader dr = cmd.ExecuteReader();
                con.Close();
                Panel3.Visible = false;

            }
            catch (Exception)
            {
                throw;
            }


        }

      

        protected void Imprimir_Click(object sender, EventArgs e)
        {

            string url = ResolveUrl("~/ImprimePdf.aspx?Cantidad_Etiquetas=" + cantidad_imprimir + "&Pedido=" +pedido_imprimir + "");

      
            int ancho = 500;
            int alto = 500;

         
            ScriptManager.RegisterStartupScript(this, GetType(), "AbrirVentana", $"window.open('{url}', '_blank', 'width={ancho}, height={alto}');", true);
        }
    }
}