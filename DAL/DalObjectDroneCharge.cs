using IDAL.DO;
using static DalObject.DataSource;

namespace DalObject
{
    partial class DalObject : IDAL.IDal
    {
        /// <summary>
        /// Charges a Drone
        /// </summary>
        public void ChargeDrone(int droneID, int stationID)
        {
            GetDrone(droneID);                                               // Forces error if drone doesn't exist
            GetStation(stationID);                                           // Forces error if stations doesn't exist

            DroneCharge dc = new DroneCharge()
            {
                DroneID = droneID,
                StationID = stationID
            };

            DroneCharges.Add(dc);
        }

        /// <summary>
        /// Releases a Drone from charging
        /// </summary>
        public void ReleaseDrone(int droneID)
        {
            GetDrone(droneID);                                                  // Forces error if drone doesn't exist

            // Finds DroneCharge with droneID and removes it
            if (DroneCharges.RemoveAll(dc => dc.DroneID == droneID) == 0)
                throw new ObjectNotFound($"Drone with ID: {droneID} was not charging.");
        }
    }
}