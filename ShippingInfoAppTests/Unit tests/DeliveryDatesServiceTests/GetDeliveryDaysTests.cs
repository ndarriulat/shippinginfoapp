using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShippingInfoApp.Logic;
using ShippingInfoApp.Models;

namespace ShippingInfoAppTests.Unit_tests.DeliveryDatesServiceTests
{
    [TestClass]
    public class GetDeliveryDaysTests
    {
        [TestMethod]
        public void GetDeliveryDaysWithCorrectDataTest()
        {
            DeliveryDatesService deliveryDatesService = new DeliveryDatesService();
            IList<Product> products = new List<Product>()
            {
                new Product("black_mug",
                            "Shirts4U",
                            new Dictionary<string, int>()
                            {
                                {"eu",1 },
                                {"us",6 },
                                {"uk",2 }
                            },
                            3),

                new Product("blue_t-shirt",
                            "Best Tshirts",
                            new Dictionary<string,int>()
                            {
                                {"eu", 1 },
                                {"us", 5 },
                                {"uk", 2 }
                            },
                            10)
            };

            int deliveryDaysResult = deliveryDatesService.GetDeliveryDays(products, productName: "blue_t-shirt", supplier: "Best Tshirts", region: "uk");

            Assert.AreEqual(expected: 2, actual: deliveryDaysResult);
        }

        [TestMethod]
        public void GetDeliveryDaysWithNonExistentRegionTest()
        {
            DeliveryDatesService deliveryDatesService = new DeliveryDatesService();
            IList<Product> products = new List<Product>()
            {
                new Product("black_mug",
                            "Shirts4U",
                            new Dictionary<string, int>()
                            {
                                {"eu",1 },
                                {"us",6 },
                                {"uk",2 }
                            },
                            3),

                new Product("blue_t-shirt",
                            "Best Tshirts",
                            new Dictionary<string,int>()
                            {
                                {"eu", 1 },
                                {"us", 5 },
                                {"uk", 2 }
                            },
                            10)
            };

            int deliveryDaysResult = deliveryDatesService.GetDeliveryDays(products, productName: "blue_t-shirt", supplier: "Best Tshirts", region: "nz");

            Assert.AreEqual(expected: 0, actual: deliveryDaysResult);
        }

        [TestMethod]
        public void GetDeliveryDaysWithNonExistentProductNameTest()
        {
            DeliveryDatesService deliveryDatesService = new DeliveryDatesService();
            IList<Product> products = new List<Product>()
            {
                new Product("black_mug",
                            "Shirts4U",
                            new Dictionary<string, int>()
                            {
                                {"eu",1 },
                                {"us",6 },
                                {"uk",2 }
                            },
                            3),

                new Product("blue_t-shirt",
                            "Best Tshirts",
                            new Dictionary<string,int>()
                            {
                                {"eu", 1 },
                                {"us", 5 },
                                {"uk", 2 }
                            },
                            10)
            };

            int deliveryDaysResult = deliveryDatesService.GetDeliveryDays(products, productName: "fakename123", supplier: "Best Tshirts", region: "nz");

            Assert.AreEqual(expected: 0, actual: deliveryDaysResult);
        }

        [TestMethod]
        public void GetDeliveryDaysWithNonExistentSupplierTest()
        {
            DeliveryDatesService deliveryDatesService = new DeliveryDatesService();
            IList<Product> products = new List<Product>()
            {
                new Product("black_mug",
                            "Shirts4U",
                            new Dictionary<string, int>()
                            {
                                {"eu",1 },
                                {"us",6 },
                                {"uk",2 }
                            },
                            3),

                new Product("blue_t-shirt",
                            "Best Tshirts",
                            new Dictionary<string,int>()
                            {
                                {"eu", 1 },
                                {"us", 5 },
                                {"uk", 2 }
                            },
                            10)
            };

            int deliveryDaysResult = deliveryDatesService.GetDeliveryDays(products, productName: "blue_t-shirt", supplier: "fakesupplier123", region: "nz");

            Assert.AreEqual(expected: 0, actual: deliveryDaysResult);
        }
    }
}
