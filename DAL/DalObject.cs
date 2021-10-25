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
            public static int DronesCharging = 0;
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
                int index = Config.FreeStation++;
                Stations[index] = new Station();

                do
                {
                    Stations[index].ID = rd.Next(100000000, 999999999);
                } while (Stations.Take(index).Any(s => s.ID == Stations[index].ID));
            }

            for (int i = 0; i < 5; ++i)
            {
                int index = Config.FreeDrone++;
                Drones[index] = new Drone();

                do {
                    Drones[index].ID = rd.Next(100000000, 999999999);
                } while (Drones.Take(index).Any(d => d.ID == Drones[index].ID));

                Drones[index].DroneStatus = (IDAL.DO.DroneStatuses)rd.Next(0, 3);

                // If Drone is currently Free, set it to be charging at a Base Station
                if (Drones[index].DroneStatus == DroneStatuses.Free)
                {
                    DroneCharges[Config.DronesCharging++] = new DroneCharge() { DroneID = Drones[index].ID, StationID = Stations.Where(s => s.ChargeSlots > DataSource.DroneCharges.Count(dc => dc.StationID == s.ID && dc.DroneID != 0)).ElementAt(rd.Next(Config.FreeStation)).ID };
                }
            }

            for (int i = 0; i < 10; ++i)
            {
                int index = Config.FreeCustomer++;
                Customers[index] = new Customer();

                do
                {
                    Customers[index].ID = rd.Next(100000000, 999999999);
                } while (Customers.Take(index).Any(c => c.ID == Customers[index].ID));
            }

            for (int i = 0; i < 10; ++i)
            {
                int index = Config.FreeParcel++;
                Parcels[index] = new Parcel();

                do
                {
                    Parcels[index].ID = rd.Next(100000000, 999999999);
                } while (Parcels.Take(index).Any(p => p.ID == Parcels[index].ID));

                Parcels[index].Priority = (IDAL.DO.Priorities)rd.Next(0, 3);
            }
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
            return DataSource.Drones.FirstOrDefault(d => d.ID == ID);
        }

        /// <summary>
        /// Returns a Base Station by its ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Station GetStation(int ID)
        {
            return DataSource.Stations.FirstOrDefault(s => s.ID == ID);
        }

        /// <summary>
        /// Returns a Customer by its ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Customer GetCustomer(int ID)
        {
            return DataSource.Customers.FirstOrDefault(c => c.ID == ID);
        }

        /// <summary>
        /// Returns a Parcel by its ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Parcel GetParcel(int ID)
        {
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
            return GetStationList().Where(s => s.ChargeSlots > DataSource.DroneCharges.Count(dc => dc.StationID == s.ID && dc.DroneID != 0)).ToArray();
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
