using IBL.BO;
using System;
using System.Collections.Generic;

namespace BL
{
    partial class BL : IBL.IBL
    {
        public void AddDrone(int ID, string model, IBL.BO.WieghtCategories weight, int stationID)
        {
            Drone drone = new Drone()
            {
                ID = ID,
                Model = model,
                Weight = weight,
                Location = GetStation(stationID).Location,
                Status = DroneStatuses.Maintenance,
                Battery = (random.NextDouble() * 20) + 20
            };

            dalObject.AddDrone(ID, model, (IDAL.DO.WeightCategories)weight);
        }

        public void UpdateDrone(int ID, string model)
        {
            // Update BLDrone
            Drones[Drones.FindIndex(d => d.ID == ID)].Model = model;

            // Update DALDrone
            IDAL.DO.Drone drone = dalObject.GetDrone(ID);
            drone.Model = model;

            dalObject.RemoveDrone(ID);
            dalObject.AddDrone(drone);
        }
    }
}
