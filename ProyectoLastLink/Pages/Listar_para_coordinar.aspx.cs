using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Drawing.Printing;
using iTextSharp.text.pdf.codec.wmf;
using QRCoder;
using ZXing;
using ZXing.Common;
using System.Drawing;
using System.IO;
using System.Security.Policy;
using System.Security.Cryptography.Xml;

namespace ProyectoLastLink.Pages
{
    public partial class Listar_para_coordinar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //  Gvbind();
            //aca debo migrar la info de las tablas del demonio de pedido y sus
            //productos

            if (!IsPostBack)
            {
                CargarDatos();
                CargarDatos2();

            }

        }


        private void CargarDatos2()
        {
            try
            {
                //buscar por fecha 

                string buscar_fecha = BuscarNombre_calendario.Text.Trim();
                string buscar2_fecha = BuscarNombre2_calendario.Text.Trim();
                var fecha1 = DateTime.Now;
                var fecha2 = DateTime.Now;
                DateTime dt, dt2;
                var date1 = DateTime.TryParse(buscar_fecha, out dt);
                var date2 = DateTime.TryParse(buscar2_fecha, out dt2);

                if (date1 && date2)
                {
                    fecha1 = dt;
                    fecha2 = dt2;
                }
                //////////////////

                /// buscar por nombre
                string buscar = BuscarNombre.Text.Trim();
                //
                DataTable datatable = new DataTable();
                DataTable datatable2 = new DataTable();

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());


                if (buscar == "" && buscar_fecha == "" && buscar2_fecha == "")
                {
                    Session["cadena"] = "Listar_Ordenes_pla";
                }
                if (buscar != "" && buscar_fecha == "" && buscar2_fecha == "")
                {
                    Session["cadena"] = "Listar_Ordenes_buscar_Proveedor_pla";
                }

                if (buscar == "" && buscar_fecha != "" && buscar2_fecha != "")
                {
                    Session["cadena"] = "Listar_Ordenes_buscar_pla";
                }
                string cadena = Convert.ToString(Session["cadena"]);
                //SqlDataAdapter da = new SqlDataAdapter("sp_datos_Movimiento", con);
                SqlCommand cmd = new SqlCommand(cadena, con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (buscar != "" && buscar_fecha == "" && buscar2_fecha == "")
                {
                    cmd.Parameters.AddWithValue("@Busqueda", buscar);

                }
                if (buscar == "" && buscar_fecha != "" && buscar2_fecha != "")
                {
                    cmd.Parameters.AddWithValue("@Busqueda", fecha1);
                    cmd.Parameters.AddWithValue("@Busqueda2", fecha2);

                }

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                con.Open();
                adapter.Fill(datatable);
                con.Close();
                GridView2.DataSource = datatable;
                GridView2.DataBind();

                if (datatable.Rows.Count == 0)
                {

                    GridView1.Visible = false;
                }
                else
                {

                    GridView1.Visible = true;
                }



            }
            catch
            {
                throw;

            }

        }





        private void CargarDatos()
        {
            try
            {
                //buscar por fecha 

                string buscar_fecha = BuscarNombre_calendario.Text.Trim();
                string buscar2_fecha = BuscarNombre2_calendario.Text.Trim();
                var fecha1 = DateTime.Now;
                var fecha2 = DateTime.Now;
                DateTime dt, dt2;
                var date1 = DateTime.TryParse(buscar_fecha, out dt);
                var date2 = DateTime.TryParse(buscar2_fecha, out dt2);

                if (date1 && date2)
                {
                    fecha1 = dt;
                    fecha2 = dt2;
                }
                //////////////////

                /// buscar por nombre
                string buscar = BuscarNombre.Text.Trim();
                //
                DataTable datatable = new DataTable();
                DataTable datatable2 = new DataTable();

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());


                if (buscar == "" && buscar_fecha == "" && buscar2_fecha == "")
                {
                    Session["cadena"] = "Listar_Ordenes_pla";
                }
                if (buscar != "" && buscar_fecha == "" && buscar2_fecha == "")
                {
                    Session["cadena"] = "Listar_Ordenes_buscar_Proveedor_pla";
                }

                if (buscar == "" && buscar_fecha != "" && buscar2_fecha != "")
                {
                    Session["cadena"] = "Listar_Ordenes_buscar_pla";
                }
                string cadena = Convert.ToString(Session["cadena"]);
                //SqlDataAdapter da = new SqlDataAdapter("sp_datos_Movimiento", con);
                SqlCommand cmd = new SqlCommand(cadena, con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (buscar != "" && buscar_fecha == "" && buscar2_fecha == "")
                {
                    cmd.Parameters.AddWithValue("@Busqueda", buscar);

                }
                if (buscar == "" && buscar_fecha != "" && buscar2_fecha != "")
                {
                    cmd.Parameters.AddWithValue("@Busqueda", fecha1);
                    cmd.Parameters.AddWithValue("@Busqueda2", fecha2);

                }

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                con.Open();
                adapter.Fill(datatable);
                con.Close();
                GridView1.DataSource = datatable;
                GridView1.DataBind();

                if (datatable.Rows.Count == 0)
                {

                    GridView1.Visible = false;
                }
                else
                {

                    GridView1.Visible = true;
                }



            }
            catch
            {
                throw;

            }

        }

        protected void Button5_Click(object sender, EventArgs e)
        {

            CargarDatos();
            CargarDatos2();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            CargarDatos();
        }

        protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            CargarDatos();
            CargarDatos2();
        }






        protected void Button3_Click(object sender, EventArgs e)
        {
            //Panel3 es seller
            //Panel_Fecha
            //buscar por fecha
            Panel3.Visible = false;
            Panel_Fecha.Visible = true;
            BuscarNombre.Text = "";


        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            //BuscarNombre por estado
            Panel3.Visible = true;
            Panel_Fecha.Visible = false;
            BuscarNombre_calendario.Text = "";
            BuscarNombre2_calendario.Text = "";



        }


        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            BuscarNombre_calendario.Text = Calendar1.SelectedDate.ToString();

        }
        protected void Calendar2_SelectionChanged(object sender, EventArgs e)
        {
            BuscarNombre2_calendario.Text = Calendar2.SelectedDate.ToString();

        }

        protected void Button6_Click(object sender, EventArgs e)
        {


            GridView2.Visible = true;


            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/ms-excel";
            Response.AddHeader("content-disposition", "attachment; filename = SacarProductosBodega.xls");
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