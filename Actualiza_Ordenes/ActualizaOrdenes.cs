using Ordenes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Threading.Tasks;

namespace ProyectoLastLink.Actualiza_Ordenes
{
    public class ActualizaOrdenes
    {
        public async Task Actualiza()
        {
            Console.WriteLine("Ingresa el numero de la orden");
            string idorden = Console.ReadLine();

            var infoActualizacion = ObtieneInfoActualizacion(idorden);
            if (infoActualizacion.origen == "Woocomerce")
            {
                string user = infoActualizacion.keyUsernameSeller;
                string password = infoActualizacion.keyPasswordSeller;
                string url = infoActualizacion.url;
                string ordenid = infoActualizacion.idOrden;
                await ActualizaInfoWoocomerce(user, password, url, ordenid);
            }
            if (infoActualizacion.origen == "JumpSeller")
            {
                string user = infoActualizacion.keyUsernameSeller;
                string password = infoActualizacion.keyPasswordSeller;
                string url = infoActualizacion.url;
                string ordenid = infoActualizacion.idOrden;
                await ActualizaInfoJumpSeller(user, password, url, ordenid);
            }
            if (infoActualizacion.origen == "Shopify")
            {
                string url = infoActualizacion.url;
                string token = infoActualizacion.token;
                string ordenid = infoActualizacion.idOrden;
                await ActualizaInfoShopify(url, token, ordenid);
            }

        }



        public static ConfiguracionActualiza ObtieneInfoActualizacion(string idorden)
        {
            ConfiguracionActualiza cf = new ConfiguracionActualiza();
            try
            {
                var con = ConfigurationManager.ConnectionStrings["conexion"].ToString();
                using (SqlConnection conexion = new SqlConnection(con))
                {
                    conexion.Open();
                    string nombreProcedimiento = "Borra_Ordenes_Seller";
                    using (SqlCommand comando = new SqlCommand(nombreProcedimiento, conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.Add(new SqlParameter("@idorden", SqlDbType.VarChar) { Value = idorden });
                        using (SqlDataReader lector = comando.ExecuteReader())
                        {
                            while (lector.Read())
                            {

                                cf.idOrden = lector["idorden"].ToString();
                                cf.nombreSeller = lector["nombre_seller"].ToString();
                                cf.origen = lector["origen"].ToString();
                                cf.token = lector["Token"].ToString();
                                cf.keyPasswordSeller = lector["key_password_seller"].ToString();
                                cf.keyUsernameSeller = lector["key_usermame_seller"].ToString();
                                cf.url = lector["url"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error: {ex.Message}");
            }
            return cf;
        }

        public static async Task ActualizaInfoWoocomerce(string user, string pass, string url, string orden)
        {
            string apiUrl = $"{url}/wp-json/wc/v3/orders/{orden}";
            string username = user;
            string password = pass;
            string newStatus = "completed";
            using (HttpClient client = new HttpClient())
            {

                string base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);
                string jsonContent = $"{{\"status\": \"{newStatus}\"}}";
                StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                try
                {
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);
                    if (response.IsSuccessStatusCode == true)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                    }
                }
                catch (Exception ex)
                {

                }


            }
        }

        public static async Task ActualizaInfoJumpSeller(string user, string pass, string url, string orden)
        {
            string apiUrl = $"{url}/v1/orders/{orden}.json";
            string username = user;
            string password = pass;
            string newStatus = "Paid";
            string shipmentStatus = "unfulfilled";

            using (HttpClient client = new HttpClient())
            {
                string base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);


                string jsonContent = $@"
        {{
            ""order"": {{
                ""status"": ""{newStatus}"",
                ""shipment_status"": ""{shipmentStatus}""
  
            }}
        }}";

                StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                try
                {
                    HttpResponseMessage response = await client.PutAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"Success: {responseContent}");
                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        public static async Task ActualizaInfoShopify(string url, string token, string orden)
        {
            string apiKey = url;
            string tokensecreto = token;
            string apiUrl = $"{url}/admin/api/2021-07/orders/{orden}.json";
            string newStatus = "paid";
            using (HttpClient client = new HttpClient())
            {
                string base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{apiKey}:{tokensecreto}"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);
                string jsonContent = $"{{\"order\": {{\"financial_status\": \"{newStatus}\"}}}}";
                StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(apiUrl, content);
                string responseContent = await response.Content.ReadAsStringAsync();
            }
        }

    }
}
