using DO;
using System;
using System.Collections.Generic;

namespace DalApi
{
    public interface IDal
    {
        public void AddDrone(Drone drone);
        public void AddStation(Station station);
        public void AddCustomer(Customer customer);
        public void AddParcel(Parcel parcel);
        public Drone GetDrone(int ID);
        public Station GetStation(int ID);
        public Customer GetCustomer(int ID);
        public Parcel GetParcel(int ID);
        public IEnumerable<Drone> GetDroneList();
        public IEnumerable<Station> GetStationList();
        public IEnumerable<Station> GetFilteredStationList(Predicate<Station> p);
        public IEnumerable<Customer> GetCustomerList();
        public IEnumerable<Parcel> GetParcelList();
        public IEnumerable<Parcel> GetFilteredParcelList(Predicate<Parcel> p);
        
        public void AddStation(int id, string name, int chargeSlots, double latitude, double longitude);
        public void AddDrone(int id, string model, WeightCategories weightCat);
        public void AddCustomer(int id, string name, string phoneNum, double latitude, double longitude, string password = "");
        public int AddParcel(int senderID, int targetID, WeightCategories weightCat, Priorities priority, int droneID);

        public void RemoveCustomer(int ID);
        public void RemoveDrone(int ID);
        public void RemoveStation(int ID);
        public void RemoveParcel(int ID);

        public void AssignParcel(int parcelID, int droneID);
        public void ParcelCollected(int parcelID);
        public void ParcelDelivered(int parcelID);
        public void ChargeDrone(int droneID, int stationID);
        public double ReleaseDrone(int droneID);
        
        public double[] PowerConsumption();
    }
}