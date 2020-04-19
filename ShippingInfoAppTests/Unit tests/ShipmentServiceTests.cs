using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShippingInfoApp.Logic;
using ShippingInfoApp.Models;

namespace ShippingInfoAppTests.Unit_tests
{
    [TestClass]
    public class ShipmentServiceTests
    {
        [TestMethod]
        public void ShipmentInformationWithNoGivenRegionAndProductsTest()
        {
            ShipmentService shipmentService = new ShipmentService();
            string region = "";
            IList<Product> products = null;

            var result = shipmentService.GetShipmentInformation(region, products);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void ShipmentInformationHasCorrectAmountTest()
        {
            ShipmentService shipmentService = new ShipmentService();
            string region = "eu";
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

            var result = shipmentService.GetShipmentInformation(region, products);

            Assert.IsNotNull(result);
            Assert.AreEqual(products.Count, 2);
        }
        
        [TestMethod]
        public void ExceptionIsThrownWhenDeliveryTimesAreEmptyTest()
        {
            ShipmentService shipmentService = new ShipmentService();
            string region = "";
            IList<Product> products = null;

            var result = shipmentService.GetShipmentInformation(region, products);

            Assert.Fail();
        } 
        
        [TestMethod]
        public void ExceptionIsThrownWhenProductNameIsEmptyTest()
        {
            ShipmentService shipmentService = new ShipmentService();
            string region = "";
            IList<Product> products = null;

            var result = shipmentService.GetShipmentInformation(region, products);

            Assert.Fail();
        }
    }
}
