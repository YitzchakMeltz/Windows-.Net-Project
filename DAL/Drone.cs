namespace DO
{
    public struct Drone
    {
        public int ID { get; set; }
        public string Model { get; set; }
        public WeightCategories WeightCategory { get; set; }
        //public DroneStatuses DroneStatus { get; set; }
        //public double Battery { get; set; }

        /// <summary>
        /// Returns a String with details about the Drone
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"ID: {ID}, Model: {Model}, WeightCategory: {System.Enum.GetName(typeof(WeightCategories), WeightCategory)}.";
        }
    }
}