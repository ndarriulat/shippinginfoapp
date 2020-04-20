using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ShippingInfoApp.Logic;
using ShippingInfoApp.Models;

namespace ShippingInfoAppTests.Integration_tests.ShipmentServiceTests
{
    [TestClass]
    public class GetShipmentInformationTests
    {
        private IShipmentService shipmentService;

        [TestInitialize]
        public void SetUp()
        {
            shipmentService = new ShipmentService(new Mock<IDeliveryDatesService>().Object);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ShipmentInformationWithNoGivenRegionAndProductsTest()
        {
            string region = string.Empty;
            IList<Product> products = null;

            shipmentService.GetDeliveryInformation(region, products);
        }


        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ExceptionIsThrownWhenDeliveryTimesAreEmptyTest()
        {
            string region = "uk";
            IList<Product> products = new List<Product>()
            {
                new Product("black_mug",
                            "Shirts4U",
                            null,
                            3)
            };

            shipmentService.GetDeliveryInformation(region, products);
        }

        [TestMethod]
        public void ExceptionIsThrownWhenProductNameIsNullTest()
        {
            string region = string.Empty;
            IList<Product> products = new List<Product>()
            {
                new Product(null,
                            "Shirts4U",
                            new Dictionary<string, int>()
                            {
                                {"eu",1 },
                                {"us",6 },
                                {"uk",10 }
                            },
                            3)
            };

            var obtainedDeliveryInformation = shipmentService.GetDeliveryInformation(region, products);

            Assert.IsNull(obtainedDeliveryInformation.FinalDeliveryDate);
            Assert.AreEqual(expected: 0, actual: obtainedDeliveryInformation.Shipments.Count);
        }

        [TestMethod]
        public void ShipmentInformationHasCorrectDeliveryDateTest()
        {
            shipmentService = new ShipmentService(new DeliveryDatesService());
            string region = "us";
            IList<Product> products = ProductsDataSet();

            var obtainedDeliveryInformation = shipmentService.GetDeliveryInformation(region, products);

            Assert.IsNotNull(obtainedDeliveryInformation.FinalDeliveryDate);
            Assert.AreEqual(expected: DateTime.Now.AddDays(30).Date, actual: obtainedDeliveryInformation.FinalDeliveryDate.Value.Date);
        }

        [TestMethod]
        public void ShipmentInformationHasCorrectAmountTest()
        {
            string region = "eu";
            IList<Product> products = ProductsDataSet();

            var obtainedDeliveryInformation = shipmentService.GetDeliveryInformation(region, products);

            Assert.IsNotNull(obtainedDeliveryInformation);
            Assert.AreEqual(expected: 3, actual: obtainedDeliveryInformation.Shipments.Count);
        }

        [TestMethod]
        public void ShipmentInformationRareRegionTest()
        {
            string region = "br";
            string expectedSupplierName = "Shirts4U";
            string expectedProductName = "white_mug";
            IList<Product> products = ProductsBigDataSet();

            var obtainedDeliveryInformation = shipmentService.GetDeliveryInformation(region, products);

            Assert.IsNotNull(obtainedDeliveryInformation.Shipments);
            Assert.AreEqual(expected: 1, actual: obtainedDeliveryInformation.Shipments.Count);
            Assert.IsTrue(obtainedDeliveryInformation.Shipments[0].ItemsStocks.ContainsKey(expectedProductName));
            Assert.AreEqual(expected: expectedSupplierName, actual: obtainedDeliveryInformation.Shipments[0].Supplier);
        }

        [TestMethod]
        public void ShipmentInformationRareProductTest()
        {
            string region = "eu";
            string expectedProductName = "weird_t-shirt";
            string expectedSupplierName = "X Tshirts";
            IList<Product> products = ProductsBigDataSet();

            var obtainedDeliveryInformation = shipmentService.GetDeliveryInformation(region, products);

            Assert.IsNotNull(obtainedDeliveryInformation.Shipments);
            Assert.IsTrue(obtainedDeliveryInformation.Shipments.Any(s => s.ItemsStocks.ContainsKey(expectedProductName)));
            Assert.IsTrue(obtainedDeliveryInformation.Shipments.Any(s => s.Supplier == expectedSupplierName));
        }

        private IList<Product> ProductsDataSet() => new List<Product>()
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

        private IList<Product> ProductsBigDataSet() => new List<Product>()
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
            new Product("white_mug",
                        "Shirts4U",
                        new Dictionary<string, int>()
                        {
                            {"eu", 1 },
                            {"us", 6 },
                            {"uk", 10 },
                            {"br", 30 }
                        },
                        3),

            new Product("weird_t-shirt",
                        "X Tshirts",
                        new Dictionary<string,int>()
                        {
                            {"eu", 1 },
                        },
                        10),

            new Product("brown_t-shirt",
                        "Shirts4U",
                        new Dictionary<string,int>()
                        {
                            {"eu", 1 },
                            {"us", 6 },
                            {"uk", 2 }
                        },
                        1),

            new Product("pink_hoodie",
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
