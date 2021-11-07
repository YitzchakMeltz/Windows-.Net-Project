using System;

namespace IDAL
{
    namespace DO
    {
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
            public TimeSpan AssignmentTime { get; set; } // Not sure this is necessary
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

    }
}