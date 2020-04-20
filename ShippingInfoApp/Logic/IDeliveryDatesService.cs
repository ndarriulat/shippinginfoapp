using ShippingInfoApp.Models;
using System;
using System.Collections.Generic;

namespace ShippingInfoApp.Logic
{
    public interface IDeliveryDatesService
    {
        DateTime GetDeliveryDateFromDeliveryItems(IEnumerable<string> itemsNames, IList<Product> products, string supplier, string region);
        int GetDeliveryDays(IList<Product> products, string productName, string supplier, string region);
        int GetMaximumDeliveryDays(IEnumerable<string> itemsNames, IList<Product> products, string supplier, string region);
        void SetFinalDeliveryDate(DeliveryInformation deliveryInformation);
    }
}