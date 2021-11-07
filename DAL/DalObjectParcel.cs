using IDAL.DO;
using System;
using System.Collections.Generic;
using static DalObject.DataSource;

namespace DalObject
{
    partial class DalObject : IDAL.IDal
    {
        /// <summary>
        /// Adds a Parcel to DataSource
        /// </summary>
        /// <param name="parcel"></param>
        /// <returns>PackageID</returns>
        public int AddParcel(Parcel parcel)
        {
            if (Parcels.Exists(p => p.ID == parcel.ID))
                throw new ObjectAlreadyExists($"Parcel with ID {parcel.ID} already exists.");

            Parcels.Add(parcel);
            return ++Config.PackageID;
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
        /// Returns an array of all Unassigned Parcels
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Parcel> GetUnassignedParcelList()
        {
            return Parcels.FindAll(p => p.DroneID == 0);
        }

        
        /// <summary>
        /// Adds a Parcel to DataSource
        /// </summary>
        /// <param name="parcel"></param>
        /// <returns>PackageID</returns>
        public void AddParcel(int senderID, int targetID, WeightCategories weightCat, Priorities priority, int droneID)
        {
            Parcel parcel = new Parcel()
            {
                SenderID = senderID,
                TargetID = targetID,
                WeightCategory = weightCat,
                Priority = priority,
                DroneID = droneID,
                Scheduled = System.DateTime.Now
            };

            do
            {
                parcel.ID = rd.Next(100000000, 999999999);
            } while (Parcels.Exists(s => s.ID == parcel.ID));


            /*
            Console.Write("Enter the picked up date (mm/dd/yyyy): ");
            parcel.PickedUp = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Please enter the time of assignment: ");
            parcel.SenderID = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter date delivered (mm/dd/yyyy): ");
            parcel.Delivered = DateTime.Parse(Console.ReadLine());
            */
            AddParcel(parcel);
        }

        /// <summary>
        /// Assigns a Parcel to a Drone
        /// </summary>
        public void AssignParcel(int parcelID, int droneID)
        {
            Parcel parcel = GetParcel(parcelID);

            GetDrone(droneID);                                           // Forces error if drone doesn't exist

            parcel.DroneID = droneID;

            parcel.AssignmentTime = DateTime.Now.Subtract(parcel.Scheduled);

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
    }
}
