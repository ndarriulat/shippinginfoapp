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
        public Product(string name, string supplier, Dictionary<string, int> deliveryTimes, int itemsInStock)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Supplier = supplier ?? throw new ArgumentNullException(nameof(supplier));
            DeliveryTimes = deliveryTimes ?? throw new ArgumentNullException(nameof(deliveryTimes));
            ItemsInStock = itemsInStock;
        }
    }

    //public enum Supplier
    //{
    //    Shirts4U,
    //    Best Tshirts,

    //}
}
