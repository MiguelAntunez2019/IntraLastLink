using Org.BouncyCastle.Math;
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
    public partial class Stock : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            contadores_cabeceras_seller();
          

        }
         
                

        public void contadores_cabeceras_seller()
        {
            try
            {
                string usuario = Convert.ToString(Session["usuario"]);
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
                SqlCommand cmd = new SqlCommand("sp_dashboard_stock_seller", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@usuario", usuario);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    int contador_sinSku = Convert.ToInt32(dr["sinSku"]);
                    int contador_SinStock = Convert.ToInt32(dr["SinStock"]);
                    int Total_Productos_Stock = Convert.ToInt32(dr["Productos_Stock"]);
                    int contador_BajoMinimo = Convert.ToInt32(dr["contador_BajoMinimo"]);
                    
                    Session["contador_sinSku"] = contador_sinSku;
                    Session["contador_SinStock"] = contador_SinStock;
                    Session["contador_BajoMinimo"] = contador_BajoMinimo;
                    Session["Total_Productos_Stock"] = Total_Productos_Stock;
                }

                //aqui va el open de conexion en caso que mande error

                con.Close();
            }
            catch
            {

                throw;
            }



        }


        protected void LinkButton1_Click(object sender, EventArgs e)
        {

            Session["consulta_seller_Stock"] = "1";
            Response.Redirect("Bodega_seller_dashboard.aspx");

        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {

            Session["consulta_seller_Stock"] = "2";
            Response.Redirect("Bodega_seller_dashboard.aspx");

        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {

            Session["consulta_seller_Stock"] = "3";
            Response.Redirect("Bodega_seller_dashboard.aspx");

        }

        protected void LinkButton4_Click(object sender, EventArgs e)
        {

            Session["consulta_seller_Stock"] = "4";
            Response.Redirect("Bodega_seller_dashboard.aspx");

        }

            
      
    }

}