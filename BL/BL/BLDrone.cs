using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace BL
{
    partial class BL : BlApi.IBL
    {
        #region private functions
        private DeliveryDrone ConvertToDeliveryDrone(DroneList drone)
        {
            return new DeliveryDrone()
            {
                ID = drone.ID,
                Battery = drone.Battery,
                Location = drone.Location
            };
        }

        private DroneList ConvertToDroneList(Drone drone)
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

        // Calculates how far a Drone can fly while Free
        private double DistanceLeft(DroneList drone)
        {
            return drone.Battery * PowerConsumption[0];
        }
        #endregion

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDrone(uint ID, string model, BO.WeightCategories weight, uint stationID)
        {
            lock (dalObject)
            {
                dalObject.AddDrone(ID, model, (DO.WeightCategories)weight);

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
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone GetDrone(uint droneID)
        {
            try
            {
                lock (dalObject)
                {
                    DO.Drone dalDrone = dalObject.GetDrone(droneID);
                    DroneList droneList = Drones.Find(d => d.ID == droneID);

                    Drone drone = new Drone()
                    {
                        ID = droneID,
                        Model = dalDrone.Model,
                        Weight = (WeightCategories)dalDrone.WeightCategory,
                        Battery = droneList.Battery,
                        Status = droneList.Status,
                        Package = droneList.PackageID == null ? null : GetEnroutePackage(droneList.PackageID.Value),
                        Location = droneList.Location
                    };


                    return drone;
                }
            }
            catch (DO.ObjectNotFound e)
            {
                throw new BO.ObjectNotFound(e.Message);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateDrone(uint ID, string model)
        {
            try
            {
                lock (dalObject)
                {
                    // Update DALDrone
                    DO.Drone drone = dalObject.GetDrone(ID);
                    drone.Model = model;

                    dalObject.RemoveDrone(ID);
                    dalObject.AddDrone(drone);
                }
            }
            catch (ObjectNotFound e)
            {
                throw new BO.ObjectNotFound(e.Message);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void ChargeDrone(uint ID)
        {
            try
            {
                lock (dalObject)
                {
                    DO.Drone dalDrone = dalObject.GetDrone(ID);
                    DroneList drone = Drones.Find(d => d.ID == ID);

                    if (drone.Status != DroneStatuses.Free)
                        throw new InvalidManeuver("Only a free Drone can be charged.");

                    BaseStation closestStation = ClosestStation(drone.Location);
                    if (Distance(closestStation.Location, drone.Location) > DistanceLeft(drone))
                        throw new InvalidManeuver("Drone is too far away.");

                    drone.Battery -= Distance(closestStation.Location, drone.Location) / PowerConsumption[0];
                    drone.Location = closestStation.Location;
                    drone.Status = DroneStatuses.Maintenance;

                    dalObject.ChargeDrone(ID, closestStation.ID);
                }
            }
            catch (DO.ObjectNotFound e)
            {
                throw new ObjectNotFound(e.Message);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void ReleaseDrone(uint ID)
        {
            try
            {
                lock (dalObject)
                {
                    DO.Drone dalDrone = dalObject.GetDrone(ID);
                    DroneList drone = Drones.Find(d => d.ID == ID);

                    if (Drones.Find(d => d.ID == ID).Status != DroneStatuses.Maintenance)
                        throw new InvalidManeuver("Only a Drone in maintenance can be released.");

                    double minutesCharging = dalObject.ReleaseDrone(ID);
                    drone.Battery = Math.Min(drone.Battery + PowerConsumption[4] * minutesCharging, 100);
                    drone.Status = DroneStatuses.Free;
                }
            }
            catch (DO.ObjectNotFound e)
            {
                throw new ObjectNotFound(e.Message);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AssignPackageToDrone(uint droneID)
        {
            lock (dalObject)
            {
                DroneList drone = Drones.Find(d => d.ID == droneID);
                if (drone.Status != DroneStatuses.Free)
                {
                    throw new InvalidManeuver($"Drone with ID {droneID} is not available.");
                }

                drone.PackageID = (ListPackagesFiltered(package => package.Status == Statuses.Created && package.Weight <= drone.Weight)
                    .OrderByDescending(x => x.Priority)
                    .ThenByDescending(x => x.Weight)
                    .ThenBy(x => Distance(GetEnroutePackage(x.ID).CollectionLocation, drone.Location)).FirstOrDefault()?.ID);

                if (drone.PackageID == null)
                    throw new InvalidManeuver($"The drone with ID: {droneID} can't carry any packages.");

                EnroutePackage enroute = GetEnroutePackage(drone.PackageID.Value);
                double batteryPickup = Distance(drone.Location, enroute.CollectionLocation) / PowerConsumption[0];
                double batteryDeliver = enroute.DeliveryDistance / PowerConsumption[(int)enroute.Weight + 1];
                double batteryCharge = Distance(enroute.DeliveryLocation, ClosestStation(enroute.DeliveryLocation).Location) / PowerConsumption[0];

                if (batteryPickup + batteryDeliver + batteryCharge > drone.Battery)
                    throw new InvalidManeuver($"Drone with ID {drone.ID} doesn't have enough battery to make this delivery.");
                else
                {
                    drone.Status = DroneStatuses.Delivering;

                    dalObject.AssignParcel(drone.PackageID.Value, droneID);
                }
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void CollectPackage(uint droneID)
        {
            lock (dalObject)
            {
                Drone drone;
                DroneList droneList;
                try
                {
                    drone = GetDrone(droneID);
                    droneList = Drones.Find(d => d.ID == droneID);
                }
                catch (DO.ObjectNotFound e)
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

                dalObject.ParcelCollected(droneList.PackageID.Value);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeliverPackage(uint droneID)
        {
            lock (dalObject)
            {
                Drone drone;
                DroneList droneList;
                try
                {
                    drone = GetDrone(droneID);
                    droneList = Drones.Find(d => d.ID == droneID);
                }
                catch (DO.ObjectNotFound e)
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

                dalObject.ParcelDelivered(droneList.PackageID.Value);

                droneList.PackageID = null;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneList> ListDrones()
        {
            return new List<DroneList>(Drones);
        }

        /// <summary>
        /// Returns a filtered array of DroneLists
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneList> ListDronesFiltered(Predicate<DroneList> pred)
        {
            return Drones.FindAll(pred);
        }
    }
}
