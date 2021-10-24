using System;
using IDAL.DO;

namespace ConsoleUI
{
    class Program
    {
        enum MenuSelection
        {
            Exit,
            Add,
            Update,
            Show,
            List
        }
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Main Menu");
                Console.WriteLine(new String('-',50));
                Console.WriteLine("0. Exit");
                Console.WriteLine("1. Add");
                Console.WriteLine("2. Update");
                Console.WriteLine("3. Show");
                Console.WriteLine("4. List");
                Console.WriteLine();
                Console.Write("Select an option: ");
                MenuSelection selection;
                Enum.TryParse(Console.ReadLine(), out selection);
                Console.WriteLine();

                switch (selection)
                {
                    case MenuSelection.Add:
                        MenuAdd();
                        break;

                    case MenuSelection.Update:
                        MenuUpdate();
                        break;

                    case MenuSelection.Show:
                        MenuShow();
                        break;

                    case MenuSelection.List:
                        MenuList();
                        break;

                    default:
                        break;
                }
            }
        }

        private static void MenuAdd()
        {
            Console.WriteLine("Please choose what to add:");
            Console.WriteLine();
            Console.WriteLine("0. Return to Main Menu");
            Console.WriteLine("1. Add Base Station");
            Console.WriteLine("2. Add Drone");
            Console.WriteLine("3. Add Customer");
            Console.WriteLine("4. Add Parcel");
        }

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
        }

        private static void MenuShow()
        {
            Console.WriteLine("Please choose what to display:");
            Console.WriteLine();
            Console.WriteLine("0. Return to Main Menu");
            Console.WriteLine("1. Base Station");
            Console.WriteLine("2. Drone");
            Console.WriteLine("3. Customer");
            Console.WriteLine("4. Parcel");
        }

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
        }
    }
}
