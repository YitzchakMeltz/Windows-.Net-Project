using System.Collections.Generic;

namespace IBL.BO
{
    public class Customer
    {
        public int ID { init; get; }
        public string Name { init; get; }
        public string Phone { init; get; }
        public Location Location { init; get; }
        public List<CustomerPackage> Outgoing { init; get; }
        public List<CustomerPackage> Incoming { init; get; }
    }
}
