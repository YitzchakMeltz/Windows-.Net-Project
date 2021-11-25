using IBL.BO;
using System;
using System.Collections.Generic;

namespace BL
{
    partial class BL : IBL.IBL
    {
        private PackageCustomer ConvertToPackageCustomer(IDAL.DO.Customer customer)
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
                Location = new Location() { Longitude = longitude, Latitude = latitude }
            };
        }

        public void UpdateCustomer(int ID, string name = null, string phone = null)
        {
            // Update DALCustomer
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
    }
}
