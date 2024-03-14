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
    public partial class Consultar_Bodega_Stock_Mov : System.Web.UI.Page
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
                string buscar = BuscarNombre.Text.Trim();
                if (buscar == "")
                {
                    DataTable datatable = new DataTable();
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
                    SqlCommand cmd = new SqlCommand("sp_datos_Producto", con);
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

                else
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
                    SqlCommand cmd = new SqlCommand("sp_datos_Producto_buscar", con);
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
            CargarDatos();

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




        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            CargarDatos();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["conexion"].ToString();




            int id_producto = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
            string Marca = ((TextBox)GridView1.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
            string Nombre = ((TextBox)GridView1.Rows[e.RowIndex].Cells[2].Controls[0]).Text;
            string Stock_inicial = ((TextBox)GridView1.Rows[e.RowIndex].Cells[3].Controls[0]).Text;
            string Valor_minimo = ((TextBox)GridView1.Rows[e.RowIndex].Cells[4].Controls[0]).Text;
            string Sku = ((TextBox)GridView1.Rows[e.RowIndex].Cells[5].Controls[0]).Text;
            string Ubicacion = ((TextBox)GridView1.Rows[e.RowIndex].Cells[8].Controls[0]).Text;
            string Codigo_barra = ((TextBox)GridView1.Rows[e.RowIndex].Cells[9].Controls[0]).Text;




            using (SqlConnection con = new SqlConnection(cs))

            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update Producto set Codigo_Barra='" + Codigo_barra + "',Ubicacion='" + Ubicacion + "',Sku='" + Sku + "',Marca='" + Marca + "',Nombre='" + Nombre + "', Stock_inicial='" + Stock_inicial + "',  Valor_minimo='" + Valor_minimo + "' where Id_producto='" + id_producto + "' ", con);
                int t = cmd.ExecuteNonQuery();
                if (t > 0)
                {
                    // Response.Write("<script>alert('Informacion Actualizada')</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "Swal.fire({icon: 'success',title: 'Ok..',  text: 'Información Bodega Actualizada!'})", true);

                    GridView1.EditIndex = -1;
                    CargarDatos();

                }
            }
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            CargarDatos();
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