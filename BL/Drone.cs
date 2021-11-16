namespace IBL.BO
{
    public class Drone
    {
        public int ID { get; set; }
        public string Model { get; set; }
        public WieghtCategories Weight { get; set; }
        public double Battery { get; set; }
        public DroneStatuses Status { get; set; }
        public EnroutePackage Package { get; set; }
        public Location Location { get; set; }
    }
}
