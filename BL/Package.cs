using System;

namespace IBL.BO
{
    public class Package
    {
        public int ID;
        public PackageCustomer Sender;
        public PackageCustomer Receiver;
        public WieghtCategories Weight;
        public Priorities Priority;
        public DeliveryDrone Drone;
        public DateTime Creation;
        public DateTime AssignmentTime;
        public DateTime? CollectionTime;
        public DateTime? DeliveryTime;
    }
}
