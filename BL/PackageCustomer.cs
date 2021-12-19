<<<<<<< HEAD
﻿namespace BL
=======
﻿namespace IBL.BO
>>>>>>> parent of d4aee0b (change namespaces IDAL/IBL to DalAPI/BlApi)
{
    public class PackageCustomer
    {
        public int ID { init; get; }
        public string Name { init; get; }
        public override string ToString()
        {
            return $"ID: {ID}, Name: {Name}";
        }
    }
}
