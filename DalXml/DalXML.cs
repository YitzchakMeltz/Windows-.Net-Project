using DO;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace Dal
{
    internal partial class DalXml : DalApi.IDal
    {
        internal static string DalFolder;

        internal XElement Config;

        internal XElement Customers;

        internal XElement Drones;

        internal XElement DroneCharges;

        internal XElement Parcels;

        internal XElement Stations;

        private DalXml()
        {
            DalFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            for (int i = 0; i < 3; i++)
                DalFolder = Path.GetDirectoryName(DalFolder);
            DalFolder += "\\Data\\";
            try
            {
                if (!Directory.Exists(DalFolder))
                {
                    Directory.CreateDirectory(DalFolder);
                    Directory.EnumerateFiles(Path.GetDirectoryName(Path.GetDirectoryName(DalFolder)) + "\\defaultData").ToList().ForEach(file => File.Copy(file, DalFolder + $"\\{Path.GetFileName(file)}"));
                }
            }
            catch (Exception e)
            {
                throw new InvalidInput("Couldn't copy default data", e);
            }

            #region debug
            /*DataSource.Initialize();
            Stations = new XElement("Stations");
            foreach (Station s in DataSource.Stations) AddStation(s);
            Drones = new XElement("Drones");
            foreach (Drone d in DataSource.Drones) AddDrone(d);
            Customers = new XElement("Customers");
            foreach (Customer c in DataSource.Customers) AddCustomer(c);
            Parcels = new XElement("Parcels");
            foreach (Parcel p in DataSource.Parcels) AddParcel(p);
            DroneCharges = new XElement("DroneCharges");
            SaveDroneCharges();*/
            #endregion

            try
            {
                Config = XElement.Load(DalFolder + "config.xml");
                Customers = XElement.Load(DalFolder + "Customers.xml");
                Drones = XElement.Load(DalFolder + "Drones.xml");
                DroneCharges = new XElement("DroneCharges");
                Parcels = XElement.Load(DalFolder + "Parcels.xml");
                Stations = XElement.Load(DalFolder + "Stations.xml");
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