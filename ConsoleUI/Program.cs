﻿using System;
using IDAL.DO;

namespace ConsoleUI
{
    class Program
    {

        static DalObject.DalObject dalObject = new DalObject.DalObject();
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
                Console.WriteLine();
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
                        break;
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
                    dalObject.AddStation();
                    break;

                case AddMenu.AddDrone:
                    dalObject.AddDrone();
                    break;

                case AddMenu.AddCustomer:
                    dalObject.AddCustomer();
                    break;

                case AddMenu.AddParcel:
                    dalObject.AddParcel();
                    break;
            }
        }

        private enum UpdateMenu { Return, AssignParcel, ParcelCollected, ParcelDelivered, ChargeDrone, ReleaseDrone }
        private static void MenuUpdate()
        {
            Console.WriteLine("Please choose what to update:");
            Console.WriteLine();
            Console.WriteLine("0. Return to Main Menu");
            Console.WriteLine("1. Assign Parcel to Drone");
            Console.WriteLine("2. Mark Parcel as Collected");
            Console.WriteLine("3. Mark Parcel as Delivered");
            Console.WriteLine("4. Assign Drone to Base Station");
            Console.WriteLine("5. Release Drone from Base Station");
            Console.WriteLine();
            Console.Write("Select an option: ");

            UpdateMenu selection;
            Enum.TryParse(Console.ReadLine(), out selection);
            Console.WriteLine();

            switch (selection)
            {
                case UpdateMenu.AssignParcel:
                    dalObject.AssignParcel();
                    break;

                case UpdateMenu.ParcelCollected:
                    dalObject.ParcelCollected();
                    break;

                case UpdateMenu.ParcelDelivered:
                    dalObject.ParcelDelivered();
                    break;

                case UpdateMenu.ChargeDrone:
                    dalObject.ChargeDrone();
                    break;

                case UpdateMenu.ReleaseDrone:
                    dalObject.ReleaseDrone();
                    break;
            }
        }

        private enum ShowMenu { Return, BaseStation, Drone, Customer, Parcel }
        private static void MenuShow()
        {
            Console.WriteLine("Please choose what to display:");
            Console.WriteLine();
            Console.WriteLine("0. Return to Main Menu");
            Console.WriteLine("1. Base Station");
            Console.WriteLine("2. Drone");
            Console.WriteLine("3. Customer");
            Console.WriteLine("4. Parcel");
            Console.WriteLine();
            Console.Write("Select an option: ");

            ShowMenu selection;
            Enum.TryParse(Console.ReadLine(), out selection);
            Console.WriteLine();

            Console.Write("Type entity ID: ");
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
            }
        }

        private enum ListMenu { Return, BaseStations, Drones, Customers, Parcels, UnassignedParcels, AvailableBaseStations }
        private static void MenuList()
        {
            Console.WriteLine("Please choose what to list:");
            Console.WriteLine();
            Console.WriteLine("0. Return to Main Menu");
            Console.WriteLine("1. Base Stations");
            Console.WriteLine("2. Drones");
            Console.WriteLine("3. Customers");
            Console.WriteLine("4. Parcels");
            Console.WriteLine("5. Unassigned Parcels");
            Console.WriteLine("6. Available Base Stations");
            Console.WriteLine();
            Console.Write("Select an option: ");

            ListMenu selection;
            Enum.TryParse(Console.ReadLine(), out selection);
            Console.WriteLine();

            switch (selection)
            {
                case ListMenu.BaseStations:
                    Console.WriteLine(dalObject.GetStationList());
                    break;

                case ListMenu.Drones:
                    Console.WriteLine(dalObject.GetDroneList());
                    break;

                case ListMenu.Customers:
                    Console.WriteLine(dalObject.GetCustomerList());
                    break;

                case ListMenu.Parcels:
                    Console.WriteLine(dalObject.GetParcelList());
                    break;

                case ListMenu.UnassignedParcels:
                    Console.WriteLine(dalObject.GetUnassignedParcelList());
                    break;

                case ListMenu.AvailableBaseStations:
                    Console.WriteLine(dalObject.GetAvailableStationList());
                    break;
            }
        }
    }
}
