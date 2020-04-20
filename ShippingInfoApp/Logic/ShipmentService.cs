using ShippingInfoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShippingInfoApp.Logic
{
    public class ShipmentService : IShipmentService
    {
        private readonly IDeliveryDatesService deliveryDatesService;

        public ShipmentService(IDeliveryDatesService _deliveryDatesService)
        {
            deliveryDatesService = _deliveryDatesService;
        }

        public DeliveryInformation GetShipmentInformation(string region, IList<Product> products)
        {
            DeliveryInformation deliveryInformation = new DeliveryInformation();
            SetShipmentList(deliveryInformation.Shipments, region, products);
            SetSuppliersDeliveryDates(deliveryInformation.Shipments, region, products);
            SetFinalDeliveryDate(deliveryInformation);
            return deliveryInformation;
        }

        public void SetShipmentList(IList<Shipment> shipments, string region, IList<Product> products)
        {
            foreach (Product product in products)
            {
                if (RegionIsAvailableForProduct(region, product))
                {
                    if (SupplierIsNotInShipmentList(shipments, product, out int supplierIndex))
                    {
                        AddSupplier(shipments, product);
                    }
                    else
                    {
                        AddProductToSupplier(shipments, product, supplierIndex);
                    }
                }
            }
        }

        public void SetSuppliersDeliveryDates(IList<Shipment> shipments, string region, IList<Product> products)
        {
            foreach (Shipment shipment in shipments)
            {
                IEnumerable<string> shipmentItemsNames = shipment.ItemsStocks?.Select(i => i.Key);
                shipment.DeliveryDate = deliveryDatesService.GetDeliveryDateFromDeliveryItems(shipmentItemsNames, products, shipment.Supplier, region);                
            }
        }

        private static bool SupplierIsNotInShipmentList(IList<Shipment> shipments, Product product, out int supplierIndex)
        {
            supplierIndex = shipments.ToList().FindIndex(s => s.Supplier == product.Supplier);
            return supplierIndex == -1;
        }

        private static bool RegionIsAvailableForProduct(string region, Product product)
        {
            return product.DeliveryTimes.ContainsKey(region);
        }

        private static void AddSupplier(IList<Shipment> shipments, Product product)
        {
            shipments.Add(new Shipment()
            {
                Supplier = product.Supplier,
                ItemsStocks = new Dictionary<string, int>()
                {
                    { product.Name, product.ItemsInStock }
                }
            });
        }

        private static void AddProductToSupplier(IList<Shipment> shipments, Product product, int supplierIndex)
        {
            shipments[supplierIndex].ItemsStocks.Add(product.Name, product.ItemsInStock);
        }

        private void SetFinalDeliveryDate(DeliveryInformation deliveryInformation)
        {
            deliveryDatesService.SetFinalDeliveryDate(deliveryInformation);
        }

    }
}
