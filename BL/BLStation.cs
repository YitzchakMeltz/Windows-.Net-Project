using IBL.BO;
using IDAL.DO;
using System;
using System.Collections.Generic;

namespace BL
{
    partial class BL : IBL.IBL
    {
        private Station ClosestTo(IDAL.Util.Coordinate location)
        {
            List<Station> listOfStations = (List<Station>)dalObject.GetStationList();
            listOfStations.Sort((x, y) => (int)(x.Location.DistanceTo(location) - y.Location.DistanceTo(location)));
            return listOfStations[0];
        }
    }
}
