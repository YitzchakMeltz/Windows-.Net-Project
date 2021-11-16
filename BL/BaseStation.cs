using System.Collections.Generic;

namespace IBL.BO
{
    public class BaseStation
    {
        public int ID { init; get; }
        public string Name { init; get; }
        public Location Location { init; get; }
        public uint AvailableChargingSlots { get; set; }
        public List<ChargingDrone> ChargingDrones;
    }
}
