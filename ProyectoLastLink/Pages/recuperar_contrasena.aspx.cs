using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using System.IO;
using static System.Net.WebRequestMethods;

namespace ProyectoLastLink.Pages
{
    public partial class recuperar_contrasena : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void recuperar_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ToString());
                SqlCommand cmd = new SqlCommand("sp_datos_extraer_coontrasena", con);
                cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = email.Text;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                string Nombre;
                string contrasena;
                string usuario;


                if (dr.Read())
                {
                   contrasena = dr["clave"].ToString();
                    Nombre = dr["Nombre"].ToString();
                    usuario = dr["usuario"].ToString();
                     enviarcorreo_recuperacion(email.Text, contrasena, Nombre, usuario);
                  //  Response.Write("<script>alert('Correo Enviado a su Casilla')</script>");
               
                }
                else
                {
                   // Response.Write("<script>alert('Correo Electrónico No Registrado')</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "Swal.fire({icon: 'error',title: 'Error..',  text: 'Correo Electrónico No Registrado!'})", true);

                }
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public void enviarcorreo_recuperacion(string correo,string contrasena, string nombre, string usuario)
        {
           
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Recuperación Contraseña", "servicioalcliente@lastlink.cl"));
            email.To.Add(new MailboxAddress("Usuario", correo));
            email.Subject = "No Responder: Recuperación de Contraseña(Lastlink)";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
              Text = "<p style='font-family: tahoma; font-size: 12px;'><strong>Estimado/a:" + nombre + ",</strong><br>A continuación podrá visualizar sus datos de ingreso al sistema:</p><br><p style='font-family: tahoma; font-size: 12px;'><strong>Nombre Usuario: </strong><strong><h1>" + usuario + "</h1></strong><br><strong>Contraseña: </strong><strong><h1>" + contrasena + "</h1></strong></p>" + "<br><p style='font-family: tahoma; font-size: 12px;'><br><img src='https://www.lastlink.cl/wp-content/uploads/2023/07/LOGO1.png' style='width:40px; height:25px;'/> LastLink.cl - Todos los derechos reservados.</p>"
            };
            using (var smtp = new SmtpClient())
            {
                smtp.Connect("lastlink.cl", 465, true);
                smtp.Authenticate("cla93309", "wQERgzdBpdhHvJKEVhRa");
                smtp.Send(email);
                smtp.Disconnect(true);

            }
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "Swal.fire({icon: 'success',title: 'Ok..',  text: 'Correo enviado a su Casilla!'})", true);


        }

        protected void volver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index.aspx");
        }
    }
}