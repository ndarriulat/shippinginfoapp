using ShippingInfoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShippingInfoApp.Logic
{
    public class ShipmentService
    {
        public DeliveryInformation GetShipmentInformation(string region, IList<Product> products)
        {
            DeliveryInformation deliveryInformation = new DeliveryInformation();
            SetProductsFromRegion(deliveryInformation.Shipments, region, products);

            SetFinalDeliveryDate(deliveryInformation);
            return deliveryInformation;
        }

        private static void SetProductsFromRegion(IList<Shipment> shipments, string region, IList<Product> products)
        {
            foreach (Product product in products)
            {
                if (product.DeliveryTimes.TryGetValue(region, out int itemDeliveryTime))
                {
                    int supplierIndex = shipments.ToList().FindIndex(s => s.Supplier == product.Supplier);
                    bool supplierIsNotInShipmentList = supplierIndex == -1;

                    if (supplierIsNotInShipmentList)
                    {
                        AddSupplier(shipments, product);
                    }
                    else
                    {
                        AddProductToSupplier(shipments, product, supplierIndex);
                    }
                }
            }
            foreach (Shipment shipment in shipments)
            {
                shipment.DeliveryDate = CalculateDeliveryDateFromDeliveryItems(shipment.Items);
            }
        }

        private static void AddProductToSupplier(IList<Shipment> shipments, Product product, int supplierIndex)
        {
            shipments[supplierIndex].Items.Add(product.Name, product.ItemsInStock);
        }

        private static void AddSupplier(IList<Shipment> shipments, Product product)
        {
            shipments.Add(new Shipment()
            {
                Supplier = product.Supplier,
                Items = new Dictionary<string, int>()
                {
                    { product.Name, product.ItemsInStock }
                }
            });
        }

        private void SetFinalDeliveryDate(DeliveryInformation deliveryInformation)
        {
            throw new NotImplementedException();
        }
    }
}
