using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL
{
    partial class BL : IBL.IBL
    {
        public void AddDrone(int ID, string model, IBL.BO.WeightCategories weight, int stationID)
        {
            dalObject.AddDrone(ID, model, (IDAL.DO.WeightCategories)weight);

            Drones.Add(new DroneList()
            {
                ID = ID,
                Model = model,
                Weight = weight,
                Location = GetStation(stationID).Location,
                Status = DroneStatuses.Maintenance,
                Battery = (random.NextDouble() * 20) + 20
            });

            dalObject.ChargeDrone(ID, stationID);
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

                drone.Battery -= Distance(closestStation.Location, drone.Location) / PowerConsumption[0];
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

                drone.Battery = Math.Min(drone.Battery + PowerConsumption[4] * minutesCharging, 100);
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

        public DroneList ConvertToDroneList(Drone drone)
        {
            return new DroneList()
            {
                ID = drone.ID,
                PackageID = drone.Package is null ? null : (uint)drone.Package.ID,
                Battery = drone.Battery,
                Location = drone.Location,
                Model = drone.Model,
                Status = drone.Status,
                Weight = drone.Weight
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

            List<PackageList> unassignedPackages = (List<PackageList>)ListPackagesFiltered(package => package.Status == Statuses.Created);//ListUnassignedPackages();
            
            unassignedPackages.RemoveAll(x => x.Weight > drone.Weight);

            if (unassignedPackages.Count == 0)
                throw new InvalidManeuver($"The drone with ID: {droneID} can't carry any packages.");

            unassignedPackages.OrderBy(x => x.Priority)
                              .ThenByDescending(x => x.Weight)
                              .ThenBy(x => Distance(GetEnroutePackage(x.ID).CollectionLocation, drone.Location));
            
            /*// Only keep highest priority packages
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
            }*/

            
            EnroutePackage enroute = GetEnroutePackage(unassignedPackages.First().ID);
            double batteryPickup = Distance(drone.Location, enroute.CollectionLocation) / PowerConsumption[0];
            double batteryDeliver = enroute.DeliveryDistance / PowerConsumption[(int)enroute.Weight + 1];
            double batteryCharge = Distance(enroute.DeliveryLocation, ClosestStation(enroute.DeliveryLocation).Location) / PowerConsumption[0];

            if (batteryPickup + batteryDeliver + batteryCharge > drone.Battery)
                throw new InvalidManeuver($"Drone with ID {drone.ID} doesn't have enough battery to make this delivery.");
            else
            {
                drone.PackageID = (uint)unassignedPackages[0].ID;
                drone.Status = DroneStatuses.Delivering;

                dalObject.AssignParcel(unassignedPackages[0].ID, droneID);

                Drones[Drones.FindIndex(d => d.ID == droneID)] = drone;
            }
        }

        public void CollectPackage(int droneID)
        {
            Drone drone;
            DroneList droneList;
            try
            {
                drone = GetDrone(droneID);
                droneList = ConvertToDroneList(drone);
            }
            catch (IDAL.DO.ObjectNotFound e)
            {
                throw new ObjectNotFound(e.Message);
            }

            if (droneList.PackageID == null)
                throw new InvalidManeuver($"Drone with ID {drone.ID} doesn't have a package assigned to it.");
            if (drone.Package.Delivering == true)
                throw new InvalidManeuver($"Package with ID {drone.Package.ID} is already being Delivered.");
            if (DistanceLeft(droneList) < Distance(drone.Location, drone.Package.CollectionLocation))
                throw new InvalidManeuver($"Drone with ID {drone.ID} can't reach the package with ID {drone.Package.ID}.");

            droneList.Battery -= Distance(drone.Location, drone.Package.CollectionLocation) / PowerConsumption[0];
            droneList.Location = drone.Package.CollectionLocation;
            
            dalObject.ParcelCollected((int)droneList.PackageID);
            Drones[Drones.FindIndex(d => d.ID == droneID)] = droneList;
        }

        public void DeliverPackage(int droneID)
        {
            Drone drone;
            DroneList droneList;
            try
            {
                drone = GetDrone(droneID);
                droneList = ConvertToDroneList(drone);
            }
            catch (IDAL.DO.ObjectNotFound e)
            {
                throw new ObjectNotFound(e.Message);
            }

            if (droneList.PackageID == null)
                throw new InvalidManeuver($"Drone with ID {droneID} doesn't have a package assigned to it.");
            if (drone.Package.Delivering == false)
                throw new InvalidManeuver($"Package with ID {drone.Package.ID} hasn't been collected.");

            droneList.Battery -= drone.Package.DeliveryDistance / PowerConsumption[(int)drone.Package.Weight + 1];
            droneList.Location = drone.Package.DeliveryLocation;
            droneList.Status = DroneStatuses.Free;

            dalObject.ParcelDelivered((int)droneList.PackageID);

            droneList.PackageID = null;
            Drones[Drones.FindIndex(d => d.ID == droneID)] = droneList;
        }

        public IEnumerable<DroneList> ListDrones()
        {
            return Drones;
        }

        /// <summary>
        /// Returns a filtered array of DroneLists
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DroneList> ListDronesFiltered(Predicate<DroneList> pred)
        {
            return Drones.FindAll(pred);
        }
    }
}
