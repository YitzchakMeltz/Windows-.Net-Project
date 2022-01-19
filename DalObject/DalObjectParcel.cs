using DO;
using System;
using System.Collections.Generic;
using static Dal.DataSource;

namespace Dal
{
    partial class DalObject : DalApi.IDal
    {
        /// <summary>
        /// Adds a Parcel to DataSource
        /// </summary>
        /// <param name="parcel"></param>
        /// <returns>PackageID</returns>
        public void AddParcel(Parcel parcel)
        {
            if (Parcels.Exists(p => p.ID == parcel.ID))
                throw new ObjectAlreadyExists($"Parcel with ID {parcel.ID} already exists.");
            Parcels.Add(parcel);
            //return parcel.ID;
        }

        /// <summary>
        /// Returns a Parcel by its ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Parcel GetParcel(int ID)
        {
            Parcel p = Parcels.Find(p => p.ID == ID);

            if (p.Equals(default(Parcel)))
                throw new ObjectNotFound($"Parcel with ID: {ID} not found.");

            return p;
        }

        /// <summary>
        /// Returns an array of all Parcels
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Parcel> GetParcelList()
        {
            return new List<Parcel>(Parcels);
        }

        /// <summary>
        /// Returns a filtered array of Parcels
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Parcel> GetFilteredParcelList(Predicate<Parcel> pred)
        {
            return Parcels.FindAll(pred);
        }

        
        /// <summary>
        /// Adds a Parcel to DataSource
        /// </summary>
        /// <param name="parcel"></param>
        /// <returns>PackageID</returns>
        public int AddParcel(int senderID, int targetID, WeightCategories weightCat, Priorities priority, int droneID)
        {
            Parcel parcel = new Parcel()
            {
                ID = ++Config.PackageID,
                SenderID = senderID,
                TargetID = targetID,
                WeightCategory = weightCat,
                Priority = priority,
                DroneID = droneID,
                Scheduled = System.DateTime.Now
            };

            AddParcel(parcel);

            return parcel.ID;
        }

        /// <summary>
        /// Assigns a Parcel to a Drone
        /// </summary>
        public void AssignParcel(int parcelID, int droneID)
        {
            Parcel parcel = GetParcel(parcelID);

            GetDrone(droneID);                                           // Forces error if drone doesn't exist

            parcel.DroneID = droneID;

            parcel.Assigned = DateTime.Now;

            Parcels[Parcels.FindIndex(p => p.ID == parcel.ID)] = parcel;
        }

        /// <summary>
        /// Marks a Parcel as Collected by a Drone
        /// </summary>
        public void ParcelCollected(int parcelID)
        {
            Parcel parcel = GetParcel(parcelID);

            parcel.PickedUp = System.DateTime.Now;

            Parcels[Parcels.FindIndex(p => p.ID == parcel.ID)] = parcel;
        }

        /// <summary>
        /// Marks a Parcel as Delivered
        /// </summary>
        public void ParcelDelivered(int parcelID)
        {
            Parcel parcel = GetParcel(parcelID);

            parcel.Delivered = System.DateTime.Now;

            Parcels[Parcels.FindIndex(p => p.ID == parcel.ID)] = parcel;
        }

        /// <summary>
        /// Deletes a Parcel from the system
        /// </summary>
        /// <param name="ID">ID of Parcel to delete</param>
        public void RemoveParcel(int ID)
        {
            if (Parcels.RemoveAll(p => p.ID == ID) == 0)
                throw new ObjectNotFound($"Parcel with ID: {ID} doesn't exist");
        }
    }
}
