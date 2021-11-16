namespace IBL.BO
{
    // package at customer
    public class DeliveredPackage
    {
        int ID;
        WieghtCategories Weight;
        Priorities Priority;
        Statuses Status;
        PackageCustomer Customer;  // Customer on other end of delivery
    }
}
