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
    public partial class ListOrdenes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //  Gvbind();
           // Calendar1.SelectedDate = DateTime.Now;
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
                SqlCommand cmd = new SqlCommand("Listar_Ordenes", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
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

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            BuscarNombre.Text = Calendar1.SelectedDate.ToString();

        }
        protected void Calendar2_SelectionChanged(object sender, EventArgs e)
        {
            BuscarNombre2.Text = Calendar2.SelectedDate.ToString();

        }
        protected void Button1_Click(object sender, EventArgs e)
        {

            //BuscarNombre2.Text = Calendar1.SelectedDate.ToString();
            string buscar = BuscarNombre.Text.Trim();
            string buscar2 = BuscarNombre2.Text.Trim();
            var fecha1 = DateTime.Now;
            var fecha2 = DateTime.Now;
            DateTime dt, dt2;
            var date1 = DateTime.TryParse(buscar, out dt);
            var date2 = DateTime.TryParse(buscar2, out dt2);

            if (date1 && date2)
            {
                fecha1 = dt;
                fecha2 = dt2;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
            SqlCommand cmd = new SqlCommand("Listar_Ordenes_buscar", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Busqueda", fecha1);
            cmd.Parameters.AddWithValue("@Busqueda2", fecha2);


            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable datatable = new DataTable();
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