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
    public partial class Graficos_seller : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected string obtenerDatos()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
            SqlCommand cmd = new SqlCommand("Grafico_totales_pedidos", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();
            //SqlDataReader dr = cmd.ExecuteReader();

            DataTable Datos = new DataTable();
            Datos.Load(cmd.ExecuteReader());
            con.Close();

            string strDatos;
            strDatos = "[['Seller','Total'],";
            foreach (DataRow dr in Datos.Rows)
            {
                strDatos = strDatos + "[";
                strDatos = strDatos + "'" + dr[0] + "'" + "," + dr[1];
                strDatos = strDatos + "],";
            }
            strDatos = strDatos + "]";



            return strDatos;//strDatos;

        }

        protected string obtenerDatos2()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
            SqlCommand cmd = new SqlCommand("Grafico_totales_Ventas", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();
            //SqlDataReader dr = cmd.ExecuteReader();

            DataTable Datos = new DataTable();
            Datos.Load(cmd.ExecuteReader());
            con.Close();

            string strDatos;
            strDatos = "[['Seller','Total'],";
            foreach (DataRow dr in Datos.Rows)
            {
                strDatos = strDatos + "[";
                strDatos = strDatos + "'" + dr[0] + "'" + "," + dr[1];
                strDatos = strDatos + "],";
            }
            strDatos = strDatos + "]";



            return strDatos;//strDatos;

        }

    }
}