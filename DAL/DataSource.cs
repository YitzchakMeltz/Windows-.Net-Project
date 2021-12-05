﻿using System;
using System.Collections.Generic;
//using System.Linq;
using IDAL.DO;
using IDAL.Util;

namespace DalObject
{
    class DataSource
    {
        internal static List<Drone> Drones = new List<Drone>();
        internal static List<DroneCharge> DroneCharges = new List<DroneCharge>(10); // I think this should be here
        internal static List<Station> Stations = new List<Station>();
        internal static List<Customer> Customers = new List<Customer>();
        internal static List<Parcel> Parcels = new List<Parcel>();


        internal class Config
        {
            public static int PackageID = 0;
            public static double Free = 50;                        // km when free
            public static double LightConsumption = 40;            // km when carrying light package
            public static double MediumConsumption = 30;           // km when carrying mid weight package
            public static double HeavyConsumption = 20;            // km when carrying heavy package
            public static double ChargeRate = 10;                  // % charged per hour
        }


        /// <summary>
        /// Initializes the entities with random data
        /// </summary>
        internal static void Initialize()
        {
            Random rd = new Random();

            for (int i = 0; i < 2; ++i) 
            {
                Station s = new Station()
                {
                    AvailableChargeSlots = rd.Next(1, 50),
                    Name = "Station" + (i + 1),
                    Location = new Coordinate(-90 + 180 * rd.NextDouble(), -180 + 360 * rd.NextDouble())
                };

                do
                {
                    s.ID = rd.Next(100000000, 999999999);
                } while (Stations.Exists(sta => sta.ID == s.ID));

                Stations.Add(s);
            }

            for (int i = 0; i < 5; ++i)
            {
                Drone d = new Drone()
                {
                    Model = "Drone" + rd.Next(10),
                    WeightCategory = (IDAL.DO.WeightCategories)rd.Next(0, 3)
                };

                do {
                    d.ID = rd.Next(100000000, 999999999);
                } while (Drones.Exists(drone => drone.ID == d.ID));

                Drones.Add(d);

                /* If Drone is currently Free, set it to be charging at a Base Station
                if (Drones[index].DroneStatus == DroneStatuses.Free)
                {
                    //int droneIndex = Config.DronesCharging++;

                    DroneCharge dc = new DroneCharge();
                    dc.DroneID = Drones[index].ID;
                    dc.StationID = Stations.Take(Config.FreeStation).Where(s => s.ChargeSlots > DataSource.DroneCharges.Count(dc => dc.StationID == s.ID && dc.DroneID != 0)).ElementAt(rd.Next(Config.FreeStation)).ID;
                    DroneCharges.Add(dc);
                }*/
            }

            for (int i = 0; i < 10; ++i)
            {
                Customer c = new Customer()
                {
                    Name = "Customer" + (i + 1),
                    Location = new Coordinate(-90 + 180 * rd.NextDouble(), -180 + 360 * rd.NextDouble())
                };

                do
                {
                    c.ID = rd.Next(100000000, 999999999);
                } while (Customers.Exists(cust => cust.ID == c.ID)); // prevent overlapping IDs

                do {
                    c.Phone = "0" + rd.Next(500000000, 589999999);
                } while (Customers.Exists(cust => cust.Phone == c.Phone)); // prevent overlapping Phone Numbers

                Customers.Add(c);
            }

            for (int i = 0; i < 10; ++i)
            {
                DateTime earliest = new DateTime(2018, 1, 1, 0, 0, 0);

                Parcel p = new Parcel()
                {
                    SenderID = Customers[rd.Next(Customers.Count)].ID,
                    WeightCategory = (IDAL.DO.WeightCategories)rd.Next(0, 3),
                    Priority = (IDAL.DO.Priorities)rd.Next(0, 3),
                    Scheduled = earliest.AddSeconds(rd.NextDouble() * DateTime.Now.Subtract(earliest).TotalSeconds)
                };

                do
                {
                    p.ID = rd.Next(100000000, 999999999);
                } while (Parcels.Exists(parc => parc.ID == p.ID));

                do
                {
                    p.TargetID = Customers[rd.Next(Customers.Count)].ID;
                } while (p.TargetID == p.SenderID); // prevent customer from sending to itself

                if (rd.NextDouble() < .3)
                {
                    p.AssignmentTime = new TimeSpan(0, rd.Next(30), 0);
                    p.PickedUp = p.Scheduled.Add(p.AssignmentTime).AddMinutes(rd.Next(5, 60));
                    p.Delivered = p.PickedUp.AddMinutes(rd.Next(10, 120));
                }
                Parcels.Add(p);
            }
        }
    }
}
