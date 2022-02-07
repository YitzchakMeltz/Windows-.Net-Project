using DO;
using DalApi.Util;
using System;
using System.Collections.Generic;
using static Dal.DataSource;
using System.Runtime.CompilerServices;

namespace Dal
{
    partial class DalObject : DalApi.IDal
    {
        /// <summary>
        /// Adds a Station to DataSource
        /// </summary>
        /// <param name="station"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Station GetStation(uint ID)
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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Station> GetStationList()
        {
            return new List<Station>(Stations);
        }

        /// <summary>
        /// Returns a filtered array of Stations
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Station> GetFilteredStationList(Predicate<Station> pred)
        {
            return Stations.FindAll(pred);
        }

        /*
        public IEnumerable<Station> GetAvailableStationList()
        {
            // Looks through DroneCharge list and counts how many have a specific StationID, then compares it to the Station's Charge Slot
            return Stations.FindAll(s => s.AvailableChargeSlots > 0);
            //return Stations.Where(s => s.ChargeSlots > DataSource.DroneCharges.Count(dc => dc.StationID == s.ID)).ToList();
        }*/


        /// <summary>
        /// Adds a Station to DataSource
        /// </summary>
        /// <param name="station"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddStation(uint id, string name, uint chargeSlots, double latitude, double longitude)
        {
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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void RemoveStation(uint ID)
        {
            if (Stations.RemoveAll(s => s.ID == ID) == 0)
                throw new ObjectNotFound($"Station with ID: {ID} doesn't exist");
        }
    }
}