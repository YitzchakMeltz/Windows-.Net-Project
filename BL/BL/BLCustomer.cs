using BO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace BL
{
    partial class BL : BlApi.IBL
    {
        private PackageCustomer ConvertToPackageCustomer(Customer customer)
        {
            return new PackageCustomer()
            {
                ID = customer.ID,
                Name = customer.Name
            };
        }

        public void AddCustomer(int ID, string name, string phone, double longitude, double latitude, string password = "")
        {
            /*Customer customer = new Customer()
            {
                ID = ID,
                Name = name,
                Phone = phone,
                Location = new Location() { Latitude = latitude, Longitude = longitude },
                Password = System.Text.Encoding.UTF8.GetBytes(password)
            };*/

            dalObject.AddCustomer(ID, name, phone, latitude, longitude, password);
        }

        public void UpdateCustomer(int ID, string name = null, string phone = null, string password = null)
        {
            // Update DALCustomer
            try
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

                if (password != null)
                {
                    customer.Password = System.Text.Encoding.UTF8.GetBytes(password);
                }

                dalObject.RemoveCustomer(ID);
                dalObject.AddCustomer(customer);
            }
            catch (DO.ObjectNotFound e)
            {
                throw new ObjectNotFound(e.Message);
            }
        }

        public Customer GetCustomer(int customerID)
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
                    Password = dalCustomer.Password
                };

                foreach (PackageList package in ListPackages())
                {
                    if (package.Sender == customer.Name)
                    {
                        customer.Outgoing.Add(ConvertToCustomerPackage(package, package.Receiver));
                    }
                    else if (package.Receiver == customer.Name)
                    {
                        customer.Incoming.Add(ConvertToCustomerPackage(package, package.Sender));
                    }
                }

                return customer;
            }
            catch (DO.ObjectNotFound e)
            {
                throw new BO.ObjectNotFound(e.Message);
            }
        }

        public bool Login(int ID, byte[] password)
        {
            if (StructuralComparisons.StructuralEqualityComparer.Equals(SHA256.Create().ComputeHash(password), GetCustomer(ID).Password))
                return true;

            return false;
        }
        public string ShowStation(int stationID)
        {
            return GetStation(stationID).ToString();
        }

        public IEnumerable<CustomerList> ListCustomers()
        {
            IEnumerable<DO.Customer> dalCustomers = dalObject.GetCustomerList();

            List<CustomerList> blCustomers = new List<CustomerList>();
            foreach (DO.Customer dalCustomer in dalCustomers)
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
            }

            return blCustomers;
        }
    }
}
