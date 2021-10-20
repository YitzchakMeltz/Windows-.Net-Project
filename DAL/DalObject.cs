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
            Stations[Config.FreeStation++] = new Station();
            Stations[Config.FreeStation++] = new Station();
            Drones[Config.FreeDrone++] = new Drone();
            Drones[Config.FreeDrone++] = new Drone();
            Drones[Config.FreeDrone++] = new Drone();
            Drones[Config.FreeDrone++] = new Drone();
            Drones[Config.FreeDrone++] = new Drone();
        }
    }

    public class DalObject
    {
        public DalObject()
        {
            DataSource.Initialize();
        }

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

    }
}
