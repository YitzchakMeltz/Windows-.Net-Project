using DO;
using System;
using System.Collections.Generic;

namespace DalApi
{
    public interface IDal
    {
        /// <summary>
        /// Adds a Drone to the system
        /// </summary>
        /// <param name="drone"></param>
        public void AddDrone(Drone drone);

        /// <summary>
        /// Creates a new Drone and adds it to the system
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="weightCat"></param>
        public void AddDrone(uint id, string model, WeightCategories weightCat);

        /// <summary>
        /// Adds a Station to the system
        /// </summary>
        /// <param name="station"></param>
        public void AddStation(Station station);

        /// <summary>
        /// Creates a new Station and adds it to the system
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="chargeSlots"></param>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        public void AddStation(uint id, string name, uint chargeSlots, double latitude, double longitude);

        /// <summary>
        /// Adds a Customer to the system
        /// </summary>
        /// <param name="customer"></param>
        public void AddCustomer(Customer customer);

        /// <summary>
        /// Creates a new Customer and adds it to the system
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="phoneNum"></param>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="password"></param>
        public void AddCustomer(uint id, string name, string phoneNum, double latitude, double longitude, string password = "");

        /// <summary>
        /// Adds a parcel to the system
        /// </summary>
        /// <param name="parcel"></param>
        public void AddParcel(Parcel parcel);

        /// <summary>
        /// Creates a new Parcel and adds it to the system
        /// </summary>
        /// <param name="senderID"></param>
        /// <param name="targetID"></param>
        /// <param name="weightCat"></param>
        /// <param name="priority"></param>
        /// <param name="droneID"></param>
        /// <returns>The ID of the newly created Parcel</returns>
        public uint AddParcel(uint senderID, uint targetID, WeightCategories weightCat, Priorities priority, uint droneID);



        /// <summary>
        /// Retrieves the Drone from the system with a specified ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Drone GetDrone(uint ID);

        /// <summary>
        /// Retrieves the Station from the system with a specified ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Station GetStation(uint ID);

        /// <summary>
        /// Retrieves the Customer from the system with a specified ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Customer GetCustomer(uint ID);

        /// <summary>
        /// Retrieves the Parcel from the system with a specified ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Parcel GetParcel(uint ID);

        /// <summary>
        /// Returns a list of all Drones in the system
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public IEnumerable<Drone> GetDroneList();

        /// <summary>
        /// Returns a list of all Stations in the system
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public IEnumerable<Station> GetStationList();

        /// <summary>
        /// Returns a filtered list of Stations in the system
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public IEnumerable<Station> GetFilteredStationList(Predicate<Station> p);

        /// <summary>
        /// Returns a list of all Customers in the system
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public IEnumerable<Customer> GetCustomerList();

        /// <summary>
        /// Returns a list of all Parcels in the system
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public IEnumerable<Parcel> GetParcelList();

        /// <summary>
        /// Returns a filtered list of Parcels in the system
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public IEnumerable<Parcel> GetFilteredParcelList(Predicate<Parcel> p);

        /// <summary>
        /// Removes a Customer from the system
        /// </summary>
        /// <param name="ID"></param>
        public void RemoveCustomer(uint ID);

        /// <summary>
        /// Removes a Drone from the system
        /// </summary>
        /// <param name="ID"></param>
        public void RemoveDrone(uint ID);

        /// <summary>
        /// Removes a Station from the system
        /// </summary>
        /// <param name="ID"></param>
        public void RemoveStation(uint ID);

        /// <summary>
        /// Removes a Parcel from the system
        /// </summary>
        /// <param name="ID"></param>
        public void RemoveParcel(uint ID);

        /// <summary>
        /// Assigns a Drone to a Parcel
        /// </summary>
        /// <param name="parcelID"></param>
        /// <param name="droneID"></param>
        public void AssignParcel(uint parcelID, uint droneID);

        /// <summary>
        /// Marks a Parcel as Collected by a Drone
        /// </summary>
        /// <param name="parcelID"></param>
        public void ParcelCollected(uint parcelID);

        /// <summary>
        /// Marks a Parcel as Delivered
        /// </summary>
        public void ParcelDelivered(uint parcelID);

        /// <summary>
        /// Charges a Drone
        /// </summary>
        public void ChargeDrone(uint droneID, uint stationID);

        /// <summary>
        /// Releases a Drone from charging
        /// </summary>
        public double ReleaseDrone(uint droneID);

        /// <summary>
        /// Returns the power consumption configuration
        /// </summary>
        /// <returns></returns>
        public double[] PowerConsumption();
    }
}