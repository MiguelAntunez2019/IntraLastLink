using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordenes
{
    public class Datos_Ordenes
    {

        public string ordenid { get; set; }
        public string estado { get; set; }

        public string numero_orden { get; set; }

        public string fechaventa { get; set; }

        public string tienda { get; set; }

        public string apiversion { get; set; }

        public string apiurl { get; set; }

        public string comuna { get; set; }

        public string nombre { get; set; }

        public string apellidos { get; set; }

        public string telefono { get; set; }

        public string correo { get; set; }

        public string courrier { get; set; }

        public string nota_orden { get; set; }

        public string direccion { get; set; }

        public string compania { get; set; }

        public string idseller { get; set; }

        public long pid { get; set; }

        public string nombre_producto { get; set; }

        public string sku { get; set; }

        public int cantidad_vendida { get; set; }

        public string precio_final { get; set; }

        public long id_producto_orden { get; set; }

        public string url { get; set; }

        public string origen { get; set; }

        public string envio { get; set; }

        public int id_producto { get; set; }

        public string precio { get; set; }
        public string proveedor { get; set; }
        public string id_plat { get; set; }
        public string proveedor2 { get; set; }

        public string es_regalo { get; set; }
        public Datos_Ordenes()
        { 
        
        }

        public Datos_Ordenes(
            string ordenid,
            string estado,
            string numeroOrden,
            string fechaVenta,
            string tienda,
            string apiVersion,
            string apiUrl,
            string comuna,
            string nombre,
            string apellidos,
            string telefono,
            string correo,
            string courrier,
            string notaOrden,
            string direccion,
            string compania,
            string idSeller,
            long pid,
            string nombreProducto,
            string sku,
            int cantidadVendida,
            string precioFinal,
            long idProductoOrden,
            string url,
            string origen,
            string envio,
            int idProducto,
            string precio,
            string proveedor,
            string id_plat,
            string proveedor2,
            string es_regalo)
        {
            this.ordenid = ordenid;
            this.estado = estado;
            this.numero_orden = numeroOrden;
            this.fechaventa = fechaVenta;
            this.tienda = tienda;
            this.apiversion = apiVersion;
            this.apiurl = apiUrl;
            this.comuna = comuna;
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.telefono = telefono;
            this.correo = correo;
            this.courrier = courrier;
            this.nota_orden = notaOrden;
            this.direccion = direccion;
            this.compania = compania;
            this.idseller = idSeller;
            this.pid = pid;
            this.nombre_producto = nombreProducto;
            this.sku = sku;
            this.cantidad_vendida = cantidadVendida;
            this.precio_final = precioFinal;
            this.id_producto_orden = idProductoOrden;
            this.url = url;
            this.origen = origen;
            this.envio = envio;
            this.id_producto = idProducto;
            this.precio = precio;
            this.proveedor = proveedor;
            this.id_plat = id_plat;
            this.proveedor2 = proveedor2;
            this.es_regalo = es_regalo;
        }


        public void Limpiar()
        {
            ordenid = string.Empty;
            estado = string.Empty;
            numero_orden = string.Empty;
            fechaventa = string.Empty;
            tienda = string.Empty;
            apiversion = string.Empty;
            apiurl = string.Empty;
            comuna = string.Empty;
            nombre = string.Empty;
            apellidos = string.Empty;
            telefono = string.Empty;
            correo = string.Empty;
            courrier = string.Empty;
            nota_orden = string.Empty;
            direccion = string.Empty;
            compania = string.Empty;
            idseller = string.Empty;
            pid = 0;
            nombre_producto = string.Empty;
            sku = string.Empty;
            cantidad_vendida = 0;
            precio_final = string.Empty;
            id_producto_orden = 0;
            url = string.Empty;
            origen = string.Empty;
            envio = string.Empty;
            id_producto = 0;
            precio = string.Empty;
            id_plat=string.Empty;
            proveedor2= string.Empty;
            es_regalo = string.Empty;
        }

    }
}
