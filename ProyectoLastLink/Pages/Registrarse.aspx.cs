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

namespace ProyectoLastLink.Pages
{
    public partial class Registrarse : System.Web.UI.Page
    {
        combos com = new combos();
        DataSet ds = new DataSet();
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AppendHeader("Cache-Control", "no-store");
            if (!IsPostBack)
            {
                Gvbind();
                
            }
            
            fnllenaHorasFin();

        }
        string Patron = "InfoToolsSV";

        void Limpiar()
        {
            nombre2.Text = "";
            usuario.Text = "";
            clave.Text = "";
            clave2.Text = "";
            email.Text = ""; ;
            rut.Text = "";

        }

        protected void Gvbind()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
            SqlCommand cmd = new SqlCommand("sp_datos", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows == true)
            {

                GridView1.DataSource = dr;
                GridView1.DataBind();

            }


        }
        private void fnllenaHorasFin()
        {
            if (IsPostBack == false)
            {
                try
                {
                    ds = com.getHoraFinReserva();
                    perfil.DataSource = ds;
                    perfil.DataValueField = "Id_Rol";
                    perfil.DataTextField = "Nombre_Rol";
                    perfil.DataSource = ds;
                    perfil.DataBind();
                    perfil.Items.Insert(0, new ListItem("-- Seleccione Rol --", "0"));
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        protected void Registrar_Click(object sender, EventArgs e)
        {

            try
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());

                SqlCommand cmd = new SqlCommand("sp_registrar", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Nombre", System.Data.SqlDbType.VarChar).Value = nombre2.Text;
                cmd.Parameters.Add("@Email", System.Data.SqlDbType.VarChar).Value = email.Text;
                cmd.Parameters.Add("@Usuario", System.Data.SqlDbType.VarChar).Value = usuario.Text;
                cmd.Parameters.Add("@Clave", System.Data.SqlDbType.VarChar).Value = clave.Text;
                cmd.Parameters.Add("@perfil", System.Data.SqlDbType.Int).Value = perfil.SelectedValue;
                cmd.Parameters.Add("@Patron", System.Data.SqlDbType.VarChar).Value = Patron;

                con.Open();
                cmd.ExecuteNonQuery();
                
                con.Close();

              //  Response.Write("<script>alert('Ingreso Realizado')</script>");
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "Swal.fire({icon: 'success',title: 'Ok..',  text: 'Ingreso Realizado!'})", true);

                Gvbind();
                Limpiar();

                //Response.Redirect("Login.aspx");
            }
            catch (Exception)
            {

                throw;
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

            int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
            String name = ((TextBox)GridView1.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
            string usuario = ((TextBox)GridView1.Rows[e.RowIndex].Cells[2].Controls[0]).Text;
             string email = ((TextBox)GridView1.Rows[e.RowIndex].Cells[3].Controls[0]).Text;
            

            using (SqlConnection con = new SqlConnection(cs))

            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update Usuarios set Nombre='" + name + "', Usuario='" + usuario + "',  Email='" + email + "' where Id_usuario='" + id + "' ", con);
                int t = cmd.ExecuteNonQuery();
                if (t > 0)
                {
                   // Response.Write("<script>alert('Informacion Actualizada')</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "Swal.fire({icon: 'success',title: 'Ok..',  text: 'Información Actualizada!'})", true);

                    GridView1.EditIndex = -1;
                    Gvbind();
                    Limpiar();
                }
            }
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            Gvbind();
        }

       

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["conexion"].ToString();

            int ide = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("delete from Usuarios  where Id_usuario='" + ide + "' ", con);
                int t = cmd.ExecuteNonQuery();
                if (t > 0)
                {

                    //Response.Write("<script>alert('Informacion Eliminada')</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "Swal.fire({icon: 'info',title: 'Ok..',  text: 'Información Eliminada!'})", true);

                    GridView1.EditIndex = -1;
                    Gvbind();
                }

            }

        }

        public void BusquedaAlumnoPorNombre()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
            SqlCommand cmd = new SqlCommand("sp_datos_buscarNombre", con);
            cmd.Parameters.Add("@nombre2", SqlDbType.VarChar).Value = BuscarNombre.Text.Trim();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows == true)
            {

                GridView1.DataSource = dr;
                GridView1.DataBind();

            }

        }


        protected void Buscar_Click(object sender, EventArgs e)
        {
            BusquedaAlumnoPorNombre();
        }
    }
}
