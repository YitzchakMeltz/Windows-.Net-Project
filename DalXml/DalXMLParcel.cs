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
        /*private void LoadParcels()
        {
            try
            {
                Parcels = XElement.Load(DalFolder + "Parcels.xml");
            }
            catch (Exception e)
            {
                throw new InvalidInput("Couldn't read xml file.", e);
            }
        }*/
        private void SaveParcels()
        {
            try
            {
                Parcels.Save(DalFolder + "Parcels.xml");
            }
            catch (Exception e)
            {
                throw new InvalidInput("Couldn't access Parcels.xml", e);
            }
        }
        public void AddParcel(Parcel parcel)
        {
            if (Parcels.Elements().Any(p => Int32.Parse(p.Element("ID").Value) == parcel.ID))
                throw new ObjectAlreadyExists($"Parcel with ID {parcel.ID} already exists.");

            Parcels.Add(new XElement("Parcel",
                    new XElement("ID", parcel.ID),
                    new XElement("SenderID", parcel.SenderID),
                    new XElement("TargetID", parcel.TargetID),
                    new XElement("Weight", parcel.WeightCategory),
                    new XElement("Priority", parcel.Priority),
                    new XElement("DroneID", parcel.DroneID),
                    new XElement("Scheduled", parcel.Scheduled.ToString("MM/dd/yyyy HH:mm:ss")),
                    parcel.Assigned is null ? null : new XElement("Assigned", parcel.Assigned.Value.ToString("MM/dd/yyyy HH:mm:ss")),
                    parcel.PickedUp is null ? null : new XElement("PickedUp", parcel.PickedUp.Value.ToString("MM/dd/yyyy HH:mm:ss")),
                    parcel.Delivered is null ? null : new XElement("Delivered", parcel.Delivered.Value.ToString("MM/dd/yyyy HH:mm:ss"))
            ));

            SaveParcels();
        }
        public int AddParcel(int senderID, int targetID, WeightCategories weightCat, Priorities priority, int droneID)
        {
            Parcel parcel = new Parcel()
            {
                ID = nextPackageID(),
                SenderID = senderID,
                TargetID = targetID,
                WeightCategory = weightCat,
                Priority = priority,
                DroneID = droneID,
                Scheduled = System.DateTime.Now
            };

            AddParcel(parcel);

            return parcel.ID;
        }

        private XElement GetParcelElement(int ID)
        {
            XElement parcel = Parcels.Elements().Where(p => Int32.Parse(p.Element("ID").Value) == ID).FirstOrDefault();

            if (parcel is null)
                throw new ObjectNotFound($"Parcel with ID: {ID} not found.");

            return parcel;
        }
        private Parcel ParcelParse(XElement p)
        {
            return new Parcel()
            {
                ID = Int32.Parse(p.Element("ID").Value),
                SenderID = Int32.Parse(p.Element("SenderID").Value),
                TargetID = Int32.Parse(p.Element("TargetID").Value),
                WeightCategory = Enum.Parse<WeightCategories>(p.Element("Weight").Value),
                Priority = Enum.Parse<Priorities>(p.Element("Priority").Value),
                DroneID = Int32.Parse(p.Element("DroneID").Value),
                Scheduled = DateTime.ParseExact(p.Element("Scheduled").Value, "MM/dd/yyyy HH:mm:ss", null),
                Assigned = p.Element("Assigned") is null ? null : DateTime.ParseExact(p.Element("Assigned").Value, "MM/dd/yyyy HH:mm:ss", null),
                PickedUp = p.Element("PickedUp") is null ? null : DateTime.ParseExact(p.Element("PickedUp").Value, "MM/dd/yyyy HH:mm:ss", null),
                Delivered = p.Element("Delivered") is null ? null : DateTime.ParseExact(p.Element("Delivered").Value, "MM/dd/yyyy HH:mm:ss", null),
            };
        }

        public Parcel GetParcel(int ID)
        {
            return ParcelParse(GetParcelElement(ID));
        }

        public IEnumerable<Parcel> GetParcelList()
        {
            return from p in Parcels.Elements()
                   select ParcelParse(p);
        }
        public IEnumerable<Parcel> GetFilteredParcelList(Predicate<Parcel> pred)
        {
            return (from p in Parcels.Elements()
                    select ParcelParse(p)).Where(pred.Invoke);
        }

        public void RemoveParcel(int ID)
        {
            GetParcelElement(ID).Remove();
            SaveParcels();
        }

        /// <summary>
        /// Assigns a Parcel to a Drone
        /// </summary>
        public void AssignParcel(int parcelID, int droneID)
        {
            XElement parcel = GetParcelElement(parcelID);

            GetDrone(droneID);                                           // Forces error if drone doesn't exist

            parcel.Element("DroneID").Value = droneID.ToString();

            parcel.Element("Assigned").Value = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

            SaveParcels();
        }

        /// <summary>
        /// Marks a Parcel as Collected by a Drone
        /// </summary>
        public void ParcelCollected(int parcelID)
        {
            XElement parcel = GetParcelElement(parcelID);

            parcel.Element("PickedUp").Value = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

            SaveParcels();
        }

        /// <summary>
        /// Marks a Parcel as Delivered
        /// </summary>
        public void ParcelDelivered(int parcelID)
        {
            XElement parcel = GetParcelElement(parcelID);

            parcel.Element("Delivered").Value = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

            SaveParcels();
        }
    }
}
