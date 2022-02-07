using BO;
using System;
using System.Collections.Generic;

namespace BlApi
{
    public interface IBL
    {
        public void AddStation(int ID, string name, double latitude, double longitude, int availableChargeStations);
        public void AddDrone(int ID, string model, BO.WeightCategories weight, int stationID);
        public void AddCustomer(int ID, string name, string phone, double longitude, double latitude, string password = "");
        public int AddPackage(int senderID, int receiverID, BO.WeightCategories weight, BO.Priorities priority);

        public void UpdateDrone(int ID, string model);
        public void UpdateStation(int ID, string name = null, int? totalChargeStation = null);
        public void UpdateCustomer(int ID, string name = null, string phone = null, string password = null);
        public void ChargeDrone(int droneID);
        public void ReleaseDrone(int droneID);
        public void AssignPackageToDrone(int droneID);
        public void CollectPackage(int droneID);
        public void DeliverPackage(int droneID);
        public void DeletePackage(int SenderID);

        public bool Login(int ID, byte[] password);

        public BaseStation GetStation(int ID);
        public Drone GetDrone(int ID);
        public Customer GetCustomer(int ID);
        public Package GetPackage(int ID);

        public IEnumerable<DroneList> ListDrones();
        public IEnumerable<DroneList> ListDronesFiltered(Predicate<DroneList> p);
        public IEnumerable<CustomerList> ListCustomers();
        public IEnumerable<PackageList> ListPackages();
        public IEnumerable<PackageList> ListPackagesFiltered(Predicate<PackageList> p);
        public IEnumerable<BaseStationList> ListStations();
        public IEnumerable<BaseStationList> ListStationsFiltered(Predicate<BaseStationList> p);
    }
}
