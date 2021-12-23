namespace BO
{
    // package in transfer
    public class EnroutePackage
    {
        public int ID { init; get; }
        public WeightCategories Weight { init; get; }
        public Priorities Priority { init; get; }
        public bool Delivering { init; get; }                // True: Delivering. False: Waiting
        public PackageCustomer Sender { init; get; }
        public PackageCustomer Receiver { init; get; }
        public Location CollectionLocation { init; get; }
        public Location DeliveryLocation { init; get; }
        public double DeliveryDistance { init; get; }
    }
}
