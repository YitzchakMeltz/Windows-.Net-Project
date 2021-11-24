using System.Collections.Generic;

namespace IBL.BO
{
    public class Customer
    {
        public int ID { init; get; }
        public string Name { init; get; }
        public string Phone { init; get; }
        public Location Location { init; get; }
        public List<DeliveredPackage> Outgoing { init; get; }
        public List<DeliveredPackage> Incoming { init; get; }
    }
}
