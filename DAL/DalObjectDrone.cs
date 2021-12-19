using DalApi.DO;
using System.Collections.Generic;
using static DalObject.DataSource;

namespace DalObject
{
    partial class DalObject : DalApi.IDal
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
            Drone d = Drones.Find(d => d.ID == ID);

            if (d.Equals(default(Drone)))
                throw new ObjectNotFound($"Drone with ID: {ID} not found.");

            return d;
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
        public void AddDrone(int id, string model, WeightCategories weightCat)
        {
            if (Drones.Exists(d => d.ID == id))
                throw new ObjectAlreadyExists($"Drone with ID: {id} already exists.");

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
        public void RemoveDrone(int ID)
        {
            if (Drones.RemoveAll(d => d.ID == ID) == 0)
                throw new ObjectNotFound($"Drone with ID: {ID} doesn't exist");
        }
    }
}
