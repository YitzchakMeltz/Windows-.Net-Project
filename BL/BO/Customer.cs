using System.Collections.Generic;

namespace BO
{
    public class Customer
    {
        public int ID { init; get; }
        public string Name { init; get; }
        public string Phone { init; get; }
        public Location Location { init; get; }
        public List<CustomerPackage> Outgoing { init; get; }
        public List<CustomerPackage> Incoming { init; get; }
        public override string ToString()
        {
            return $"ID: {ID}, Name: {Name}, Phone: {Phone}, Location: ({Location})";
        }
    }
}
