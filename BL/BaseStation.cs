using System.Collections.Generic;

namespace IBL.BO
{
    public class BaseStation
    {
        int ID;
        string Name;
        Location Location;
        uint AvailableChargingSlots;
        List<ChargingDrone> ChargingDrones;
    }
}
