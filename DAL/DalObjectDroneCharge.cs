using IDAL.DO;
using IDAL.Util;
using System.Collections.Generic;
using static DalObject.DataSource;

namespace DalObject
{
    partial class DalObject : IDAL.IDal
    {
        /// <summary>
        /// Charges a Drone
        /// </summary>
        public void ChargeDrone()
        {
            DroneCharge dc = new DroneCharge();

            Console.WriteLine("Enter the ID of the drone to charge: ");
            dc.DroneID = Convert.ToInt32(Console.ReadLine());
            GetDrone(dc.DroneID);                                               // Forces error if drone doesn't exist

            Console.WriteLine("Enter the ID of the station to be assigned: ");
            dc.StationID = Convert.ToInt32(Console.ReadLine());
            GetStation(dc.StationID);                                           // Forces error if stations doesn't exist

            DroneCharges.Add(dc);
        }

        /// <summary>
        /// Releases a Drone from charging
        /// </summary>
        public void ReleaseDrone()
        {
            int droneID;

            Console.WriteLine("Enter the ID of the drone to release: ");
            droneID = Convert.ToInt32(Console.ReadLine());
            GetDrone(droneID);                                                  // Forces error if drone doesn't exist

            // Finds DroneCharge with droneID and removes it
            DroneCharges.RemoveAll(dc => dc.DroneID == droneID);
        }

    }
}