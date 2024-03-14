using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ProyectoLastLink.clases;
using System.Net.NetworkInformation;
using System.Xml.Linq;
using System.IO;
using System.Data.OleDb;
using System.Drawing;
using System.Web.ModelBinding;
using System.Security.Claims;
using ClosedXML;
using ClosedXML.Excel;
using Microsoft.Win32;

namespace ProyectoLastLink.Pages
{
    public partial class Productos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Formato();

            }
        }


        private void Formato()
        {
            try
            {

                DataTable datatable = new DataTable();
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
                //SqlDataAdapter da = new SqlDataAdapter("sp_datos_Movimiento", con);
                SqlCommand cmd = new SqlCommand("Formato_producto", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                con.Open();
                adapter.Fill(datatable);
                con.Close();
                GridView2.DataSource = datatable;

                GridView2.DataBind();

            }
            catch
            {
                throw;

            }

        }


        protected void LinkButton1_Click1(object sender, EventArgs e)
        {
            try
            {
                //carga archivo excel en ruta Files
                if (FileUpload1.HasFile)
                {
                    string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                    string filepath = Server.MapPath("~/Files/" + filename);
                    FileUpload1.SaveAs(filepath);

                    Button2.Visible = true;

                }
            }
            catch (Exception)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "Swal.fire({icon: 'error',title: 'Error..',  text: 'El Archivo esta Abierto, Favor Cerrarlo!'})", true);


            }


        }


        public void eliminar_duplicados_productos()
        {
            try
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());

                SqlCommand cmd = new SqlCommand("sp_eliminar_duplicados", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception)
            {

                throw;
            }

        }





        protected void Button2_Click(object sender, EventArgs e)
        {

            try
            {
                string filePath = Server.MapPath("~/Files/Productos.xlsx");

                using (var workbook = new XLWorkbook(filePath))
                {
                    var firstSheet = workbook.Worksheet(1);

                    // Obtener los datos de todas las celdas de la hoja
                    var range = firstSheet.RangeUsed();

                    // Convertir el rango a un DataTable
                    var dataTable = range.AsTable().AsNativeDataTable();
                    using (SqlConnection conexionDestino = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString()))
                    {
                        conexionDestino.Open();

                        using (SqlBulkCopy importar = new SqlBulkCopy(conexionDestino))
                        {
                            importar.DestinationTableName = "Producto";
                            importar.WriteToServer(dataTable);
                        }

                        conexionDestino.Close();
                    }

                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "Swal.fire({icon: 'success', title: 'Ok..', text: 'Archivo Cargado con Éxito!'})", true);

                    eliminar_duplicados_productos();
                    Gvbind();
                    Button2.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", $"Swal.fire({{ icon: 'error', title: 'Error', text: 'Hubo un Error en la Carga. Detalles: {ex.Message}'}})", true);
                Button2.Visible = true;
            }
        }

        protected void Gvbind()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
            SqlCommand cmd = new SqlCommand("sp_datos_Producto", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows == true)
            {

                GridView1.DataSource = dr;
                GridView1.DataBind();

            }


        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            Gvbind();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["conexion"].ToString();

            int id_producto = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
            string Marca = ((TextBox)GridView1.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
            string Nombre = ((TextBox)GridView1.Rows[e.RowIndex].Cells[2].Controls[0]).Text;
            string Stock_inicial = ((TextBox)GridView1.Rows[e.RowIndex].Cells[3].Controls[0]).Text;
            string Valor_minimo = ((TextBox)GridView1.Rows[e.RowIndex].Cells[4].Controls[0]).Text;
            string sku = ((TextBox)GridView1.Rows[e.RowIndex].Cells[5].Controls[0]).Text;
            string Ubicacion = ((TextBox)GridView1.Rows[e.RowIndex].Cells[6].Controls[0]).Text;

            //int id_seller = ((TextBox)GridView1.Rows[e.RowIndex].Cells[6].Controls[0]).Text;

            //  int id_seller = Convert.ToInt32(GridView1.Rows[e.RowsIndex].Cells[6].Value);


            using (SqlConnection con = new SqlConnection(cs))

            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update Producto set Marca='" + Marca + "', Nombre='" + Nombre + "',  Stock_inicial='" + Stock_inicial + "' ,  Valor_minimo='" + Valor_minimo + "',  sku='" + sku + "' where id_producto='" + id_producto + "' ", con);
                int t = cmd.ExecuteNonQuery();
                if (t > 0)
                {
                    //  Response.Write("<script>alert('Informacion Actualizada')</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "Swal.fire({icon: 'success',title: 'Ok..',  text: 'Información Actualizada!'})", true);

                    GridView1.EditIndex = -1;
                    Gvbind();
                    // Limpiar();
                }
            }
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            Gvbind();
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {

            GridView2.Visible = true;
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/ms-excel";
            Response.AddHeader("content-disposition", "attachment; filename = Productos_formato.xls");
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
    }

}