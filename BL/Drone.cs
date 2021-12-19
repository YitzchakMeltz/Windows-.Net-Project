<<<<<<< HEAD
﻿namespace BL
=======
﻿namespace IBL.BO
>>>>>>> parent of d4aee0b (change namespaces IDAL/IBL to DalAPI/BlApi)
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
        public override string ToString()
        {
            return $"ID: {ID}, Model: {Model}, Weight: {Weight}, Battery: {Battery}, Status: {Status}, Location: ({Location})";
        }
    }
}
