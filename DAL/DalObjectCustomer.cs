using IDAL.DO;
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
            Customer c = Customers.Find(c => c.ID == ID);

            if (c.Equals(default(Customer)))
                throw new ObjectNotFound($"Customer with ID: {ID} not found.");

            return c;
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
        public void AddCustomer(string name, string phoneNum, double latitude, double longitude)
        {
            Customer customer = new Customer()
            {
                Name = name,
                Phone = phoneNum,
                Location = new IDAL.Util.Coordinate(latitude, longitude)
            };

            do
            {
                customer.ID = rd.Next(100000000, 999999999);
            } while (DataSource.Customers.Exists(s => s.ID == customer.ID));

            AddCustomer(customer);
        }
    }
}