<<<<<<< HEAD
﻿namespace BL
=======
﻿namespace IBL.BO
>>>>>>> parent of d4aee0b (change namespaces IDAL/IBL to DalAPI/BlApi)
{
    // package at customer
    public class CustomerPackage
    {
        public int ID { init; get; }
        public WeightCategories Weight { init; get; }
        public Priorities Priority { init; get; }
        public Statuses Status { init; get; }
        public PackageCustomer Customer { init; get; } // Customer on other end of delivery
    }
}
