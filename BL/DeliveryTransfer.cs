namespace IBL.BO
{
    public class DeliveryTransfer
    {
        int ID;
        WieghtCategories Weight;
        Priorities Priority;
        bool Status;                // True: Delivering. False: Waiting
        Location CollectionLocation;
        Location DeliveryLocation;
        double DeliveryDistance;
    }
}
