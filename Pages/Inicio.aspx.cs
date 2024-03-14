using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static ProyectoLastLink.clases.combos;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using ProyectoLastLink.clases;
using static iTextSharp.awt.geom.Point2D;
using System.Linq.Expressions;

namespace ProyectoLastLink.Pages
{
    public partial class Inicio : System.Web.UI.Page
    {
        combos com = new combos();
        DataSet ds = new DataSet();
        protected void Page_Load(object sender, EventArgs e)
        {

            string usuariovalida = Convert.ToString(Session["usuario"]);
            if (usuariovalida == "")
            {
                Response.Redirect("finsesion.aspx");
            }

            //contadores_cabeceras();
            string fecha_inicio = fecha1.Text;
            string fecha_fin = fecha2.Text;

            Session["fecha_inicio"] = fecha_inicio;
            Session["fecha_fin"] = fecha_fin;

            String Mes = Pedido.SelectedValue;
            Session["Mes"] = Mes;
            if (Mes == "")
            {
                Mes = "0";
            }

            
            contadores_cabeceras_seller(Mes);
            obtenerDatos_barra();
            fnllenaSelect();

        }



        private void fnllenaSelect()
        {
            if (IsPostBack == false)
            {
                try
                {

                    ds = com.getComboMes();
                    Pedido.DataSource = ds;
                    Pedido.DataValueField = "Mes_detalle";
                    Pedido.DataTextField = "Mes_detalle";
                    Pedido.DataSource = ds;
                    Pedido.DataBind();
                    Pedido.Items.Insert(0, new ListItem("-- Seleccione Mes --", "0"));
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }


        
        protected string obtenerDatos_barra()
        {
            string Mes= Session["Mes"].ToString() ;
            string Fecha_inicio = Session["fecha_inicio"].ToString();
            string Fecha_fin = Session["fecha_fin"].ToString();

            string Roles = Session["Id_rol"].ToString();


            if (Session["Id_rol"].ToString() == "2") { 
                if (Mes == "0" && Fecha_inicio == "" && Fecha_fin == "")
                    {
                        Session["cadena_busqueda"] = "Grafico_totales_pedidos_barra";

                    }
                    if (Mes != "0" && Fecha_inicio == "" && Fecha_fin == "")
                    {
                        Session["cadena_busqueda"] = "Grafico_totales_pedidos_barra_seller_filtro";
                    }

                    if (Mes == "0" && Fecha_inicio != "" && Fecha_fin != "")
                    {
                        Session["cadena_busqueda"] = "Grafico_totales_pedidos_barra_filtro_Rango";
                    }
            }

            if (Session["Id_rol"].ToString() == "1") //Todos
            {
                if (Mes == "0" && Fecha_inicio == "" && Fecha_fin == "")
                {
                    Session["cadena_busqueda"] = "Grafico_totales_pedidos_barra_todos";

                }
                if (Mes != "0" && Fecha_inicio == "" && Fecha_fin == "")
                {
                    Session["cadena_busqueda"] = "Grafico_totales_pedidos_barra_todos_filtro";
                }

                if (Mes == "0" && Fecha_inicio != "" && Fecha_fin != "")
                {
                    Session["cadena_busqueda"] = "Grafico_totales_pedidos_barra_filtro_Rango_todos";
                }
            }




            string consulta = Session["cadena_busqueda"].ToString();
            string usuario = Convert.ToString(Session["usuario"]);
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
            SqlCommand cmd = new SqlCommand(consulta, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Seller", usuario);
            cmd.Parameters.AddWithValue("@Mes", Mes);
            cmd.Parameters.AddWithValue("@Fecha_inicio", Fecha_inicio);
            cmd.Parameters.AddWithValue("@Fecha_fin", Fecha_fin);



            con.Open();
            DataTable Datos = new DataTable();
            Datos.Load(cmd.ExecuteReader());
            con.Close();
            string strDatos;
            strDatos = "[['Fecha','Sin Preparar','Ingresados','Armados','En Ruta','Entregados','No Entregados'],";
            foreach (DataRow dr in Datos.Rows)
            {
                strDatos = strDatos + "[";
                // strDatos = strDatos + "'" + dr[0] + "'" + "," + dr[1];
                //   strDatos = strDatos + "'" + dr[0] + "'" + "," + dr[1] +  "," + dr[2] +  "," + dr[3] +  "," + dr[4] +  "," + dr[5];
                strDatos = strDatos + "'" + dr[0] + "'" + "," + dr[1] + "," + dr[2] + "," + dr[3] + "," + dr[4] + "," + dr[5] + "," + dr[6];
                strDatos = strDatos + "]";
            }
            strDatos = strDatos + "]";



            return strDatos;//strDatos;

        }

        public void contadores_cabeceras_seller(string Mes)
        {
            try
            {

                Session["Mes"] = Mes;
                string Fecha_inicio=Session["fecha_inicio"].ToString();
                string Fecha_fin = Session["fecha_fin"].ToString();


                if (Session["Id_rol"].ToString() == "2") { 

                    if (Mes == "0" && Fecha_inicio=="" && Fecha_fin=="")
                {
                     Session["cadena_busqueda"] = "sp_dashboard_contadores_seller";

                }
                if (Mes != "0" && Fecha_inicio == "" && Fecha_fin == "")
                {
                    Session["cadena_busqueda"] = "sp_dashboard_contadores_seller_filtro";
                }

                if (Mes == "0" && Fecha_inicio != "" && Fecha_fin != "")
                {
                    Session["cadena_busqueda"] = "sp_dashboard_contadores_seller_filtro_Rango";
                }
                
                }
                if (Session["Id_rol"].ToString() == "1") // adm
                {

                    if (Mes == "0" && Fecha_inicio == "" && Fecha_fin == "")
                    {
                        Session["cadena_busqueda"] = "sp_dashboard_contadores_todos";

                    }
                    if (Mes != "0" && Fecha_inicio == "" && Fecha_fin == "")
                    {
                        Session["cadena_busqueda"] = "sp_dashboard_contadores_todos_filtro";
                    }

                    if (Mes == "0" && Fecha_inicio != "" && Fecha_fin != "")
                    {
                        Session["cadena_busqueda"] = "sp_dashboard_contadores_todos_filtro_Rango";
                    }

                }






                string consulta = Session["cadena_busqueda"].ToString();
                string usuario = Convert.ToString(Session["usuario"]);
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
                SqlCommand cmd = new SqlCommand(consulta, con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@usuario", usuario);
                cmd.Parameters.AddWithValue("@Mes", Mes);
                cmd.Parameters.AddWithValue("@Fecha_inicio", Fecha_inicio);
                cmd.Parameters.AddWithValue("@Fecha_fin", Fecha_fin);


                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {



                 
                    int contador_pendientes= Convert.ToInt32(dr["contador_pendientes"]);
                    Session["cantidad_pendientes"] = contador_pendientes.ToString();




                    int contador_ingresados = Convert.ToInt32(dr["contador_ingresados"]);
                    Session["cantidad_ingresado"] = contador_ingresados.ToString();



                    int contador_ingresados_acumulados = Convert.ToInt32(dr["contador_ingresados_acumulados"]);
                      Session["cantidad_ingresado_acumulados"] = contador_ingresados_acumulados.ToString();









                    int Cerrados = Convert.ToInt32(dr["Cerrados"]);
                     Session["cantidad_entregados"] = Cerrados.ToString();


                    int Cerrados_acumulados = Convert.ToInt32(dr["Cerrados_acumulados"]);
                     Session["cantidad_entregados_acumulados"] = Cerrados_acumulados.ToString();






                    int totales = Convert.ToInt32(dr["Totales"]);
                    Session["cantidad_ruta"] = totales.ToString();



                    int armados1 = Convert.ToInt32(dr["Armados"]);
                     Session["cantidad_Armados"] = armados1.ToString();

                    //Bodega
                    int sinstock = Convert.ToInt32(dr["Sin_Stock"]);
                    //sinStock_seller.Text = Convert.ToString(sinstock);

                    //int rechazados = Convert.ToInt32(dr["Rechazados"]);

                    int rechazados2 = Convert.ToInt32(dr["Rechazados"]);
                       Session["cantidad_rechazados"] = rechazados2.ToString();


                    int rechazadosacumulados = Convert.ToInt32(dr["Rechazados_acumulados"]);
                    Session["cantidad_rechazados_acumulados"] = rechazadosacumulados.ToString();



                }

                //aqui va el open de conexion en caso que mande error

                con.Close();
            }
            catch
            {

                throw; 
            }



        }


        public void limpiar_variables() {
            
             fecha1.Text="";
             fecha2.Text="";
        }


        protected  string obtenerDatos()
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



       

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            string usuariovalida = Convert.ToString(Session["usuario"]);
            if (usuariovalida == "")
            {
                Response.Redirect("finsesion.aspx");
            }

            Session["consulta_seller"]="1";
            Response.Redirect("Pedidos_seller.aspx");
            
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            string usuariovalida = Convert.ToString(Session["usuario"]);
            if (usuariovalida == "")
            {
                Response.Redirect("finsesion.aspx");
            }


            Session["consulta_seller"] = "2";
            Response.Redirect("Pedidos_seller.aspx");

        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            string usuariovalida = Convert.ToString(Session["usuario"]);
            if (usuariovalida == "")
            {
                Response.Redirect("finsesion.aspx");
            }


            Session["consulta_seller"] = "3";
            Response.Redirect("Pedidos_seller.aspx");

        }

        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            string usuariovalida = Convert.ToString(Session["usuario"]);
            if (usuariovalida == "")
            {
                Response.Redirect("finsesion.aspx");
            }

            Session["consulta_seller"] = "4";   
            Response.Redirect("Pedidos_seller.aspx");

        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            string usuariovalida = Convert.ToString(Session["usuario"]);
            if (usuariovalida == "")
            {
                Response.Redirect("finsesion.aspx");
            }

            Session["consulta_seller"] = "5";
            Response.Redirect("Pedidos_seller.aspx");

        }

        protected void LinkButton6_Click(object sender, EventArgs e)
        {
            string usuariovalida = Convert.ToString(Session["usuario"]);
            if (usuariovalida == "")
            {
                Response.Redirect("finsesion.aspx");
            }


            Session["consulta_seller"] = "6";
            Response.Redirect("Pedidos_seller.aspx");

        }


        protected void Seleccionar_Seller_Click(object sender, EventArgs e)
        {
            try
            {
                string Mes = Pedido.SelectedValue;
                Session["fecha_inicio"] = "";
                Session["fecha_fin"] = "";

                contadores_cabeceras_seller(Mes);
                obtenerDatos_barra();




                //    Session["cantidad_rechazados"] = rechazados2.ToString();


            }
            catch
            {


                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "Swal.fire({icon: 'error',title: 'Ops..',  text: 'No se pudo Ingresar a BD!'})", true);

            }

        }

        protected void Registrar_Click(object sender, EventArgs e)
        {

            string usuariovalida = Convert.ToString(Session["usuario"]);
            if (usuariovalida == "")
            {
                Response.Redirect("finsesion.aspx");
            }



            string fecha_inicio = fecha1.Text;
            string fecha_fin = fecha2.Text;
            Session["fecha_inicio"] = fecha1.Text;
            Session["fecha_fin"] = fecha2.Text;
            string Mes = "0";
            contadores_cabeceras_seller(Mes);
            obtenerDatos_barra();




        }








        protected void Button3_Click(object sender, EventArgs e)
        {

            string usuariovalida = Convert.ToString(Session["usuario"]);
            if (usuariovalida == "")
            {
                Response.Redirect("finsesion.aspx");
            }


            limpiar_variables();
            PorMes.Visible = true;
            PorRango.Visible = false;
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            string usuariovalida = Convert.ToString(Session["usuario"]);
            if (usuariovalida == "")
            {
                Response.Redirect("finsesion.aspx");
            }

            limpiar_variables();
            PorMes.Visible = false;
            PorRango.Visible = true;
        }

        protected void Bodega_Resumen_Click(object sender, EventArgs e)
        {
            string usuariovalida = Convert.ToString(Session["usuario"]);
            if (usuariovalida == "")
            {
                Response.Redirect("finsesion.aspx");
            }


            Response.Redirect("Bodega_resumen_todos.aspx");
        }

        protected void Ventas_Resumen_Click(object sender, EventArgs e)
        {

        }

        protected void LinkButton7_Click(object sender, EventArgs e)
        {
            string usuariovalida = Convert.ToString(Session["usuario"]);
            if (usuariovalida == "")
            {
                Response.Redirect("finsesion.aspx");
            }

            Session["consulta_seller"] = "10";
            Response.Redirect("Pedidos_seller.aspx");


        }

        protected void LinkButton8_Click(object sender, EventArgs e)
        {

        }

        protected void LinkButton9_Click(object sender, EventArgs e)
        {

        }
    }

}