using System;
using IDAL.DO;

namespace ConsoleUI
{
    class Program
    {

        static IDAL.IDal dalObject = new DalObject.DalObject();
        private enum MainMenu { Exit, Add, Update, Show, List, Distance }
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Main Menu");
                Console.WriteLine(new String('-', 50));
                Console.WriteLine("0. Exit");
                Console.WriteLine("1. Add");
                Console.WriteLine("2. Update");
                Console.WriteLine("3. Show");
                Console.WriteLine("4. List");
                Console.WriteLine("5. Calculate Distance\n");
                Console.Write("Select an option: ");

                MainMenu selection;
                Enum.TryParse(Console.ReadLine(), out selection);
                Console.WriteLine();

                switch (selection)
                {
                    case MainMenu.Add:
                        MenuAdd();
                        break;

                    case MainMenu.Update:
                        MenuUpdate();
                        break;

                    case MainMenu.Show:
                        MenuShow();
                        break;

                    case MainMenu.List:
                        MenuList();
                        break;

                    case MainMenu.Distance:
                        MenuDistance();
                        break;

                    default:
                        return;
                }
            }
        }


        private enum AddMenu { Return, AddBaseStation, AddDrone, AddCustomer, AddParcel }
        private static void MenuAdd()
        {
            Console.WriteLine("Please choose what to add:");
            Console.WriteLine();
            Console.WriteLine("0. Return to Main Menu");
            Console.WriteLine("1. Add Base Station");
            Console.WriteLine("2. Add Drone");
            Console.WriteLine("3. Add Customer");
            Console.WriteLine("4. Add Parcel");
            Console.WriteLine();
            Console.Write("Select an option: ");

            AddMenu selection;
            Enum.TryParse(Console.ReadLine(), out selection);
            Console.WriteLine();

            switch (selection)
            {

                case AddMenu.AddBaseStation:
                    {
                        Console.WriteLine("Please enter the station name: ");
                        string name = Console.ReadLine();

                        Console.WriteLine("Please enter the amount of charge slots: ");
                        int chargeSlots = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Please enter the station coordinate latitude: ");
                        double latitude = Convert.ToDouble(Console.ReadLine());

                        Console.WriteLine("Please enter the station coordinate longitude: ");
                        double longitude = Convert.ToDouble(Console.ReadLine());

                        dalObject.AddStation(name, chargeSlots, latitude, longitude);
                        break;
                    }

                case AddMenu.AddDrone:
                    {
                        Console.WriteLine("Please enter the model name: ");
                        string model = Console.ReadLine();

                        Console.WriteLine("Please enter the weight: \n0 for Light \n1 for Medium \n2 for Heavy");
                        WeightCategories weight = (IDAL.DO.WeightCategories)Convert.ToInt32(Console.ReadLine());

                        dalObject.AddDrone(model, weight);
                        break;
                    }

                case AddMenu.AddCustomer:
                    {
                        Console.WriteLine("Please enter the customer's name: ");
                        string name = Console.ReadLine();

                        Console.WriteLine("Please enter the customer's phone number: ");
                        string phone = Console.ReadLine();

                        Console.WriteLine("Please enter the customer's coordinate latitude: ");
                        double latitude = Convert.ToDouble(Console.ReadLine());

                        Console.WriteLine("Please enter the customer's coordinate longitude: ");
                        double longitude = Convert.ToDouble(Console.ReadLine());

                        dalObject.AddCustomer(name, phone, latitude, longitude);
                        break;
                    }

                case AddMenu.AddParcel:
                    {
                        Console.WriteLine("Please enter the sender's ID: ");
                        int senderID = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Please enter the target ID: ");
                        int targetID = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Please enter the weight: \n0 for Light \n1 for Medium \n2 for Heavy");
                        WeightCategories weight = (IDAL.DO.WeightCategories)Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Please enter the priority: \n0 for Regular \n1 for Fast \n2 for Emergency");
                        Priorities priority = (Priorities)Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Please enter the drone ID: ");
                        int droneID = Convert.ToInt32(Console.ReadLine());

                        /*
                        Console.Write("Enter the scheduled date (mm/dd/yyyy): ");
                        parcel.Scheduled = DateTime.Parse(Console.ReadLine());

                        Console.Write("Enter the picked up date (mm/dd/yyyy): ");
                        parcel.PickedUp = DateTime.Parse(Console.ReadLine());

                        Console.WriteLine("Please enter the time of assignment: ");
                        parcel.SenderID = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Enter date delivered (mm/dd/yyyy): ");
                        parcel.Delivered = DateTime.Parse(Console.ReadLine());
                        */

                        dalObject.AddParcel(senderID, targetID, weight, priority, droneID);
                        break;
                    }

                default:
                    return;
            }

            Console.WriteLine();
        }

        private enum UpdateMenu { Return, AssignParcel, ParcelCollected, ParcelDelivered, ChargeDrone, ReleaseDrone }
        private static void MenuUpdate()
        {
            Console.WriteLine("Please choose what to update:\n");
            Console.WriteLine("0. Return to Main Menu");
            Console.WriteLine("1. Assign Parcel to Drone");
            Console.WriteLine("2. Mark Parcel as Collected");
            Console.WriteLine("3. Mark Parcel as Delivered");
            Console.WriteLine("4. Assign Drone to Base Station");
            Console.WriteLine("5. Release Drone from Base Station\n");
            Console.Write("Select an option: ");

            UpdateMenu selection;
            Enum.TryParse(Console.ReadLine(), out selection);
            Console.WriteLine();

            int droneID;
            switch (selection)
            {
                case UpdateMenu.AssignParcel:
                    Console.WriteLine("Enter the ID of the parcel to assign: ");
                    int parcelID = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Enter the ID of the drone to be assigned: ");
                    droneID = Convert.ToInt32(Console.ReadLine());

                    dalObject.AssignParcel(parcelID, droneID);
                    break;

                case UpdateMenu.ParcelCollected:
                    Console.WriteLine("Enter the ID of the parcel to mark collected: ");
                    dalObject.ParcelCollected(Convert.ToInt32(Console.ReadLine()));
                    break;

                case UpdateMenu.ParcelDelivered:
                    Console.WriteLine("Enter the ID of the parcel to mark delivered: ");
                    dalObject.ParcelDelivered(Convert.ToInt32(Console.ReadLine()));
                    break;

                case UpdateMenu.ChargeDrone:

                    Console.WriteLine("Enter the ID of the drone to charge: ");
                    droneID = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Enter the ID of the station to be assigned: ");
                    int stationID = Convert.ToInt32(Console.ReadLine());

                    dalObject.ChargeDrone(droneID, stationID);
                    break;

                case UpdateMenu.ReleaseDrone:
                    Console.WriteLine("Enter the ID of the drone to release: ");
                    dalObject.ReleaseDrone(Convert.ToInt32(Console.ReadLine()));
                    break;

                default:
                    return;
            }

            Console.WriteLine();
        }

        private enum ShowMenu { Return, BaseStation, Drone, Customer, Parcel }
        private static void MenuShow()
        {
            Console.WriteLine("Please choose what to display:\n");
            Console.WriteLine("0. Return to Main Menu");
            Console.WriteLine("1. Base Station");
            Console.WriteLine("2. Drone");
            Console.WriteLine("3. Customer");
            Console.WriteLine("4. Parcel\n");
            Console.Write("Select an option: ");

            ShowMenu selection;
            Enum.TryParse(Console.ReadLine(), out selection);
            Console.WriteLine();

            Console.Write($"Enter {Enum.GetName(selection)} ID: ");
            int ID;
            int.TryParse(Console.ReadLine(), out ID);
            Console.WriteLine();

            switch (selection)
            {
                case ShowMenu.BaseStation:
                    Console.WriteLine(dalObject.GetStation(ID));
                    break;

                case ShowMenu.Drone:
                    Console.WriteLine(dalObject.GetDrone(ID));
                    break;

                case ShowMenu.Customer:
                    Console.WriteLine(dalObject.GetCustomer(ID));
                    break;

                case ShowMenu.Parcel:
                    Console.WriteLine(dalObject.GetParcel(ID));
                    break;

                default:
                    return;
            }

            Console.WriteLine();
        }

        private enum ListMenu { Return, BaseStations, Drones, Customers, Parcels, UnassignedParcels, AvailableBaseStations }
        private static void MenuList()
        {
            Console.WriteLine("Please choose what to list:\n");
            Console.WriteLine("0. Return to Main Menu");
            Console.WriteLine("1. Base Stations");
            Console.WriteLine("2. Drones");
            Console.WriteLine("3. Customers");
            Console.WriteLine("4. Parcels");
            Console.WriteLine("5. Unassigned Parcels");
            Console.WriteLine("6. Available Base Stations\n");
            Console.Write("Select an option: ");

            ListMenu selection;
            Enum.TryParse(Console.ReadLine(), out selection);
            Console.WriteLine();

            switch (selection)
            {
                case ListMenu.BaseStations:
                    foreach (Station s in dalObject.GetStationList())
                    {
                        Console.WriteLine(s);
                    }
                    break;

                case ListMenu.Drones:
                    foreach (Drone d in dalObject.GetDroneList())
                    {
                        Console.WriteLine(d);
                    }
                    break;

                case ListMenu.Customers:
                    foreach (Customer c in dalObject.GetCustomerList())
                    {
                        Console.WriteLine(c);
                    }
                    break;

                case ListMenu.Parcels:
                    foreach (Parcel p in dalObject.GetParcelList())
                    {
                        Console.WriteLine(p);
                    }
                    break;

                case ListMenu.UnassignedParcels:
                    foreach (Parcel p in dalObject.GetUnassignedParcelList())
                    {
                        Console.WriteLine(p);
                    }
                    break;

                case ListMenu.AvailableBaseStations:
                    foreach (Station s in dalObject.GetAvailableStationList())
                    {
                        Console.WriteLine(s);
                    }
                    break;

                default:
                    return;
            }

            Console.WriteLine();
        }

        private enum DistanceMenu { Return, BaseStation, Customer }
        private static void MenuDistance()
        {
            Console.WriteLine("Please choose which location you would like to get the distance from:\n");
            Console.WriteLine("0. Return to Main Menu");
            Console.WriteLine("1. Base Station");
            Console.WriteLine("2. Customer\n");
            Console.Write("Select an option: ");

            DistanceMenu selection;
            Enum.TryParse(Console.ReadLine(), out selection);
            Console.WriteLine();

            if (selection == DistanceMenu.Return) return;

            Console.Write($"Enter {Enum.GetName(selection)} ID: ");
            int ID;
            int.TryParse(Console.ReadLine(), out ID);
            Console.WriteLine("Enter your location");
            Console.Write("Latitude: ");
            double latitude = Double.Parse(Console.ReadLine());
            Console.Write("Longitude: ");
            double longitude = Double.Parse(Console.ReadLine());
            IDAL.Util.Coordinate location = new IDAL.Util.Coordinate(latitude, longitude);
            Console.WriteLine();

            switch (selection)
            {
                case DistanceMenu.BaseStation:
                    Console.WriteLine(dalObject.GetStation(ID).Location.DistanceTo(location) + " meters.");
                    break;

                case DistanceMenu.Customer:
                    Console.WriteLine(dalObject.GetStation(ID).Location.DistanceTo(location) + " meters.");
                    break;
            }

            Console.WriteLine();
        }
    }
}
