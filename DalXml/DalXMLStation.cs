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
        /*private void LoadStations()
        {
            try
            {
                Stations = XElement.Load(DalFolder + "Stations.xml");
            }
            catch (Exception e)
            {
                throw new InvalidInput("Couldn't read Stations.xml.", e);
            }
        }*/
        private void SaveStations()
        {
            try
            {
                Stations.Save(DalFolder + "\\Stations.xml");
            }
            catch (Exception e)
            {
                throw new InvalidInput("Error accessing Stations.xml", e);
            }
        }

        /// <summary>
        /// Adds a Station
        /// </summary>
        /// <param name="station"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddStation(Station station)
        {
            if (Stations.Elements().Any(s => Int32.Parse(s.Element("ID").Value) == station.ID))
                throw new ObjectAlreadyExists($"Station with ID {station.ID} already exists.");

            Stations.Add(new XElement("Station",
                    new XElement("ID", station.ID),
                    new XElement("Name", station.Name),
                    new XElement("AvailableChargeSlots", station.AvailableChargeSlots),
                    new XElement("Location",
                        new XElement("Latitude", station.Location.Latitude),
                        new XElement("Longitude", station.Location.Longitude))
            ));

            SaveStations();
        }

        /// <summary>
        /// Adds a Station
        /// </summary>
        /// <param name="station"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddStation(uint id, string name, uint chargeSlots, double latitude, double longitude)
        {
            Station station = new Station()
            {
                ID = id,
                Name = name,
                AvailableChargeSlots = chargeSlots,
                Location = new DalApi.Util.Coordinate(latitude, longitude)
            };

            AddStation(station);
        }

        private XElement GetStationElement(uint ID)
        {
            XElement station = Stations.Elements().Where(s => Int32.Parse(s.Element("ID").Value) == ID).FirstOrDefault();

            if (station is null)
                throw new ObjectNotFound($"Station with ID: {ID} not found.");

            return station;
        }

        private Station StationParse(XElement s)
        {
            return new Station()
            {
                ID = UInt32.Parse(s.Element("ID").Value),
                Name = s.Element("Name").Value,
                AvailableChargeSlots = UInt32.Parse(s.Element("AvailableChargeSlots").Value),
                Location = new DalApi.Util.Coordinate(Double.Parse(s.Element("Location").Element("Latitude").Value), Double.Parse(s.Element("Location").Element("Longitude").Value))
            };
        }

        /// <summary>
        /// Returns a Base Station by its ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Station GetStation(uint ID)
        {
            return StationParse(GetStationElement(ID));
        }

        /// <summary>
        /// Returns an array of all Base Stations
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Station> GetStationList()
        {
            return Stations.Elements().Select(s => StationParse(s));
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Station> GetFilteredStationList(Predicate<Station> pred)
        {
            return (from s in Stations.Elements()
                    select StationParse(s)).Where(pred.Invoke);
        }

        /// <summary>
        /// Deletes a Station
        /// </summary>
        /// <param name="ID"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void RemoveStation(uint ID)
        {
            GetStationElement(ID).Remove();
            SaveStations();
        }
    }
}
