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
        private static Drone[] Drones = new Drone[10];
        private static Station[] Stations = new Station[5];
        private static Customer[] Customers = new Customer[100];
        private static Parcel[] Parcels = new Parcel[1000];

        private class Config
        {
            public static int FreeDrone = 0;
            public static int FreeStation = 0;
            public static int FreeCustomer = 0;
            public static int FreeParcel = 0;
            public static int PackageID;
        }

        static void Initialize()
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

    }
}
