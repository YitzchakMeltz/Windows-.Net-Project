using IDAL.DO;
using IDAL.Util;
using System.Collections.Generic;
using static DalObject.DataSource;

namespace DalObject
{
    partial class DalObject : IDAL.IDal
    {
        /// <summary>
        /// Adds a Customer to DataSource
        /// </summary>
        /// <param name="customer"></param>
        public void AddCustomer(Customer customer)
        {
            if (Customers.Exists(c => c.ID == customer.ID))
                throw new ObjectAlreadyExists($"Customer with ID: {customer.ID} already exists.");

            Customers.Add(customer);
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
                throw new ObjectNotFound($"Customer with ID: {ID} not found.");
            }
        }

        /// <summary>
        /// Returns an array of all Customers
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Customer> GetCustomerList()
        {
            return new List<Customer>(Customers);
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
            } while (DataSource.Customers.Exists(s => s.ID == customer.ID));

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
    }
}