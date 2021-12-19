namespace BlApi.BO
{
    public class DroneList
    {
        public int ID { init; get; }
        public string Model { set; get; }
        public WeightCategories Weight { init; get; }
        public double Battery { get; set; }
        public DroneStatuses Status { get; set; }
        public Location Location { get; set; }
        public uint? PackageID { get; set; } // Can be null

        public override string ToString()
        {
            return $"ID: {ID}, Model: {Model}, Weight: {Weight}, Battery: {Battery}, Status: {Status}, Location: ({Location})" + (PackageID == null ? "" : $", {PackageID}");
        }
    }
}
