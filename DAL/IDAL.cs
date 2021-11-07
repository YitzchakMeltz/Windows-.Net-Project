using IDAL.DO;
using System.Collections.Generic;

namespace IDAL
{
    public interface IDal
    {
        public void AddDrone(Drone drone);
        public void AddStation(Station station);
        public void AddCustomer(Customer customer);
        public int AddParcel(Parcel parcel);
        public Drone GetDrone(int ID);
        public Station GetStation(int ID);
        public Customer GetCustomer(int ID);
        public Parcel GetParcel(int ID);
        public IEnumerable<Drone> GetDroneList();
        public IEnumerable<Station> GetStationList();
        public IEnumerable<Station> GetAvailableStationList();
        public IEnumerable<Customer> GetCustomerList();
        public IEnumerable<Parcel> GetParcelList();
        public IEnumerable<Parcel> GetUnassignedParcelList();
        
        public void AddStation(string name, int chargeSlots, double latitude, double longitude);
        public void AddDrone(string model, WeightCategories weightCat);
        public void AddCustomer(string name, string phoneNum, double latitude, double longitude);
        public void AddParcel(int senderID, int targetID, WeightCategories weightCat, Priorities priority, int droneID);
        
        public void AssignParcel(int parcelID, int droneID);
        public void ParcelCollected(int parcelID);
        public void ParcelDelivered(int parcelID);
        public void ChargeDrone(int droneID, int stationID);
        public void ReleaseDrone(int droneID);
        
        public double[] PowerConsumption();
    }
}