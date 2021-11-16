using IBL.BO;
using System;
using System.Collections.Generic;

namespace BL
{
    partial class BL : IBL.IBL
    {
        // gets EnroutePackage based off parcel ID
        private EnroutePackage GetEnroutePackage(int parcelID)
        {
            try
            {
                IDAL.DO.Parcel parcel = dalObject.GetParcel(parcelID);
            
                if (ParcelStatus(parcel) != Statuses.Collected)
                    throw new ObjectNotFound($"Parcel with ID: {parcelID} is not enroute");

                EnroutePackage package = new EnroutePackage()
                {
                    ID = parcelID,
                    Weight = (WieghtCategories)parcel.WeightCategory,
                    Priority = (Priorities)parcel.Priority,
                    Delivering = ParcelStatus(parcel) == Statuses.Collected
                };

                IDAL.DO.Customer sender = dalObject.GetCustomer(parcel.SenderID);
                package.Sender = ConvertToPackageCustomer(sender);
                package.CollectionLocation = CoordinateToLocation(sender.Location);

                IDAL.DO.Customer receiver = dalObject.GetCustomer(parcel.TargetID);
                package.Receiver = ConvertToPackageCustomer(receiver);
                package.DeliveryLocation = CoordinateToLocation(receiver.Location);

                package.DeliveryDistance = Distance(package.CollectionLocation, package.DeliveryLocation);
                return package;
            }
            catch (IDAL.DO.ObjectNotFound e)
            {
                throw new ObjectNotFound(e.Message);
            }
        }
    }
}
