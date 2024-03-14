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

namespace ProyectoLastLink.Pages
{
    public partial class ListaPedidos : System.Web.UI.Page
    {
        //private string imagePath = @"C:\Users\ivan.carreno\Desktop\Lastlink\Etiquetas\Etiquetas\Imagen\primerapartelogo.png";
         protected void Page_Load(object sender, EventArgs e)
            {
            //  Gvbind();
            //aca debo migrar la info de las tablas del demonio de pedido y sus
            //productos
           


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
            string id_pedido = Convert.ToString(Session["ID_PLAT_MOV"]);
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


            ImprimirTexto(e, "Zona", new Font("Arial", 8), e.PageBounds.Width - 70, nuevaPosicionY - 50);
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




       

       
        private void CargarDatos()
        {
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
     
    }
}