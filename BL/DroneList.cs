namespace IBL.BO
{
    public class DroneList
    {
        int ID;
        string Model;
        WieghtCategories Weight;
        double Battery;
        Statuses Status;
        Location Location;
        uint? PackageID; // Can be null
    }
}
