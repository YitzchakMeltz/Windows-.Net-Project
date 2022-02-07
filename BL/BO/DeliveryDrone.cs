namespace BO
{
    public class DeliveryDrone
    {
        public uint ID { init; get; }
        public double Battery { init; get; }
        public Location Location { init; get; }
        public override string ToString()
        {
            return $"ID: {ID}, Battery: {Battery}, Location: {Location}";
        }
    }
}
