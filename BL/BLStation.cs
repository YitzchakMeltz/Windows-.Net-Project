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

        private BaseStation GetStation(int stationID)
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

                return baseStation;
            }
            catch (IDAL.DO.ObjectNotFound e)
            {
                throw new IBL.BO.ObjectNotFound(e.Message);
            }
        }
    }
}
