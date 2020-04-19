using ShippingInfoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShippingInfoApp.Logic
{
    public class DeliveryDatesService
    {
        public static DateTime CalculateDeliveryDateFromDeliveryItems(Dictionary<string, int> deliveryItemsStocks, IList<Product> products, string region)
        {
            int maximumDeliveryDays = GetMaximumDeliveryDays(deliveryItemsStocks, products, region);
            DateTime deliveryDate = DateTime.Now.AddDays(maximumDeliveryDays);
            return deliveryDate;
        }

        public static int GetMaximumDeliveryDays(Dictionary<string, int> deliveryItemsCounts, IList<Product> products, string region)
        {
            int maximumDeliveryDays = 0;
            foreach (var item in deliveryItemsCounts)
            {
                if (item.Value > maximumDeliveryDays)
                {
                    maximumDeliveryDays = item.Value;
                }
            }
            return maximumDeliveryDays;
        }
    }
}
