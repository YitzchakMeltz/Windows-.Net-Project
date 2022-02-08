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

        private volatile bool stopSimulation;
        private BackgroundWorker worker;
        public void Simulate(RunWorkerCompletedEventHandler onComplete)
        {
            stopSimulation = false;

            worker = new();

            worker.DoWork += ((sender, e) =>
            {
                //worker.ReportProgress(0);
                // Call BL Simulate function here
                bl.ActivateSimulator(ID, () => worker.ReportProgress(0), () => stopSimulation);
                //Thread.Sleep(30000);
                //worker.ReportProgress(70);
                //(DataContext as PO.Drone).Simulate(() => SimulatorToggleButton.IsEnabled);
            });

            worker.ProgressChanged += Reload;

            worker.RunWorkerCompleted += onComplete;

            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;

            worker.RunWorkerAsync();
        }
        public void StopSimulator()
        {
            stopSimulation = true;
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