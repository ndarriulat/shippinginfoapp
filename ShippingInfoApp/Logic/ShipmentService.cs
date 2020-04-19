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
        private readonly DeliveryDatesService deliveryDatesService;

        public ShipmentService(DeliveryDatesService _deliveryDatesService)
        {
            deliveryDatesService = _deliveryDatesService;
        }

        public DeliveryInformation GetShipmentInformation(string region, IList<Product> products)
        {
            DeliveryInformation deliveryInformation = new DeliveryInformation();
            SetProductsFromRegion(deliveryInformation.Shipments, region, products);

            SetFinalDeliveryDate(deliveryInformation);
            return deliveryInformation;
        }

        private void SetProductsFromRegion(IList<Shipment> shipments, string region, IList<Product> products)
        {
            foreach (Product product in products)
            {
                if (RegionIsAvailableForProduct(region, product))
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
                IEnumerable<string> shipmentItemsNames = shipment.Items.Select(i => i.Key);
                shipment.DeliveryDate = deliveryDatesService.GetDeliveryDateFromDeliveryItems(shipmentItemsNames, products, shipment.Supplier, region);
            }
        }

        private static bool RegionIsAvailableForProduct(string region, Product product)
        {
            return product.DeliveryTimes.ContainsKey(region);
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
            DateTime biggestDeliveryDate = DateTime.MinValue;
            foreach (Shipment shipment in deliveryInformation.Shipments)
            {
                if(shipment.DeliveryDate > biggestDeliveryDate)
                {
                    biggestDeliveryDate = shipment.DeliveryDate;
                }
            }

            deliveryInformation.FinalDeliveryDate = biggestDeliveryDate;
        }
    }
}
