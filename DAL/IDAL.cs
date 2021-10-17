using System;

namespace IDAL
{
    namespace DO
    {
        public enum WeightCategories { Light, Medium, Heavy }

        public struct Station
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public int ChargeSlots { get; set; }
            public double Longitute { get; set; }
            public double Latitude { get; set; }
            public override string ToString()
            {
                return $"ID: {ID}, Name: {Name}, ChargeSlots: {ChargeSlots}, Longitude: {Longitude}, Latitude: {Latitude}.";
            }
        }

        public struct Drone
        {
            public int ID { get; set; }
            public string Model { get; set; }
            public WeightCategories WeightCategory { get; set; }
            public enum DroneStatuses { Free, Delivery, Maintenance }
            public DroneStatuses DroneStatus { get; set; }
            public double Battery { get; set; }
            public override string ToString()
            {
                return $"ID: {ID}, Model: {Model}, WeightCategory: {Enum.GetName(typeof(WeightCategories), WeightCategory)}, DroneStatus: {Enum.GetName(typeof(DroneStatuses), DroneStatus)}, Battery: {Battery}.";
            }
        }

        public struct Customer
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }
            public override string ToString()
            {
                return $"ID: {ID}, Name: {Name}, Phone: {Phone}, Longitude: {Longitude}, Latitude: {Latitude}.";
            }
        }

        public struct Parcel
        {
            public int ID { get; set; }
            public int SenderID { get; set; }
            public int TargetID { get; set; }
            public WeightCategories WeightCategory { get; set; }
            public enum Priorities { Regular, Fast, Emergency }
            public Priorities Priority { get; set; }
            public int DroneID { get; set; }
            public DateTime Scheduled { get; set; }
            public DateTime PickedUp { get; set; }
            public int AssignmentTime { get; set; } // Not sure this is necessary
            public DateTime Delivered { get; set; }
            public override string ToString()
            {
                return $"ID: {ID}, SenderID: {SenderID}, TargetID: {TargetID}, WeightCategory: {Enum.GetName(typeof(WeightCategories), WeightCategory)}, Priority: {Enum.GetName(typeof(Priorities), Priority)}, DroneID: {DroneID}, Scheduled: {Scheduled}, PickedUp: {PickedUp}, Delivered: {Delivered}.";
            }
        }

        public struct DroneCharge
        {
            public int DroneID { get; set; }
            public int StationID { get; set; }
            public override string ToString()
            {
                return $"DroneID: {DroneID}, StationID: {StationID}";
            }
        }
    }
}