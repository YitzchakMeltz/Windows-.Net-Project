using System;

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
            public DateTime? Scheduled { get; set; }
            public DateTime? Assigned { get; set; }
            public DateTime? PickedUp { get; set; }
            public DateTime? Delivered { get; set; }

            /// <summary>
            /// Returns a String with details about the Parcel
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return $"ID: {ID}, SenderID: {SenderID}, TargetID: {TargetID}, WeightCategory: {Enum.GetName(typeof(WeightCategories), WeightCategory)}, Priority: {Enum.GetName(typeof(Priorities), Priority)}, DroneID: {DroneID}, Scheduled: {Scheduled}, Assigned: {Assigned}, PickedUp: {PickedUp}, Delivered: {Delivered}.";
            }
        }

    }