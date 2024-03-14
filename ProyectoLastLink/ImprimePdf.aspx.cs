using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QRCoder;
using System.Drawing;
using System.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Web.Script.Services;
using Microsoft.AspNetCore.Authorization;
using static ProyectoLastLink.Pages.MigracionVentas;
using System.Threading.Tasks;
using iTextSharp.text;
using Font = System.Drawing.Font;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using static ClosedXML.Excel.XLPredefinedFormat;
using System.Security.Policy;

namespace ProyectoLastLink
{
    public partial class ImprimePdf : System.Web.UI.Page
    {

        public class DatosImpresion
        {
            public string nombre { get; set; }
            public string direccion { get; set; }
            public string comuna { get; set; }
            public string telefono { get; set; }

            public string idproducto { get; set; }

            public string proveedor { get; set; }

            public string zona { get; set; }
            public string id_pedido { get; set; }

            public string cantidad { get; set; }

        }


        private string id_pedido;
        private string etiquetas;
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (IsPostBack)
            {
                Response.Redirect("~/Pages/Armado_pedido.cs");
            }else
            {
               
            }
            


        }

        [WebMethod(EnableSession = true)]
        [AllowAnonymous]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static async Task<DatosImpresion> MandaImprimir(string id_pedido)
        {

            DatosImpresion datos = new DatosImpresion();
            // string id_pedido = ((ImprimePdf)HttpContext.Current.Handler).id_pedido;
            string etiquetas = ((ImprimePdf)HttpContext.Current.Handler).etiquetas;

            string conexion = ConfigurationManager.ConnectionStrings["conexion"].ToString();
            using (SqlConnection c = new SqlConnection(conexion))
            {

                c.Open();


                SqlCommand com = new SqlCommand("sp_Crear_Etiqueta_final", c);
                com.CommandType = CommandType.StoredProcedure;


                com.Parameters.AddWithValue("@id_orden", id_pedido);


                SqlDataReader dr = com.ExecuteReader();

                if (dr.Read())
                {
                    datos.nombre = dr["nombres"].ToString();
                    datos.direccion = dr["direccion"].ToString();
                    datos.comuna = dr["comuna"].ToString();
                    datos.telefono = dr["telefono"].ToString();
                    //nota = dr["nota_pedido"].ToString();
                    datos.idproducto = dr["id_producto"].ToString();
                    // fecha_Venta = dr["fecha_venta"].ToString();
                    datos.proveedor = dr["proveedor"].ToString();
                    datos.zona = dr["zona"].ToString();
                }

                dr.Close();
                c.Close();
            }
            return new DatosImpresion { nombre = datos.nombre, direccion = datos.direccion, comuna = datos.comuna, telefono = datos.telefono, idproducto = datos.idproducto, proveedor = datos.proveedor, zona = datos.zona };
   
        }
        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            // Lógica de servidor para manejar el evento del botón aquí

            // Puedes agregar más lógica aquí si es necesario...

            // Redirigir a la página deseada
            Response.Redirect("~/Pages/Armado_pedido.aspx");
        }
    }
}