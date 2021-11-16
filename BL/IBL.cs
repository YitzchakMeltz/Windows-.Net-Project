using IBL.BO;
using System.Collections.Generic;

namespace IBL
{
    public interface IBL
    {
        public void AddStation(int ID, string name, double latitude, double longitude, int availableChargeStations);
        public void AddDrone(int ID, string model, BO.WieghtCategories weight, int stationID);
        public void AddCustomer(int ID, string name, string phone, double longitude, double latitude);
        public void AddPackage(int senderID, int receiverID, BO.WieghtCategories weight, BO.Priorities priority);

        public void UpdateDrone(int ID, string model);
        public void UpdateStation(int ID, string name = null, int? totalChargeStation = null);
        public void UpdateCustomer(int ID, string name = null, string phone = null);
        public void ChargeDrone(int droneID);
        public void ReleaseDrone(int droneID, double chargingTime);
        public void AssignPackageToDrone(int droneID);
        public void CollectPackage(int droneID);
        public void DeliverPackage(int droneID);

        public void ShowStation(int ID);
        public void ShowDrone(int ID);
        public void ShowCustomer(int ID);
        public void ShowPackage(int ID);

        public IEnumerable<BaseStationList> ListStations();
        public IEnumerable<DroneList> ListDrones();
        public IEnumerable<CustomerList> ListCustomers();
        public IEnumerable<PackageList> ListPackages();
        public IEnumerable<PackageList> ListUnassignedPackages();
        public IEnumerable<BaseStationList> ListStationsWithAvailableChargeSlots();
    }
}
