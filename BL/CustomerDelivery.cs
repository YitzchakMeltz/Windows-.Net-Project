namespace IBL.BO
{
    public class CustomerDelivery
    {
        int ID;
        WieghtCategories Weight;
        Priorities Priority;
        Statuses Status;
        DeliveryCustomer Customer;  // Customer on other end of delivery
    }
}
