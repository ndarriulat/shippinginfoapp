using ShippingInfoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShippingInfoApp.Logic
{
    public class DeliveryDatesService : IDeliveryDatesService
    {
        /// <summary>
        /// Returns the maximum delivery date, given a list of items
        /// </summary>
        /// <param name="itemsNames">List of items from where to get the maximum delivery date</param>
        /// <param name="products">Products original list, used to get more information about each item</param>
        /// <param name="supplier"></param>
        /// <param name="region"></param>
        /// <returns></returns>
        public DateTime GetDeliveryDateFromDeliveryItems(IEnumerable<string> itemsNames, IList<Product> products, string supplier, string region)
        {
            int maximumDeliveryDays = GetMaximumDeliveryDays(itemsNames, products, supplier, region);
            DateTime deliveryDate = DateTime.Now.AddDays(maximumDeliveryDays);
            return deliveryDate;
        }

        /// <summary>
        /// Gets the maximum delivery days value from all the items with the same supplier and region, in the given list
        /// </summary>
        /// <param name="itemsNames">List of items from where to get the maximum</param>
        /// <param name="products">Products original list, used to get more information about each item</param>
        /// <param name="supplier"></param>
        /// <param name="region"></param>
        /// <returns></returns>
        public int GetMaximumDeliveryDays(IEnumerable<string> itemsNames, IList<Product> products, string supplier, string region)
        {
            int maximumDeliveryDays = -1;
            foreach (var itemName in itemsNames)
            {
                int deliveryDays = GetDeliveryDays(products, itemName, supplier, region);
                if (deliveryDays > maximumDeliveryDays)
                {
                    maximumDeliveryDays = deliveryDays;
                }
            }
            return maximumDeliveryDays;
        }

        /// <summary>
        /// Gets the amount of delivery days for a given product name, supplier and region
        /// </summary>
        /// <param name="products">Provided list of products in which to search</param>
        /// <param name="supplier"></param>
        /// <param name="region"></param>
        /// <returns></returns>
        public int GetDeliveryDays(IList<Product> products, string productName, string supplier, string region)
        {
            Product productWithGivenName = GetProductWithGivenName(products, supplier, productName);

            int deliveryDays = -1;
            productWithGivenName?.DeliveryTimes.TryGetValue(region, out deliveryDays);
            if (deliveryDays == 0)
            {
                deliveryDays = -1;
            }
            return deliveryDays;
        }

        private static Product GetProductWithGivenName(IList<Product> products, string supplier, string productNameToSearch)
        {
            return products.SingleOrDefault(p => p.Name == productNameToSearch && p.Supplier == supplier);
        }

        /// <summary>
        /// Sets the delivery date to a DeliveryInformation object, based on the maximum delivery date from its shipments
        /// </summary>
        /// <param name="deliveryInformation"></param>
        public void SetFinalDeliveryDate(DeliveryInformation deliveryInformation)
        {
            DateTime? biggestDeliveryDate = DateTime.MinValue;
            foreach (Shipment shipment in deliveryInformation.Shipments)
            {
                if (shipment.DeliveryDate > biggestDeliveryDate)
                {
                    biggestDeliveryDate = shipment.DeliveryDate;
                }
            }

            deliveryInformation.FinalDeliveryDate = biggestDeliveryDate;
        }
    }
}
