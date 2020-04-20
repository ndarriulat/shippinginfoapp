using System;
using System.Collections.Generic;

namespace ShippingInfoApp.Models
{
    public class Shipment
    {
        public string Supplier { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public Dictionary<string,int> ItemsStocks { get; set; }

        public Shipment(string supplier, DateTime? deliveryDate, Dictionary<string, int> itemStocks)
        {
            Supplier = supplier;
            DeliveryDate = deliveryDate;
            ItemsStocks = itemStocks;
        }

        public Shipment()
        {
        }
    }
}