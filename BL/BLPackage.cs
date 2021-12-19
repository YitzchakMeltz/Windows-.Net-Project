using BlApi.BO;
using DalApi;
using System;
using System.Collections.Generic;

namespace BL
{
    partial class BL : BlApi.IBL
    {
        private Statuses ParcelStatus(DalApi.DO.Parcel p)
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

        public void AddPackage(int senderID, int receiverID, BlApi.BO.WeightCategories weight, BlApi.BO.Priorities priority)
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

            dalObject.AddParcel(senderID, receiverID, (DalApi.DO.WeightCategories)weight, (DalApi.DO.Priorities)priority, 0);
        }

        public CustomerPackage ConvertToCustomerPackage(PackageList package, string otherCustomer)
        {
            int customerID = ((List<DalApi.DO.Customer>)dalObject.GetCustomerList()).Find(customer => customer.Name == otherCustomer).ID;
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
                DalApi.DO.Parcel parcel = dalObject.GetParcel(packageID);
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
            catch (DalApi.DO.ObjectNotFound e)
            {
                throw new BlApi.BO.ObjectNotFound(e.Message);
            }
        }

        public IEnumerable<PackageList> ListPackages()
        {
            IEnumerable<DalApi.DO.Parcel> dalParcels = dalObject.GetParcelList();
            List<PackageList> packages = new List<PackageList>();

            foreach(DalApi.DO.Parcel parcel in dalParcels)
            {
                packages.Add(new PackageList() { ID = parcel.ID, Sender = dalObject.GetCustomer(parcel.SenderID).Name, Receiver = dalObject.GetCustomer(parcel.TargetID).Name, Weight = (WeightCategories)parcel.WeightCategory, Priority = (Priorities)parcel.Priority, Status = ParcelStatus(parcel) });
            }

            return packages;
        }
        public IEnumerable<PackageList> ListPackagesFiltered(Predicate<PackageList> pred)
        {
            return ((List<PackageList>)ListPackages()).FindAll(pred);
        }
        /*public IEnumerable<PackageList> ListUnassignedPackages()
        {
            IEnumerable<IDAL.DO.Parcel> dalParcels = dalObject.GetUnassignedParcelList();
            List<PackageList> packages = new List<PackageList>();

            foreach (IDAL.DO.Parcel parcel in dalParcels)
            {
                if (ParcelStatus(parcel) != Statuses.Created)
                    throw new IBL.BO.LogicError($"Package with ID {parcel.ID} has status {ParcelStatus(parcel)} but is listed as unassigned.");
                packages.Add(new PackageList() { ID = parcel.ID, Sender = dalObject.GetCustomer(parcel.SenderID).Name, Receiver = dalObject.GetCustomer(parcel.TargetID).Name, Weight = (WeightCategories)parcel.WeightCategory, Priority = (Priorities)parcel.Priority, Status = Statuses.Created });
            }

            return packages;
        }*/
    }
}
