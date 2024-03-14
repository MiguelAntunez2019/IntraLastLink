using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Ordenes
{
    public class Woocomerce
    {
        public class Order
        {
            public int id { get; set; }
            public string status { get; set; }
            public string total { get; set; }
            public Billing billing { get; set; }
           
            public List<LineItem> line_items { get; set; }
   
            public string payment_url { get; set; }
            public string date_created { get; set; }
            public string checkout_url { get; set; }
            public string proveedor { get; set; }
            public string discount_total { get; set; }
            public string customer_note { get; set; }
        }

        public class Billing
        {
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string company { get; set; }
            public string city { get; set; }
            public string email { get; set; }
            public string phone { get; set; }
            public string address_1 { get; set; }
        }

        public class LineItem
        {
            public string name { get; set; }
            public int quantity { get; set; }
            public decimal price { get; set; }
            public Image image { get; set; }
            public string sku { get; set; }
            public int total { get; set; }
            public int id { get; set; }
            public string subtotal { get; set; }
        }

       

        public class Image
        {
            public int id { get; set; }
            public string src { get; set; }
        }
    }
}