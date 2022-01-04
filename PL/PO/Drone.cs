using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PL.PO
{
    public class Drone : INotifyPropertyChanged
    {
        private IBL bl;
        public Drone(int ID, IBL bl) {
            this.ID = ID;
            this.bl = bl;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public int ID { init; get; }
        public string Model {
            get { return bl.GetDrone(ID).Model; }
            set {
                bl.UpdateDrone(ID, value);
                PropertyChanged(this, new PropertyChangedEventArgs("Model"));
            }
        }
        public WeightCategories Weight { init; get; }
        public double Battery {
            get { return bl.GetDrone(ID).Battery; }
        }
        public DroneStatuses Status {
            get { return bl.GetDrone(ID).Status; }
        }
        public Location Location => bl.GetDrone(ID).Location;
        public EnroutePackage Package
        {
            get { return bl.GetDrone(ID).Package; }
        }

        public void Charge()
        {
            bl.ChargeDrone(ID);
            PropertyChanged(this, new PropertyChangedEventArgs("Status"));
            PropertyChanged(this, new PropertyChangedEventArgs("Battery"));
            PropertyChanged(this, new PropertyChangedEventArgs("Location"));
        }

        public void Release()
        {
            bl.ReleaseDrone(ID);
            PropertyChanged(this, new PropertyChangedEventArgs("Status"));
            PropertyChanged(this, new PropertyChangedEventArgs("Battery"));
        }
        public void Assign()
        {
            bl.AssignPackageToDrone(ID);
            PropertyChanged(this, new PropertyChangedEventArgs("Status"));
            PropertyChanged(this, new PropertyChangedEventArgs("PackageID"));
        }
        public void Collect()
        {
            bl.CollectPackage(ID);
            PropertyChanged(this, new PropertyChangedEventArgs("Battery"));
            PropertyChanged(this, new PropertyChangedEventArgs("Location"));
        }
        public void Deliver()
        {
            bl.DeliverPackage(ID);
            PropertyChanged(this, new PropertyChangedEventArgs("Status"));
            PropertyChanged(this, new PropertyChangedEventArgs("Battery"));
            PropertyChanged(this, new PropertyChangedEventArgs("Location"));
            PropertyChanged(this, new PropertyChangedEventArgs("Package"));
        }

    }
}