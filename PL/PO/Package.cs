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
    public class Package : INotifyPropertyChanged
    {
        private IBL bl;
        public Package(int ID, IBL bl)
        {
            this.ID = ID;
            this.bl = bl;
        }

        public int ID { init; get; }
        public PackageCustomer Sender => bl.GetPackage(ID).Sender;
        public PackageCustomer Receiver => bl.GetPackage(ID).Receiver;
        public WeightCategories Weight => bl.GetPackage(ID).Weight;
        public Priorities Priority => bl.GetPackage(ID).Priority; 
        public DateTime Creation => bl.GetPackage(ID).Creation;
        public DeliveryDrone Drone => bl.GetPackage(ID).Drone;
        public Statuses Status => bl.GetPackage(ID).Status;
        public DateTime? AssignmentTime => bl.GetPackage(ID).AssignmentTime;
        public DateTime? CollectionTime => bl.GetPackage(ID).CollectionTime;
        public DateTime? DeliveryTime => bl.GetPackage(ID).DeliveryTime;

        public void DroneChanged()
        {
            PropertyChanged(this, new PropertyChangedEventArgs("Status"));
            PropertyChanged(this, new PropertyChangedEventArgs("Drone"));
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
