using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.PO
{
    class Package : INotifyPropertyChanged
    {
        private IBL bl;
        public Package(int ID, IBL bl)
        {
            this.ID = ID;
            this.bl = bl;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public int ID { init; get; }
        public PackageCustomer Sender { init; get; }
        public PackageCustomer Receiver { init; get; }
        public WeightCategories Weight { init; get; }
        public Priorities Priority { init; get; }
        public DateTime? Creation { init; get; }
        public DateTime? AssignmentTime { init; get; }
        public DateTime? CollectionTime { init; get; }
        public DateTime? DeliveryTime { init; get; }
    }
}
