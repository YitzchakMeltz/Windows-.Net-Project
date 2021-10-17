using System;
using IDAL.DO;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Main Menu");
                Console.WriteLine(new String('-',50));
                Console.WriteLine("1. Add");
                Console.WriteLine("2. Update");
                Console.WriteLine("3. Show");
                Console.WriteLine("4. List");
                Console.WriteLine("5. Exit");
                Console.WriteLine();
                Console.Write("Select an option: ");
                Console.ReadLine();
                Console.WriteLine();
            }
        }
    }
}
