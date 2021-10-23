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
                Drones[Config.FreeDrone].DroneStatus = (IDAL.DO.Drone.DroneStatuses)rd.Next(0, 3);
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
                Parcels[Config.FreeParcel].Priority = (IDAL.DO.Parcel.Priorities)rd.Next(0, 3);
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

        // Add methods
        public void AddDrone(Drone drone)
        {
            DataSource.Drones[DataSource.Config.FreeDrone++] = drone;
        }

        public void AddStation(Station station)
        {
            DataSource.Stations[DataSource.Config.FreeStation++] = station;
        }

        public void AddCustomer(Customer customer)
        {
            DataSource.Customers[DataSource.Config.FreeCustomer++] = customer;
        }

        public int AddParcel(Parcel parcel)
        {
            DataSource.Parcels[DataSource.Config.FreeParcel++] = parcel;
            return ++DataSource.Config.PackageID;
        }

        // Return Entity Methods
        public Drone GetDrone(int ID)
        {
            return DataSource.Drones[DataSource.FindDroneIndex(ID)];
        }

        public Station GetStation(int ID)
        {
            return DataSource.Stations[DataSource.FindStationIndex(ID)];
        }

        public Customer GetCustomer(int ID)
        {
            return DataSource.Customers[DataSource.FindCustomerIndex(ID)];
        }

        public Parcel GetParcel(int ID)
        {
            return DataSource.Parcels[DataSource.FindParcelIndex(ID)];
        }

        // Return Entity Lists Methods
        public Drone[] GetDroneList()
        {
            Drone[] drones = new Drone[DataSource.Config.FreeDrone];
            Array.Copy(DataSource.Drones, drones, DataSource.Config.FreeDrone);
            return drones;
        }

        public Station[] GetStationList()
        {
            Station[] stations = new Station[DataSource.Config.FreeStation];
            Array.Copy(DataSource.Stations, stations, DataSource.Config.FreeStation);
            return stations;
        }

        public Customer[] GetCustomerList()
        {
            Customer[] customers = new Customer[DataSource.Config.FreeCustomer];
            Array.Copy(DataSource.Customers, customers, DataSource.Config.FreeCustomer);
            return customers;
        }

        public Parcel[] GetParcelList()
        {
            Parcel[] parcels = new Parcel[DataSource.Config.FreeParcel];
            Array.Copy(DataSource.Parcels, parcels, DataSource.Config.FreeParcel);
            return parcels;
        }

    }
}
