using QRCoder;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Drawing.Printing;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using ProyectoLastLink.clases;

namespace ProyectoLastLink.Pages
{
    public partial class MisVentas : System.Web.UI.Page
    {
        combos com = new combos();
        DataSet ds = new DataSet();
        protected void Page_Load(object sender, EventArgs e)
        {
           
           

            if (!IsPostBack)
            {
                CargarDatos();

            }

        }


        protected void Button6_Click(object sender, EventArgs e)
        {


            GridView2.Visible = true;
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/ms-excel";
            Response.AddHeader("content-disposition", "attachment; filename = Migrar_Seller_Ventas.xls");
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
        private void CargarDatos()
        {
            string usuario = Session["usuario"].ToString();
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


                if (buscar == "" && buscar_fecha == "" && buscar2_fecha == "")
                {
                    Session["cadena"] = "Listar_Ordenes_seller";
                }
                if (buscar != "" && buscar_fecha == "" && buscar2_fecha == "")
                {
                    Session["cadena"] = "Listar_Ordenes_buscar_Proveedor_seller";
                }

                if (buscar == "" && buscar_fecha != "" && buscar2_fecha != "")
                {
                    Session["cadena"] = "Listar_Ordenes_buscar_seller";
                }

                string cadena = Convert.ToString(Session["cadena"]);
                //SqlDataAdapter da = new SqlDataAdapter("sp_datos_Movimiento", con);
                SqlCommand cmd = new SqlCommand(cadena, con);
                cmd.CommandType = CommandType.StoredProcedure;
                 cmd.Parameters.AddWithValue("@usuario", usuario);
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
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            CargarDatos();
        }
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView11.EditIndex = e.NewEditIndex;
            // CargarDatos_movimiento(Convert.ToString(Session["ID_PLAT_MOV"]));
        }

        protected void Button5_Click(object sender, EventArgs e)
        {

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



            string Seller = Convert.ToString(Session["usuario"]);
            fnllenaNombreProducto(Seller);
            fnllenaCantidad();
            string id = GridView1.SelectedRow.Cells[0].Text;
            string Id_Plat = GridView1.SelectedRow.Cells[2].Text;
            string Estado= GridView1.SelectedRow.Cells[8].Text;
            if (Estado == "Ingresado")
            {
                Panel4.Visible = true;
               // Boton12.Visible = true;
            }
            else
            {
                Panel4.Visible = false;
               // Boton12.Visible = false;
            }

            Session["ID_PLAT_MOV"] = GridView1.SelectedRow.Cells[2].Text;
            CargarDatos_movimiento(Id_Plat);



            Origen.Text = GridView1.SelectedRow.Cells[1].Text;
            Id_plat.Text = GridView1.SelectedRow.Cells[2].Text;
            Nombre_Comprador.Text = GridView1.SelectedRow.Cells[3].Text;
            Direccion.Text = GridView1.SelectedRow.Cells[4].Text;
            //Comuna.Text = GridView1.SelectedRow.Cells[6].Text;
            //Telefono.Text = GridView1.SelectedRow.Cells[7].Text;
            //Correo_electronico.Text = GridView1.SelectedRow.Cells[5].Text;
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

        

        protected void ButtonArticulo_Click(object sender, EventArgs e)
        {
            try
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
                SqlCommand cmd = new SqlCommand("sp_registrar_venta_producto_ext", con);
                string valo = Nombre_Producto.SelectedValue;
                string[] valores = Nombre_Producto.SelectedValue.Split('&');
                string Nombre_Producto2 = valores[0];

                string Sku2 = valores[1];


                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Plat", System.Data.SqlDbType.VarChar).Value = Id_plat.Text;
                cmd.Parameters.Add("@Nombre_producto", System.Data.SqlDbType.VarChar).Value = Nombre_Producto2;
                cmd.Parameters.Add("@Cantidad", System.Data.SqlDbType.VarChar).Value = cantidad.SelectedValue;
                cmd.Parameters.Add("@SKU", System.Data.SqlDbType.VarChar).Value = Sku2;
                con.Open();
                cmd.ExecuteNonQuery();

                con.Close();
                //Limpiar2();
                // Response.Write("<script>alert('Ingreso de Producto Realizado..')</script>");
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "Swal.fire({icon: 'success',title: 'Ok..',  text: 'Ingreso de Producto Realizado, Pedido Modificado!'})", true);

                //   Gvbind();


                //Response.Redirect("Login.aspx");
            }
            catch (Exception)
            {

                throw;
            }


        }


        private void fnllenaNombreProducto(string Seller)
        {

            try
            {

               
                ds = com.getNombreProducto(Seller);
                Nombre_Producto.DataSource = ds;
                Nombre_Producto.DataValueField = "Nombre_combi";
                Nombre_Producto.DataTextField = "Nombre_combi";
                Nombre_Producto.DataSource = ds;
                Nombre_Producto.DataBind();
                Nombre_Producto.Items.Insert(0, new ListItem("-- Seleccione Nombre Producto --", "0"));
            }
            catch (Exception)
            {
                throw;
            }

        }

        private void fnllenaCantidad()
        {
            
                try
                {
                    ds = com.getCantidad();
                    cantidad.DataSource = ds;
                    cantidad.DataValueField = "id_cantidad";
                    cantidad.DataTextField = "cantidad";
                    cantidad.DataSource = ds;
                    cantidad.DataBind();
                    cantidad.Items.Insert(0, new ListItem("-- Seleccione Cantidad --", "0"));
                }
                catch (Exception)
                {
                    throw;
                }
            
        }




        protected void GridView11_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["conexion"].ToString();

            int ide = Convert.ToInt32(GridView11.DataKeys[e.RowIndex].Value.ToString());

           

            SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
            SqlCommand cmd2 = new SqlCommand("Ver_estado_Eliminar_Seller", con2);
            cmd2.Parameters.Add("@id_pedido", SqlDbType.Int).Value = ide;
            cmd2.CommandType = System.Data.CommandType.StoredProcedure;
            con2.Open();
            SqlDataReader dr2 = cmd2.ExecuteReader();
            //string Id_producto;
            int valor;
            if (dr2.Read())
            {
                 //  valor = Convert.ToInt32(dr2["Estado"]);
                 Session["Id_producto"] = dr2["Estado"].ToString();


            }
            con2.Close();


            if (Convert.ToString(Session["Id_producto"]) == "1")
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("delete from Producto_Pedido  where id_prod_ped='" + ide + "' ", con);
                    int t = cmd.ExecuteNonQuery();
                    if (t > 0)
                    {

                        //Response.Write("<script>alert('Informacion Eliminada')</script>");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "Swal.fire({icon: 'info',title: 'Ok..',  text: 'Producto Eliminado, Pedido Modificado!'})", true);

                        GridView11.EditIndex = -1;

                        CargarDatos();
                    }

                }

            }
            else
            {

                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "Swal.fire({icon: 'info',title: 'Ok..',  text: 'Producto No Eliminado, No esta En Etapa para Modificar!'})", true);
            }

        }
    }
  
}