using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShippingInfoApp.Logic;
using ShippingInfoApp.Models;

namespace ShippingInfoAppTests.Unit_tests.DeliveryDatesServiceTests
{
    [TestClass]
    public class GetMaximumDeliveryDaysTests
    {
        private readonly DeliveryDatesService deliveryDatesService = new DeliveryDatesService();

        [TestMethod]
        public void GetMaximumDeliveryDaysWithCorrectDataTest()
        {
            IList<Product> products = ProductsDataSet();

            IEnumerable<string> itemsNames = new List<string>()
            {
                "pink_t-shirt",
                "black_mug",
            };

            int obtainedMaximumDeliveryDays = deliveryDatesService.GetMaximumDeliveryDays(itemsNames, products, supplier: "Shirts4U", region: "uk");

            Assert.AreEqual(expected: 10, actual: obtainedMaximumDeliveryDays);
        }

        [TestMethod]
        public void GetMaximumDeliveryDaysWithNonExistentRegionTest()
        {
            IList<Product> products = ProductsDataSet();

            IEnumerable<string> itemsNames = new List<string>()
            {
                "pink_t-shirt",
                "black_mug",
            };

            int obtainedMaximumDeliveryDays = deliveryDatesService.GetMaximumDeliveryDays(itemsNames, products, supplier: "Shirts4U", region: "xy");

            Assert.AreEqual(expected: -1, actual: obtainedMaximumDeliveryDays);
        }

        [TestMethod]
        public void GetMaximumDeliveryDaysWithNonExistentSupplierTest()
        {
            IList<Product> products = ProductsDataSet();

            IEnumerable<string> itemsNames = new List<string>()
            {
                "pink_t-shirt",
                "black_mug",
            };

            int obtainedMaximumDeliveryDays = deliveryDatesService.GetMaximumDeliveryDays(itemsNames, products, supplier: "fakesupplier123", region: "uk");

            Assert.AreEqual(expected: -1, actual: obtainedMaximumDeliveryDays);
        }

        [TestMethod]
        public void GetMaximumDeliveryDaysWithNoGivenItemsTest()
        {
            IList<Product> products = ProductsDataSet();

            IEnumerable<string> itemsNames = new List<string>();

            int obtainedMaximumDeliveryDays = deliveryDatesService.GetMaximumDeliveryDays(itemsNames, products, supplier: "Shirts4U", region: "uk");

            Assert.AreEqual(expected: -1, actual: obtainedMaximumDeliveryDays);
        }

        [TestMethod]
        public void GetMaximumDeliveryDaysWithNoGivenProductsTest()
        {
            IList<Product> products = new List<Product>();

            IEnumerable<string> itemsNames = new List<string>()
            {
                "pink_t-shirt",
                "black_mug",
            };

            int obtainedMaximumDeliveryDays = deliveryDatesService.GetMaximumDeliveryDays(itemsNames, products, supplier: "Shirts4U", region: "uk");

            Assert.AreEqual(expected: -1, actual: obtainedMaximumDeliveryDays);
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
