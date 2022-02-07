using System;

namespace DO
    {
        public struct DroneCharge
        {
            public uint DroneID { get; set; }
            public uint StationID { get; set; }
            public DateTime ChargeTime { init; get; }

            /// <summary>
            /// Returns a String that matches a Drone to its Base Station
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return $"DroneID: {DroneID}, StationID: {StationID}, ChargeTime: {ChargeTime}";
            }
        }

    }