using ProyectoLastLink.clases;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace ProyectoLastLink.Pages
{
    public partial class Movimientos_Bodega : System.Web.UI.Page
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

            fnllenaSelect();
            
        }

        private void fnllenaSelect()
        {
            if (IsPostBack == false)
            {
                try
                {
                    
                    ds = com.getComboSeller();
                    comuna_seller.DataSource = ds;
                    comuna_seller.DataValueField = "id_seller";
                    comuna_seller.DataTextField = "nombre_seller";
                    comuna_seller.DataSource = ds;
                    comuna_seller.DataBind();
                    comuna_seller.Items.Insert(0, new ListItem("-- Seleccione Seller --", "0"));
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

            string usuariovalida = Convert.ToString(Session["usuario"]);
            if (usuariovalida == "")
            {
                Response.Redirect("finsesion.aspx");
            }

            try
            {
                producto_lectura.Focus();
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
                SqlCommand cmd = new SqlCommand("sp_datos_extraer_productos", con);
                cmd.Parameters.Add("@Codigobarra", SqlDbType.VarChar).Value = producto_lectura.Text.Trim();
                cmd.Parameters.Add("@id_seller", SqlDbType.Int).Value = Convert.ToString(Session["id_seller"]);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                //string Id_producto;

                if (dr.Read())
                {
                    Session["Id_producto"] = dr["Id_producto"].ToString();
                    Session["sumar_a_stock"] = dr["Stock_Inicial"].ToString();
                    insertar_movimiento();
                    sumar_stock_inicial();
                }
                else
                {

                     ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "Swal.fire({icon: 'error',title: 'Ok..',  text: 'Seller No Posee este Producto!'})", true);

                }


               

                con.Close();
            }
            catch (Exception) {
                  throw;
            }
        }



        protected void sumar_stock_inicial()
        {
            try
            {
                int suma = Convert.ToInt32(Session["sumar_a_stock"]) + 1;
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
            string Comentario = "Ingreso Bodega Normal";
            cmd2.Parameters.Add("@id_producto", SqlDbType.Int).Value = Convert.ToString(Session["Id_producto"]);
            cmd2.Parameters.Add("@id_seller", SqlDbType.Int).Value = Convert.ToString(Session["id_seller"]);
            cmd2.Parameters.Add("@cantidad", SqlDbType.Int).Value = 1;
            cmd2.Parameters.Add("@tipo_movimiento", SqlDbType.Int).Value = 1; //Entrada
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
            string variable = Session["id_seller"].ToString();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
            SqlCommand cmd = new SqlCommand("sp_datos_Movimiento_Ingreso", con);
             cmd.Parameters.Add("@id_seller", SqlDbType.Int).Value = Convert.ToString(Session["id_seller"]);
             cmd.Parameters.Add("@tipo_movimiento", SqlDbType.Int).Value = 1;

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            

                GridView1.DataSource = dr;
                GridView1.DataBind();

           


        }

        protected void Seleccionar_Seller_Click(object sender, EventArgs e)
        {

            string usuariovalida = Convert.ToString(Session["usuario"]);
            if (usuariovalida == "")
            {
                Response.Redirect("finsesion.aspx");
            }

            string id_seller;
            id_seller=comuna_seller.SelectedValue;
            Session["id_seller"]=id_seller;
            Seleccionar_Seller.Visible = false;
            Panel1.Visible = true;
            producto_lectura.Focus();

        }


    



    }
}