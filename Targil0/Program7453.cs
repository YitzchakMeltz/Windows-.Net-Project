using System;

namespace Targil0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome7453();
            Welcome3510();
            Console.ReadKey(true);
        }

        static partial void Welcome3510();
        private static void Welcome7453()
        {
            Console.Write("Enter your name: ");
            Console.WriteLine("{0}, welcome to my first console application", Console.ReadLine());
            Console.Write("Press any key to continue . . . ");
        }
    }
}
