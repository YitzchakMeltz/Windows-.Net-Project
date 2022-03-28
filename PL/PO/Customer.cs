using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.PO
{
    public class Customer : INotifyPropertyChanged
    {
        private IBL bl;
        public Customer(uint ID, IBL bl)
        {
            this.ID = ID;
            this.bl = bl;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public uint ID { init; get; }
        public string Name { 
            get => bl.GetCustomer(ID).Name;
            set
            {
                bl.UpdateCustomer(ID, name: value);
                PropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }
        public string Phone { 
            get => bl.GetCustomer(ID).Phone;
            set
            {
                bl.UpdateCustomer(ID, phone: value);
                PropertyChanged(this, new PropertyChangedEventArgs("Phone"));
            }
        }
        public Location Location { get => bl.GetCustomer(ID).Location; }
        public List<CustomerPackage> Outgoing { get => bl.GetCustomer(ID).Outgoing; }
        public List<CustomerPackage> Incoming { get => bl.GetCustomer(ID).Incoming; }

        public void PackagesChanged()
        {
            PropertyChanged(this, new PropertyChangedEventArgs("Incoming"));
            PropertyChanged(this, new PropertyChangedEventArgs("Outgoing"));
        }

        public void UpdatePassword(string[] passwordArray)
        {
            bl.UpdateCustomer(ID, null, null, passwordArray);
        }
    }
}
