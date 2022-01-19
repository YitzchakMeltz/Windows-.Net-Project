using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal
{
    partial class DalXml
    {
        /*private void LoadConfig()
        {
            try
            {
                Config = XElement.Load(DalFolder + "config.xml");
            }
            catch (Exception e)
            {
                throw new InvalidInput("Couldn't read config.xml.", e);
            }
        }*/
        private void SaveConfig()
        {
            try
            {
                Config.Save(DalFolder + "config.xml");
            }
            catch (Exception e)
            {
                throw new InvalidInput("Error accessing config.xml", e);
            }
        }

        public double[] PowerConsumption()
        {
            XElement BatteryUsage = Config.Element("Battery").Element("Usage");
            return new double[] 
            { 
                Double.Parse(BatteryUsage.Attribute("Free").Value), 
                Double.Parse(BatteryUsage.Attribute("Light").Value),
                Double.Parse(BatteryUsage.Attribute("Medium").Value),
                Double.Parse(BatteryUsage.Attribute("Heavy").Value),
                Double.Parse(BatteryUsage.Parent.Element("ChargeRate").Value)
            };
        }
        private int nextPackageID()
        {
            try
            {
                int result = Int32.Parse(Config.Element("PackageID").Value);
                Config.SetElementValue("PackageID", result + 1);
                SaveConfig();
                return result;
            }
            catch (Exception e)
            {
                throw new DO.XmlError(e.Message);
            }
        }
    }
}
