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

            DroneCharge dc = new DroneCharge();

            //Console.WriteLine("Enter the ID of the drone to charge: ");
            dc.DroneID = droneID;

            //Console.WriteLine("Enter the ID of the station to be assigned: ");
            dc.StationID = stationID;

            DroneCharges.Add(dc);
        }

        /// <summary>
        /// Releases a Drone from charging
        /// </summary>
        public void ReleaseDrone(int droneID)
        {
            //Console.WriteLine("Enter the ID of the drone to release: ");
            //droneID = Convert.ToInt32(Console.ReadLine());

            GetDrone(droneID);                                                  // Forces error if drone doesn't exist

            // Finds DroneCharge with droneID and removes it
            DroneCharges.RemoveAll(dc => dc.DroneID == droneID);
        }
    }
}