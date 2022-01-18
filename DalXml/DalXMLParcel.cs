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
        private XElement LoadParcels()
        {
            try
            {
                return XElement.Load(Parcels);
            }
            catch (Exception e)
            {
                throw new InvalidInput("Couldn't read xml file.", e);
            }
        }
        private void SaveParcels(XElement root)
        {
            try
            {
                root.Save(Parcels);
            }
            catch (Exception e)
            {
                throw new InvalidInput("Couldn't read xml file.", e);
            }
        }
        public int AddParcel(Parcel parcel)
        {
            XElement parcels = LoadParcels();
            parcels.Add(new XElement("Parcel",
                    new XElement("ID", parcel.ID),
                    new XElement("SenderID", parcel.SenderID),
                    new XElement("TargetID", parcel.TargetID),
                    new XElement("Weight", parcel.WeightCategory),
                    new XElement("Priority", parcel.Priority),
                    new XElement("DroneID", parcel.DroneID),
                    new XElement("Scheduled", parcel.Scheduled),
                    new XElement("Assigned", parcel.Assigned),
                    new XElement("PickedUp", parcel.PickedUp),
                    new XElement("Delivered", parcel.Delivered)
            ));
            SaveParcels(parcels);
        }
    }
}
