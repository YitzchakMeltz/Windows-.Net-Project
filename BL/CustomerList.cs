namespace IBL.BO
{
    public class CustomerList
    {
        public int ID { init; get; }
        public string Name { init; get; }
        public string Phone { init; get; }
        public uint PackagesSentDelivered { init; get; }
        public uint PackagesSentNotDelivered { init; get; }
        public uint PackagesRecieved { init; get; }
        public uint PackagesExpected { init; get; }
    }
}
