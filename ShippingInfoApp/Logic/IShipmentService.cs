using ShippingInfoApp.Models;
using System.Collections.Generic;

namespace ShippingInfoApp.Logic
{
    public interface IShipmentService
    {
        DeliveryInformation GetShipmentInformation(string region, IList<Product> products);
        void SetShipmentList(IList<Shipment> shipments, string region, IList<Product> products);
        void SetSuppliersDeliveryDates(IList<Shipment> shipments, string region, IList<Product> products);
    }
}