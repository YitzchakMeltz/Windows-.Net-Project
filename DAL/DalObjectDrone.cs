using IDAL.DO;
using System.Collections.Generic;
using static DalObject.DataSource;

namespace DalObject
{
    partial class DalObject : IDAL.IDal
    {
        /// <summary>
        /// Adds a Drone to DataSource
        /// </summary>
        /// <param name="drone"></param>
        public void AddDrone(Drone drone)
        {
            if (Drones.Exists(d => d.ID == drone.ID))
                throw new ObjectAlreadyExists($"Drone with ID: {drone.ID} already exists.");

            Drones.Add(drone);
        }

        /// <summary>
        /// Returns a Drone by its ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Drone GetDrone(int ID)
        {
            try
            {
                return Drones.Find(d => d.ID == ID);
            }
            catch
            {
                throw new ObjectNotFound($"Drone with ID: {ID} not found.");
            }
        }

        /// <summary>
        /// Returns an array of all Drones
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Drone> GetDroneList()
        {
            return new List<Drone>(Drones);
        }

        /// <summary>
        /// Adds a Drone to DataSource
        /// </summary>
        /// <param name="drone"></param>
        public void AddDrone()
        {
            Drone drone = new Drone();

            do
            {
                drone.ID = rd.Next(100000000, 999999999);
            } while (DataSource.Drones.Exists(s => s.ID == drone.ID));

            Console.WriteLine("Please enter the model name: ");
            drone.Model = Console.ReadLine();

            Console.WriteLine("Please enter the weight: \n0 for Light \n1 for Medium \n2 for Heavy");
            drone.WeightCategory = (IDAL.DO.WeightCategories)Convert.ToInt32(Console.ReadLine());

            /*Console.WriteLine("Please enter the drone's status: \n0 for Free \n1 for Delivery \n2 for Maintanence");
            drone.DroneStatus = (IDAL.DO.DroneStatuses)Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Please enter the drone battery level: ");
            drone.Battery = Convert.ToDouble(Console.ReadLine());*/

            DataSource.Drones.Add(drone);

            Console.WriteLine("\n" + drone);
        }
    }
}
