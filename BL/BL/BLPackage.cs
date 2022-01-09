using BO;
using DalApi;
using System;
using System.Collections.Generic;

namespace BL
{
    partial class BL : BlApi.IBL
    {
        private Statuses ParcelStatus(DO.Parcel p)
        {
            if (p.Assigned is null)
            {
                return Statuses.Created;
            }
            if (p.PickedUp is null)
            {
                return Statuses.Assigned;
            }
            if (p.Delivered is null)
            {
                return Statuses.Collected;
            }
            return Statuses.Delivered;
        }

        public int AddPackage(int senderID, int receiverID, BO.WeightCategories weight, BO.Priorities priority)
        {
            Package package = new()
            {
                Sender = new PackageCustomer { ID = senderID, Name = dalObject.GetCustomer(senderID).Name },
                Receiver = new PackageCustomer { ID = receiverID, Name = dalObject.GetCustomer(receiverID).Name },
                Weight = weight,
                Priority = priority,
                Drone = null,
                Creation = DateTime.Now,
            };

            return dalObject.AddParcel(senderID, receiverID, (DO.WeightCategories)weight, (DO.Priorities)priority, 0);
        }

        public CustomerPackage ConvertToCustomerPackage(PackageList package, string otherCustomer)
        {
            int customerID = ((List<DO.Customer>)dalObject.GetCustomerList()).Find(customer => customer.Name == otherCustomer).ID;
            return new CustomerPackage()
            {
                ID = package.ID,
                Weight = package.Weight,
                Priority = package.Priority,
                Status = package.Status,
                Customer = new PackageCustomer() { ID = customerID, Name = otherCustomer }
            };
        }

        public Package GetPackage(int packageID)
        {
            try
            {
                DO.Parcel parcel = dalObject.GetParcel(packageID);
                DroneList drone = Drones.Find(d => d.PackageID == packageID);
                Package package = new Package()
                {
                    ID = packageID,
                    Sender = ConvertToPackageCustomer(GetCustomer(parcel.SenderID)),
                    Receiver = ConvertToPackageCustomer(GetCustomer(parcel.TargetID)),
                    Weight = (WeightCategories)parcel.WeightCategory,
                    Priority = (Priorities)parcel.Priority,
                    Drone = (drone == null ? null : ConvertToDeliveryDrone(drone)),
                    Creation = parcel.Scheduled,
                    AssignmentTime = parcel.Assigned,
                    CollectionTime = parcel.PickedUp,
                    DeliveryTime = parcel.Delivered
                };

                return package;
            }
            catch (DO.ObjectNotFound e)
            {
                throw new BO.ObjectNotFound(e.Message);
            }
        }

        public void DeletePackage(int ID)
        {
            if (ParcelStatus(dalObject.GetParcel(ID)) == Statuses.Collected)
                throw new BO.InvalidManeuver("Can not delete a package once it's been collected");

            dalObject.RemoveParcel(ID);
        }

        public IEnumerable<PackageList> ListPackages()
        {
            IEnumerable<DO.Parcel> dalParcels = dalObject.GetParcelList();
            List<PackageList> packages = new List<PackageList>();

            foreach(DO.Parcel parcel in dalParcels)
            {
                packages.Add(new PackageList() { ID = parcel.ID, Sender = dalObject.GetCustomer(parcel.SenderID).Name, Receiver = dalObject.GetCustomer(parcel.TargetID).Name, Weight = (WeightCategories)parcel.WeightCategory, Priority = (Priorities)parcel.Priority, Status = ParcelStatus(parcel) });
            }

            return packages;
        }
        public IEnumerable<PackageList> ListPackagesFiltered(Predicate<PackageList> pred)
        {
            return ((List<PackageList>)ListPackages()).FindAll(pred);
        }
    }
}
