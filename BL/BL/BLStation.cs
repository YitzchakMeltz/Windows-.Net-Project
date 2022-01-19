using BO;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL
{
    partial class BL : BlApi.IBL
    {
        private BaseStation ClosestStation(Location location)
        {
            DalApi.Util.Coordinate coord = LocationToCoordinate(location);
            return GetStation(dalObject.GetStationList().OrderBy(s => s.Location.DistanceTo(coord)).First().ID);
        }

        public BaseStation GetStation(int stationID)
        {
            try
            {
                DO.Station station = dalObject.GetStation(stationID);

                BaseStation baseStation = new BaseStation()
                {
                    ID = stationID,
                    Name = station.Name,
                    Location = CoordinateToLocation(station.Location),
                    AvailableChargingSlots = (uint)station.AvailableChargeSlots,
                    ChargingDrones = new List<ChargingDrone>()
                };

                Drones.FindAll(d => d.Status == DroneStatuses.Maintenance && d.Location == baseStation.Location).ForEach(drone =>
                    baseStation.ChargingDrones.Add(new ChargingDrone() { ID = drone.ID, Battery = drone.Battery }));

                return baseStation;
            }
            catch (DO.ObjectNotFound e)
            {
                throw new BO.ObjectNotFound(e.Message);
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
                DO.Station station = dalObject.GetStation(ID);

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
            catch (DO.ObjectNotFound e)
            {
                throw new BO.ObjectNotFound(e.Message);
            }
        }

        public IEnumerable<BaseStationList> ListStations()
        {
            IEnumerable<Station> dalStations = dalObject.GetStationList();

            List<BaseStationList> blStations = new List<BaseStationList>();
            dalStations.ToList().ForEach(dalStation =>
            {
                BaseStation blStation = GetStation(dalStation.ID);
                blStations.Add(new BaseStationList() { ID = blStation.ID, Name = blStation.Name, ChargingSlotsAvailable = blStation.AvailableChargingSlots, ChargingSlotsOccupied = (uint)blStation.ChargingDrones.Count });
            });

            return blStations;
        }

        public IEnumerable<BaseStationList> ListStationsFiltered(Predicate<BaseStationList> pred)
        {
            return ListStations().Where(pred.Invoke);
        }
    }
}
