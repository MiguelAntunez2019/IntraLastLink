using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordenes
{
    public class Variant
    {
        public long id { get; set; }
        public long product_id { get; set; }
        public string title { get; set; }
        public string price { get; set; }
        public string sku { get; set; }
        // Otros campos según sea necesario
    }

    public class Option
    {
        public long id { get; set; }
        public long product_id { get; set; }
        public string name { get; set; }
        public int position { get; set; }
        public List<string> values { get; set; }
    }

    public class Image
    {
        public long id { get; set; }
        public int position { get; set; }
        public long product_id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string src { get; set; }
    }

    public class Product
    {
        public long id { get; set; }
        public string title { get; set; }
        public string body_html { get; set; }
        public string vendor { get; set; }
        public string product_type { get; set; }
        public DateTime created_at { get; set; }
        public string handle { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime published_at { get; set; }
        public string template_suffix { get; set; }
        public string published_scope { get; set; }
        public string tags { get; set; }
        public string status { get; set; }
        public string admin_graphql_api_id { get; set; }
        public List<Variant> variants { get; set; }
        public List<Option> options { get; set; }
        public List<Image> images { get; set; }
        public Image image { get; set; }
    }

    public class RootProductos
    {
        public Product product { get; set; }
    }
}
