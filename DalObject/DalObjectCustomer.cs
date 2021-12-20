using DO;
using System.Collections.Generic;
using static Dal.DataSource;

namespace Dal
{
    partial class DalObject : DalApi.IDal
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
        public void AddCustomer(int id, string name, string phoneNum, double latitude, double longitude)
        {
            if (Customers.Exists(c => c.ID == id))
                throw new ObjectAlreadyExists($"Customer with ID: {id} already exists.");

            Customer customer = new Customer()
            {
                ID = id,
                Name = name,
                Phone = phoneNum,
                Location = new DalApi.Util.Coordinate(latitude, longitude)
            };

            AddCustomer(customer);
        }

        /// <summary>
        /// Deletes a Customer from DataSource
        /// </summary>
        /// <param name="ID"></param>
        public void RemoveCustomer(int ID)
        {
            if (Customers.RemoveAll(c => c.ID == ID) == 0)
                throw new ObjectNotFound($"Customer with ID: {ID} doesn't exist");
        }
    }
}