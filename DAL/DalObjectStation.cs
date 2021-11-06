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
            try
            {
                return DataSource.Stations.Find(s => s.ID == ID);
            }
            catch
            {
                throw new ObjectNotFound($"Station with ID: {ID} not found.");
            }
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
        public void AddStation()
        {
            Station station = new Station();

            do
            {
                station.ID = rd.Next(100000000, 999999999);
            } while (DataSource.Stations.Exists(s => s.ID == station.ID));

            Console.WriteLine("Please enter the station name: ");
            station.Name = Console.ReadLine();

            Console.WriteLine("Please enter the amount of charge slots: ");
            station.AvailableChargeSlots = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Please enter the station coordinate latitude: ");
            double latitude = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Please enter the station coordinate longitude: ");
            double longitude = Convert.ToDouble(Console.ReadLine());

            station.Location = new Coordinate(latitude, longitude);

            DataSource.Stations.Add(station);

            Console.WriteLine("\n" + station);
        }
    }
}