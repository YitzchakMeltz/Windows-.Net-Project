namespace IBL.BO
{
    public class Drone
    {
        public int ID { init; get; }
        public string Model { get; set; }
        public WeightCategories Weight { init; get; }
        public double Battery { init; get; }
        public DroneStatuses Status { init; get; }
        public EnroutePackage Package { init; get; }
        public Location Location { init; get; }
    }
}
