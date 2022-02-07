using DO;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using static Dal.DataSource;

namespace Dal
{
    partial class DalObject : DalApi.IDal
    {
        /// <summary>
        /// Adds a Drone to DataSource
        /// </summary>
        /// <param name="drone"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone GetDrone(uint ID)
        {
            Drone d = Drones.Find(d => d.ID == ID);

            if (d.Equals(default(Drone)))
                throw new ObjectNotFound($"Drone with ID: {ID} not found.");

            return d;
        }

        /// <summary>
        /// Returns an array of all Drones
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Drone> GetDroneList()
        {
            return new List<Drone>(Drones);
        }


        /// <summary>
        /// Adds a Drone to DataSource
        /// </summary>
        /// <param name="drone"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDrone(uint id, string model, WeightCategories weightCat)
        {
            Drone drone = new Drone()
            {
                ID = id,
                Model = model,
                WeightCategory = weightCat
            };

            AddDrone(drone);
        }

        /// <summary>
        /// Deletes a Drone from DataSource
        /// </summary>
        /// <param name="ID"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void RemoveDrone(uint ID)
        {
            if (Drones.RemoveAll(d => d.ID == ID) == 0)
                throw new ObjectNotFound($"Drone with ID: {ID} doesn't exist");
        }
    }
}
