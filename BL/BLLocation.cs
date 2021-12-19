using IBL.BO;
using System;
using System.Collections.Generic;

namespace BL
{
    partial class BL : IBL.IBL
    {
        private Location CoordinateToLocation(DalApi.Util.Coordinate coord)
        {
            return new Location()
            {
                Latitude = coord.Latitude,
                Longitude = coord.Longitude
            };
        }

        private double Distance(Location source, Location dest)
        {
            return LocationToCoordinate(source).DistanceTo(LocationToCoordinate(dest)) / 1000;
        }

        private DalApi.Util.Coordinate LocationToCoordinate(Location location)
        {
            return new DalApi.Util.Coordinate(location.Latitude, location.Longitude);
        }
    }
}
