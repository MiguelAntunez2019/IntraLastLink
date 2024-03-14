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

namespace ProyectoLastLink.Pages
{
    public partial class Crear_pedido : System.Web.UI.Page
    {
        combos com = new combos();
        DataSet ds = new DataSet();
        protected void Page_Load(object sender, EventArgs e)
        {

            string usuariovalida = Convert.ToString(Session["usuario"]);
            if (usuariovalida == "")
            {
                Response.Redirect("finsesion.aspx");
            }



            Response.AppendHeader("Cache-Control", "no-store");
            if (!IsPostBack)
            {
                Gvbind();

            }

            fnllenaHorasFin();
            fnllenaOrigen();
            fnllenaSeller();
            fnllenaCantidad();
        }
        //string Patron = "InfoToolsSV";

        void Limpiar()
        {
            Id_Plataforma.Text = "";
            Nombre_comprador.Text = "";
            Apellido_comprador.Text = "";
            Direccion_comprador.Text = "";
            Telefono_comprador.Text = "";
            Correo_electronico_comp.Text = "";
            Nota_Cliente.Text = "";
            

        }
        void Limpiar2()
        {
            Id_Plataforma.Text = "";

            Id_plat2.Text = "";
            //Nombre_Producto.SelectedValue = "";
            //cantidad.SelectedValue = "";
            //Sku2.Text = "";
        }

        protected void Gvbind()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
            SqlCommand cmd = new SqlCommand("Listar_Ordenes_externas", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows == true)
            {

                GridView1.DataSource = dr;
                GridView1.DataBind();

            }


        }

        protected void Gvbind2(string id_plat)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
            SqlCommand cmd = new SqlCommand("Listar_Productos_externas", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@id_plat", System.Data.SqlDbType.VarChar).Value = id_plat;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows == true)
            {

                GridView2.DataSource = dr;
                GridView2.DataBind();

            }


        }


        private void fnllenaCantidad()
        {
            if (IsPostBack == false)
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
        }
        /// <summary>
        /// 
        /// </summary>
        private void fnllenaHorasFin()
        {
            if (IsPostBack == false)
            {
                try
                {
                    ds = com.getHoraFinReserva2();
                    comuna_seller.DataSource = ds;
                    comuna_seller.DataValueField = "Nombre_Comuna";
                    comuna_seller.DataTextField = "Nombre_Comuna";
                    comuna_seller.DataSource = ds;
                    comuna_seller.DataBind();
                    comuna_seller.Items.Insert(0, new ListItem("-- Seleccione Comuna --", "0"));
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        private void fnllenaOrigen()
        {
            if (IsPostBack == false)
            {
                try
                {
                    ds = com.getOrigen();
                    Origen.DataSource = ds;
                    Origen.DataValueField = "nombre_origen";
                    Origen.DataTextField = "nombre_origen";
                    Origen.DataSource = ds;
                    Origen.DataBind();
                    Origen.Items.Insert(0, new ListItem("-- Seleccione Origen --", "0"));
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private void fnllenaSeller()
        {
            if (IsPostBack == false)
            {
                try
                {
                    ds = com.getComboSeller();
                    Seller.DataSource = ds;
                    Seller.DataValueField = "nombre_seller";
                    Seller.DataTextField = "nombre_seller";
                    Seller.DataSource = ds;
                    Seller.DataBind();
                    Seller.Items.Insert(0, new ListItem("-- Seleccione Seller --", "0"));
                }
                catch (Exception)
                {
                    throw;
                }
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


        protected void Registrar_Click(object sender, EventArgs e)
        {

            string usuariovalida = Convert.ToString(Session["usuario"]);
            if (usuariovalida == "")
            {
                Response.Redirect("finsesion.aspx");
            }


            try
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());

                SqlCommand cmd = new SqlCommand("sp_registrar_venta_pedido_ext", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Origen", System.Data.SqlDbType.VarChar).Value = Origen.SelectedValue;
                cmd.Parameters.Add("@Proveedor", System.Data.SqlDbType.VarChar).Value = Seller.SelectedValue;
                cmd.Parameters.Add("@Id_Plat", System.Data.SqlDbType.VarChar).Value = Id_Plataforma.Text;
                cmd.Parameters.Add("@Nombre_comprador", System.Data.SqlDbType.VarChar).Value = Nombre_comprador.Text;
                cmd.Parameters.Add("@Apellido_comprador", System.Data.SqlDbType.VarChar).Value = Apellido_comprador.Text;
                cmd.Parameters.Add("@Direccion", System.Data.SqlDbType.VarChar).Value = Direccion_comprador.Text; 
                cmd.Parameters.Add("@Comuna", System.Data.SqlDbType.VarChar).Value = comuna_seller.SelectedValue;
                cmd.Parameters.Add("@Telefono", System.Data.SqlDbType.VarChar).Value = Telefono_comprador.Text;
                cmd.Parameters.Add("@Correo_electronico", System.Data.SqlDbType.VarChar).Value = Correo_electronico_comp.Text;
                cmd.Parameters.Add("@Nota_Cliente", System.Data.SqlDbType.VarChar).Value = Nota_Cliente.Text;
                con.Open();
                cmd.ExecuteNonQuery();
                int Estado = 4;
                cargar_estado_log(Id_Plataforma.Text, Estado);

                con.Close();

                 ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "Swal.fire({icon: 'success',title: 'Ok..',  text: 'Ingreso de Pedido Realizado, ahora debe ingresar Productos!'})", true);

                Gvbind();
                Limpiar();

                //Response.Redirect("Login.aspx");
            }
            catch (Exception)
            {

                throw;
            }
        }


        //public void BusquedaAlumnoPorNombre()
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
        //    SqlCommand cmd = new SqlCommand("sp_datos_seller_nombre", con);
        //    cmd.Parameters.Add("@nombre2", SqlDbType.VarChar).Value = BuscarNombre.Text.Trim();
        //    cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //    con.Open();
        //    SqlDataReader dr = cmd.ExecuteReader();
        //    if (dr.HasRows == true)
        //    {

        //        GridView1.DataSource = dr;
        //        GridView1.DataBind();

        //    }

        //}
        //protected void Buscar_Click(object sender, EventArgs e)
       // {
       //     BusquedaAlumnoPorNombre();
       // }

        //protected void GridView11_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //   GridView1.PageIndex = e.NewPageIndex;
        //}
        protected void Button1_Click(object sender, EventArgs e)
        {

        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            string id = GridView1.SelectedRow.Cells[0].Text;
            string id_plat = GridView1.SelectedRow.Cells[1].Text;

            Id_plat2.Text = GridView1.SelectedRow.Cells[1].Text;
            string Seller = GridView1.SelectedRow.Cells[3].Text;
            fnllenaNombreProducto(Seller);
            Gvbind2(id_plat);


            ModalPopupExtender1.Show();
        }

        private void cargar_estado_log(string idplat, int Estado)
        {
            try
            {
                string Comentario = "Crear Pedido Externo";
                string usuario = Session["usuario"].ToString();


                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
                SqlCommand cmd = new SqlCommand("sp_registrar_log_estado", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Plat", System.Data.SqlDbType.VarChar).Value = idplat;
                cmd.Parameters.Add("@Estado", SqlDbType.Int).Value = Estado;
                cmd.Parameters.Add("@usuario", SqlDbType.VarChar).Value = usuario;
                cmd.Parameters.Add("@Comentario", SqlDbType.VarChar).Value = Comentario;

                con.Open();
                cmd.ExecuteNonQuery();

                con.Close();

            }
            catch (Exception)
            {

                throw;
            }


        }
        protected void ButtonArticulo_Click(object sender, EventArgs e)
        {

            string usuariovalida = Convert.ToString(Session["usuario"]);
            if (usuariovalida == "")
            {
                Response.Redirect("finsesion.aspx");
            }

            try
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
                SqlCommand cmd = new SqlCommand("sp_registrar_venta_producto_ext", con);
                string[] valores = Nombre_Producto.SelectedValue.Split('&');
                string Nombre_Producto2 = valores[0];

                string Sku2 = valores[1];


                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Plat", System.Data.SqlDbType.VarChar).Value = Id_plat2.Text;
                cmd.Parameters.Add("@Nombre_producto", System.Data.SqlDbType.VarChar).Value = Nombre_Producto2;
                cmd.Parameters.Add("@Cantidad", System.Data.SqlDbType.VarChar).Value = cantidad.SelectedValue;
                cmd.Parameters.Add("@SKU", System.Data.SqlDbType.VarChar).Value = Sku2;
                con.Open();
                cmd.ExecuteNonQuery();

                con.Close();
                Limpiar2();
               // Response.Write("<script>alert('Ingreso de Producto Realizado..')</script>");
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "Swal.fire({icon: 'success',title: 'Ok..',  text: 'Ingreso de Producto Realizado!'})", true);

                //   Gvbind();


                //Response.Redirect("Login.aspx");
            }
            catch (Exception)
            {

                throw;
            }



        }
    } 
}
