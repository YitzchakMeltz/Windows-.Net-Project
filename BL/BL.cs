using IBL.BO;
using IDAL;
using System;
using System.Collections.Generic;

namespace BL
{
    partial class BL : IBL.IBL
    {
        Random random = new Random();
        List<Drone> Drones = new List<Drone>();
        IDal dalObject;
        double[] PowerConsumption;

        public BL()
        {
            dalObject = new DalObject.DalObject();
            PowerConsumption = dalObject.PowerConsumption();
            foreach (IDAL.DO.Drone d in dalObject.GetDroneList())
            {
                Drone drone = new Drone()
                {
                    ID = d.ID,
                    Model = d.Model,
                    Weight = (WieghtCategories)d.WeightCategory
                };

                if (((List<IDAL.DO.Parcel>)dalObject.GetParcelList()).Exists(p => p.DroneID == d.ID && ParcelStatus(p) != Statuses.Delivered))
                {
                    drone.Status = DroneStatuses.Delivering;
                    if (((List<IDAL.DO.Parcel>)dalObject.GetParcelList()).Exists(p => p.DroneID == d.ID && ParcelStatus(p) == Statuses.Collected))
                    {
                        IDAL.DO.Station closestStation = ClosestTo()
                        drone.Location = new Location() { Latitude = }
                    }
                }
                else
                {
                    drone.Status = (DroneStatuses)(random.Next(0, 2) * 2);
                }
                }
            }
        }
    }
}
