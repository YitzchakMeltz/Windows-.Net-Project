namespace IBL.BO
{
    // package in transfer
    public class EnroutePackage
    {
        public int ID { init; get; }
        public WieghtCategories Weight { init; get; }
        public Priorities Priority { init; get; }
        public bool Delivering { init; get; }                // True: Delivering. False: Waiting
        public PackageCustomer Sender { get; set; }
        public PackageCustomer Receiver { get; set; }
        public Location CollectionLocation { get; set; }
        public Location DeliveryLocation { get; set; }
        public double DeliveryDistance { get; set; }
    }
}
