using IDAL.DO;
using System;
using System.Collections.Generic;

namespace IDAL
{
    interface IDal
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
        public void AddStation();
        public void AddDrone();
        public void AddCustomer();
        public void AddParcel();
        public void AssignParcel();
        public void ParcelCollected();
        public void ParcelDelivered();
        public void ChargeDrone();
        public void ReleaseDrone();
        public double[] PowerConsumption();
    }
}