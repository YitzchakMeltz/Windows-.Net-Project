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
        public Drone GetDrone(uint ID);
        public Station GetStation(uint ID);
        public Customer GetCustomer(uint ID);
        public Parcel GetParcel(uint ID);
        public IEnumerable<Drone> GetDroneList();
        public IEnumerable<Station> GetStationList();
        public IEnumerable<Station> GetFilteredStationList(Predicate<Station> p);
        public IEnumerable<Customer> GetCustomerList();
        public IEnumerable<Parcel> GetParcelList();
        public IEnumerable<Parcel> GetFilteredParcelList(Predicate<Parcel> p);
        
        public void AddStation(uint id, string name, uint chargeSlots, double latitude, double longitude);
        public void AddDrone(uint id, string model, WeightCategories weightCat);
        public void AddCustomer(uint id, string name, string phoneNum, double latitude, double longitude, string password = "");
        public uint AddParcel(uint senderID, uint targetID, WeightCategories weightCat, Priorities priority, uint droneID);

        public void RemoveCustomer(uint ID);
        public void RemoveDrone(uint ID);
        public void RemoveStation(uint ID);
        public void RemoveParcel(uint ID);

        public void AssignParcel(uint parcelID, uint droneID);
        public void ParcelCollected(uint parcelID);
        public void ParcelDelivered(uint parcelID);
        public void ChargeDrone(uint droneID, uint stationID);
        public double ReleaseDrone(uint droneID);
        
        public double[] PowerConsumption();
    }
}