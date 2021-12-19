using IBL.BO;
using IDAL;
using System;
using System.Linq;
using System.Collections.Generic;

namespace BL
{
    public partial class BL : IBL.IBL
    {
        Random random = new Random();
        List<DroneList> Drones = new List<DroneList>();
        IDal dalObject;
        double[] PowerConsumption;

        public BL()
        {
            dalObject = new DalObject.DalObject();
            PowerConsumption = dalObject.PowerConsumption();
<<<<<<< HEAD
            foreach (DO.Drone d in dalObject.GetDroneList())
=======
            foreach (IDAL.DO.Drone d in dalObject.GetDroneList())
>>>>>>> parent of d4aee0b (change namespaces IDAL/IBL to DalAPI/BlApi)
            {
                DroneList drone = new DroneList()
                {
                    ID = d.ID,
                    Model = d.Model,
                    Weight = (WeightCategories)d.WeightCategory
                };

<<<<<<< HEAD
                DO.Parcel parcel = ((List<DO.Parcel>)dalObject.GetParcelList()).Find(p => p.DroneID == d.ID && ParcelStatus(p) != Statuses.Delivered);

                if (!parcel.Equals(default(DO.Parcel))) {
=======
                IDAL.DO.Parcel parcel = ((List<IDAL.DO.Parcel>)dalObject.GetParcelList()).Find(p => p.DroneID == d.ID && ParcelStatus(p) != Statuses.Delivered);

                if (!parcel.Equals(default(IDAL.DO.Parcel))) {
>>>>>>> parent of d4aee0b (change namespaces IDAL/IBL to DalAPI/BlApi)
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
                    double minBattery = (distToDest / PowerConsumption[(int)drone.Weight + 1]) + distToStation / PowerConsumption[0];

                    drone.Battery = (random.NextDouble() * (100 - minBattery) + minBattery);
                }
                else
                {
                    drone.Status = (DroneStatuses)(random.Next(0, 2) * 2);

                    if (drone.Status == DroneStatuses.Maintenance)
                    {
<<<<<<< HEAD
                        DO.Station randStation = dalObject.GetStationList().ElementAt(random.Next(0, dalObject.GetStationList().Count()));
=======
                        IDAL.DO.Station randStation = dalObject.GetStationList().ElementAt(random.Next(0, dalObject.GetStationList().Count()));
>>>>>>> parent of d4aee0b (change namespaces IDAL/IBL to DalAPI/BlApi)
                        drone.Location = CoordinateToLocation(randStation.Location);
                        drone.Battery = random.NextDouble() * 20;
                        dalObject.ChargeDrone(d.ID, randStation.ID);
                    }
                    else // Drone is Free
                    {
<<<<<<< HEAD
                        IEnumerable<DO.Parcel> deliveredParcels = dalObject.GetParcelList().Where(parcel => !parcel.Delivered.Equals(DateTime.MinValue)).ToArray();
=======
                        IEnumerable<IDAL.DO.Parcel> deliveredParcels = dalObject.GetParcelList().Where(parcel => !parcel.Delivered.Equals(DateTime.MinValue)).ToArray();
>>>>>>> parent of d4aee0b (change namespaces IDAL/IBL to DalAPI/BlApi)
                        /*if (deliveredParcels.Count() == 0)
                            throw new InvalidManeuver("No Parcels have been delivered so starting drone location could not be determined.");*/
                        drone.Location = CoordinateToLocation(dalObject.GetCustomer(deliveredParcels.ElementAt(random.Next(0, deliveredParcels.Count())).TargetID).Location);

                        double batteryToStation = Distance(ClosestStation(drone.Location).Location, drone.Location) / PowerConsumption[0];
                        if (batteryToStation > 100)
                            throw new InvalidManeuver("Drone is too far away to be charged.");
                        drone.Battery = batteryToStation + (random.NextDouble() * (100 - batteryToStation));
                    }
                }

                Drones.Add(drone);
            }
        }
    }
}
