using IBL.BO;
using IDAL.DO;
using System;
using System.Collections.Generic;

namespace BL
{
    partial class BL : IBL.IBL
    {
        private BaseStation ClosestStation(Location location)
        {
            List<Station> listOfStations = (List<Station>)dalObject.GetStationList();
            IDAL.Util.Coordinate coord = LocationToCoordinate(location);

            listOfStations.Sort((x, y) => (int)(x.Location.DistanceTo(coord) - y.Location.DistanceTo(coord)));
            return GetStation(listOfStations[0].ID);
        }

        public BaseStation GetStation(int stationID)
        {
            try
            {
                IDAL.DO.Station station = dalObject.GetStation(stationID);

                BaseStation baseStation = new BaseStation()
                {
                    ID = stationID,
                    Name = station.Name,
                    Location = CoordinateToLocation(station.Location),
                    AvailableChargingSlots = (uint)station.AvailableChargeSlots,
                    ChargingDrones = new List<ChargingDrone>()
                };

                foreach (IBL.BO.DroneList drone in Drones.FindAll(d => d.Location == baseStation.Location && d.Status == DroneStatuses.Maintenance))
                {
                    baseStation.ChargingDrones.Add(new ChargingDrone() { ID = drone.ID, Battery = drone.Battery });
                }

                return baseStation;
            }
            catch (IDAL.DO.ObjectNotFound e)
            {
                throw new IBL.BO.ObjectNotFound(e.Message);
            }
        }

        public void AddStation(int ID, string name, double latitude, double longitude, int availableChargeStations)
        {
            dalObject.AddStation(ID, name, availableChargeStations, latitude, longitude);
        }

        public void UpdateStation(int ID, string name = null, int? totalChargeStation = null)
        {
            // Update DALStation

            try
            {
                IDAL.DO.Station station = dalObject.GetStation(ID);

                if (name != null)
                {
                    station.Name = name;
                }

                if(totalChargeStation != null)
                {
                    station.AvailableChargeSlots = (int)totalChargeStation;
                }

                dalObject.RemoveStation(ID);
                dalObject.AddStation(station);
            }
            catch (IDAL.DO.ObjectNotFound e)
            {
                throw new IBL.BO.ObjectNotFound(e.Message);
            }
        }

        public IEnumerable<BaseStationList> ListStations()
        {
            IEnumerable<Station> dalStations = dalObject.GetStationList();

            List<BaseStationList> blStations = new List<BaseStationList>();
            foreach (Station dalStation in dalStations)
            {
                BaseStation blStation = GetStation(dalStation.ID);
                blStations.Add(new BaseStationList() { ID = blStation.ID, Name = blStation.Name, ChargingSlotsAvailable = blStation.AvailableChargingSlots, ChargingSlotsOccupied = (uint)blStation.ChargingDrones.Count });
            }

            return blStations;
        }

        public IEnumerable<BaseStationList> ListStationsWithAvailableChargeSlots()
        {
            IEnumerable<Station> dalStations = dalObject.GetAvailableStationList();

            List<BaseStationList> availableStations = new List<BaseStationList>();
            foreach (Station dalStation in dalStations)
            {
                BaseStation blStation = GetStation(dalStation.ID);
                if (blStation.AvailableChargingSlots == 0)
                    throw new LogicError($"Base Station with ID {blStation.ID} has no available charge slots but was listed as available.");
                availableStations.Add(new BaseStationList() { ID = blStation.ID, Name = blStation.Name, ChargingSlotsAvailable = blStation.AvailableChargingSlots, ChargingSlotsOccupied = (uint)blStation.ChargingDrones.Count });
            }

            return availableStations;
        }
    }
}
