using System;

namespace DO
    {
        public struct DroneCharge
        {
            public int DroneID { get; set; }
            public int StationID { get; set; }
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