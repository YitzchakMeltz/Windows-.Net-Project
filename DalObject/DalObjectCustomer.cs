using DO;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using static Dal.DataSource;

namespace Dal
{
    partial class DalObject : DalApi.IDal
    {
        /// <summary>
        /// Adds a Customer to DataSource
        /// </summary>
        /// <param name="customer"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddCustomer(Customer customer)
        {
            if (!customer.Phone.All(char.IsDigit))
                throw new ObjectNotFound($"Phone number must only contain digits.");
            if (Customers.Exists(c => c.ID == customer.ID))
                throw new ObjectAlreadyExists($"Customer with ID: {customer.ID} already exists.");

            Customers.Add(customer);
        }


        /// <summary>
        /// Returns a Customer by its ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer GetCustomer(uint ID)
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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Customer> GetCustomerList()
        {
            return new List<Customer>(Customers);
        }


        /// <summary>
        /// Adds a Customer to DataSource
        /// </summary>
        /// <param name="customer"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddCustomer(uint id, string name, string phoneNum, double latitude, double longitude, string password = null)
        {
            if (latitude < -90 || latitude > 90 || longitude < -180 || longitude > 180)
                throw new InvalidInput("Location is invalid.");
            Customer customer = new Customer()
            {
                ID = id,
                Name = name,
                Phone = phoneNum,
                Location = new DalApi.Util.Coordinate(latitude, longitude),
                Password = password
            };

            AddCustomer(customer);
        }

        /// <summary>
        /// Deletes a Customer from DataSource
        /// </summary>
        /// <param name="ID"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void RemoveCustomer(uint ID)
        {
            if (Customers.RemoveAll(c => c.ID == ID) == 0)
                throw new ObjectNotFound($"Customer with ID: {ID} doesn't exist");
        }
    }
}