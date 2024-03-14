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
    public partial class Consultar_Bodega_Mov : System.Web.UI.Page
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
                SqlCommand cmd = new SqlCommand("sp_datos_Producto_mov", con);
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            string buscar = BuscarNombre.Text.Trim();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
            SqlCommand cmd = new SqlCommand("sp_datos_Producto_buscar_mov", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Busqueda", buscar);
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

        protected void GridView11_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView11.PageIndex = e.NewPageIndex;
        }


        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            string id_producto = GridView1.SelectedRow.Cells[0].Text;
            string id_seller = GridView1.SelectedRow.Cells[6].Text;

            Label13.Text = GridView1.SelectedRow.Cells[7].Text;

            CargarDatos_movimiento(id_producto, id_seller);

            // Label10.Text = GridView1.SelectedRow.Cells[0].Text;
            // Label11.Text = GridView1.SelectedRow.Cells[6].Text;
            // Label12.Text = GridView1.SelectedRow.Cells[2].Text;
            // Label13.Text = GridView1.SelectedRow.Cells[3].Text;
            ModalPopupExtender1.Show();
        }


        private void CargarDatos_movimiento(string id_producto, string id_seller)
        {
            try
            {

                DataTable datatable = new DataTable();
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());

                //SqlDataAdapter da = new SqlDataAdapter("sp_datos_Movimiento", con);
                SqlCommand cmd = new SqlCommand("sp_datos_Movimiento_popup", con);
                cmd.Parameters.Add("@id_seller", SqlDbType.Int).Value = id_seller; //Convert.ToString(Session["id_seller"]);
                cmd.Parameters.Add("@id_producto", SqlDbType.Int).Value = id_producto; //Convert.ToString(Session["id_seller"]);


                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                con.Open();
                adapter.Fill(datatable);
                con.Close();
                GridView11.DataSource = datatable;
                GridView11.DataBind();
                if (datatable.Rows.Count == 0)
                {
                    lblNoresult.Visible = true;
                    GridView11.Visible = false;
                }
                else
                {
                    lblNoresult.Visible = false;
                    GridView11.Visible = true;
                }



            }
            catch
            {
                throw;

            }

        }


    }
}