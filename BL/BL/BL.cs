using BO;
using DalApi;
using System;
using System.Linq;
using System.Collections.Generic;

namespace BL
{
    internal partial class BL : BlApi.IBL
    {
        Random random = new Random();
        List<DroneList> Drones = new List<DroneList>();
        internal IDal dalObject;
        double[] PowerConsumption;

        private BL()
        {
            dalObject = DalFactory.GetDal();
            PowerConsumption = dalObject.PowerConsumption();
            dalObject.GetDroneList().ToList().ForEach(d =>
            {
                DroneList drone = new DroneList()
                {
                    ID = d.ID,
                    Model = d.Model,
                    Weight = (WeightCategories)d.WeightCategory
                };

                DO.Parcel parcel = dalObject.GetParcelList().Where(p => p.DroneID == d.ID && ParcelStatus(p) != Statuses.Delivered).FirstOrDefault();

                if (!parcel.Equals(default(DO.Parcel)))
                {
                    drone.PackageID = (uint)parcel.ID;
                    drone.Status = DroneStatuses.Delivering;

                    EnroutePackage enroute = GetEnroutePackage(parcel.ID);
                    if (ParcelStatus(parcel) == Statuses.Assigned)
                    {
                        drone.Location = ClosestStation(enroute.CollectionLocation).Location;
                    }
                    else
                    {
                        drone.Location = enroute.DeliveryLocation;
                    }

                    double distToDest = Distance(drone.Location, enroute.DeliveryLocation);
                    double distToStation = Distance(enroute.DeliveryLocation, ClosestStation(enroute.DeliveryLocation).Location);
                    double minBattery = (distToDest / PowerConsumption[(int)parcel.WeightCategory + 1]) + distToStation / PowerConsumption[0];

                    drone.Battery = random.NextDouble() * (100 - minBattery) + minBattery;
                }
                else
                {
                    drone.Status = (DroneStatuses)(random.Next(0, 2) * 2);

                    if (drone.Status == DroneStatuses.Maintenance)
                    {
                        DO.Station randStation = dalObject.GetStationList().ElementAt(random.Next(0, dalObject.GetStationList().Count()));
                        drone.Location = CoordinateToLocation(randStation.Location);
                        drone.Battery = random.NextDouble() * 20;
                        dalObject.ChargeDrone(d.ID, randStation.ID);
                    }
                    else // Drone is Free
                    {
                        IEnumerable<DO.Parcel> deliveredParcels = dalObject.GetParcelList().Where(parcel => !parcel.Delivered.Equals(null)).ToArray();

                        drone.Location = CoordinateToLocation(dalObject.GetCustomer(deliveredParcels.ElementAt(random.Next(0, deliveredParcels.Count())).TargetID).Location);

                        double batteryToStation = Distance(ClosestStation(drone.Location).Location, drone.Location) / PowerConsumption[0];
                        if (batteryToStation > 100)
                            throw new InvalidManeuver("Drone is too far away to be charged.");
                        drone.Battery = batteryToStation + (random.NextDouble() * (100 - batteryToStation));
                    }
                }

                Drones.Add(drone);
            });
        }

        private static readonly Lazy<BL> lazy = new Lazy<BL>(() => new BL());
        public static BL Instance
        {
            get
            {
                return lazy.Value;
            }
        }

        public void ActivateSimulator(uint droneID, Action updateAction, Func<bool> stopCheck)
        {
            new Simulator(this, droneID, updateAction, stopCheck);
        }
    }
}
