using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoLastLink.Pages
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AppendHeader("Cache-Control", "no-store");
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
        string Patron = "InfoToolsSV";

        protected void ingresar_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_login", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Usuario", System.Data.SqlDbType.VarChar).Value = usuario.Text;
                cmd.Parameters.Add("@Clave", System.Data.SqlDbType.VarChar).Value = clave.Text;
                cmd.Parameters.Add("@Patron", System.Data.SqlDbType.VarChar).Value = Patron;
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();

                if (rd.Read())
                {
                    Session["Id_rol"] = rd[4].ToString();
                    Session["usuario"] = rd[1].ToString();
                    Response.Redirect("~/Pages/Inicio.aspx");

                    con.Close();
                }
                else
                {
                    Response.Write("<script>alert('Error En El Usuario, No existe o Contraseña Incorrecta ')</script>");
                    //Response.Redirect("Index.aspx");
                    con.Close();
                }
                


            }
            catch (Exception)
            {

                throw;
            }
        }
        protected void recuperar_Click(object sender, EventArgs e)
        {
            Response.Redirect("recuperar_contrasena.aspx");
        }
    }
}