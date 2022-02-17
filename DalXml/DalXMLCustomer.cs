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
        /*private void LoadCustomers()
        {
            try
            {
                Customers = XElement.Load(DalFolder + "Customers.xml");
            }
            catch (Exception e)
            {
                throw new InvalidInput("Couldn't read xml file.", e);
            }
        }*/
        private void SaveCustomers()
        {
            try
            {
                Customers.Save(DalFolder + "Customers.xml");
            }
            catch (Exception e)
            {
                throw new InvalidInput("Couldn't access Customers.xml", e);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddCustomer(Customer customer)
        {

            if (!customer.Phone.All(char.IsDigit))
                throw new ObjectNotFound($"Phone number must only contain digits.");

            if (Customers.Elements().Any(p => Int32.Parse(p.Element("ID").Value) == customer.ID))
                throw new ObjectAlreadyExists($"Customer with ID {customer.ID} already exists.");

            Customers.Add(new XElement("Customer",
                            new XElement("ID", customer.ID),
                            new XElement("Name", customer.Name),
                            new XElement("Phone", customer.Phone),
                            new XElement("Location", 
                                new XElement("Latitude", customer.Location.Latitude),
                                new XElement("Longitude", customer.Location.Longitude)),
                            customer.PasswordHash is null ? null : new XElement("PasswordHash", System.Convert.ToBase64String(customer.PasswordHash))
            ));

            SaveCustomers();
        }

        /// <summary>
        /// Adds a Customer
        /// </summary>
        /// <param name="customer"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddCustomer(uint id, string name, string phoneNum, double latitude, double longitude, string password = "")
        {
            if (latitude < -90 || latitude > 90 || longitude < -180 || longitude > 180)
                throw new InvalidInput("Location is invalid.");

            AddCustomer(new Customer()
            {
                ID = id,
                Name = name,
                Phone = phoneNum,
                Location = new DalApi.Util.Coordinate(latitude, longitude),
                Password = password
            });
        }

        private XElement GetCustomerElement(uint ID)
        {
            XElement customer = Customers.Elements().Where(p => Int32.Parse(p.Element("ID").Value) == ID).FirstOrDefault();

            if (customer is null)
                throw new ObjectNotFound($"Customer with ID: {ID} not found.");

            return customer;
        }

        private Customer CustomerParse(XElement c)
        {
            return new Customer()
            {
                ID = UInt32.Parse(c.Element("ID").Value),
                Name = c.Element("Name").Value,
                Phone = c.Element("Phone").Value,
                Location = new DalApi.Util.Coordinate(Double.Parse(c.Element("Location").Element("Latitude").Value), Double.Parse(c.Element("Location").Element("Longitude").Value)),
                PasswordHash = c.Element("PasswordHash") is null ? null : System.Convert.FromBase64String(c.Element("PasswordHash").Value)
            };
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer GetCustomer(uint ID)
        {
            return CustomerParse(GetCustomerElement(ID));
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Customer> GetCustomerList()
        {
            return from c in Customers.Elements()
                   select CustomerParse(c);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void RemoveCustomer(uint ID)
        {
            GetCustomerElement(ID).Remove();
            SaveCustomers();
        }
    }
}
