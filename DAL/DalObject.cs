using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DalObject.DataSource;
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
            public static double Free;                        // km when free
            public static double LightConsumption;            // km when carrying light package
            public static double MediumConsumption;           // km when carrying mid weight package
            public static double HeavyConsumption;            // km when carrying heavy package
            public static double ChargeRate;                  // % charged per hour
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
                } while (Stations.Any(sta => sta.ID == s.ID));

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
                } while (Drones.Any(drone => drone.ID == d.ID));

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
                } while (Customers.Any(cust => cust.ID == c.ID)); // prevent overlapping IDs

                do {
                    c.Phone = "0" + rd.Next(500000000, 589999999);
                } while (Customers.Any(cust => cust.Phone == c.Phone)); // prevent overlapping Phone Numbers

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
                } while (Parcels.Any(parc => parc.ID == p.ID));

                do
                {
                    p.TargetID = Customers[rd.Next(Customers.Count)].ID;
                } while (p.TargetID == p.SenderID); // prevent customer from sending to itself

                Parcels.Add(p);
            }
        }
    }

    public class DalObject : IDAL.IDal
    {
        private Random rd = new Random();
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
            if (Drones.Any(d => d.ID == drone.ID))
                throw new ObjectAlreadyExists();

            Drones.Add(drone);
        }

        /// <summary>
        /// Adds a Station to DataSource
        /// </summary>
        /// <param name="station"></param>
        public void AddStation(Station station)
        {
            if (Stations.Any(s => s.ID == station.ID))
                throw new ObjectAlreadyExists();

            Stations.Add(station);
        }

        /// <summary>
        /// Adds a Customer to DataSource
        /// </summary>
        /// <param name="customer"></param>
        public void AddCustomer(Customer customer)
        {
            if (Customers.Any(c => c.ID == customer.ID))
                throw new ObjectAlreadyExists();

            Customers.Add(customer);
        }

        /// <summary>
        /// Adds a Parcel to DataSource
        /// </summary>
        /// <param name="parcel"></param>
        /// <returns>PackageID</returns>
        public int AddParcel(Parcel parcel)
        {
            if (Parcels.Any(p => p.ID == parcel.ID))
                throw new ObjectAlreadyExists();

            Parcels.Add(parcel);
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
            try
            {
                return Drones.Find(d => d.ID == ID);
            }
            catch
            {
                throw new ObjectNotFound();
            }
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
                throw new ObjectNotFound();
            }
        }

        /// <summary>
        /// Returns a Customer by its ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Customer GetCustomer(int ID)
        {
            try
            {
                return Customers.Find(c => c.ID == ID);
            }
            catch
            {
                throw new ObjectNotFound();
            }
        }

        /// <summary>
        /// Returns a Parcel by its ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Parcel GetParcel(int ID)
        {
            try
            {
                return Parcels.Find(p => p.ID == ID);
            }
            catch
            {
                throw new ObjectNotFound();
            }
        }


        // Return Entity List Methods

        /// <summary>
        /// Returns an array of all Drones
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Drone> GetDroneList()
        {
            return Drones.ToList();
        }

        /// <summary>
        /// Returns an array of all Base Stations
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Station> GetStationList()
        {
            return Stations.ToList();
        }

        public IEnumerable<Station> GetAvailableStationList()
        {
            // Looks through DroneCharge list and counts how many have a specific StationID, then compares it to the Station's Charge Slot
            return Stations.Where(s => s.AvailableChargeSlots > 0).ToList();
            //return Stations.Where(s => s.ChargeSlots > DataSource.DroneCharges.Count(dc => dc.StationID == s.ID)).ToList();
        }

        /// <summary>
        /// Returns an array of all Customers
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Customer> GetCustomerList()
        {
            return Customers.ToList();
        }

        /// <summary>
        /// Returns an array of all Parcels
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Parcel> GetParcelList()
        {
            return Parcels.ToList();
        }

        /// <summary>
        /// Returns an array of all Unassigned Parcels
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Parcel> GetUnassignedParcelList()
        {
            return Parcels.Where(p => p.DroneID == 0).ToList();
        }


        // Add functions with user interaction

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
            } while (DataSource.Stations.Any(s => s.ID == station.ID));

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

        /// <summary>
        /// Adds a Drone to DataSource
        /// </summary>
        /// <param name="drone"></param>
        public void AddDrone()
        {
            Drone drone = new Drone();

            do
            {
                drone.ID = rd.Next(100000000, 999999999);
            } while (DataSource.Drones.Any(s => s.ID == drone.ID));

            Console.WriteLine("Please enter the model name: ");
            drone.Model = Console.ReadLine();

            Console.WriteLine("Please enter the weight: \n0 for Light \n1 for Medium \n2 for Heavy");
            drone.WeightCategory = (IDAL.DO.WeightCategories)Convert.ToInt32(Console.ReadLine());

            /*Console.WriteLine("Please enter the drone's status: \n0 for Free \n1 for Delivery \n2 for Maintanence");
            drone.DroneStatus = (IDAL.DO.DroneStatuses)Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Please enter the drone battery level: ");
            drone.Battery = Convert.ToDouble(Console.ReadLine());*/

            DataSource.Drones.Add(drone);

            Console.WriteLine("\n" + drone);
        }

        /// <summary>
        /// Adds a Customer to DataSource
        /// </summary>
        /// <param name="customer"></param>
        public void AddCustomer()
        {
            Customer customer = new Customer();

            do
            {
                customer.ID = rd.Next(100000000, 999999999);
            } while (DataSource.Customers.Any(s => s.ID == customer.ID));

            Console.WriteLine("Please enter the customer's name: ");
            customer.Name = Console.ReadLine();

            Console.WriteLine("Please enter the customer's phone number: ");
            customer.Phone = Console.ReadLine();

            Console.WriteLine("Please enter the customer's coordinate latitude: ");
            double latitude = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Please enter the customer's coordinate longitude: ");
            double longitude = Convert.ToDouble(Console.ReadLine());

            customer.Location = new Coordinate(latitude, longitude);

            DataSource.Customers.Add(customer);

            Console.WriteLine("\n" + customer);
        }

        /// <summary>
        /// Adds a Parcel to DataSource
        /// </summary>
        /// <param name="parcel"></param>
        /// <returns>PackageID</returns>
        public void AddParcel()
        {
            Parcel parcel = new Parcel();

            do
            {
                parcel.ID = rd.Next(100000000, 999999999);
            } while (DataSource.Parcels.Any(s => s.ID == parcel.ID));

            Console.WriteLine("Please enter the sender's ID: ");
            parcel.SenderID = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Please enter the target ID: ");
            parcel.TargetID = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Please enter the weight: \n0 for Light \n1 for Medium \n2 for Heavy");
            parcel.WeightCategory = (IDAL.DO.WeightCategories)Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Please enter the priority: \n0 for Regular \n1 for Fast \n2 for Emergency");
            parcel.Priority = (IDAL.DO.Priorities)Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Please enter the drone ID: ");
            parcel.DroneID = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter the scheduled date (mm/dd/yyyy): ");
            parcel.Scheduled = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter the picked up date (mm/dd/yyyy): ");
            parcel.PickedUp = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Please enter the time of assignment: ");
            parcel.SenderID = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter date delivered (mm/dd/yyyy): ");
            parcel.Delivered = DateTime.Parse(Console.ReadLine());

            DataSource.Parcels.Add(parcel);

            Console.WriteLine("\n" + parcel);
        }

        // Update Menu Functions

        /// <summary>
        /// Assigns a Parcel to a Drone
        /// </summary>
        public void AssignParcel()
        {
            Console.WriteLine("Enter the ID of the parcel to assign: ");
            //int parcelID = Convert.ToInt32(Console.ReadLine());
            Parcel parcel = GetParcel(Convert.ToInt32(Console.ReadLine()));

            Console.WriteLine("Enter the ID of the drone to be assigned: ");
            parcel.DroneID = Convert.ToInt32(Console.ReadLine());
            GetDrone(parcel.DroneID);                                           // Forces error if drone doesn't exist

            //GetDrone(parcel.DroneID).DroneStatus = DroneStatuses.Delivery;

            Parcels[Parcels.FindIndex(p => p.ID == parcel.ID)] = parcel;
        }

        /// <summary>
        /// Marks a Parcel as Collected by a Drone
        /// </summary>
        public void ParcelCollected()
        {
            Console.WriteLine("Enter the ID of the parcel to mark collected: ");

            Parcel parcel = GetParcel(Convert.ToInt32(Console.ReadLine()));

            parcel.PickedUp = DateTime.Now;

            //Console.Write("Enter the date collected (mm/dd/yyyy): ");

            Parcels[Parcels.FindIndex(p => p.ID == parcel.ID)] = parcel;
        }

        /// <summary>
        /// Marks a Parcel as Delivered
        /// </summary>
        public void ParcelDelivered()
        {
            Console.WriteLine("Enter the ID of the parcel to mark delivered: ");
            Parcel parcel = GetParcel(Convert.ToInt32(Console.ReadLine()));

            parcel.Delivered = DateTime.Now;

            //Console.Write("Enter the date delivered (mm/dd/yyyy): ");

            Parcels[Parcels.FindIndex(p => p.ID == parcel.ID)] = parcel;
        }

        /// <summary>
        /// Charges a Drone
        /// </summary>
        public void ChargeDrone()
        {
            DroneCharge dc = new DroneCharge();

            Console.WriteLine("Enter the ID of the drone to charge: ");
            dc.DroneID = Convert.ToInt32(Console.ReadLine());
            GetDrone(dc.DroneID);                                               // Forces error if drone doesn't exist

            Console.WriteLine("Enter the ID of the station to be assigned: ");
            dc.StationID = Convert.ToInt32(Console.ReadLine());
            GetStation(dc.StationID);                                           // Forces error if stations doesn't exist

            DroneCharges.Add(dc);
        }

        /// <summary>
        /// Releases a Drone from charging
        /// </summary>
        public void ReleaseDrone()
        {
            int droneID;

            Console.WriteLine("Enter the ID of the drone to release: ");
            droneID = Convert.ToInt32(Console.ReadLine());
            GetDrone(droneID);                                                  // Forces error if drone doesn't exist

            // Finds DroneCharge with droneID and removes it
            DroneCharges.RemoveAll(dc => dc.DroneID == droneID);
        }

        public double[] PowerConsumption()
        {
            return new double[] { Config.Free, Config.LightConsumption, Config.MediumConsumption, Config.HeavyConsumption, Config.ChargeRate };
        }
    }
}
