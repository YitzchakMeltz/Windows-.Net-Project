using System.Collections.Generic;

<<<<<<< HEAD
namespace BL
=======
namespace IBL.BO
>>>>>>> parent of d4aee0b (change namespaces IDAL/IBL to DalAPI/BlApi)
{
    public class BaseStation
    {
        public int ID { init; get; }
        public string Name { init; get; }
        public Location Location { init; get; }
        public uint AvailableChargingSlots { get; set; }
        public List<ChargingDrone> ChargingDrones { init; get; }
        public override string ToString()
        {
            return $"ID: {ID}, Name: {Name}, Location: ({Location}), Available Charge Slots: {AvailableChargingSlots}";
        }
    }
}
