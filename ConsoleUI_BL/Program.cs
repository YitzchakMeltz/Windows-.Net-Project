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
            while (true) {
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
            }
}
