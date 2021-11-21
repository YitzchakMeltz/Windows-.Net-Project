using System;
using IBL;

namespace ConsoleUI_BL
{
    class Program
    {
        static IBL.IBL bl = new BL.BL();

        private enum MainMenu { Exit, Add, Update, Show, List }
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

                    default:
                        return;
                }
            }
        }

        private enum AddMenu { Return, AddBaseStation, AddDrone, AddCustomer, AddPackage }
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
                    Console.WriteLine("Please enter the Station ID: ");
                    int stationID = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Please enter the station name: ");
                    string name = Console.ReadLine();

                    Console.WriteLine("Please enter the station coordinate latitude: ");
                    double latitude = Convert.ToDouble(Console.ReadLine());

                    Console.WriteLine("Please enter the station coordinate longitude: ");
                    double longitude = Convert.ToDouble(Console.ReadLine());

                    Console.WriteLine("Please enter the total amount of available charge slots: ");
                    int chargeSlots = Convert.ToInt32(Console.ReadLine());

                    bl.AddStation(stationID, name, latitude, longitude, chargeSlots);
                    break;

                case AddMenu.AddDrone:
                    Console.WriteLine("Please enter the Drone manufacture ID: ");
                    int droneID = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Please enter the drone model: ");
                    string model = Console.ReadLine();

                    Console.WriteLine("Please enter the weight: ");
                    Console.WriteLine("0 Light ");
                    Console.WriteLine("1 Medium ");
                    Console.WriteLine("2 Heavy ");
                    IBL.BO.WieghtCategories weight = (IBL.BO.WieghtCategories)Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Please enter the station ID for first charging: ");
                    int firstChargingStationID = Convert.ToInt32(Console.ReadLine());

                    bl.AddDrone(droneID, model, weight, firstChargingStationID);
                    break;

                case AddMenu.AddCustomer:
                    Console.WriteLine("Please enter the Customer ID: ");
                    int customerID = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Please enter the customer name: ");
                    string customerName = Console.ReadLine();

                    Console.WriteLine("Please enter the customer phone number: ");
                    string customerPhoneNo = Console.ReadLine();

                    Console.WriteLine("Please enter the customer coordinate latitude: ");
                    double customerLatitude = Convert.ToDouble(Console.ReadLine());

                    Console.WriteLine("Please enter the customer coordinate longitude: ");
                    double customerLongitude = Convert.ToDouble(Console.ReadLine());

                    bl.AddCustomer(customerID, customerName, customerPhoneNo, customerLatitude, customerLongitude);
                    break;

                case AddMenu.AddPackage:
                    Console.WriteLine("Please enter the Sender ID: ");
                    int senderID = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Please enter the Receiver ID: ");
                    int receiverID = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Please enter the weight of the package: ");
                    Console.WriteLine("0 Light ");
                    Console.WriteLine("1 Medium ");
                    Console.WriteLine("2 Heavy ");
                    IBL.BO.WieghtCategories packageWeight = (IBL.BO.WieghtCategories)Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Please enter the priority of the package: ");
                    Console.WriteLine("0 Regular ");
                    Console.WriteLine("1 Fast ");
                    Console.WriteLine("2 Emergency ");
                    IBL.BO.Priorities packagePriority = (IBL.BO.Priorities)Convert.ToInt32(Console.ReadLine());

                    bl.AddPackage(senderID, receiverID, packageWeight, packagePriority);
                    break;
            }
        }

        private enum UpdateMenu { Return, UpdateDrone, UpdateStation, UpdateCustomer, UpdateDroneCharge }
        private static void MenuUpdate()
        {
            Console.WriteLine("Please choose what to update:");
            Console.WriteLine();
            Console.WriteLine("0. Return to Main Menu");
            Console.WriteLine("1. Update Drone");
            Console.WriteLine("2. Update Station");
            Console.WriteLine("3. Update Customer");
            Console.WriteLine("4. Send a drone to be charged");
            Console.WriteLine();
            Console.Write("Select an option: ");

            UpdateMenu selection;
            Enum.TryParse(Console.ReadLine(), out selection);
            Console.WriteLine();

            switch (selection)
            {
                case UpdateMenu.UpdateDrone:
                    Console.WriteLine("Please enter the Drone ID: ");
                    int droneID = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Please enter the new drone model: ");
                    string model = Console.ReadLine();

                    bl.UpdateDrone(droneID, model);
                    break;

                case UpdateMenu.UpdateStation:
                    Console.WriteLine("Please enter the Station ID: ");
                    int stationID = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("1 Update Station Name \n2 Update Total Number of Availible Slots \n3 Update Both");
                    int updateOption = Convert.ToInt32(Console.ReadLine());

                    switch(updateOption)
                    {
                        case 1:
                            Console.WriteLine("Please enter the station name: ");
                            string newName = Console.ReadLine();
                            bl.UpdateStation(stationID,newName);
                            break;
                        case 2:
                            Console.WriteLine("Please enter the total amount of available charge slots: ");
                            int chargeSlots = Convert.ToInt32(Console.ReadLine());
                            bl.UpdateStation(stationID, null, chargeSlots);
                            break;
                        case 3:
                            Console.WriteLine("Please enter the station name: ");
                            newName = Console.ReadLine();

                            Console.WriteLine("Please enter the total amount of available charge slots: ");
                            chargeSlots = Convert.ToInt32(Console.ReadLine());

                            bl.UpdateStation(stationID, newName, chargeSlots);
                            break;
                    }
                    break;

                case UpdateMenu.UpdateCustomer:
                    Console.WriteLine("Please enter the Station ID: ");
                    int customerID = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("1 Update Customer Name \n2 Update Customer Phone Number \n3 Update Both");
                    updateOption = Convert.ToInt32(Console.ReadLine());

                    switch (updateOption)
                    {
                        case 1:
                            Console.WriteLine("Please enter the customer name: ");
                            string newName = Console.ReadLine();
                            bl.UpdateCustomer(customerID, newName);
                            break;
                        case 2:
                            Console.WriteLine("Please enter the new phone number: ");
                            string newPhoneNumber = Console.ReadLine();
                            bl.UpdateCustomer(customerID, null, newPhoneNumber);
                            break;
                        case 3:
                            Console.WriteLine("Please enter the customer name: ");
                            newName = Console.ReadLine();

                            Console.WriteLine("Please enter the new phone number: ");
                            newPhoneNumber = Console.ReadLine();

                            bl.UpdateCustomer(customerID, newName, newPhoneNumber);
                            break;
                    }
                    break;

                case UpdateMenu.UpdateDroneCharge:
                    Console.WriteLine("Please enter the Drone ID: ");
                    droneID = Convert.ToInt32(Console.ReadLine());

                    bl.ChargeDrone(droneID);
                    break;

                default:
                    return;
            }
        }

        private enum ShowMenu { Return, ShowBaseStation, ShowDrone, ShowCustomer, ShowPackage }
        private static void MenuShow()
        {
            Console.WriteLine("Please choose what to show:");
            Console.WriteLine();
            Console.WriteLine("0. Return to Main Menu");
            Console.WriteLine("1. Show Base Station");
            Console.WriteLine("2. Show Drone");
            Console.WriteLine("3. Show Customer");
            Console.WriteLine("4. Show Parcel");
            Console.WriteLine();
            Console.Write("Select an option: ");

            ShowMenu selection;
            Enum.TryParse(Console.ReadLine(), out selection);
            Console.WriteLine();

            switch (selection)
            {
                case ShowMenu.ShowBaseStation:
                    Console.WriteLine("Please enter the Station ID: ");
                    int stationID = Convert.ToInt32(Console.ReadLine());

                    bl.ShowStation(stationID);
                    break;

                case ShowMenu.ShowDrone:
                    Console.WriteLine("Please enter the Drone ID: ");
                    int droneID = Convert.ToInt32(Console.ReadLine());

                    bl.ShowDrone(droneID);
                    break;

                case ShowMenu.ShowCustomer:
                    Console.WriteLine("Please enter the Customer ID: ");
                    int customerID = Convert.ToInt32(Console.ReadLine());

                    bl.ShowCustomer(customerID);
                    break;

                case ShowMenu.ShowPackage:
                    Console.WriteLine("Please enter the Package ID: ");
                    int packageID = Convert.ToInt32(Console.ReadLine());

                    bl.ShowPackage(packageID);
                    break;

                default:
                    return;
            }
        }

        private enum ListMenu { Return, ListBaseStations, ListDrones, ListCustomers, ListPackages }
        private static void MenuList()
        {
            Console.WriteLine("Please choose what to list:");
            Console.WriteLine();
            Console.WriteLine("0. Return to Main Menu");
            Console.WriteLine("1. List Base Stations");
            Console.WriteLine("2. List Drones");
            Console.WriteLine("3. List Customers");
            Console.WriteLine("4. List Parcels");
            Console.WriteLine();
            Console.Write("Select an option: ");

            ListMenu selection;
            Enum.TryParse(Console.ReadLine(), out selection);
            Console.WriteLine();

            switch (selection)
            {
                case ListMenu.ListBaseStations:
                    bl.ListStations();
                    break;
                case ListMenu.ListDrones:
                    bl.ListDrones();
                    break;
                case ListMenu.ListCustomers:
                    bl.ListCustomers();
                    break;
                case ListMenu.ListPackages:
                    bl.ListPackages();
                    break;
                default:
                    return;
            }
        }
    }
}
