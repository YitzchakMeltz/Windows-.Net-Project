using System;

namespace IDAL
{
    namespace DO
    {
        public struct Station
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public int ChargeSlots { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }

            /// <summary>
            /// Returns a String with details about the Station
            /// </summary>
            /// <returns></returns>
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
            public DroneStatuses DroneStatus { get; set; }
            public double Battery { get; set; }

            /// <summary>
            /// Returns a String with details about the Drone
            /// </summary>
            /// <returns></returns>
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

            /// <summary>
            /// Returns a String with details about the Customer
            /// </summary>
            /// <returns></returns>
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
            public Priorities Priority { get; set; }
            public int DroneID { get; set; }
            public DateTime Scheduled { get; set; }
            public DateTime PickedUp { get; set; }
            public int AssignmentTime { get; set; } // Not sure this is necessary
            public DateTime Delivered { get; set; }

            /// <summary>
            /// Returns a String with details about the Parcel
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return $"ID: {ID}, SenderID: {SenderID}, TargetID: {TargetID}, WeightCategory: {Enum.GetName(typeof(WeightCategories), WeightCategory)}, Priority: {Enum.GetName(typeof(Priorities), Priority)}, DroneID: {DroneID}, Scheduled: {Scheduled}, PickedUp: {PickedUp}, Delivered: {Delivered}.";
            }
        }

        public struct DroneCharge
        {
            public int DroneID { get; set; }
            public int StationID { get; set; }

            /// <summary>
            /// Returns a String that matches a Drone to its Base Station
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return $"DroneID: {DroneID}, StationID: {StationID}";
            }
        }
    }
}