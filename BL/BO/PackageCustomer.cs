namespace BO
{
    public class PackageCustomer
    {
        public uint ID { init; get; }
        public string Name { init; get; }
        public override string ToString()
        {
            return $"ID: {ID}, Name: {Name}";
        }
    }
}
