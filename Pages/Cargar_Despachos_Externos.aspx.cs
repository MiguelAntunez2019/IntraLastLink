﻿using System;
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
using ClosedXML.Excel;
using System.Windows.Controls;

namespace ProyectoLastLink.Pages
{
    public partial class Cargar_Despachos_Externos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            {
                Formato();

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
               // Response.Write("<script>alert('El Archivo esta Abierto, Favor Cerrarlo!!!')</script>");
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "Swal.fire({icon: 'error',title: 'Error..',  text: 'El Archivo esta Abierto, Favor Cerrarlo!'})", true);

            }


        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {

            GridView2.Visible = true;
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/ms-excel";
            Response.AddHeader("content-disposition", "attachment; filename = Pedido_externo.xls");
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GridView2.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.End();

        }


        public void eliminar_duplicados_productos()
        {
            try
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());

                SqlCommand cmd = new SqlCommand("sp_eliminar_duplicados_Pedido", con);
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
                string filePath = Server.MapPath("~/Files/Pedido_externo.xlsx");

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
                            importar.DestinationTableName = "Cabecera_Pedido";
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
            SqlCommand cmd = new SqlCommand("sp_datos_Pedido_Externo", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows == true)
            {
             
                GridView1.DataSource = dr;
                GridView1.DataBind();

            }


        }
        private void Formato()
        {
            try
            {

                DataTable datatable = new DataTable();
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
                //SqlDataAdapter da = new SqlDataAdapter("sp_datos_Movimiento", con);
                SqlCommand cmd = new SqlCommand("Formato_Externo", con);
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






    }
}