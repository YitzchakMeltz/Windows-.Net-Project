namespace BO
{
    // package at customer
    public class CustomerPackage
    {
        public uint ID { init; get; }
        public WeightCategories Weight { init; get; }
        public Priorities Priority { init; get; }
        public Statuses Status { init; get; }
        public PackageCustomer Customer { init; get; } // Customer on other end of delivery
    }
}
