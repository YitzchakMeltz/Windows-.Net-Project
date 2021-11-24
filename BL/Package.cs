using System;

namespace IBL.BO
{
    public class Package
    {
        public int ID { init; get; }
        public PackageCustomer Sender { init; get; }
        public PackageCustomer Receiver { init; get; }
        public WieghtCategories Weight { init; get; }
        public Priorities Priority { init; get; }
        public DeliveryDrone Drone { init; get; }
        public DateTime Creation { init; get; }
        public DateTime AssignmentTime { init; get; }
        public DateTime? CollectionTime { init; get; }
        public DateTime? DeliveryTime { init; get; }
    }
}
