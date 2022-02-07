using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal
{
    partial class DalXml
    {
        /*private void LoadDroneCharges()
        {
            try
            {
                DroneCharges = XElement.Load(DalFolder + "DroneCharges.xml");
            }
            catch (Exception e)
            {
                throw new InvalidInput("Couldn't read DroneCharges.xml.", e);
            }
        }*/
        private void SaveDroneCharges()
        {
            try
            {
                DroneCharges.Save(DalFolder + "DroneCharges.xml");
            }
            catch (Exception e)
            {
                throw new InvalidInput("Error accessing DroneCharges.xml", e);
            }
        }

        /// <summary>
        /// Charges a Drone
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void ChargeDrone(uint droneID, uint stationID)
        {
            GetDrone(droneID);                                               // Forces error if drone doesn't exist

            XElement station = GetStationElement(stationID);
            uint availableChargeSlots = UInt32.Parse(station.Element("AvailableChargeSlots").Value);
            if (availableChargeSlots == 0)
                throw new DO.InvalidInput("No charging slots available in nearest station.");
            station.Element("AvailableChargeSlots").SetValue(--availableChargeSlots);

            SaveStations();

            DroneCharges.Add(new XElement("DroneCharge",
                    new XElement("DroneID", droneID),
                    new XElement("StationID", stationID),
                    new XElement("ChargeTime", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"))
            ));

            SaveDroneCharges();
        }

        /// <summary>
        /// Releases a Drone from charging
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public double ReleaseDrone(uint droneID)
        {
            Drone drone = GetDrone(droneID);                                                  // Forces error if drone doesn't exist

            // Finds DroneCharge with droneID and removes it
            XElement dc = DroneCharges.Elements().Where(dc => Int32.Parse(dc.Element("DroneID").Value) == droneID).FirstOrDefault();

            if (dc is null)
                throw new ObjectNotFound($"Drone with ID: {droneID} was not charging.");

            dc.Remove();
            SaveDroneCharges();

            // Add 1 to Available Charging Slots of corresponding station
            XElement station = GetStationElement(UInt32.Parse(dc.Element("StationID").Value));
            int availableChargeSlots = Int32.Parse(station.Element("AvailableChargeSlots").Value);
            station.Element("AvailableChargeSlots").SetValue(++availableChargeSlots);
            SaveStations();

            return DateTime.Now.Subtract(DateTime.ParseExact(dc.Element("ChargeTime").Value, "MM/dd/yyyy HH:mm:ss", null)).TotalMinutes;
        }
    }
}
