<<<<<<< HEAD
﻿namespace BL
=======
﻿namespace IBL.BO
>>>>>>> parent of d4aee0b (change namespaces IDAL/IBL to DalAPI/BlApi)
{
    // drone in charge
    public class ChargingDrone
    {
        public int ID { init; get; }
        public double Battery { init; get; }
        public override string ToString()
        {
            return $"ID: {ID}, Battery: {Battery}";
        }
    }
}
