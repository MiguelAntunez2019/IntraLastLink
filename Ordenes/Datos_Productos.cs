using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordenes
{
    internal class Datos_Productos
    {

          public string estado { get; set; }
          public string nombre_producto { get; set; }
          public string sku { get; set; }
            
           public int cantidad_vendida { get; set; }
           public string precio { get; set; }
           public string id_producto_orden { get; set; }
           public long pid { get; set; }
            
            
            public string url { get; set; }


        public void Limpiar()
        { 
        this.estado= string.Empty;
            this.nombre_producto= string.Empty;
            this.sku= string.Empty;
            this.cantidad_vendida = 0;
            this.precio= string.Empty;
            this.id_producto_orden= string.Empty;
            this.pid= 0;
            this.url= string.Empty;

        }
    }
}
