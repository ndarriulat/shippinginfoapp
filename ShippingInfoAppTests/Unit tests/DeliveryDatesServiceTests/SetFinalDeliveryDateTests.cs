using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShippingInfoApp.Logic;
using ShippingInfoApp.Models;

namespace ShippingInfoAppTests.Unit_tests.DeliveryDatesServiceTests
{
    [TestClass]
    public class SetFinalDeliveryDateTests
    {
        private IDeliveryDatesService deliveryDatesService;

        [TestInitialize]
        public void SetUp()
        {
            deliveryDatesService = new DeliveryDatesService();
        }

        [TestMethod]
        public void SetFinalDeliveryDateWhenThreeDifferentShipmentsTest()
        {
            DateTime expectedDateTime = DateTime.Now.AddDays(50);
            IList<Shipment> shipments = new List<Shipment>() { new Shipment() { DeliveryDate = DateTime.Now.AddDays(20) },
                                                               new Shipment() { DeliveryDate = expectedDateTime },
                                                               new Shipment() { DeliveryDate = DateTime.MinValue } };

            DeliveryInformation deliveryInformation = new DeliveryInformation() { Shipments = shipments };

            deliveryDatesService.SetFinalDeliveryDate(deliveryInformation);

            Assert.AreEqual(expected: expectedDateTime, actual: deliveryInformation.FinalDeliveryDate);
        }

        [TestMethod]
        public void SetFinalDeliveryDateWhenNoShipmentsTest()
        {
            DateTime expectedDateTime = DateTime.MinValue;
            IList<Shipment> shipments = new List<Shipment>();

            DeliveryInformation deliveryInformation = new DeliveryInformation() { Shipments = shipments };

            deliveryDatesService.SetFinalDeliveryDate(deliveryInformation);

            Assert.AreEqual(expected: expectedDateTime, actual: deliveryInformation.FinalDeliveryDate);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void SetFinalDeliveryDateWhenNullShipmentsTest()
        {
            DateTime expectedDateTime = DateTime.MinValue;
            IList<Shipment> shipments = null;

            DeliveryInformation deliveryInformation = new DeliveryInformation() { Shipments = shipments };

            deliveryDatesService.SetFinalDeliveryDate(deliveryInformation);

        }
    }
}
