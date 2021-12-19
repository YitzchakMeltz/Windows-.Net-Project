namespace DalApi
{
    namespace DO
    {
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