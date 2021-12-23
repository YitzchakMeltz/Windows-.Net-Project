namespace BO
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

        public override string ToString()
        {
            return $"ID: {ID}, Name: {Name}, Phone: {Phone}, Packages Sent: (Delivered: {PackagesSentDelivered}, Not Delivered: {PackagesSentNotDelivered}), Packages Incoming: (Recieved: {PackagesRecieved}, Expected: {PackagesExpected})";
        }
    }
}
