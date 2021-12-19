<<<<<<< HEAD
﻿using BL;
using DalApi;
=======
﻿using IBL.BO;
using IDAL;
>>>>>>> parent of d4aee0b (change namespaces IDAL/IBL to DalAPI/BlApi)
using System;
using System.Collections.Generic;

namespace BL
{
    partial class BL : IBL.IBL
    {
<<<<<<< HEAD
        private Statuses ParcelStatus(DO.Parcel p)
=======
        private Statuses ParcelStatus(IDAL.DO.Parcel p)
>>>>>>> parent of d4aee0b (change namespaces IDAL/IBL to DalAPI/BlApi)
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

<<<<<<< HEAD
        public void AddPackage(int senderID, int receiverID, BL.WeightCategories weight, BL.Priorities priority)
=======
        public void AddPackage(int senderID, int receiverID, IBL.BO.WeightCategories weight, IBL.BO.Priorities priority)
>>>>>>> parent of d4aee0b (change namespaces IDAL/IBL to DalAPI/BlApi)
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

<<<<<<< HEAD
            dalObject.AddParcel(senderID, receiverID, (DO.WeightCategories)weight, (DO.Priorities)priority, 0);
=======
            dalObject.AddParcel(senderID, receiverID, (IDAL.DO.WeightCategories)weight, (IDAL.DO.Priorities)priority, 0);
>>>>>>> parent of d4aee0b (change namespaces IDAL/IBL to DalAPI/BlApi)
        }

        public CustomerPackage ConvertToCustomerPackage(PackageList package, string otherCustomer)
        {
<<<<<<< HEAD
            int customerID = ((List<DO.Customer>)dalObject.GetCustomerList()).Find(customer => customer.Name == otherCustomer).ID;
=======
            int customerID = ((List<IDAL.DO.Customer>)dalObject.GetCustomerList()).Find(customer => customer.Name == otherCustomer).ID;
>>>>>>> parent of d4aee0b (change namespaces IDAL/IBL to DalAPI/BlApi)
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
<<<<<<< HEAD
                DO.Parcel parcel = dalObject.GetParcel(packageID);
=======
                IDAL.DO.Parcel parcel = dalObject.GetParcel(packageID);
>>>>>>> parent of d4aee0b (change namespaces IDAL/IBL to DalAPI/BlApi)
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
<<<<<<< HEAD
            catch (DO.ObjectNotFound e)
            {
                throw new BL.ObjectNotFound(e.Message);
=======
            catch (IDAL.DO.ObjectNotFound e)
            {
                throw new IBL.BO.ObjectNotFound(e.Message);
>>>>>>> parent of d4aee0b (change namespaces IDAL/IBL to DalAPI/BlApi)
            }
        }

        public IEnumerable<PackageList> ListPackages()
        {
<<<<<<< HEAD
            IEnumerable<DO.Parcel> dalParcels = dalObject.GetParcelList();
            List<PackageList> packages = new List<PackageList>();

            foreach(DO.Parcel parcel in dalParcels)
=======
            IEnumerable<IDAL.DO.Parcel> dalParcels = dalObject.GetParcelList();
            List<PackageList> packages = new List<PackageList>();

            foreach(IDAL.DO.Parcel parcel in dalParcels)
>>>>>>> parent of d4aee0b (change namespaces IDAL/IBL to DalAPI/BlApi)
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
