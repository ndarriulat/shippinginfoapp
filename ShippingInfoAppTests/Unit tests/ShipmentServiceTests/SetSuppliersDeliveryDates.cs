using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ShippingInfoApp.Logic;
using ShippingInfoApp.Models;

namespace ShippingInfoAppTests.Unit_tests.ShipmentServiceTests
{
    [TestClass]
    public class SetSuppliersDeliveryDates
    {
        private IShipmentService shipmentService;
        private DateTime fixedDateTime;

        [TestInitialize]
        public void SetUp()
        {
            fixedDateTime = new DateTime(2060, 10, 15);
            var mock = new Mock<IDeliveryDatesService>();
            mock.Setup(x => x.GetDeliveryDateFromDeliveryItems(A.CollectionOfDummy<string>(5), A.CollectionOfFake<Product>(15), 
                A.Dummy<string>(), A.Dummy<string>())).Returns(fixedDateTime);
            shipmentService = new ShipmentService(new Mock<IDeliveryDatesService>().Object);
        }

        [TestMethod]
        public void SetSuppliersDeliveryDatesWithShipmentsListSetsNoNullDeliveryDatesTest()
        {
            var fakeShipments = A.CollectionOfFake<Shipment>(6);
            IList <Shipment> shipments = fakeShipments;

            shipmentService.SetSuppliersDeliveryDates(shipments: A.CollectionOfFake<Shipment>(6), region: A.Dummy<string>(), products: A.CollectionOfFake<Product>(10));

            Assert.IsTrue(shipments.All(s => s.DeliveryDate != null));
            Assert.IsTrue(shipments.All(s => s.DeliveryDate == fixedDateTime));
        }
        

        [TestMethod]
        public void SetSuppliersDeliveryDatesWithEmptyShipmentsListSetsNullDeliveryDatesTest()
        {
            IList<Shipment> shipments = A.CollectionOfFake<Shipment>(0);

            shipmentService.SetSuppliersDeliveryDates(shipments, region: A.Dummy<string>(), products: A.CollectionOfFake<Product>(5));

            Assert.IsTrue(shipments.All(s => s.DeliveryDate == null));
        }

    }
}
