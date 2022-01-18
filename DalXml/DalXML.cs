using DO;
using System;
using System.Xml.Linq;

namespace Dal
{
    internal partial class DalXml : DalApi.IDal
    {
        private static string DalFolder = @"Data\";
        private string Customers = DalFolder + "Customers.xml";
        private string Drones = DalFolder + "Drones.xml";
        private string DroneCharges = DalFolder + "DroneCharges.xml";
        private string Parcels = DalFolder + "Parcels.xml";
        private string Stations = DalFolder + "Stations.xml";

        private DalXml()
        {
            try
            {
                //Customers = XElement.Load(DalFolder + "Customer.xml");
                //Drones = XElement.Load(DalFolder + "Drone.xml");
                //DroneCharges = XElement.Load(DalFolder + "DroneCharge.xml");
                //Parcels = XElement.Load(DalFolder + "Parcel.xml");
                //Stations = XElement.Load(DalFolder + "Station.xml");
            }
            catch (Exception e)
            {
                throw new InvalidInput("Couldn't read xml file.", e);
            }
        }

        private static readonly Lazy<DalXml> lazy = new Lazy<DalXml>(() => new DalXml());
        public static DalXml Instance
        {
            get
            {
                return lazy.Value;
            }
        }
    }
}