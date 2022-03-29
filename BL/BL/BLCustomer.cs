using BO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace BL
{
    partial class BL : BlApi.IBL
    {
        #region private functions
        private PackageCustomer ConvertToPackageCustomer(Customer customer)
        {
            return new PackageCustomer()
            {
                ID = customer.ID,
                Name = customer.Name
            };
        }
        #endregion

        /// <summary>
        /// Adds a Customer
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <param name="password"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddCustomer(uint ID, string name, string phone, double longitude, double latitude, string password = null)
        {
            CheckPassword(password);

            dalObject.AddCustomer(ID, name, phone, latitude, longitude, password);
        }

        /// <summary>
        /// Retrieves a Customer by ID
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        /// <exception cref="BO.ObjectNotFound"></exception>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer GetCustomer(uint customerID)
        {
            try
            {
                DO.Customer dalCustomer = dalObject.GetCustomer(customerID);

                Customer customer = new Customer()
                {
                    ID = customerID,
                    Name = dalCustomer.Name,
                    Phone = dalCustomer.Phone,
                    Location = CoordinateToLocation(dalCustomer.Location),
                    Incoming = new List<CustomerPackage>(),
                    Outgoing = new List<CustomerPackage>(),
                    PasswordHash = dalCustomer.PasswordHash
                };

                ListPackages().ToList().ForEach(package =>
                {
                    if (package.Sender == customer.Name)
                    {
                        customer.Outgoing.Add(ConvertToCustomerPackage(package, package.Receiver));
                    }
                    if (package.Receiver == customer.Name)
                    {
                        customer.Incoming.Add(ConvertToCustomerPackage(package, package.Sender));
                    }
                });

                return customer;
            }
            catch (DO.ObjectNotFound e)
            {
                throw new BO.ObjectNotFound(e.Message);
            }
        }

        /// <summary>
        /// Updates a Customer
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="password"></param>
        /// <exception cref="ObjectNotFound"></exception>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateCustomer(uint ID, string name = null, string phone = null, string[] passwordArray = null)
        {
            // Update DALCustomer
            try
            {
                lock (dalObject)
                {
                    DO.Customer customer = dalObject.GetCustomer(ID);

                    if (name != null)
                    {
                        customer.Name = name;
                    }

                    if (phone != null)
                    {
                        customer.Phone = phone;
                    }

                    if (passwordArray != null)
                    {
                        if (!Login(ID, System.Text.Encoding.UTF8.GetBytes(passwordArray[0])))
                            throw new BO.SecurityError("Original password is incorrect!");
                        if (passwordArray[1] != passwordArray[2])
                            throw new BO.SecurityError("New passwords must match!");
                        if (passwordArray[0] == passwordArray[1])
                            throw new BO.SecurityError("New password cannot be the same as the original password!");

                        CheckPassword(passwordArray[1]);

                        customer.Password = passwordArray[1];
                    }

                    dalObject.RemoveCustomer(ID);
                    dalObject.AddCustomer(customer);
                }
            }
            catch (DO.ObjectNotFound e)
            {
                throw new ObjectNotFound(e.Message);
            }
        }

        private void CheckPassword(string p)
        {
            Regex regex = new Regex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[-+_!@#$%^&*., ?]).+$");
            if (p.Length < 8) throw new BO.SecurityError("Password must be at least 8 characters!");
            if (!regex.IsMatch(p)) throw new BO.SecurityError("Password must contain at least 1 upper & lowercase letter and a special character!");
        }

        /// <summary>
        /// Compares entered password hash to stored hash
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="password"></param>
        /// <returns>True if passwords match</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool Login(uint ID, byte[] password)
        {
            if (GetCustomer(ID).PasswordHash is null || SHA256.Create().ComputeHash(password).SequenceEqual(GetCustomer(ID).PasswordHash))
                return true;

            return false;
        }

        /*[MethodImpl(MethodImplOptions.Synchronized)]
        public string ShowStation(int stationID)
        {
            return GetStation(stationID).ToString();
        }*/

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<CustomerList> ListCustomers()
        {
            lock (dalObject)
            {
                IEnumerable<DO.Customer> dalCustomers = dalObject.GetCustomerList().Where(c => c.ID != 0);

                List<CustomerList> blCustomers = new List<CustomerList>();
                dalCustomers.ToList().ForEach(dalCustomer =>
                {
                    Customer customer = GetCustomer(dalCustomer.ID);
                    blCustomers.Add(new CustomerList()
                    {
                        ID = dalCustomer.ID,
                        Name = dalCustomer.Name,
                        Phone = dalCustomer.Phone,
                        PackagesSentDelivered = (uint)customer.Outgoing.FindAll(p => p.Status == Statuses.Delivered).Count,
                        PackagesSentNotDelivered = (uint)customer.Outgoing.FindAll(p => p.Status != Statuses.Delivered).Count,
                        PackagesRecieved = (uint)customer.Incoming.FindAll(p => p.Status == Statuses.Delivered).Count,
                        PackagesExpected = (uint)customer.Incoming.FindAll(p => p.Status != Statuses.Delivered).Count
                    });
                });


                return blCustomers;
            }
        }
    }
}
