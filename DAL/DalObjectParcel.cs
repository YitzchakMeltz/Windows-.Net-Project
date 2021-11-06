using IDAL.DO;
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
            return ++DataSource.Config.PackageID;
        }

        /// <summary>
        /// Returns a Parcel by its ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Parcel GetParcel(int ID)
        {
            try
            {
                return Parcels.Find(p => p.ID == ID);
            }
            catch
            {
                throw new ObjectNotFound($"Parcel with ID: {ID} not found.");
            }
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

        /*
        /// <summary>
        /// Adds a Parcel to DataSource
        /// </summary>
        /// <param name="parcel"></param>
        /// <returns>PackageID</returns>
        public void AddParcel()
        {
            Parcel parcel = new Parcel();

            do
            {
                parcel.ID = rd.Next(100000000, 999999999);
            } while (DataSource.Parcels.Exists(s => s.ID == parcel.ID));

            Console.WriteLine("Please enter the sender's ID: ");
            parcel.SenderID = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Please enter the target ID: ");
            parcel.TargetID = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Please enter the weight: \n0 for Light \n1 for Medium \n2 for Heavy");
            parcel.WeightCategory = (IDAL.DO.WeightCategories)Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Please enter the priority: \n0 for Regular \n1 for Fast \n2 for Emergency");
            parcel.Priority = (IDAL.DO.Priorities)Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Please enter the drone ID: ");
            parcel.DroneID = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter the scheduled date (mm/dd/yyyy): ");
            parcel.Scheduled = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter the picked up date (mm/dd/yyyy): ");
            parcel.PickedUp = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Please enter the time of assignment: ");
            parcel.SenderID = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter date delivered (mm/dd/yyyy): ");
            parcel.Delivered = DateTime.Parse(Console.ReadLine());

            DataSource.Parcels.Add(parcel);

            Console.WriteLine("\n" + parcel);
        }*/

        /// <summary>
        /// Assigns a Parcel to a Drone
        /// </summary>
        public void AssignParcel(int parcelID, int droneID)
        {
            //Console.WriteLine("Enter the ID of the parcel to assign: ");
            //int parcelID = Convert.ToInt32(Console.ReadLine());
            Parcel parcel = GetParcel(parcelID);

            //Console.WriteLine("Enter the ID of the drone to be assigned: ");
            GetDrone(droneID);                                           // Forces error if drone doesn't exist
            parcel.DroneID = droneID;

            Parcels[Parcels.FindIndex(p => p.ID == parcel.ID)] = parcel;
        }

        /// <summary>
        /// Marks a Parcel as Collected by a Drone
        /// </summary>
        public void ParcelCollected(int parcelID)
        {
            //Console.WriteLine("Enter the ID of the parcel to mark collected: ");

            Parcel parcel = GetParcel(parcelID);

            parcel.PickedUp = System.DateTime.Now;

            //Console.Write("Enter the date collected (mm/dd/yyyy): ");

            Parcels[Parcels.FindIndex(p => p.ID == parcel.ID)] = parcel;
        }

        /// <summary>
        /// Marks a Parcel as Delivered
        /// </summary>
        public void ParcelDelivered(int parcelID)
        {
            //Console.WriteLine("Enter the ID of the parcel to mark delivered: ");
            Parcel parcel = GetParcel(parcelID);

            parcel.Delivered = System.DateTime.Now;

            //Console.Write("Enter the date delivered (mm/dd/yyyy): ");

            Parcels[Parcels.FindIndex(p => p.ID == parcel.ID)] = parcel;
        }
    }
}
