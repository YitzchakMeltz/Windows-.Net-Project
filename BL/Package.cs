using System;

namespace IBL.BO
{
    public class Package
    {
        int ID;
        DeliveryCustomer Sender;
        DeliveryCustomer Receiver;
        WieghtCategories Weight;
        Priorities Priority;
        Drone Drone;
        DateTime Creation;
        int AssigningTime;
        DateTime Collection;
        DateTime Delivery;
    }
}
