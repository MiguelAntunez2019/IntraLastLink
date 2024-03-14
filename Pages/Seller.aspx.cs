using ProyectoLastLink.clases;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoLastLink.Pages
{
    public partial class Seller : System.Web.UI.Page
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
        //string Patron = "InfoToolsSV";

        void Limpiar()
        {
            rut_seller.Text = "";
            nombre_seller.Text = "";
            direccion_seller.Text = "";
            fono_seller.Text = "";
            key_usermame_seller.Text = ""; ;
            key_password_seller.Text = "";
      
        }

        protected void Gvbind()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
            SqlCommand cmd = new SqlCommand("sp_datos_seller", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows == true)
            {

                GridView1.DataSource = dr;
                GridView1.DataBind();

            }


        }
        /// <summary>
        /// 
        /// </summary>
        private void fnllenaHorasFin()
        {
            if (IsPostBack == false)
            {
                try
                {
                    ds = com.getHoraFinReserva2();
                    comuna_seller.DataSource = ds;
                    comuna_seller.DataValueField = "Id_comuna";
                    comuna_seller.DataTextField = "Nombre_Comuna";
                    comuna_seller.DataSource = ds;
                    comuna_seller.DataBind();
                    comuna_seller.Items.Insert(0, new ListItem("-- Seleccione Comuna --", "0"));
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

                SqlCommand cmd = new SqlCommand("sp_registrar_Seller", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                 cmd.Parameters.Add("@rut_seller", System.Data.SqlDbType.VarChar).Value = rut_seller.Text;
                 cmd.Parameters.Add("@nombre_seller", System.Data.SqlDbType.VarChar).Value = nombre_seller.Text;
                cmd.Parameters.Add("@direccion_seller", System.Data.SqlDbType.VarChar).Value = direccion_seller.Text;
                 cmd.Parameters.Add("@comuna_seller", System.Data.SqlDbType.Int).Value = comuna_seller.SelectedValue;
                cmd.Parameters.Add("@fono_seller", System.Data.SqlDbType.VarChar).Value = fono_seller.Text;
                cmd.Parameters.Add("@key_usermame_seller", System.Data.SqlDbType.VarChar).Value = key_usermame_seller.Text;
                cmd.Parameters.Add("@key_password_seller", System.Data.SqlDbType.VarChar).Value = key_password_seller.Text;

                con.Open();
                cmd.ExecuteNonQuery();

                con.Close();

               // Response.Write("<script>alert('Ingreso de Seller Realizado')</script>");
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "Swal.fire({icon: 'success',title: 'Ok..',  text: 'Ingreso de Seller Realizado!'})", true);

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
            string rut = ((TextBox)GridView1.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
            String name = ((TextBox)GridView1.Rows[e.RowIndex].Cells[2].Controls[0]).Text;
            string fono = ((TextBox)GridView1.Rows[e.RowIndex].Cells[3].Controls[0]).Text;
            string key_user = ((TextBox)GridView1.Rows[e.RowIndex].Cells[4].Controls[0]).Text; 
            string key_pass = ((TextBox)GridView1.Rows[e.RowIndex].Cells[5].Controls[0]).Text;




            using (SqlConnection con = new SqlConnection(cs))

            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update Seller set nombre_seller='" + name + "',  rut_seller='"+ rut + "' ,fono_seller='" + fono + "',key_usermame_seller='" + key_user + "',key_password_seller='" + key_pass + "' where id_seller='" + id + "' ", con);
                int t = cmd.ExecuteNonQuery();
                if (t > 0)
                {
                  //  Response.Write("<script>alert('Informacion Actualizada')</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "Swal.fire({icon: 'success',title: 'Ok..',  text: 'Informacion Actualizada!'})", true);

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
                SqlCommand cmd = new SqlCommand("delete from Seller  where id_seller='" + ide + "' ", con);
                int t = cmd.ExecuteNonQuery();
                if (t > 0)
                {

                    //Response.Write("<script>alert('Informacion Eliminada')</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "Swal.fire({icon: 'info',title: 'Ok..',  text: 'Informacion Eliminada!'})", true);

                    GridView1.EditIndex = -1;
                    Gvbind();
                }




            }

        }

        public void BusquedaAlumnoPorNombre()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
            SqlCommand cmd = new SqlCommand("sp_datos_seller_nombre", con);
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
