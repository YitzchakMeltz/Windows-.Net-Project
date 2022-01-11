using DO;
using System;
using static Dal.DataSource;

namespace Dal
{
    partial class DalObject : DalApi.IDal
    {
        /// <summary>
        /// Charges a Drone
        /// </summary>
        public void ChargeDrone(int droneID, int stationID)
        {
            GetDrone(droneID);                                               // Forces error if drone doesn't exist

            Station station = GetStation(stationID);
            if (station.AvailableChargeSlots == 0)
                throw new DO.InvalidInput("No charging slots available in nearest station.");
            station.AvailableChargeSlots -= 1;
            Stations[Stations.FindIndex(s => s.ID == stationID)] = station;

            DroneCharge dc = new DroneCharge()
            {
                DroneID = droneID,
                StationID = stationID,
                ChargeTime = System.DateTime.Now
            };

            DroneCharges.Add(dc);
        }

        /// <summary>
        /// Releases a Drone from charging
        /// </summary>
        public double ReleaseDrone(int droneID)
        {
            Drone drone = GetDrone(droneID);                                                  // Forces error if drone doesn't exist

            // Finds DroneCharge with droneID and removes it
            DroneCharge dc = DroneCharges.Find(dc => dc.DroneID == droneID);
            if (dc.Equals(default(DroneCharge)))
                throw new ObjectNotFound($"Drone with ID: {droneID} was not charging.");

            DroneCharges.Remove(dc);

            // Add 1 to Available Charging Slots of corresponding station
            Station station = GetStation(dc.StationID);
            station.AvailableChargeSlots += 1;
            Stations[Stations.FindIndex(s => s.ID == dc.StationID)] = station;

            return DateTime.Now.Subtract(dc.ChargeTime).TotalMinutes;
        }
    }
}