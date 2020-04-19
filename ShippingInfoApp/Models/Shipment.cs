using System;
using System.Collections.Generic;

namespace ShippingInfoApp.Models
{
    public class Shipment
    {
        public string Supplier { get; set; }
        public DateTime DeliveryDate{ get; set; }
        public Dictionary<string,int> Items{ get; set; }
    }
}