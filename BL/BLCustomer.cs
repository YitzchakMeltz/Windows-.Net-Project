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
    }
}
