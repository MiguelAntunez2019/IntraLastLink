using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordenes
{
    public class Shopify
    {



        public class Order
        {
            public long Id { get; set; }
            public string financial_status { get; set; }
            public int order_number { get; set; }
            public object updated_at { get; set; }
            public object order_status_url { get; set; }

            public string name { get; set; }

            public List<Line_Items> Line_Items { get; set; }

            public BillingAddress billing_address { get; set; }

            public string total_line_items_price { get; set; }
            public List<note_attributes> note_attributes { get; set; }
            public string email { get; set; }
            public string note { get; set; }

            public Customer customer { get; set; }

        }

        public class Customer
        {

            public Default_Address default_address { get; set; }
        }



        public class Default_Address
        {
            public long id { get; set; }
            public long customer_id { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public object company { get; set; }
            public string address1 { get; set; }
            public string address2 { get; set; }
            public string city { get; set; }
            public string province { get; set; }
            public string country { get; set; }
            public object zip { get; set; }
            public string phone { get; set; }
            public string name { get; set; }
            public string province_code { get; set; }
            public string country_code { get; set; }
            public string country_name { get; set; }
            public bool @default { get; set; }
        }

        public class Line_Items
        {
            public long id { get; set; }
            public string name { get; set; }
            public string price { get; set; }
            public string sku { get; set; }
            public List<Tax_Lines> tax_Lines { get; set; }

            public long? product_id { get; set; }
            public int quantity { get; set; }

            public string fulfillment_service { get; set; }
        }


        public class BillingAddress
        {
            public string first_name { get; set; }
            public string address1 { get; set; }
            public string phone { get; set; }
            public string city { get; set; }
            public string zip { get; set; }
            public string province { get; set; }
            public string country { get; set; }
            public string last_name { get; set; }
            public string address2 { get; set; }
            public string company { get; set; }
            public object latitude { get; set; }
            public object longitude { get; set; }
            public string name { get; set; }
            public string country_code { get; set; }
            public string province_code { get; set; }



        }

        public class Tax_Lines
        {
            public int price { get; set; }
        }

        public class Root
        {
            public List<Order> Orders { get; set; }
        }

        public class note_attributes
        {
            public string name { get; set; }
        }



    }
}
