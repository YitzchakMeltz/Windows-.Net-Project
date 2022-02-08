using BO;
using System;
using System.Collections.Generic;

namespace BlApi
{
    public interface IBL
    {
        public void AddStation(uint ID, string name, double latitude, double longitude, uint availableChargeStations);
        public void AddDrone(uint ID, string model, BO.WeightCategories weight, uint stationID);
        public void AddCustomer(uint ID, string name, string phone, double longitude, double latitude, string password = "");
        public uint AddPackage(uint senderID, uint receiverID, BO.WeightCategories weight, BO.Priorities priority);

        public BaseStation GetStation(uint ID);
        public Drone GetDrone(uint ID);
        public Customer GetCustomer(uint ID);
        public Package GetPackage(uint ID);

        public void UpdateDrone(uint ID, string model);
        public void UpdateStation(uint ID, string name = null, uint? totalChargeStation = null);
        public void UpdateCustomer(uint ID, string name = null, string phone = null, string password = null);
        public void ChargeDrone(uint droneID);
        public void ReleaseDrone(uint droneID);
        public void AssignPackageToDrone(uint droneID);
        public void CollectPackage(uint droneID);
        public void DeliverPackage(uint droneID);
        public void DeletePackage(uint SenderID);

        public bool Login(uint ID, byte[] password);

        public IEnumerable<DroneList> ListDrones();
        public IEnumerable<DroneList> ListDronesFiltered(Predicate<DroneList> p);
        public IEnumerable<CustomerList> ListCustomers();
        public IEnumerable<PackageList> ListPackages();
        public IEnumerable<PackageList> ListPackagesFiltered(Predicate<PackageList> p);
        public IEnumerable<BaseStationList> ListStations();
        public IEnumerable<BaseStationList> ListStationsFiltered(Predicate<BaseStationList> p);

        public void ActivateSimulator(uint droneID, Action updateAction, Func<bool> stopCheck);
    }
}
