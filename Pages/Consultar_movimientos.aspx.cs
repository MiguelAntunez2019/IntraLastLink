using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection.Emit;

namespace ProyectoLastLink.Pages
{
    public partial class Consultar_movimientos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //  Gvbind();
            if (!IsPostBack)
            {
                CargarDatos();
            }
        }

        private void CargarDatos()
        {
            try
            {
                DataTable datatable = new DataTable();
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());

                //SqlDataAdapter da = new SqlDataAdapter("sp_datos_Movimiento", con);
                SqlCommand cmd = new SqlCommand("sp_datos_Movimiento", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                con.Open();
                adapter.Fill(datatable);
                con.Close();
                GridView1.DataSource = datatable;
                GridView1.DataBind();
                if(datatable.Rows.Count==0)
                {
                    lblNoresult.Visible = true;
                    GridView1.Visible = false;  
                }else
                {
                    lblNoresult.Visible = false;
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
            string buscar=BuscarNombre.Text.Trim();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
            SqlCommand cmd = new SqlCommand("sp_datos_Movimiento_buscar", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Busqueda", buscar);
            SqlDataAdapter adapter= new SqlDataAdapter(cmd);
            DataTable datatable= new DataTable();
            con.Open();
            adapter.Fill(datatable);
            con.Close();
            GridView1.DataSource = datatable;
            GridView1.DataBind();
            
            if (datatable.Rows.Count == 0)
            {
                lblNoresult.Visible = true;
                GridView1.Visible = false;
            }
            else
            {
                lblNoresult.Visible = false;
                GridView1.Visible = true;
            }




        }

      
    }
}