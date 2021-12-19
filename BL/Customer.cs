using System.Collections.Generic;

<<<<<<< HEAD
namespace BL
=======
namespace IBL.BO
>>>>>>> parent of d4aee0b (change namespaces IDAL/IBL to DalAPI/BlApi)
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
