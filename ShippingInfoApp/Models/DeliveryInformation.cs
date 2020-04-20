using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShippingInfoApp.Models
{
    public class DeliveryInformation
    {
        public DateTime? FinalDeliveryDate { get; set; }
        public IList<Shipment> Shipments { get; set; }
        public DeliveryInformation()
        {
            Shipments = new List<Shipment>();
        }
    }
}
