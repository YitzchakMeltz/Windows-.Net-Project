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
        public Drone(uint ID, IBL bl) {
            this.ID = ID;
            this.bl = bl;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public uint ID { init; get; }
        public string Model {
            get => bl.GetDrone(ID).Model;
            set {
                bl.UpdateDrone(ID, value);
                PropertyChanged(this, new PropertyChangedEventArgs("Model"));
            }
        }
        public WeightCategories Weight => bl.GetDrone(ID).Weight;
        public double Battery => bl.GetDrone(ID).Battery;
        public DroneStatuses Status => bl.GetDrone(ID).Status;
        public Location Location => bl.GetDrone(ID).Location;
        public EnroutePackage Package => bl.GetDrone(ID).Package;

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
            PropertyChanged(this, new PropertyChangedEventArgs("Package"));
        }
        public void Collect()
        {
            bl.CollectPackage(ID);
            PropertyChanged(this, new PropertyChangedEventArgs("Battery"));
            PropertyChanged(this, new PropertyChangedEventArgs("Location"));
            PropertyChanged(this, new PropertyChangedEventArgs("Package"));
        }
        public void Deliver()
        {
            bl.DeliverPackage(ID);
            PropertyChanged(this, new PropertyChangedEventArgs("Status"));
            PropertyChanged(this, new PropertyChangedEventArgs("Battery"));
            PropertyChanged(this, new PropertyChangedEventArgs("Location"));
            PropertyChanged(this, new PropertyChangedEventArgs("Package"));
        }

        public BackgroundWorker Worker { get; set; }
        public void Simulate()
        {
            Worker = new();

            Worker.DoWork += (sender, e) =>
            {
                bl.ActivateSimulator(ID, () => Worker.ReportProgress(0), () => Worker.CancellationPending);
            };

            Worker.ProgressChanged += Reload;

            //dataBindSpinner runs first because onComplete displays MsgBox and waits for user input
            Worker.RunWorkerCompleted += onComplete;

            Worker.WorkerSupportsCancellation = true;
            Worker.WorkerReportsProgress = true;

            Worker.RunWorkerAsync();
            PropertyChanged(this, new PropertyChangedEventArgs("Worker"));
        }

        public void StopSimulator()
        {
            Worker.CancelAsync();
            PropertyChanged(this, new PropertyChangedEventArgs("Worker"));
            MsgBox.Show("Success", "Simulator Cancelled Successfully");
        }

        private void onComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            PropertyChanged(this, new PropertyChangedEventArgs("Worker"));
            if(!e.Cancelled) MsgBox.Show("Success", "Simulation Completed Successfully");
        }

        public void Reload(object sender, ProgressChangedEventArgs a)
        {
            PropertyChanged(this, new PropertyChangedEventArgs("Status"));
            PropertyChanged(this, new PropertyChangedEventArgs("Battery"));
            PropertyChanged(this, new PropertyChangedEventArgs("Location"));
            PropertyChanged(this, new PropertyChangedEventArgs("Package"));
        }
    }
}