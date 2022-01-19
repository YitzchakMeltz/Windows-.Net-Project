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
        /*private void LoadDrones()
        {
            try
            {
                Drones = XElement.Load(DalFolder + "Drones.xml");
            }
            catch (Exception e)
            {
                throw new InvalidInput("Couldn't read Drones.xml.", e);
            }
        }*/
        private void SaveDrones()
        {
            try
            {
                Drones.Save(DalFolder + "Drones.xml");
            }
            catch (Exception e)
            {
                throw new InvalidInput("Error accessing Drones.xml", e);
            }
        }

        /// <summary>
        /// Adds a Drone
        /// </summary>
        /// <param name="drone"></param>
        public void AddDrone(Drone drone)
        {
            if (Drones.Elements().Any(d => Int32.Parse(d.Element("ID").Value) == drone.ID))
                throw new ObjectAlreadyExists($"Drone with ID {drone.ID} already exists.");

            Drones.Add(new XElement("Drone",
                    new XElement("ID", drone.ID),
                    new XElement("Model", drone.Model),
                    new XElement("Weight", drone.WeightCategory)
            ));

            SaveDrones();
        }

        /// <summary>
        /// Adds a Drone
        /// </summary>
        /// <param name="drone"></param>
        public void AddDrone(int id, string model, WeightCategories weightCat)
        {
            AddDrone(new Drone()
            {
                ID = id,
                Model = model,
                WeightCategory = weightCat
            });
        }
        private XElement GetDroneElement(int ID)
        {
            XElement drone = Drones.Elements().Where(d => Int32.Parse(d.Element("ID").Value) == ID).FirstOrDefault();

            if (drone is null)
                throw new ObjectNotFound($"Drone with ID: {ID} not found.");

            return drone;
        }
        private Drone DroneParse(XElement d)
        {
            return new Drone()
            {
                ID = Int32.Parse(d.Element("ID").Value),
                Model = d.Element("Model").Value,
                WeightCategory = Enum.Parse<WeightCategories>(d.Element("Weight").Value),
            };
        }

        /// <summary>
        /// Returns a Drone by its ID
        /// </summary>
        /// <param name="ID"></param>
        public Drone GetDrone(int ID)
        {
            return DroneParse(GetDroneElement(ID));
        }

        /// <summary>
        /// Returns an array of all Drones
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Drone> GetDroneList()
        {
            return from d in Drones.Elements()
                   select DroneParse(d);
        }

        /// <summary>
        /// Deletes a Drone
        /// </summary>
        /// <param name="ID"></param>
        public void RemoveDrone(int ID)
        {
            GetDroneElement(ID).Remove();
            SaveDrones();
        }
    }
}
