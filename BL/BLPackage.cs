using IBL.BO;
using IDAL;
using System;
using System.Collections.Generic;

namespace BL
{
    partial class BL : IBL.IBL
    {
        private Statuses ParcelStatus(IDAL.DO.Parcel p)
        {
            if (p.AssignmentTime.Equals(default(TimeSpan)))
            {
                return Statuses.Created;
            }
            if (p.PickedUp.Equals(default(DateTime)))
            {
                return Statuses.Assigned;
            }
            if (p.Delivered.Equals(default(DateTime)))
            {
                return Statuses.Collected;
            }
            return Statuses.Delivered;
        }

        private Package FromParcel(IDAL.DO.Parcel parcel)
        {
            Package package = new Package()
            {
                ID = parcel.ID,
                AssignmentTime = parcel.Scheduled.Add(parcel.AssignmentTime),
                CollectionTime = parcel.PickedUp,
                Creation = parcel.Scheduled,
                DeliveryTime = parcel.Delivered,
                Drone = parcel.DroneID
            }
        }
    }
}
