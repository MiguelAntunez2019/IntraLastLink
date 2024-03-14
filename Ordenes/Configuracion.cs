using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordenes
{
    public class Configuracion
    {
        public string pass { get; set; }
        public string login { get; set; }
        public string url { get; set; }
        public string token { get; set; }
        public string seller { get; set; }
    }



    public class ConfiguracionMayorista
    {
        public string pass { get; set; }
        public string login { get; set; }
        public string url { get; set; }
        public string token { get; set; }
    }

    public class ConfiguracionActualiza
    {
        public string idOrden { get; set; }
        public string nombreSeller { get; set; }
        public string origen { get; set; }
        public string token { get; set; }
        public string keyPasswordSeller { get; set; }
        public string keyUsernameSeller { get; set; }

        public string url { get; set; }
    }
}
