using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProyectoLastLink.clases;
using System.Text.RegularExpressions;
using System.Security.Principal;
using System.Data.SqlTypes;
using iTextSharp.text.pdf.codec.wmf;
using QRCoder;
using ZXing;
using ZXing.Common;
using System.Drawing.Printing;
using System.Drawing;
using System.IO;

namespace ProyectoLastLink.Pages
{
   
    public partial class Armado_pedido_Mayorista : System.Web.UI.Page
    {
        private string imagePath = @"C:\Users\ivan.carreno\Desktop\Lastlink\Etiquetas\Etiquetas\Imagen\primerapartelogo.png";

        combos com = new combos();
        DataSet ds = new DataSet();
        protected void Page_Load(object sender, EventArgs e)
        {
            fnllenaSelect();

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
                    Pedido.Items.Insert(0, new ListItem("-- Seleccione Pedido --", "0"));
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


                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
                    SqlCommand cmd = new SqlCommand("sp_datos_extraer_productos", con);
                    cmd.Parameters.Add("@Codigobarra", SqlDbType.VarChar).Value = producto_lectura.Text.Trim();
                    cmd.Parameters.Add("@id_seller", SqlDbType.Int).Value = Convert.ToString(Session["id_seller"]);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();


                    if (dr.Read())
                    {
                        Session["Id_producto"] = dr["Id_producto"].ToString();
                        Session["sumar_a_stock"] = dr["Stock_Inicial"].ToString();
                        Nombre_Articulo.Text = dr["Nombre"].ToString();
                        Marca.Text = dr["Marca"].ToString();
                        //   insertar_movimiento();
                        //   sumar_stock_inicial();
                        Panel4.Visible = true;


                    }
                    con.Close();

                    Gvbind();
                }
                else
                {

                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "Swal.fire({icon: 'error',title: 'Error..',  text: 'Código No encontrado en Seller, Ingrese el Producto a Bodega!'})", true);



                }

                con2.Close();






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
                    cantidad = Convert.ToInt32(dr["Cantidad_pistoleo"].ToString()) + Convert.ToInt32(cantidad_pistoleo.Text);

                    // Session["id_Lista_Bodega_Batch"] = dr["id_Lista_Bodega_Batch"].ToString();
                    // Session["cantidad"] = dr["cantidad_ped"].ToString();


                    //Session["sumar_a_stock"] = dr["Stock_Inicial"].ToString();

                    Insertar_Cantidad_Pistoleo(id_Lista_Bodega_Batch, cantidad);
                }
                else
                {
                   // Response.Write("<script>alert('Seller No Posee este Producto')</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "Swal.fire({icon: 'error',title: 'Error..',  text: 'Seller No Posee Este Producto!'})", true);



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



        protected void Ingreso_Bodega_Click(object sender, EventArgs e)
        {
            string cantidad_final = cantidad_pistoleo.Text;
            string CodigoBarra = producto_lectura.Text;
            Insertar_pistoleo_lista_Bach(CodigoBarra); //no va a aqui
            verificar_total_Lista_Bodega_Batch();
            insertar_movimiento();
            Panel4.Visible = false;
           

        }




        protected void sumar_stock_inicial()
        {
            try
            {
                int suma = Convert.ToInt32(Session["sumar_a_stock"]) - Convert.ToInt32(cantidad_pistoleo.Text); 
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
            string cantidad = cantidad_pistoleo.Text;
            SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
            SqlCommand cmd2 = new SqlCommand("sp_registrar_movimientos", con2);
            string Comentario = "Venta mayorista";
            cmd2.Parameters.Add("@id_producto", SqlDbType.Int).Value = Convert.ToString(Session["Id_producto"]);
            cmd2.Parameters.Add("@id_seller", SqlDbType.Int).Value = Convert.ToString(Session["id_seller"]);
            cmd2.Parameters.Add("@cantidad", SqlDbType.Int).Value = Convert.ToInt32(cantidad);
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
            cmd.Parameters.Add("@Pedido", System.Data.SqlDbType.VarChar).Value = variable;


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
            try
            {

                string id_pedido = Pedido.SelectedValue;
                SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
                SqlCommand cmd2 = new SqlCommand("sp_datos_Actualizar_cabeceraylistaBach", con2);
                cmd2.Parameters.Add("@id_pedido", System.Data.SqlDbType.VarChar).Value = id_pedido;
                cmd2.CommandType = System.Data.CommandType.StoredProcedure;
                con2.Open();
                //aqui va el open de conexion en caso que mande error
                SqlDataReader dr = cmd2.ExecuteReader();
                con2.Close();
                 Armado.Visible = false;
                Otro.Visible = true;
                cargar_estado_log(id_pedido);
                //aqui va el tema de la impresion
                int cantidad_etiquetas = 0;
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
                        cantidad_etiquetas = (int)dataReader["cantidad_package"];
                        con.Close();
                    }
                }
                for (int i = 1; i <= cantidad_etiquetas; i++)
                {
                    PrintDocument print = new PrintDocument();
                    PrinterSettings printerSettings = new PrinterSettings();
                    var ee = new object();
                    print.PrinterSettings = printerSettings;
                    print.PrintPage += (s, args) => ThermalPrintPage(s, args, i, cantidad_etiquetas, id_pedido);

                    print.Print();
                }

                /////////////////////////////////

                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "Swal.fire({icon: 'success',title: 'Ok..',  text: 'Empaque Realizado Correctamente!'})", true);




            }
            catch (Exception)
            {
                throw;
            }

        }


        private void ThermalPrintPage(object sender, PrintPageEventArgs e, int numeroImpresion, int cantidad_etiquetas, string id_pedido)

        {
            HttpServerUtility server = HttpContext.Current.Server;
            string imagePath = server.MapPath(@"~/Imagenes/primerapartelogo.png");

            //string cs = "Server=localhost;Database=LastLink;Integrated Security=True;";

            string conexion = ConfigurationManager.ConnectionStrings["conexion"].ToString();

            string nombre = "";
            string direccion = "";
            string comuna = "";
            string telefono = "";
            string nota = "";
            string idproducto = "";
            //string fecha_Venta = "";
            string zona = "";
            string proveedor = "";
            using (SqlConnection c = new SqlConnection(conexion))
            {
                c.Open();


                SqlCommand com = new SqlCommand("sp_Crear_Etiqueta_final", c);
                com.CommandType = CommandType.StoredProcedure;


                com.Parameters.AddWithValue("@id_orden", id_pedido);


                SqlDataReader dr = com.ExecuteReader();

                if (dr.Read())
                {
                    nombre = dr["nombres"].ToString();
                    direccion = dr["direccion"].ToString();
                    comuna = dr["comuna"].ToString();
                    telefono = dr["telefono"].ToString();
                    //nota = dr["nota_pedido"].ToString();
                    idproducto = dr["id_producto"].ToString();
                    // fecha_Venta = dr["fecha_venta"].ToString();
                    proveedor = dr["proveedor"].ToString();
                    zona = dr["zona"].ToString();
                }

                dr.Close();
                c.Close();
            }
            e.PageSettings.Landscape = false;

            e.PageSettings.PaperSize = new PaperSize("Custom", 30000, 10000);
            Font font = new Font("Arial", 10);
            Font fontbulto = new Font("Arial Black", 12);
            System.Drawing.Image image = System.Drawing.Image.FromFile(imagePath);
            int nuevoAnchoImagen = 140;
            int nuevaAlturaImagen = 65;
            int nuevaPosicionX = 0;
            int nuevaPosicionY = 0;
            Rectangle imageRect = new Rectangle(nuevaPosicionX, nuevaPosicionY, nuevoAnchoImagen, nuevaAlturaImagen);


            e.Graphics.DrawImage(image, imageRect);


            nuevaPosicionY += nuevaAlturaImagen + 5;
            int margenX = -5;
            int nuevaPosicionXLinea = margenX;
            int nuevaPosicionYLinea = nuevaPosicionY;
            int nuevoGrosorLinea = 1;



            ImprimirTexto(e, "Datos de Cliente:", fontbulto, 5, nuevaPosicionY + 3);
            ImprimirTexto(e, nombre, font, 5, nuevaPosicionY + 25);
            ImprimirTexto(e, direccion, font, 5, nuevaPosicionY + 40);
            ImprimirTexto(e, comuna, font, 5, nuevaPosicionY + 55);
            ImprimirTexto(e, telefono, font, 5, nuevaPosicionY + 70);
            ImprimirTexto(e, nota, font, 5, nuevaPosicionY + 75);


            e.Graphics.DrawRectangle(new Pen(Color.Black, nuevoGrosorLinea), e.PageBounds.Width - 75, nuevaPosicionY - 55, 45, 55);


            ImprimirTexto(e, "Ruta N", new Font("Arial", 8), e.PageBounds.Width - 70, nuevaPosicionY - 50);
            ImprimirTexto(e, zona, new Font("Arial Black", 20), e.PageBounds.Width - 70, nuevaPosicionY - 40);

            ImprimirTexto(e, "" + idproducto, new Font("Arial Black", 17), 5, nuevaPosicionY + 85);
            int margenX1 = -15;
            int nuevaPosicionXLinea1 = margenX;
            int nuevaPosicionYLinea1 = nuevaPosicionY;
            int nuevoGrosorLinea1 = 1;
            e.Graphics.DrawLine(new Pen(Color.Black, nuevoGrosorLinea1), nuevaPosicionXLinea1, nuevaPosicionYLinea1, e.PageBounds.Width - margenX1, nuevaPosicionYLinea1);


            string texto2 = proveedor;




            ImprimirTexto(e, texto2.ToUpper(), font, 5, nuevaPosicionY + 125);



            string contenidoQRS4 = idproducto;

            int nuevoAnchoQRS4 = 15;
            int nuevaAlturaQRS4 = 15;


            ImprimirCodigoQR(e, contenidoQRS4, e.PageBounds.Width - 86, nuevaPosicionY + 95, nuevoAnchoQRS4, nuevaAlturaQRS4);


            string textoBultoS4 = "Bulto " + numeroImpresion + " de " + cantidad_etiquetas;
            ImprimirTexto(e, textoBultoS4, fontbulto, 5, nuevaPosicionY + 165);


        }

        private void ImprimirTexto(PrintPageEventArgs e, string texto, Font font, float x, float y)
        {
            e.Graphics.DrawString(texto, font, Brushes.Black, x, y);
        }
        private void ImprimirCodigoQR(PrintPageEventArgs e, string contenido, int x, int y, int ancho, int alto)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(contenido, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);


            int escala = 5;
            int anchoQR = ancho * escala;
            int altoQR = alto * escala;


            Bitmap qrBitmap = qrCode.GetGraphic(20);


            e.Graphics.DrawImage(qrBitmap, x, y, anchoQR, altoQR);
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
                Panel4.Visible= false;

            }
            catch (Exception)
            {
                throw;
            }


        }

        protected void Otro_Click(object sender, EventArgs e)
        {
            Response.Redirect("Armado_pedido_Mayorista.aspx");
        }

       
    }
}