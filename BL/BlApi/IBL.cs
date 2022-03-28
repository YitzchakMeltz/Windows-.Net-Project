using BO;
using System;
using System.Collections.Generic;

namespace BlApi
{
    public interface IBL
    {
        /// <summary>
        /// Adds a new Station to the system
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="name"></param>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="availableChargeStations"></param>
        public void AddStation(uint ID, string name, double latitude, double longitude, uint availableChargeStations);

        /// <summary>
        /// Adds a new Drone to the system
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="model"></param>
        /// <param name="weight"></param>
        /// <param name="stationID"></param>
        public void AddDrone(uint ID, string model, BO.WeightCategories weight, uint stationID);

        /// <summary>
        /// Adds a new Customer to the system
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <param name="password"></param>
        public void AddCustomer(uint ID, string name, string phone, double longitude, double latitude, string password = "");

        /// <summary>
        /// Adds a new Package to the system
        /// </summary>
        /// <param name="senderID"></param>
        /// <param name="receiverID"></param>
        /// <param name="weight"></param>
        /// <param name="priority"></param>
        /// <returns></returns>
        public uint AddPackage(uint senderID, uint receiverID, BO.WeightCategories weight, BO.Priorities priority);



        /// <summary>
        /// Retrieves the Station from the system with a specified ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>BaseStation with matching ID</returns>
        public BaseStation GetStation(uint ID);

        /// <summary>
        /// Retrieves the Drone from the system with a specified ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>Drone with matching ID</returns>
        public Drone GetDrone(uint ID);

        /// <summary>
        /// Retrieves the Customer from the system with a specified ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>Customer with matching ID</returns>
        public Customer GetCustomer(uint ID);

        /// <summary>
        /// Retrieves the Package from the system with a specified ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>Package with matching ID</returns>
        public Package GetPackage(uint ID);


        
        /// <summary>
        /// Updates the model of a Drone
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="model"></param>
        public void UpdateDrone(uint ID, string model);

        /// <summary>
        /// Updates a station. Fields left null will not be changed
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="name"></param>
        /// <param name="totalChargeStation"></param>
        public void UpdateStation(uint ID, string name = null, uint? totalChargeStation = null);

        /// <summary>
        /// Updates a Customer. Fields left null will not be changed.
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="passwords">[old, new1, new2]</param>
        public void UpdateCustomer(uint ID, string name = null, string phone = null, string[] passwords = null);

        /// <summary>
        /// Sends a Drone to nearest BaseStation to charge
        /// </summary>
        /// <param name="droneID"></param>
        public void ChargeDrone(uint droneID);

        /// <summary>
        /// Releases a Drone from charging. Battery gets updated accordingly
        /// </summary>
        /// <param name="droneID"></param>
        public void ReleaseDrone(uint droneID);

        /// <summary>
        /// Assigns Package to Drone according to DroneIcon proprietary algorithm
        /// </summary>
        /// <param name="droneID"></param>
        public void AssignPackageToDrone(uint droneID);

        /// <summary>
        /// Sends a Drone to collect its assigned Package
        /// </summary>
        /// <param name="droneID"></param>
        public void CollectPackage(uint droneID);

        /// <summary>
        /// Sends a Drone to deliver its collected Package
        /// </summary>
        /// <param name="droneID"></param>
        public void DeliverPackage(uint droneID);

        /// <summary>
        /// Removes a Package from the system. Only available for Packages that have not yet been assigned
        /// </summary>
        /// <param name="SenderID"></param>
        public void DeletePackage(uint SenderID);



        /// <summary>
        /// Authenticates a user 
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="password"></param>
        /// <returns>True if authorized. False if unauthorized</returns>
        public bool Login(uint ID, byte[] password);



        /// <summary>
        /// Returns a list of all Drones in the system
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DroneList> ListDrones();

        /// <summary>
        /// Returns a filtered list of Drones in the system
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public IEnumerable<DroneList> ListDronesFiltered(Predicate<DroneList> p);

        /// <summary>
        /// Returns a list of all Customers in the system
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CustomerList> ListCustomers();

        /// <summary>
        /// Returns a list of all Packages in the system
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PackageList> ListPackages();

        /// <summary>
        /// Returns a filtered list of Packages in the system
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public IEnumerable<PackageList> ListPackagesFiltered(Predicate<PackageList> p);

        /// <summary>
        /// Returns a list of all Stations in the system
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BaseStationList> ListStations();

        /// <summary>
        /// Returns a filtered list of Stations in the system
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public IEnumerable<BaseStationList> ListStationsFiltered(Predicate<BaseStationList> p);


        /// <summary>
        /// Activates the Simulator for a specified drone.
        /// </summary>
        /// <param name="droneID">The ID of the drone to activate the simulator on</param>
        /// <param name="updateAction">The callback action</param>
        /// <param name="stopCheck">The function to check if the simulator should stop</param>
        public void ActivateSimulator(uint droneID, Action updateAction, Func<bool> stopCheck);
    }
}
