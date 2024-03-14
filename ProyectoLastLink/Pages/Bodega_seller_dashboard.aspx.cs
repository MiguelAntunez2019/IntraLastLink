using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoLastLink.Pages
{
    public partial class Bodega_seller_dashboard : System.Web.UI.Page
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
                //buscar por fecha 
                string usuario = Convert.ToString(Session["usuario"]);
               
                DataTable datatable = new DataTable();
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());

                SqlCommand cmd = new SqlCommand("Listar_Stock_Bodega_Propio", con);
                cmd.Parameters.AddWithValue("@usuario", usuario);

                if (Session["consulta_seller_Stock"].ToString() == "1")
                {
                    cmd.Parameters.AddWithValue("@Estado", 1);
                }
                if (Session["consulta_seller_Stock"].ToString() == "2")
                {
                    cmd.Parameters.AddWithValue("@Estado", 2);
                }

                if (Session["consulta_seller_Stock"].ToString() == "3")
                {
                    cmd.Parameters.AddWithValue("@Estado", 3);
                }
                if (Session["consulta_seller_Stock"].ToString() == "4")
                {
                    cmd.Parameters.AddWithValue("@Estado", 4);
                }


                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                con.Open();
                adapter.Fill(datatable);
                con.Close();
                GridView1.DataSource = datatable;
                GridView1.DataBind();
                GridView2.DataSource = datatable;
                GridView2.DataBind();
                
            }
            catch
            {
                throw;

            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            CargarDatos();
        }

        protected void Button6_Click(object sender, EventArgs e)
        {


            GridView2.Visible = true;
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/ms-excel";
            Response.AddHeader("content-disposition", "attachment; filename = Migrar_Seller_Stock.xls");
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

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            CargarDatos();
        }

        

        protected void GridView11_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView11.PageIndex = e.NewPageIndex;
        }


        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            string id_producto = GridView1.SelectedRow.Cells[0].Text;
            string id_seller = GridView1.SelectedRow.Cells[1].Text;



            CargarDatos_movimiento(id_producto, id_seller);

           
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