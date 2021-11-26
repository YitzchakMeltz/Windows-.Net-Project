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

        public void AddPackage(int senderID, int receiverID, IBL.BO.WieghtCategories weight, IBL.BO.Priorities priority)
        {
            Package package = new()
            {
                Sender = new PackageCustomer { ID = senderID, Name = dalObject.GetCustomer(senderID).Name },
                Receiver = new PackageCustomer { ID = receiverID, Name = dalObject.GetCustomer(receiverID).Name },
                Weight = weight,
                Priority = priority,
                Drone = null,
                Creation = DateTime.Now,
                AssignmentTime = DateTime.MinValue,
                CollectionTime = DateTime.MinValue,
                DeliveryTime = DateTime.MinValue
            };

            dalObject.AddParcel(senderID, receiverID, (IDAL.DO.WeightCategories)weight, (IDAL.DO.Priorities)priority, 0);
        }
    }
}
