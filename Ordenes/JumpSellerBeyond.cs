using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Ordenes.Shopify;
using static ProyectoLastLink.Ordenes.JumpSellerBeyond;

namespace ProyectoLastLink.Ordenes
{
    public class JumpSellerBeyond
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
            public shipping_address _shipping_address;
            public shipping_address shipping_address
            {
                get { return _shipping_address; }
                set
                {
                    if (value == null)
                    {
                        _shipping_address = new shipping_address();
                    }
                    else
                    {
                        _shipping_address = value;
                    }
                }
            }
            public billing_address _billing_address;
            public billing_address billing_address 
            {
                get { return _billing_address; }
                set
                {
                    if (value == null)
                    {
                        _billing_address = new billing_address();
                    }
                    else
                    {
                        _billing_address = value;
                    }
                }
            }
            private pickup_address _pickup_address;
            public pickup_address pickup_address
            {
                get { return _pickup_address; }
                set
                {
                    if (value == null)
                    {
                        _pickup_address = new pickup_address();
                    }
                    else
                    {
                        _pickup_address = value;
                    }
                }
            }
            public string shipping_method_name { get; set; }
            public customer customer { get; set; }
            public source source { get; set; }

            public string review_url { get; set; }
            public List<products> products { get; set; }

            public string status_enum { get; set; }

            public string checkout_url { get; set; }

            public int total { get; set; }

            public string shipping_option { get; set; }


            public List<additional_fields> additional_fields { get; set; }
        }

        public class shipping_address
        {
            public string name { get; set; }
            public string surname { get; set; }
            public string address { get; set; }
            public string municipality { get; set; }
            public string complement { get; set; }
        }


        public class billing_address
        {
            public string name { get; set; }
            public string surname { get; set; }
            public string address { get; set; }
            public string municipality { get; set; }
            public string complement { get; set; }
        }


        public class pickup_address
        {
            public string address { get; set; }
            public string complement { get; set; }
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
        public class additional_fields
        {

            private string _value;

            public string label { get; set; }
        

            public string value
            {
                get { return _value; }
                set { _value = value ?? "0"; } 
            }

            public int id { get; set; }
            public string area { get; set; }

        }

    }
}