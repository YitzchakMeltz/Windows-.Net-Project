using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DalObject
{
    class DataSource
    {
        internal static Drone[] Drones = new Drone[10];
        internal static DroneCharge[] DroneCharges = new DroneCharge[10]; // I think this should be here
        internal static Station[] Stations = new Station[5];
        internal static Customer[] Customers = new Customer[100];
        internal static Parcel[] Parcels = new Parcel[1000];

        internal class Config
        {
            public static int FreeDrone = 0;
            public static int FreeStation = 0;
            public static int FreeCustomer = 0;
            public static int FreeParcel = 0;
            public static int PackageID = 0;
        }

        /// <summary>
        /// Initializes the entities with random data
        /// </summary>
        internal static void Initialize()
        {
            Random rd = new Random();

            for (int i = 0; i < 2; ++i) 
            {
                Stations[Config.FreeStation++] = new Station();
                Stations[Config.FreeStation].ID = rd.Next(100000000, 999999999);
            }

            for (int i = 0; i < 5; ++i)
            {
                Drones[Config.FreeDrone++] = new Drone();
                Drones[Config.FreeDrone].ID = rd.Next(100000000, 999999999);
                Drones[Config.FreeDrone].DroneStatus = (IDAL.DO.DroneStatuses)rd.Next(0, 3);
            }

            for (int i = 0; i < 10; ++i)
            {
                Customers[Config.FreeCustomer++] = new Customer();
                Customers[Config.FreeCustomer].ID = rd.Next(100000000, 999999999);
            }

            for (int i = 0; i < 10; ++i)
            {
                Parcels[Config.FreeParcel++] = new Parcel();
                Parcels[Config.FreeParcel].ID = rd.Next(100000000, 999999999);
                Parcels[Config.FreeParcel].Priority = (IDAL.DO.Priorities)rd.Next(0, 3);
            }
        }

        // Functions that return index of an entity in array

        internal static int FindDroneIndex(int ID)
        {
            int index = 0;

            foreach(Drone item in DataSource.Drones)
            {
                ++index;

                if (item.ID == ID)
                    return index;
            }

            return -1;
        }

        internal static int FindStationIndex(int ID)
        {
            int index = 0;

            foreach (Station item in DataSource.Stations)
            {
                ++index;

                if (item.ID == ID)
                    return index;
            }

            return -1;
        }

        internal static int FindCustomerIndex(int ID)
        {
            int index = 0;

            foreach (Customer item in DataSource.Customers)
            {
                ++index;

                if (item.ID == ID)
                    return index;
            }

            return -1;
        }

        internal static int FindParcelIndex(int ID)
        {
            int index = 0;

            foreach (Parcel item in DataSource.Parcels)
            {
                ++index;

                if (item.ID == ID)
                    return index;
            }

            return -1;
        }
    }

    public class DalObject
    {
        public DalObject()
        {
            DataSource.Initialize();
        }

        // Add Methods

        /// <summary>
        /// Adds a Drone to DataSource
        /// </summary>
        /// <param name="drone"></param>
        public void AddDrone(Drone drone)
        {
            DataSource.Drones[DataSource.Config.FreeDrone++] = drone;
        }

        /// <summary>
        /// Adds a Station to DataSource
        /// </summary>
        /// <param name="station"></param>
        public void AddStation(Station station)
        {
            DataSource.Stations[DataSource.Config.FreeStation++] = station;
        }

        /// <summary>
        /// Adds a Customer to DataSource
        /// </summary>
        /// <param name="customer"></param>
        public void AddCustomer(Customer customer)
        {
            DataSource.Customers[DataSource.Config.FreeCustomer++] = customer;
        }

        /// <summary>
        /// Adds a Parcel to DataSource
        /// </summary>
        /// <param name="parcel"></param>
        /// <returns>PackageID</returns>
        public int AddParcel(Parcel parcel)
        {
            DataSource.Parcels[DataSource.Config.FreeParcel++] = parcel;
            return ++DataSource.Config.PackageID;
        }


        // Return Entity Methods
        
        /// <summary>
        /// Returns a Drone by its ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Drone GetDrone(int ID)
        {
            //return DataSource.Drones[DataSource.FindDroneIndex(ID)];
            return DataSource.Drones.FirstOrDefault(d => d.ID == ID);
        }

        /// <summary>
        /// Returns a Base Station by its ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Station GetStation(int ID)
        {
            //return DataSource.Stations[DataSource.FindStationIndex(ID)];
            return DataSource.Stations.FirstOrDefault(s => s.ID == ID);
        }

        /// <summary>
        /// Returns a Customer by its ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Customer GetCustomer(int ID)
        {
            //return DataSource.Customers[DataSource.FindCustomerIndex(ID)];
            return DataSource.Customers.FirstOrDefault(c => c.ID == ID);
        }

        /// <summary>
        /// Returns a Parcel by its ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Parcel GetParcel(int ID)
        {
            //return DataSource.Parcels[DataSource.FindParcelIndex(ID)];
            return DataSource.Parcels.FirstOrDefault(p => p.ID == ID);
        }


        // Return Entity List Methods

        /// <summary>
        /// Returns an array of all Drones
        /// </summary>
        /// <returns></returns>
        public Drone[] GetDroneList()
        {
            Drone[] drones = new Drone[DataSource.Config.FreeDrone];
            Array.Copy(DataSource.Drones, drones, DataSource.Config.FreeDrone);
            return drones;
        }

        /// <summary>
        /// Returns an array of all Base Stations
        /// </summary>
        /// <returns></returns>
        public Station[] GetStationList()
        {
            Station[] stations = new Station[DataSource.Config.FreeStation];
            Array.Copy(DataSource.Stations, stations, DataSource.Config.FreeStation);
            return stations;
        }

        public Station[] GetAvailableStationList()
        {
            // Looks through DroneCharge list and counts how many have a specific StationID, then compares it to the Station's Charge Slot
            return GetStationList().Where(s => s.ChargeSlots > DataSource.DroneCharges.Count(dc => dc.StationID == s.ID)).ToArray();
        }

        /// <summary>
        /// Returns an array of all Customers
        /// </summary>
        /// <returns></returns>
        public Customer[] GetCustomerList()
        {
            Customer[] customers = new Customer[DataSource.Config.FreeCustomer];
            Array.Copy(DataSource.Customers, customers, DataSource.Config.FreeCustomer);
            return customers;
        }

        /// <summary>
        /// Returns an array of all Parcels
        /// </summary>
        /// <returns></returns>
        public Parcel[] GetParcelList()
        {
            Parcel[] parcels = new Parcel[DataSource.Config.FreeParcel];
            Array.Copy(DataSource.Parcels, parcels, DataSource.Config.FreeParcel);
            return parcels;
        }

        /// <summary>
        /// Returns an array of all Unassigned Parcels
        /// </summary>
        /// <returns></returns>
        public Parcel[] GetUnassignedParcelList()
        {
            return GetParcelList().Where(p => p.DroneID == 0).ToArray();
        }

    }
}
