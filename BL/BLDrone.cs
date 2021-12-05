using IBL.BO;
using System;
using System.Collections.Generic;

namespace BL
{
    partial class BL : IBL.IBL
    {
        public void AddDrone(int ID, string model, IBL.BO.WeightCategories weight, int stationID)
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

                drone.Battery += PowerConsumption[4] * minutesCharging;
                drone.Status = DroneStatuses.Free;

                Drones[Drones.FindIndex(d => d.ID == ID)] = drone;

                dalObject.ReleaseDrone(ID);
            }
            catch (IDAL.DO.ObjectNotFound e)
            {
                throw new ObjectNotFound(e.Message);
            }
        }

        public DeliveryDrone ConvertToDeliveryDrone(DroneList drone)
        {
            return new DeliveryDrone()
            {
                ID = drone.ID,
                Battery = drone.Battery,
                Location = drone.Location
            };
        }

        public Drone GetDrone(int droneID)
        {
            try
            {
                IDAL.DO.Drone dalDrone = dalObject.GetDrone(droneID);
                DroneList droneList = Drones.Find(d => d.ID == droneID);

                Drone drone = new Drone()
                {
                    ID = droneID,
                    Model = dalDrone.Model,
                    Weight = (WeightCategories)dalDrone.WeightCategory,
                    Battery = droneList.Battery,
                    Status = droneList.Status,
                    Package = (droneList.PackageID == null ? null : GetEnroutePackage((int)droneList.PackageID)),
                    Location = droneList.Location
                };

                return drone;
            }
            catch (IDAL.DO.ObjectNotFound e)
            {
                throw new IBL.BO.ObjectNotFound(e.Message);
            }
        }

        public void AssignPackageToDrone(int droneID)
        {
            DroneList drone = Drones.Find(d => d.ID == droneID);
            if (drone.Status != DroneStatuses.Free)
            {
                throw new InvalidManeuver($"Drone with ID {droneID} is not available.");
            }

            List<PackageList> unassignedPackages = (List<PackageList>)ListUnassignedPackages();

            // Only keep highest priority packages
            Priorities highestPriority = Priorities.Regular;
            foreach (PackageList package in unassignedPackages)
            {
                if ((int)package.Priority > (int)highestPriority)
                    highestPriority = package.Priority;
            }
            unassignedPackages.RemoveAll(x => x.Priority != highestPriority);

            // Only keep heaviest packages
            WeightCategories heaviest = WeightCategories.Light;
            foreach (PackageList package in unassignedPackages)
            {
                if ((int)package.Weight > (int)heaviest)
                    heaviest = package.Weight;
            }
            unassignedPackages.RemoveAll(x => x.Weight != heaviest);

            unassignedPackages.Sort((x, y) => Distance());
        }

        public IEnumerable<DroneList> ListDrones()
        {
            return Drones;
        }
    }
}
