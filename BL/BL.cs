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
            foreach (IDAL.DO.Drone d in dalObject.GetDroneList())
            {
                DroneList drone = new DroneList()
                {
                    ID = d.ID,
                    Model = d.Model,
                    Weight = (WieghtCategories)d.WeightCategory
                };

                IDAL.DO.Parcel parcel = ((List<IDAL.DO.Parcel>)dalObject.GetParcelList()).Find(p => p.DroneID == d.ID && ParcelStatus(p) != Statuses.Delivered);

                if (!parcel.Equals(default(IDAL.DO.Parcel))) {
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
                        drone.Location = CoordinateToLocation(dalObject.GetStationList().ElementAt(random.Next(0, dalObject.GetStationList().Count())).Location);
                        drone.Battery = random.NextDouble() * 20;
                    }
                    else
                    {
                        IEnumerable<IDAL.DO.Parcel> deliveredParcels = dalObject.GetParcelList().Where(parcel => !parcel.Delivered.Equals(default(DateTime))).ToArray();
                        drone.Location = CoordinateToLocation(dalObject.GetCustomer(deliveredParcels.ElementAt(random.Next(0, deliveredParcels.Count())).TargetID).Location);
                    }
                }

                Drones.Add(drone);
            }
        }

        public void AddStation(int ID, string name, double latitude, double longitude, int availableChargeStations)
        {
            BaseStation station = new BaseStation()
            {
                ID = ID,
                Name = name,
                Location = new Location() { Longitude = longitude, Latitude = latitude },
                AvailableChargingSlots = (uint)availableChargeStations, // Why is it an in in the function declaration???
                ChargingDrones = new List<ChargingDrone>()
            };
        }
        public void AddDrone(int ID, string model, IBL.BO.WieghtCategories weight, int stationID)
        {
            Drone drone = new Drone()
            {
                ID = ID,
                Model = model,
                Weight = weight,
                Location = GetStation(stationID).Location, 
                Status = DroneStatuses.Maintenance,
                Battery = (random.NextDouble() * 20 ) + 20
            };
        }

        public void AddCustomer(int ID, string name, string phone, double longitude, double latitude)
        {
            Customer customer = new Customer()
            {
                ID = ID,
                Name = name,
                Phone = phone,
                Location = new Location() { Longitude = longitude, Latitude = latitude }
            };
        }

        public void AddPackage(int senderID, int receiverID, IBL.BO.WieghtCategories weight, IBL.BO.Priorities priority)
        {
            Package package = new Package()
            {
                Sender = new PackageCustomer { ID = senderID },
                Receiver = new PackageCustomer { ID = receiverID },
                Weight = weight,
                Priority = priority,
                Drone = null,
                Creation = DateTime.Now,
                AssignmentTime = DateTime.MinValue,
                CollectionTime = DateTime.MinValue,
                DeliveryTime = DateTime.MinValue
            };
        }


        public void UpdateDrone(int ID, string model)
        {

        }
    }
}
