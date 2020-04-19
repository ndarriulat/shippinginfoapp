using System;
using System.Collections.Generic;
using System.Text;

namespace ShippingInfoApp.Models
{
    public class Product
    {
        public string Name { get; set; }
        public string Supplier { get; set; }
        public Dictionary<string, int> DeliveryTimes { get; set; }
        public int ItemsInStock { get; set; }
    }
}
