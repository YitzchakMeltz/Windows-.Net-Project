using IBL.BO;
using System;
using System.Collections.Generic;

namespace BL
{
    partial class BL : IBL.IBL
    {
        private PackageCustomer ConvertToPackageCustomer(Customer customer)
        {
            return new PackageCustomer()
            {
                ID = customer.ID,
                Name = customer.Name
            };
        }

        public void AddCustomer(int ID, string name, string phone, double longitude, double latitude)
        {
            Customer customer = new Customer()
            {
                ID = ID,
                Name = name,
                Phone = phone,
                Location = new Location() { Latitude = latitude, Longitude = longitude }
            };

            dalObject.AddCustomer(ID, name, phone, latitude, longitude);
        }

        public void UpdateCustomer(int ID, string name = null, string phone = null)
        {
            // Update DALCustomer
            try
            {
                IDAL.DO.Customer customer = dalObject.GetCustomer(ID);

                if (name != null)
                {
                    customer.Name = name;
                }

                if (phone != null)
                {
                    customer.Phone = phone;
                }

                dalObject.RemoveCustomer(ID);
                dalObject.AddCustomer(customer);
            }
            catch (IDAL.DO.ObjectNotFound e)
            {
                throw new ObjectNotFound(e.Message);
            }
        }

        public Customer GetCustomer(int customerID)
        {
            try
            {
                IDAL.DO.Customer dalCustomer = dalObject.GetCustomer(customerID);

                Customer customer = new Customer()
                {
                    ID = customerID,
                    Name = dalCustomer.Name,
                    Phone = dalCustomer.Phone,
                    Location = CoordinateToLocation(dalCustomer.Location)
                };

                foreach (PackageList package in ListPackages())
                {
                    if (package.Sender == customer.Name)
                        customer.Outgoing.Add(ConvertToCustomerPackage(package, package.Receiver));
                    else if (package.Receiver == customer.Name)
                    {
                        customer.Incoming.Add(ConvertToCustomerPackage(package, package.Sender));
                    }
                }

                return customer;
            }
            catch (IDAL.DO.ObjectNotFound e)
            {
                throw new IBL.BO.ObjectNotFound(e.Message);
            }
        }

        public IEnumerable<CustomerList> ListCustomers()
        {
            IEnumerable<IDAL.DO.Customer> dalCustomers = dalObject.GetCustomerList();

            List<CustomerList> blCustomers = new List<CustomerList>();
            foreach (IDAL.DO.Customer dalCustomer in dalCustomers)
            {
                blCustomers.Add(new CustomerList() { ID = dalCustomer.ID, Name = dalCustomer.Name, Phone = dalCustomer.Phone, PackagesSentDelivered });
            }

            return blCustomers;
        }
    }
}
