using IBL.BO;
using System;
using System.Collections.Generic;

namespace BL
{
    partial class BL : IBL.IBL
    {
        private Location CoordinateToLocation(IDAL.Util.Coordinate coord)
        {
            return new Location()
            {
                Latitude = coord.Latitude,
                Longitude = coord.Longitude
            };
        }

        private double Distance(Location source, Location dest)
        {
            return LocationToCoordinate(source).DistanceTo(LocationToCoordinate(dest));
        }

        private IDAL.Util.Coordinate LocationToCoordinate(Location location)
        {
            return new IDAL.Util.Coordinate(location.Latitude, location.Longitude);
        }
    }
}
