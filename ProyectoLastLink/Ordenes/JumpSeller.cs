using Newtonsoft.Json;
using System.Collections.Generic;

namespace Ordenes
{
    public class JumpSeller
    {
        public class OrderWrapper
        {
            [JsonProperty("order")]
            public Order Order { get; set; }
        }

        public class Order
        {
            public int id { get; set; }
            public string created_at { get; set; }

            public shipping_address shipping_address { get; set; }
            public string shipping_method_name { get; set; }
            public customer customer { get; set; }
            public source source { get; set; }

            public string review_url { get; set; }
            public List<products> products { get; set; }

            public string status_enum { get; set; }

            public string checkout_url { get; set; }

            public int total { get; set; }

            public string shipping_option { get; set; }
        }

        public class shipping_address
        {
            public string name { get; set; }
            public string surname { get; set; }
            public string address { get; set; }
            public string municipality { get; set; }
        }

        public class customer
        {
            public string email { get; set; }
            public string phone { get; set; }
        }

        public class source
        {
            public string referral_url { get; set; }
            public string first_page_visited { get; set; }
        }
        public class products
        {
            public int id { get; set; }
            public string sku { get; set; }
            public string name { get; set; }
            public int qty { get; set; }
            public decimal price { get; set; }
            public string image { get; set; }
        }


    }
}