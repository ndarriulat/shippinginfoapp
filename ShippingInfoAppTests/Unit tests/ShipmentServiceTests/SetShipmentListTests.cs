using System;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShippingInfoApp.Logic;
using System.Collections.Generic;
using ShippingInfoApp.Models;
using FakeItEasy;
using System.Linq;

namespace ShippingInfoAppTests.Unit_tests.ShipmentServiceTests
{
    [TestClass]
    public class SetShipmentListTests
    {
        private IShipmentService shipmentService;
        private IList<Product> products;
        private IList<Shipment> shipments;

        [TestInitialize]
        public void SetUp()
        {
            products = ProductDataSet();
            shipments = new List<Shipment>();
            shipmentService = new ShipmentService(new Mock<IDeliveryDatesService>().Object);
        }

        [TestMethod]
        public void SetShipmentListWithAllTheSuppliersCountTest()
        {
            shipmentService.SetShipmentList(shipments, region: "eu", products);

            Assert.AreEqual(expected: 3, actual: shipments.Count);
        }

        [TestMethod]
        public void SetShipmentListWithAllTheSuppliersNamesCheckTest()
        {
            shipmentService.SetShipmentList(shipments, region: "eu", products);

            Assert.AreEqual(expected: 3, actual: shipments.Count);
            Assert.IsTrue(shipments.Any(s => s.Supplier == "Shirts4U"));
            Assert.IsTrue(shipments.Any(s => s.Supplier == "Best Tshirts"));
            Assert.IsTrue(shipments.Any(s => s.Supplier == "Shirts Unlimited"));
        }
        

        [TestMethod]
        public void SetShipmentListWithAllTheSuppliersStocksTest()
        {
            shipmentService.SetShipmentList(shipments, region: "eu", products);

            Assert.AreEqual(expected: 3, actual: shipments.Count);
            Assert.IsTrue(shipments.Single(s => s.Supplier == "Shirts4U").ItemsStocks["black_mug"] == 3);
            Assert.IsTrue(shipments.Single(s => s.Supplier == "Shirts4U").ItemsStocks["pink_t-shirt"] == 8);
            Assert.IsTrue(shipments.Single(s => s.Supplier == "Best Tshirts").ItemsStocks["blue_t-shirt"] == 10);
            Assert.IsTrue(shipments.Single(s => s.Supplier == "Shirts Unlimited").ItemsStocks["pink_t-shirt"] == 8);
        }

        [TestMethod]
        public void SetShipmentListWithAllTheSuppliersDeliveryDatesTest()
        {
            shipmentService.SetShipmentList(shipments, region: "eu", products);

            Assert.AreEqual(expected: 3, actual: shipments.Count);
            Assert.IsTrue(shipments.All(s => s.DeliveryDate == null));
        }
        

        [TestMethod]
        public void SetShipmentListWithFakeRegionTest()
        {
            shipmentService.SetShipmentList(shipments, region: A.Dummy<string>(), products);

            Assert.AreEqual(expected: 0, actual: shipments.Count);
        }
        

        [TestMethod]
        public void SetShipmentListWithNoProductsTest()
        {
            shipmentService.SetShipmentList(shipments, region: A.Dummy<string>(), products);

            Assert.AreEqual(expected: 0, actual: shipments.Count);
        }

        private IList<Product> ProductDataSet()
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
