using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShippingInfoApp.Logic;
using ShippingInfoApp.Models;

namespace ShippingInfoAppTests.Unit_tests.DeliveryDatesServiceTests
{
    [TestClass]
    public class GetDeliveryDateFromDeliveryItemsTests
    {
        private readonly DeliveryDatesService deliveryDatesService = new DeliveryDatesService();

        [TestMethod]
        public void GetDeliveryDateFromDeliveryItemsWithCorrectDataTest()
        {
            IList<Product> products = ProductsDataSet();
            IEnumerable<string> itemsNames = new List<string>()
            {
                "pink_t-shirt",
                "black_mug",
            };

            DateTime obtainedMaximumDeliveryDate = deliveryDatesService.GetDeliveryDateFromDeliveryItems(itemsNames, products, supplier: "Shirts4U", region: "uk");

            Assert.IsTrue(DateTime.Now.AddDays(9) < obtainedMaximumDeliveryDate);
            Assert.IsTrue(obtainedMaximumDeliveryDate < DateTime.Now.AddDays(11));
        }

        private IList<Product> ProductsDataSet()
        {
            return new List<Product>()
            {
                new Product("black_mug",
                            "Shirts4U",
                            new Dictionary<string, int>()
                            {
                                {"eu",1 },
                                {"us",6 },
                                {"uk",10 }
                            },
                            3),

                new Product("blue_t-shirt",
                            "Best Tshirts",
                            new Dictionary<string,int>()
                            {
                                {"eu", 1 },
                                {"us", 30 },
                                {"uk", 2 }
                            },
                            10),

                new Product("pink_t-shirt",
                            "Shirts4U",
                            new Dictionary<string,int>()
                            {
                                {"eu", 1 },
                                {"us", 6 },
                                {"uk", 2 }
                            },
                            8),

                new Product("pink_t-shirt",
                            "Shirts Unlimited",
                            new Dictionary<string,int>()
                            {
                                {"eu", 1 },
                                {"us", 12 },
                                {"uk", 2 }
                            },
                            8),
            };
        }
    }
}
