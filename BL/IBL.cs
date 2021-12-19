<<<<<<< HEAD
﻿using BL;
=======
﻿using IBL.BO;
>>>>>>> parent of d4aee0b (change namespaces IDAL/IBL to DalAPI/BlApi)
using System;
using System.Collections.Generic;

namespace IBL
{
    public interface IBL
    {
        public void AddStation(int ID, string name, double latitude, double longitude, int availableChargeStations);
        public void AddDrone(int ID, string model, BL.WeightCategories weight, int stationID);
        public void AddCustomer(int ID, string name, string phone, double longitude, double latitude);
        public void AddPackage(int senderID, int receiverID, BL.WeightCategories weight, BL.Priorities priority);

        public void UpdateDrone(int ID, string model);
        public void UpdateStation(int ID, string name = null, int? totalChargeStation = null);
        public void UpdateCustomer(int ID, string name = null, string phone = null);
        public void ChargeDrone(int droneID);
        public void ReleaseDrone(int droneID, double chargingTime);
        public void AssignPackageToDrone(int droneID);
        public void CollectPackage(int droneID);
        public void DeliverPackage(int droneID);

        public BaseStation GetStation(int ID);
        public Drone GetDrone(int ID);
        public Customer GetCustomer(int ID);
        public Package GetPackage(int ID);

        public IEnumerable<BaseStationList> ListStations();
        public IEnumerable<DroneList> ListDrones();
        public IEnumerable<DroneList> ListDronesFiltered(Predicate<DroneList> p);
        public IEnumerable<CustomerList> ListCustomers();
        public IEnumerable<PackageList> ListPackages();
        public IEnumerable<PackageList> ListPackagesFiltered(Predicate<PackageList> p);
        public IEnumerable<BaseStationList> ListStationsFiltered(Predicate<BaseStationList> p);
    }
}
