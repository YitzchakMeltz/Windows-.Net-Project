using System.Collections.Generic;

namespace IBL.BO
{
    public class Customer
    {
        public int ID;
        public string Name;
        public string Phone;
        public Location Location;
        public List<DeliveredPackage> Outgoing;
        public List<DeliveredPackage> Incoming;
    }
}
