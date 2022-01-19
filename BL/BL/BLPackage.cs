using BO;
using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;

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
            try
            {
                dalObject.GetCustomer(senderID);
                dalObject.GetCustomer(receiverID);

                return dalObject.AddParcel(senderID, receiverID, (DO.WeightCategories)weight, (DO.Priorities)priority, 0);
            }
            catch (DO.ObjectNotFound e)
            {
                throw new ObjectNotFound(e.Message);
            }
        }

        public CustomerPackage ConvertToCustomerPackage(PackageList package, string otherCustomer)
        {
            int customerID = dalObject.GetCustomerList().First(customer => customer.Name == otherCustomer).ID;
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
                    Status = ParcelStatus(parcel),
                    Drone = drone == null ? null : ConvertToDeliveryDrone(drone),
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
            Statuses status = ParcelStatus(dalObject.GetParcel(ID));
            if (status != Statuses.Created)
                throw new BO.InvalidManeuver("Can not delete a package once it has been assigned.");

            dalObject.RemoveParcel(ID);
        }

        public IEnumerable<PackageList> ListPackages()
        {
            IEnumerable<DO.Parcel> dalParcels = dalObject.GetParcelList();
            List<PackageList> packages = new List<PackageList>();

            dalParcels.ToList().ForEach(parcel =>
            {
                packages.Add(new PackageList() { ID = parcel.ID, Sender = dalObject.GetCustomer(parcel.SenderID).Name, Receiver = dalObject.GetCustomer(parcel.TargetID).Name, Weight = (WeightCategories)parcel.WeightCategory, Priority = (Priorities)parcel.Priority, Status = ParcelStatus(parcel) });
            });

            return packages;
        }
        public IEnumerable<PackageList> ListPackagesFiltered(Predicate<PackageList> pred)
        {
            return ListPackages().Where(pred.Invoke);
        }
    }
}
