using IDAL.DO;
using IDAL.Util;
using System.Collections.Generic;
using static DalObject.DataSource;

namespace DalObject
{
    partial class DalObject : IDAL.IDal
    {
        /// <summary>
        /// Adds a Station to DataSource
        /// </summary>
        /// <param name="station"></param>
        public void AddStation(Station station)
        {
            if (Stations.Exists(s => s.ID == station.ID))
                throw new ObjectAlreadyExists($"Station with ID: {station.ID} already exists.");

            Stations.Add(station);
        }


        /// <summary>
        /// Returns a Base Station by its ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Station GetStation(int ID)
        {
            Station s = Stations.Find(s => s.ID == ID);

            if (s.Equals(default(Station)))
                throw new ObjectNotFound($"Station with ID: {ID} not found.");

            return s;
        }


        /// <summary>
        /// Returns an array of all Base Stations
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Station> GetStationList()
        {
            return new List<Station>(Stations);
        }

        public IEnumerable<Station> GetAvailableStationList()
        {
            // Looks through DroneCharge list and counts how many have a specific StationID, then compares it to the Station's Charge Slot
            return Stations.FindAll(s => s.AvailableChargeSlots > 0);
            //return Stations.Where(s => s.ChargeSlots > DataSource.DroneCharges.Count(dc => dc.StationID == s.ID)).ToList();
        }

        
        /// <summary>
        /// Adds a Station to DataSource
        /// </summary>
        /// <param name="station"></param>
        public void AddStation(int id, string name, int chargeSlots, double latitude, double longitude)
        {
            if (Stations.Exists(s => s.ID == id))
                throw new IDAL.DO.ObjectAlreadyExists($"Station with ID: {id} already exists.");

            Station station = new Station()
            {
                ID = id,
                Name = name,
                AvailableChargeSlots = chargeSlots,
                Location = new Coordinate(latitude, longitude)
            };


            AddStation(station);
        }

        /// <summary>
        /// Deletes a Station from DataSource
        /// </summary>
        /// <param name="ID"></param>
        public void RemoveStation(int ID)
        {
            if (Stations.RemoveAll(s => s.ID == ID) == 0)
                throw new ObjectNotFound($"Station with ID: {ID} doesn't exist");
        }
    }
}