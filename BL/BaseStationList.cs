namespace IBL.BO
{
    public class BaseStationList
    {
        public int ID { init; get; }
        public string Name { init; get; }
        public uint ChargingSlotsAvailable { init; get; }
        public uint ChargingSlotsOccupied { init; get; }
    }
}
