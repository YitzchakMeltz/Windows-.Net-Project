<<<<<<< HEAD
﻿namespace BL
=======
﻿namespace IBL.BO
>>>>>>> parent of d4aee0b (change namespaces IDAL/IBL to DalAPI/BlApi)
{
    public class BaseStationList
    {
        public int ID { init; get; }
        public string Name { init; get; }
        public uint ChargingSlotsAvailable { init; get; }
        public uint ChargingSlotsOccupied { init; get; }

        public override string ToString()
        {
            return $"ID: {ID}, Name: {Name}, Charging Slots Used: {ChargingSlotsOccupied}/{ChargingSlotsAvailable}";
        }
    }
}
