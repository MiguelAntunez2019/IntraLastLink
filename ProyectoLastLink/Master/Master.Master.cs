using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoLastLink.Master
{
    public partial class Master : System.Web.UI.MasterPage
    {
        int id_rol = 0;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AppendHeader("Cache-Control", "no-store");


            if (!IsPostBack && Session["usuario"] != null)
            {
                id_rol = Convert.ToInt32(Session["id_rol"].ToString());
               
                Permisos(id_rol);
            }


            if (Session["usuario"] != null)
            {
                divuser.Visible = true;
                divbuttons.Visible = false;
              //  Pedido.Visible = false;

                 lblusuario.Text = Session["usuario"].ToString();
            }
            else
            {
                divuser.Visible = false;
                divbuttons.Visible = true;
                lblusuario.Text = string.Empty;
            }
        
        
        }
        void Permisos(int id_rol)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_permisos", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_rol", SqlDbType.Int).Value = id_rol;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                bool Read, Update, Delete, Configurac, Read1;
                while (reader.Read())
                {
                       switch (reader[0].ToString())
                        {
                            case "Pedido":
                                Read = Convert.ToBoolean(reader[1].ToString());
                               if (Read)
                                {
                                Pedido.Visible = true;
                                }
                                else
                                {
                                Pedido.Visible = false;
                                }
                             break;
                        case "Armado":
                            Read1 = Convert.ToBoolean(reader[1].ToString());
                            if (Read1)
                            {
                                Armado.Visible = true;
                            }
                            else
                            {
                                Armado.Visible = false;
                            }
                         break;



                        case "Despacho":
                                Update = Convert.ToBoolean(reader[1].ToString());
                                if (Update)
                                Despacho.Visible = true;
                                else
                                Despacho.Visible = false;
                                break;
                            case "Bodega":
                                Delete = Convert.ToBoolean(reader[1].ToString());
                                if (Delete)
                                Bodega.Visible = true;
                                else
                                Bodega.Visible = false;
                                break;
                             case "Configura":
                                Configurac = Convert.ToBoolean(reader[1].ToString());
                            if (Configurac)
                                Configura.Visible= true;
                            else
                                Configura.Visible = false;
                            break;

                             case "Seller":
                            Configurac = Convert.ToBoolean(reader[1].ToString());
                            if (Configurac)
                                Seller.Visible = true;
                            else
                                Seller.Visible = false;
                            break;

                    }
                    
                }


                con.Close();
                reader.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }
        protected void Unnamed_Click(object sender, EventArgs e)
        {
            Response.Redirect("Registrarse.aspx");
        }
        protected void Unnamed_Click1(object sender, EventArgs e)
        {
            Response.Redirect("index.aspx");
        }
        protected void Unnamed_Click2(object sender, EventArgs e)
        {
            Session["usuario"] = null;
            Session["Id_rol"] = null;
            Response.Redirect("index.aspx");
            HttpContext.Current.Session.Abandon();
        }

        protected void Unnamed_Click3(object sender, EventArgs e)
        {
            Response.Redirect("inicio.aspx");
        }
    }
}