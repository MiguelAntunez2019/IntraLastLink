using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;
using Ordenes;
using System.Security.Cryptography.X509Certificates;
using static Ordenes.Shopify;
using static Ordenes.Woocomerce;
using RestSharp;
using System.Configuration;
using System.Numerics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime;
using System.Security.Policy;
using System.Globalization;
//using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using System.Transactions;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Linq;
using System.Linq.Expressions;
using System.Diagnostics.Eventing.Reader;
using System.Net;
using ProyectoLastLink.Ordenes;

namespace Ordenes
{
    public class Program
    {



        public async Task<string> EjecutaPrograma()
        {
            Console.WriteLine("Inicia programa");

            BorrarOrdenes();
            BorrarProductos();

            try
            {
                var credencialesShopify = ObtieneCredencialesShopify();
                if (credencialesShopify != null)
                {
                    foreach (var item in credencialesShopify)
                    {
                        var configuracion = new Configuracion { token = item.token, url = item.url };
                        var configuraciones = new List<Configuracion> { configuracion };
                        await OrdenesShopify(configuraciones);
                    }
                }

                var credencialesShopifyMayorista = ObtieneCredencialesShopifyMayorista();
                if (credencialesShopifyMayorista != null)
                {
                    foreach (var item in credencialesShopifyMayorista)
                    {
                        var configuracionMayorista = new ConfiguracionMayorista { token = item.token, url = item.url };
                        var configuraciones = new List<ConfiguracionMayorista> { configuracionMayorista };
                        await OrdenesShopifyMayorista(configuraciones);
                    }
                }

                var credencialesWoocommerce = ObtieneCredencialesWoocomerce();
                if (credencialesWoocommerce != null)
                {
                    foreach (var item in credencialesWoocommerce)
                    {
                        var configuracion = new Configuracion { pass = item.pass, login = item.login, url = item.url };
                        var configuraciones = new List<Configuracion> { configuracion };
                        await OrdenesWoocomerce(configuraciones);
                    }
                }

                var credencialesJump = ObtieneCredencialesJump();
                if (credencialesJump != null)
                {
                    foreach (var item in credencialesJump)
                    {
                        var configuracion = new Configuracion { pass = item.pass, login = item.login, url = item.url };
                        var configuraciones = new List<Configuracion> { configuracion };
                        await llenaJump(configuraciones);
                    }
                }

                var credencialesJumpBeyond = ObtieneCredencialesJumpBeyond();
                if (credencialesJumpBeyond != null)
                {
                    foreach (var item in credencialesJumpBeyond)
                    {
                        var configuracion = new Configuracion { pass = item.pass, login = item.login, url = item.url };
                        var configuraciones = new List<Configuracion> { configuracion };
                        await llenaJumpBeyond(configuraciones);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return "";
        }







        public static List<Configuracion> ObtieneCredencialesShopify()
        {

            var con = ConfigurationManager.ConnectionStrings["conexion"].ToString();

            using (SqlConnection conexion = new SqlConnection(con))
            {
                List<Configuracion> configuracion = new List<Configuracion>();
                conexion.Open();
                var comando = "SELECT Token,url FROM seller where integracion='Shopify' and mayorista=0 and estado_seller=1 ";
                SqlCommand com = new SqlCommand(comando, conexion);

                SqlDataReader dr = com.ExecuteReader();

                while (dr.Read())
                {
                    Configuracion cf = new Configuracion();
                    cf.token = dr["Token"].ToString();
                    cf.url = dr["url"].ToString();
                    configuracion.Add(cf);
                }

                return configuracion;
            }
        }

        public static List<Configuracion> ObtieneCredencialesShopifyMayorista()
        {

            var con = ConfigurationManager.ConnectionStrings["conexion"].ToString();

            using (SqlConnection conexion = new SqlConnection(con))
            {
                List<Configuracion> configuracion = new List<Configuracion>();
                conexion.Open();
                var comando = "SELECT Token,url FROM seller where integracion='Shopify' and mayorista=1  and estado_seller=1 ";
                SqlCommand com = new SqlCommand(comando, conexion);

                SqlDataReader dr = com.ExecuteReader();

                while (dr.Read())
                {
                    Configuracion cf = new Configuracion();
                    cf.token = dr["Token"].ToString();
                    cf.url = dr["url"].ToString();
                    configuracion.Add(cf);
                }

                return configuracion;
            }
        }



        public static List<Configuracion> ObtieneCredencialesWoocomerce()
        {

            var con = ConfigurationManager.ConnectionStrings["conexion"].ToString();

            using (SqlConnection conexion = new SqlConnection(con))
            {
                List<Configuracion> configuracion = new List<Configuracion>();
                conexion.Open();
                var comando = "SELECT [key_usermame_seller],[key_password_seller],[url] FROM seller where integracion='woocommerce'  and estado_seller=1 ";
                SqlCommand com = new SqlCommand(comando, conexion);

                SqlDataReader dr = com.ExecuteReader();

                while (dr.Read())
                {
                    Configuracion cf = new Configuracion();
                    cf.login = dr["key_usermame_seller"].ToString();
                    cf.pass = dr["key_password_seller"].ToString();
                    cf.url = dr["url"].ToString();
                    configuracion.Add(cf);
                }

                return configuracion;
            }
        }
        public static List<Configuracion> ObtieneCredencialesJumpSeller()
        {

            var con = ConfigurationManager.ConnectionStrings["conexion"].ToString();

            using (SqlConnection conexion = new SqlConnection(con))
            {
                List<Configuracion> configuracion = new List<Configuracion>();
                conexion.Open();
                var comando = "SELECT [key_usermame_seller],[key_password_seller],[url] FROM seller where integracion='JumpSeller'  and estado_seller=1 ";
                SqlCommand com = new SqlCommand(comando, conexion);

                SqlDataReader dr = com.ExecuteReader();

                while (dr.Read())
                {
                    Configuracion cf = new Configuracion();
                    cf.login = dr["key_usermame_seller"].ToString();
                    cf.pass = dr["key_password_seller"].ToString();
                    cf.url = dr["url"].ToString();

                    configuracion.Add(cf);
                }

                return configuracion;
            }
        }


        public static List<Configuracion> ObtieneCredencialesJump()
        {
          
            var con = ConfigurationManager.ConnectionStrings["conexion"].ToString();

            using (SqlConnection conexion = new SqlConnection(con))
            {
                List<Configuracion> configuracion = new List<Configuracion>();
                conexion.Open();
                var comando = "SELECT [key_usermame_seller],[key_password_seller],url,nombre_seller FROM seller where integracion='JumpSeller'  and estado_seller='1' and codigo_seller_plataforma!='beyondtheorigin' ";
                SqlCommand com = new SqlCommand(comando, conexion);

                SqlDataReader dr = com.ExecuteReader();

                while (dr.Read())
                {
                    Configuracion cf = new Configuracion();
                    cf.login = dr["key_usermame_seller"].ToString();
                    cf.pass = dr["key_password_seller"].ToString();
                    cf.url = dr["url"].ToString();
                    configuracion.Add(cf);
                }

                return configuracion;
            }
        }


        public static List<Configuracion> ObtieneCredencialesJumpBeyond()
        {

            var con = ConfigurationManager.ConnectionStrings["conexion"].ToString();

            using (SqlConnection conexion = new SqlConnection(con))
            {
                List<Configuracion> configuracion = new List<Configuracion>();
                conexion.Open();
                var comando = "SELECT [key_usermame_seller],[key_password_seller],url,nombre_seller,codigo_seller_plataforma FROM seller where integracion='JumpSeller' and codigo_seller_plataforma='beyondtheorigin' and estado_seller='1' ";
                SqlCommand com = new SqlCommand(comando, conexion);

                SqlDataReader dr = com.ExecuteReader();

                while (dr.Read())
                {
                    Configuracion cf = new Configuracion();
                    cf.login = dr["key_usermame_seller"].ToString();
                    cf.pass = dr["key_password_seller"].ToString();
                    cf.url = dr["url"].ToString();
                    cf.seller = dr["codigo_seller_plataforma"].ToString();
                    configuracion.Add(cf);
                }

                return configuracion;
            }
        }

        public static async Task OrdenesWoocomerce(List<Configuracion> token)
        {
            Datos_Ordenes dto = new Datos_Ordenes();
            Datos_Productos dtp = new Datos_Productos();
            foreach (var configuracion in token)
            {
                string url = configuracion.url;
                string baseUrl = $"{url}/wp-json/wc/v3/orders";
                string username = configuracion.login;
                string password = configuracion.pass;
                List<decimal> precio = new List<decimal>();
                List<string> imagen = new List<string>();
                using (HttpClient client = new HttpClient())
                {
                    Woocomerce woo = new Woocomerce();

                    string authHeaderValue = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", authHeaderValue);


                    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                    ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

                    client.DefaultRequestHeaders.Add("User-Agent", "PostmanRuntime/7.28.4");

                    HttpResponseMessage response = client.GetAsync($"{baseUrl}").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();

                        List<Woocomerce.Order> orders = JsonConvert.DeserializeObject<List<Woocomerce.Order>>(content);
                        List<decimal> preciosuma = new List<decimal>();
                        foreach (Woocomerce.Order order in orders)
                        {
                            dto.ordenid = order.id.ToString() ?? "";
                            dto.estado = order.status ?? "";
                            dto.fechaventa = order.date_created ?? "";
                            dto.comuna = order.billing.city?.ToString() ?? "";
                            dto.nombre = order.billing.first_name?.ToString() ?? "";
                            dto.apellidos = order.billing.last_name?.ToString() ?? "";
                            dto.correo = order.billing.email?.ToString() ?? "";
                            dto.nota_orden = order.customer_note?.ToString() ?? "";
                            dto.telefono = order.billing.phone?.ToString() ?? "";
                            dto.courrier = "";
                            dto.envio = "";
                            dto.direccion = order.billing.address_1 ?? "";
                            dto.compania = order.billing.company?.ToString() ?? "";
                            dto.id_plat = order.proveedor + dto.ordenid;
                            dto.precio = order.discount_total;
                            dto.origen = "Woocomerce";
                            dto.idseller = dto.id_plat;
                            long ordenprevia = order.id;
                            List<Woocomerce.LineItem> lines = order.line_items;
                            dto.proveedor2 = ObtenerProveedor(lines);
                            dto.proveedor2 = ExtraerNombreProveedorWoocomerce(dto.proveedor2, "https://", ".cl");


                            if (dto.estado == "processing")
                            {

                                foreach (Woocomerce.LineItem lineItem in order.line_items)
                                {
                                    dtp.nombre_producto = lineItem.name?.ToString() ?? "";
                                    dtp.sku = lineItem.sku?.ToString() ?? "";
                                    dtp.cantidad_vendida = lineItem.quantity;
                                    dtp.url = lineItem.image?.src?.ToString() ?? "";
                                    dtp.id_producto_orden = dto.id_plat;
                                    dtp.pid = lineItem.id;
                                    imagen.Add(lineItem.image?.src?.ToString() ?? "");
                                    preciosuma.Add(Convert.ToDecimal(lineItem.subtotal));


                                    InsertaProductos(dto.estado, dtp.nombre_producto, dtp.sku, dtp.cantidad_vendida.ToString(), dtp.id_producto_orden, dtp.pid, dtp.url.ToString());
                                    dtp.Limpiar();
                                }
                                Inserta_Ordenes(Convert.ToInt64(dto.ordenid), dto.estado, dto.ordenid, dto.origen, dto.comuna, dto.ordenid, dto.fechaventa, dto.proveedor2, dto.nombre, dto.apellidos, dto.telefono, dto.correo, dto.nota_orden, dto.courrier, dto.envio, Convert.ToString(preciosuma.Sum()), dto.direccion, dto.compania,null,null);
                                dto.Limpiar();
                                preciosuma.Clear();

                            }
                        }

                    }

                }
            }
        }

        public static string ObtenerProveedor(List<Woocomerce.LineItem> line)
        {
            var src = "";
            foreach (var item in line)
            {
                src = item.image.src;

            }
            return src;
        }



        public static async Task OrdenesShopify(List<Configuracion> token)
        {
            Datos_Ordenes datos = new Datos_Ordenes();
            Datos_Productos datos_producto = new Datos_Productos();

            foreach (var tokens in token)
            {
                string shopifyStoreUrl = tokens.url;
                string apiVersion = "2023-10";
                string apiUrl = $"{shopifyStoreUrl}/admin/api/{apiVersion}/orders.json";
                List<string> imagen = new List<string>();

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("X-Shopify-Access-Token", tokens.token);

                    try
                    {
                        HttpResponseMessage response = client.GetAsync(apiUrl).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            string jsonContent = await response.Content.ReadAsStringAsync();
                            Root root = Newtonsoft.Json.JsonConvert.DeserializeObject<Root>(jsonContent);

                            foreach (var order in root.Orders)
                            {
                                if (order.name.ToString() == "#7867")
                                { 
                                
                                }
                                datos.ordenid = order.name ?? "";
                                datos.estado = order.financial_status?.ToString() ?? "";
                                datos.numero_orden = order.name ?? "";
                                datos.fechaventa = order.updated_at?.ToString() ?? "";
                                datos.precio = order.total_line_items_price?.ToString() ?? "";
                                datos.proveedor = ExtraerNombreProveedorShopify(order.order_status_url?.ToString(), "https://www.", ".cl") ?? ExtraerNombreProveedorShopify(order.order_status_url?.ToString(), "https://", ".cl") ?? "";
                                datos.origen = "Shopify";
                                datos.nombre = order.billing_address?.first_name ?? "";
                                datos.apellidos = order.billing_address?.last_name ?? "";
                                datos.comuna = order.billing_address?.city ?? "";
                                datos.direccion = order.billing_address?.address1 ?? "";
                                datos.compania = "";
                                datos.correo = order.email ?? "";
                                datos.telefono = order.billing_address?.phone ?? "";
                                datos.id_producto_orden = order.Id;
                                datos.nota_orden = order.note_attributes?.Select(z => z.name).FirstOrDefault() ?? "";
                                datos.envio = order.note_attributes?.Select(z => z.name).FirstOrDefault() ?? "";

                                foreach (var item in order.Line_Items)
                                {
                                    datos_producto.pid = item.product_id ?? 0;
                                    datos_producto.nombre_producto = item.name ?? "";
                                    datos_producto.cantidad_vendida = item.quantity;
                                    datos_producto.sku = item.sku ?? "";
                                    datos_producto.precio = item.price?.ToString() ?? "";
                                    datos_producto.url = "";

                                   
                                        InsertaProductos(datos.estado, datos_producto.nombre_producto, datos_producto.sku, datos_producto.cantidad_vendida.ToString(), datos.numero_orden, datos_producto.pid, datos_producto.url);
                                    datos_producto.Limpiar();
                             
                                }

                                string proveedorsh = datos.proveedor;

                                try
                                {
                                    Inserta_Ordenes(datos.id_producto_orden, datos.estado, datos.ordenid, datos.origen, datos.comuna, datos.ordenid, datos.fechaventa, proveedorsh, datos.nombre, datos.apellidos, datos.telefono, datos.correo, datos.nota_orden, datos.courrier, datos.envio, datos.precio, datos.direccion, datos.compania, null,null);
                                }
                                catch (Exception ex)
                                {
                                    ex.ToString();
                                }

                                datos_producto.Limpiar();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ex.ToString();
                    }
                }
            }
        }


        public static async Task OrdenesShopifyMayorista(List<ConfiguracionMayorista> token)
        {
            Datos_Ordenes datos = new Datos_Ordenes();
            Datos_Productos datos_producto = new Datos_Productos();

            foreach (var tokens in token)
            {
                string shopifyStoreUrl = tokens.url;
                string apiVersion = "2023-10";
                string apiUrl = $"{shopifyStoreUrl}/admin/api/{apiVersion}/orders.json?status=any";
                List<string> imagen = new List<string>();

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("X-Shopify-Access-Token", tokens.token);

                    try
                    {
                        HttpResponseMessage response = client.GetAsync(apiUrl).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            string jsonContent = await response.Content.ReadAsStringAsync();
                            Root root = Newtonsoft.Json.JsonConvert.DeserializeObject<Root>(jsonContent);

                            foreach (var order in root.Orders)
                            {
                                datos.ordenid = order.name ?? "";
                                datos.estado = order.financial_status?.ToString() ?? "";
                                datos.numero_orden = order.name ?? "";
                                datos.fechaventa = order.updated_at?.ToString() ?? "";
                                datos.precio = order.total_line_items_price?.ToString() ?? "";
                                datos.proveedor = ExtraerNombreProveedorShopify(order.order_status_url?.ToString(), "https://www.", ".cl") ?? ExtraerNombreProveedorShopify(order.order_status_url?.ToString(), "https://", ".myshopify.com") ?? "";
                                datos.origen = "Shopify";
                                datos.nombre = order.billing_address?.first_name ?? "";
                                datos.apellidos = order.billing_address?.last_name ?? "";
                                datos.comuna = order.billing_address?.city ?? "";
                                datos.direccion = order.billing_address?.address1 ?? "";
                                datos.compania = "";
                                datos.correo = order.email ?? "";
                                datos.telefono = order.billing_address?.phone ?? "";
                                datos.id_producto_orden = order.Id;
                                datos.nota_orden = order.note_attributes?.Select(z => z.name).FirstOrDefault() ?? "";
                                datos.envio = order.note_attributes?.Select(z => z.name).FirstOrDefault() ?? "";

                                foreach (var item in order.Line_Items)
                                {
                                    datos_producto.pid = item.product_id ?? 0;
                                    datos_producto.nombre_producto = item.name ?? "";
                                    datos_producto.cantidad_vendida = item.quantity;
                                    datos_producto.sku = item.sku ?? "";
                                    datos_producto.precio = item.price?.ToString() ?? "";
                                    datos_producto.url = "";
                                    InsertaProductos(datos.estado, datos_producto.nombre_producto, datos_producto.sku, datos_producto.cantidad_vendida.ToString(), datos.numero_orden, datos_producto.pid, datos_producto.url);
                                    datos_producto.Limpiar();
                                }
                                   

                                 string proveedorsh = datos.proveedor;

                                    try
                                    {
                                        Inserta_Ordenes(datos.id_producto_orden, datos.estado, datos.ordenid, datos.origen, datos.comuna, datos.ordenid, datos.fechaventa, proveedorsh, datos.nombre, datos.apellidos, datos.telefono, datos.correo, datos.nota_orden, datos.courrier, datos.envio, datos.precio, datos.direccion, datos.compania, null, null);
                                    }
                                    catch (Exception ex)
                                    {
                                        ex.ToString();
                                    }

                                    datos_producto.Limpiar();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ex.ToString();
                    }
                }
            }
        }
        /*valida ultimo*/
        private static async Task<Datos_Productos> ObtieneProductosShopify(long productoid, string url, string version, Datos_Productos dp, string token, Datos_Ordenes dt)
        {
            List<Datos_Productos> datos_ = new List<Datos_Productos>();
            string shopifyStoreUrl = url;
            string apiVersion = version;
            var datos_producto = new Datos_Productos { pid = dp.pid };

            string apiUrl = $"{shopifyStoreUrl}/admin/api/{apiVersion}/products/{datos_producto.pid}.json";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-Shopify-Access-Token", token);

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                string jsonResponse = await response.Content.ReadAsStringAsync();
                RootProductos root = JsonConvert.DeserializeObject<RootProductos>(jsonResponse);

                datos_producto.nombre_producto = root.product.title;
                datos_producto.estado = dt.estado;
                if (root.product.image != null)
                {
                    datos_producto.url = root.product.image.src;
                }
                else
                {
                    datos_producto.url = "";
                }



                foreach (var item in root.product.variants)
                {
                    if (item.product_id == null)
                    {
                        datos_producto.pid = 0;
                    }
                    else
                    {
                        datos_producto.pid = (long)item.product_id;
                    }

                    datos_producto.sku = item.sku;

                    if (datos_producto.sku != null)
                    {
                        datos_producto.sku = item.sku.ToString();
                    }
                    else
                    {
                        datos_producto.sku = "";
                    }
                }
            }

            return datos_producto;
        }




        public static async Task llenaJump(List<Configuracion> token)
        {
            Datos_Ordenes datos = new Datos_Ordenes();
            Datos_Productos dtp = new Datos_Productos();
            List<string> imagen = new List<string>();

            foreach (var info in token)
            {
                string loginjump = info.login;
                string authtokenjump = info.pass;
                string apiJump = info.url;

                using (HttpClient client = new HttpClient())
                {
                    string llamadojumpo = $"{apiJump}/v1/orders/status/paid.json?login={loginjump}&authtoken={authtokenjump}";
                    HttpResponseMessage response = client.GetAsync(llamadojumpo).Result;
                    List<decimal> preciosuma = new List<decimal>();

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonContent = await response.Content.ReadAsStringAsync();
                        List<JumpSeller.OrderWrapper> orderWrappers = JsonConvert.DeserializeObject<List<JumpSeller.OrderWrapper>>(jsonContent);

                        foreach (var item in orderWrappers)
                        {
                            datos.numero_orden = item.Order.id.ToString() ?? "0";

                            if (DateTime.TryParseExact(item.Order.created_at, "yyyy-MM-dd HH:mm:ss UTC", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDateTime))
                            {
                                DateTime localDateTime = parsedDateTime.ToLocalTime();
                                datos.fechaventa = localDateTime.ToString();
                            }
                            else
                            {
                                datos.fechaventa = "";
                            }

                            datos.nombre = item.Order.shipping_address?.name ?? "";
                            datos.apellidos = item.Order.shipping_address?.surname ?? "";
                            datos.direccion = item.Order.shipping_address?.address ?? "";
                            datos.envio = item.Order.shipping_method_name ?? "";
                            datos.correo = item.Order.customer?.email ?? "";
                            datos.numero_orden = item.Order.id.ToString() ?? "";
                            datos.courrier = "";
                            datos.origen = "JumpSeller";
                            if (item.Order != null && item.Order.source != null && item.Order.source.referral_url != null)
                            {
                                datos.proveedor = ObtenerTextoEntre(item.Order.review_url, "https://www.", ".cl");
                            }
                            else
                            {
                                datos.proveedor = ObtenerTextoEntre(item.Order.checkout_url, "https://www.", ".cl");
                            }

                            datos.comuna = item.Order.shipping_address?.municipality ?? "";
                            datos.telefono = item.Order.customer?.phone ?? "";
                            datos.envio = item.Order.shipping_option ?? "";
                            datos.compania = datos.proveedor ?? "";
                            datos.estado = item.Order.status_enum ?? "";

                            string proveedorsh = datos.proveedor;

                            foreach (var item2 in item.Order.products)
                            {
                                dtp.url = item2?.image ?? "";
                                dtp.id_producto_orden = item2?.id.ToString() ?? "0";
                                dtp.sku = item2?.sku ?? "";
                                dtp.nombre_producto = item2?.name ?? "";
                                dtp.cantidad_vendida = item2?.qty ?? 0;

                                if (decimal.TryParse(item2?.price.ToString() ?? "0", out decimal price))
                                {
                                    preciosuma.Add(price);
                                }

                                InsertaProductos(datos.estado, dtp.nombre_producto, dtp.sku, dtp.cantidad_vendida.ToString(), datos.numero_orden, dtp.pid, dtp.url);
                                dtp.Limpiar();
                            }
                 

                            Inserta_Ordenes(Convert.ToInt64(datos.numero_orden), datos.estado, datos.numero_orden, datos.origen, datos.comuna, datos.numero_orden, datos.fechaventa, datos.proveedor, datos.nombre, datos.apellidos, datos.telefono, datos.correo, datos.nota_orden, datos.courrier, datos.envio, Convert.ToString(preciosuma.Sum()), datos.direccion, datos.compania, null, null);
                            datos.Limpiar();
                            preciosuma.Clear();
                        }
                    }
                }
            }
        }


        public static async Task llenaJumpBeyond(List<Configuracion> token)
        {
            Datos_Ordenes datos = new Datos_Ordenes();
            Datos_Productos dtp = new Datos_Productos();
            List<string> imagen = new List<string>();
            var informacionregalo = String.Empty;
            var instrucciones = String.Empty;
            foreach (var info in token)
            {
                string loginjump = info.login;
                string authtokenjump = info.pass;
                string apiJump = info.url;

                using (HttpClient client = new HttpClient())
                {
                    string llamadojumpo = $"{apiJump}/v1/orders/status/paid.json?login={loginjump}&authtoken={authtokenjump}";
                    HttpResponseMessage response = client.GetAsync(llamadojumpo).Result;
                    List<decimal> preciosuma = new List<decimal>();

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonContent = await response.Content.ReadAsStringAsync();
                        List<JumpSellerBeyond.OrderWrapper> orderWrappers = JsonConvert.DeserializeObject<List<JumpSellerBeyond.OrderWrapper>>(jsonContent);

                        foreach (var item in orderWrappers)
                        {
                            if (item.Order.id.ToString() == "13949")
                            {

                            }
                            datos.numero_orden = item.Order.id.ToString() ?? "0";

                            if (DateTime.TryParseExact(item.Order.created_at, "yyyy-MM-dd HH:mm:ss UTC", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDateTime))
                            {
                                DateTime localDateTime = parsedDateTime.ToLocalTime();
                                datos.fechaventa = localDateTime.ToString();
                            }
                            else
                            {
                                datos.fechaventa = "";
                            }

                            datos.nombre = item.Order.billing_address?.name ?? "";
                            datos.apellidos = item.Order.billing_address?.surname ?? "";
                            string numeracion = item.Order.billing_address?.complement ?? "";

                            var datosdireccioncompleto = string.Empty;
                            if (item.Order.shipping_address.address != null )
                            {
                                datosdireccioncompleto = item.Order.shipping_address.address + " " + item.Order.shipping_address?.complement;
                            }
                            else if (item.Order.billing_address.address != null && item.Order.billing_address.address!= ".")
                            {
                                datosdireccioncompleto = item.Order.billing_address.address + " " + item.Order.billing_address?.complement;
                            }
                            else {

                                datosdireccioncompleto = item.Order.pickup_address.address + " " + item.Order.pickup_address?.complement;
                            }

                            
                                
                            datos.direccion = datosdireccioncompleto;
                            datos.envio = item.Order.shipping_method_name ?? "";
                            datos.correo = item.Order.customer?.email ?? "";
                            datos.numero_orden = item.Order.id.ToString() ?? "";
                            datos.courrier = "";
                            datos.origen = "JumpSeller";
                            if (item.Order != null && item.Order.source != null && item.Order.source.referral_url != null)
                            {
                                datos.proveedor = ObtenerTextoEntre(item.Order.review_url, "https://www.", ".cl");
                            }
                            else 
                            {
                                datos.proveedor = ObtenerTextoEntre(item.Order.checkout_url, "https://www.", ".cl");
                            }
                            var camposadicionales = item.Order.additional_fields.ToList();
                        
                            if (camposadicionales[0].value != null || camposadicionales[1].value != null || camposadicionales[2].value != null)
                            {
                                for (int i = 0; i <= 2; i++)
                                {


                                    var instruccionesregalo = camposadicionales[i].label.ToString();
                                    var esregalo = camposadicionales[i].value.ToString();

                                    if (instruccionesregalo == "¿Tu pedido es regalo?")
                                    {
                                        if (esregalo == "Sí")
                                        {
                                            informacionregalo = "Sí";
                                        }
                                        else
                                        {
                                            informacionregalo = "No";
                                        }
                                    }

                                    if (instruccionesregalo == "Puedes agregar una nota personalizada o si hay instrucciones especiales en el recuadro:")
                                    {
                                        if (esregalo != null)
                                        {
                                            instrucciones = esregalo;
                                        }
                                       
                                    }


                                }
                            }
                             
                            
                           
                            
                            datos.es_regalo= informacionregalo;
                            datos.comuna = item.Order.billing_address?.municipality ?? "";
                            datos.telefono = item.Order.customer?.phone ?? "";
                            datos.envio = item.Order.shipping_option ?? "";
                            datos.compania = datos.proveedor ?? "";
                            datos.estado = item.Order.status_enum ?? "";

                            string proveedorsh = datos.proveedor;

                            foreach (var item2 in item.Order.products)
                            {
                                dtp.url = item2?.image ?? "";
                                dtp.id_producto_orden = item2?.id.ToString() ?? "0";
                                dtp.sku = item2?.sku ?? "";
                                dtp.nombre_producto = item2?.name ?? "";
                                dtp.cantidad_vendida = item2?.qty ?? 0;

                                if (decimal.TryParse(item2?.price.ToString() ?? "0", out decimal price))
                                {
                                    preciosuma.Add(price);
                                }

                                InsertaProductos(datos.estado, dtp.nombre_producto, dtp.sku, dtp.cantidad_vendida.ToString(), datos.numero_orden, dtp.pid, dtp.url);
                                dtp.Limpiar();
                            }



                            Inserta_Ordenes(Convert.ToInt64(datos.numero_orden), datos.estado, datos.numero_orden, datos.origen, datos.comuna, datos.numero_orden, datos.fechaventa, datos.proveedor, datos.nombre, datos.apellidos, datos.telefono, datos.correo, datos.nota_orden, datos.courrier, datos.envio, Convert.ToString(preciosuma.Sum()), datos.direccion, datos.compania,datos.es_regalo,instrucciones);
                            datos.Limpiar();
                            preciosuma.Clear();
                        }
                    }
                }
            }
        }

        public static string ObtenerInformacionRegalo()
        {

            return "";
        }
        public static string ObtenerTextoEntre(string texto, string inicio, string fin)
        {
            int indiceInicio = texto.IndexOf(inicio);
            if (indiceInicio != -1)
            {
                indiceInicio += inicio.Length;
                int indiceFin = texto.IndexOf(fin, indiceInicio);
                if (indiceFin != -1)
                {
                    return texto.Substring(indiceInicio, indiceFin - indiceInicio);
                }
            }
            return null;
        }









        public static string ObtenerPrecioProductoShopify(List<Line_Items> ls)
        {
            List<decimal> valor = new List<decimal>();
            var precio1 = "";
            decimal precio = 0;
            foreach (var item in ls)
            {

                precio = Convert.ToDecimal(item.price);
                valor.Add(precio);
                precio = valor.Sum();
                precio1 = Convert.ToString(precio);
            }
            if (precio1 == "")
            {
                return precio1 = Convert.ToString(0);
            }

            else
            {
                return precio1;
            }
        }


        public static void Inserta_Ordenes(long idordenprevia, string status, string numeroorden, string origen, string comuna, string idPlat, string fechaVenta, string proveedor, string nombres, string apellidos,
            string telefono, string correo, string nota_orden, string courrier, string envio, string precio, string direccion, string compania,string regalo,string instrucciones )
        {
            bool insertaregalo;
            try
            {
                if (regalo == "Sí")
                {
                    insertaregalo = true;
                }
                else
                { 
                  insertaregalo= false;
                }
                if (instrucciones == "0")
                {
                    instrucciones = "";
                }
                if (status == "processing" || status == "paid")
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ToString();
                    int idestado = 1;
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        var fecha1 = fechaVenta;

                        DateTime dt;
                        var date1 = DateTime.TryParse(fecha1, out dt);
                        long idorden = idordenprevia;
                        if (date1)
                        {
                            fecha1 = dt.ToString("yyyyMMdd HH:mm:ss");
                        }
                        using (SqlCommand cmd = new SqlCommand("Insert_Ordenes_Migracion", con))
                        {
                            try
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("idorden", idorden);
                                cmd.Parameters.AddWithValue("@origen", origen);
                                cmd.Parameters.AddWithValue("@proveedor", proveedor);
                                cmd.Parameters.AddWithValue("@estado", status);
                                cmd.Parameters.AddWithValue("@comuna", comuna);
                                cmd.Parameters.AddWithValue("@Id_Plat", numeroorden);
                                cmd.Parameters.AddWithValue("@fecha_venta_str", fecha1);
                                cmd.Parameters.AddWithValue("@nombre", nombres);
                                cmd.Parameters.AddWithValue("apellido", apellidos);
                                cmd.Parameters.AddWithValue("@telefono", telefono);
                                cmd.Parameters.AddWithValue("@idestado", idestado);
                                cmd.Parameters.AddWithValue("@courrier", courrier ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@metodo_envio", envio);
                                cmd.Parameters.AddWithValue("@correo", correo);
                                cmd.Parameters.AddWithValue("@nota_orden", nota_orden);
                                cmd.Parameters.AddWithValue("@direccion", direccion);
                                cmd.Parameters.AddWithValue("@compania", compania);
                                cmd.Parameters.AddWithValue("@precio", Convert.ToDecimal(precio));

                              
                                cmd.Parameters.AddWithValue("@esregalo",Convert.ToBoolean(insertaregalo));
                                if (instrucciones == null)
                                {
                                    instrucciones = "";
                                }
                               
                                cmd.Parameters.AddWithValue("@instrucciones",instrucciones);
                                try
                                {
                                    cmd.ExecuteNonQuery();
                                } catch (Exception ex)
                                {
                                    ex.ToString();
                                }
                                Console.WriteLine("Se inserto orde" + numeroorden);
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                        con.Close();
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error al insertar la orden Shopify: {ex.Message}");

            }

        }




        public static void InsertaProductos(string estado, string nombre_producto, string sku, string cantidad_vendida, string numero_orden, long pid, string url)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ToString();
            int idestado = 1;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                if (estado == "processing" || estado == "paid")
                {
                    using (SqlCommand cmd = new SqlCommand("Inserta_Productos_Migracion", con))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@nombre_producto", nombre_producto);
                        cmd.Parameters.AddWithValue("@PID", pid);
                        cmd.Parameters.AddWithValue("@sku", sku);
                        cmd.Parameters.AddWithValue("@cantidad_vendida", cantidad_vendida);
                        cmd.Parameters.AddWithValue("@url", url);
                        cmd.Parameters.AddWithValue("@id_producto_orden", numero_orden);

                        var inserta = cmd.ExecuteNonQuery();
                        if (inserta < 1)
                        {

                        }

                    }
                    con.Close();
                }
            }

        }

        public static void BorrarOrdenes()
        {
            try
            {

                string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ToString();
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("Borrar_Ordenes_Migracion", con))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }

            }
            catch (Exception ex) { Console.WriteLine(ex); }
        }

        public static void BorrarProductos()
        {
            try
            {

                string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ToString();
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("Borrar_Productos", con))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }

            }
            catch (Exception ex) { Console.WriteLine(ex); }
        }




        static string ExtraerNombreProveedorWoocomerce(string input, string startPattern, string endPattern)
        {
            Regex regex = new Regex(Regex.Escape(startPattern) + "(.*?)" + Regex.Escape(endPattern));
            if (input == null)
            {
                return null;
            }
            Match match = regex.Match(input);

            if (match.Success)
            {
                return match.Groups[1].Value;
            }

            return null;
        }


        static string ExtraerNombreProveedorShopify(string input, string startPattern, string endPattern)
        {
            Regex regex = new Regex(Regex.Escape(startPattern) + "(.*?)" + Regex.Escape(endPattern));
            Match match = regex.Match(input);

            if (match.Success)
            {
                return match.Groups[1].Value;
            }

            return null;
        }



    }
}