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

        // Calculates how far a Drone can fly while Free
        public double DistanceLeft(DroneList drone)
        {
            return drone.Battery * PowerConsumption[0];
        }
        public void ChargeDrone(int ID)
        {
            try
            {
                IDAL.DO.Drone dalDrone = dalObject.GetDrone(ID);
                DroneList drone = Drones.Find(d => d.ID == ID);

                if (Drones.Find(d => d.ID == ID).Status != DroneStatuses.Free)
                    throw new InvalidManeuver("Only a free Drone can be charged.");

                BaseStation closestStation = ClosestStation(drone.Location);
                if (Distance(closestStation.Location, drone.Location) > DistanceLeft(drone))
                    throw new InvalidManeuver("Drone is too far away.");

                drone.Battery -= Distance(closestStation.Location, drone.Location) * PowerConsumption[0];
                drone.Location = closestStation.Location;
                drone.Status = DroneStatuses.Maintenance;

                Drones[Drones.FindIndex(d => d.ID == ID)] = drone;

                dalObject.ChargeDrone(ID, closestStation.ID);
            }
            catch (IDAL.DO.ObjectNotFound e)
            {
                throw new ObjectNotFound(e.Message);
            }
        }

        public void ReleaseDrone(int ID, double minutesCharging)
        {
            try
            {
                IDAL.DO.Drone dalDrone = dalObject.GetDrone(ID);
                DroneList drone = Drones.Find(d => d.ID == ID);

                if (Drones.Find(d => d.ID == ID).Status != DroneStatuses.Maintenance)
                    throw new InvalidManeuver("Only a Drone in maintenance can be released.");

                drone.Battery += PowerConsumption[3] * minutesCharging;
                drone.Status = DroneStatuses.Free;

                Drones[Drones.FindIndex(d => d.ID == ID)] = drone;

                dalObject.ReleaseDrone(ID);
            }
            catch (IDAL.DO.ObjectNotFound e)
            {
                throw new ObjectNotFound(e.Message);
            }
        }

        public void AssignPackageToDrone(int droneID)
        {
            if (Drones.Find(d => d.ID == droneID).Status != DroneStatuses.Free)
            {
                throw new InvalidManeuver($"Drone with ID {droneID} is not available.");
            }

            List<IDAL.DO.Parcel> unassignedPackages = (List<IDAL.DO.Parcel>)dalObject.GetUnassignedParcelList();

            IDAL.DO.Priorities highestPriority = IDAL.DO.Priorities.Regular;
            foreach (IDAL.DO.Parcel package in unassignedPackages)
            {
                if ((int)package.Priority > (int)highestPriority)
                    highestPriority = package.Priority;
            }
            unassignedPackages.RemoveAll(x => x.Priority != highestPriority);

            IDAL.DO.WeightCategories highestWeight = IDAL.DO.WeightCategories.Light;
            foreach (IDAL.DO.Parcel package in unassignedPackages)
            {
                if ((int)package.WeightCategory > (int)highestWeight)
                    highestWeight = package.WeightCategory;
            }
            unassignedPackages.RemoveAll(x => x.WeightCategory != highestWeight);

            unassignedPackages.Sort((x, y) => Distance(y.))
        }
    }
}
