using System;
using BO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using BlApi;
using System.Windows;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace PL.Models
{
    /// <summary>
    /// Represents the Model that controls the Drones List Page and its selected Drones
    /// </summary>
    public class DronesModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private IBL bl;

        /// <summary>
        /// Collection of all Packages
        /// </summary>
        private ObservableCollection<PO.Drone> _collection = new ObservableCollection<PO.Drone>();

        public DronesModel(IBL bl)
        {
            this.bl = bl;

            //======================================================================
            // Old Code
            //bl.ListDrones().ToList().ForEach(drone => _collection.Add(new PO.Drone(drone.ID, bl)));

            //Try adding Multi-threading using Background Worker
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.ProgressChanged += addDrone;
            worker.DoWork += (sender, e) => { bl.ListDrones().ToList().
                          ForEach(drone => worker.ReportProgress(0, drone.ID)); };

            worker.RunWorkerAsync();
            //======================================================================

            CollectionView = CollectionViewSource.GetDefaultView(_collection);
            CollectionView.Filter += (o) =>
            {
                if ((Status == "All Statuses" || (o as PO.Drone).Status.ToString() == Status) &&
                    (Weight == "All Weights" || (o as PO.Drone).Weight.ToString() == Weight)) return true;
                else return false;
            };
        }

        /// <summary>
        /// Progress report handler for the constructor's BackgroundWorker. Enables BackgroundWorker to add Packages to the Observable Collection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addDrone(object sender, ProgressChangedEventArgs e)
        {
            _collection.Add(new PO.Drone((uint)e.UserState, bl));
        }

        /// <summary>
        /// Constructor for Drone of specific Package
        /// </summary>
        /// <param name="bl"></param>
        /// <param name="package"></param>
        public DronesModel(IBL bl, PO.Package package) : this(bl)
        {
            State = WindowState.Update;
            SelectedDrone = new PO.Drone(package.Drone.ID, bl);
            SelectedDrone.PropertyChanged += (object sender, PropertyChangedEventArgs args) => { if (args.PropertyName == "Package") package.DroneChanged(); };
        }

        /// <summary>
        /// DataBinding Collection View with Filter (see constructor)
        /// </summary>
        public ICollectionView CollectionView { init; get; }

        #region Filter by Status

        public IEnumerable<string> Statuses { get => Enum.GetValues<BO.DroneStatuses>().Select(s => s.ToString()).Prepend("All Statuses"); }

        private string _status = "All Statuses";

        /// <summary>
        /// DataBinding for filtering Drones by Status
        /// </summary>
        public string Status {
            get => _status;
            set
            {
                _status = value;
                CollectionView.Refresh();
            }
        }

        #endregion

        #region Filter by Weight

        public IEnumerable<string> Weights { get => Enum.GetValues<BO.WeightCategories>().Select(w => w.ToString()).Prepend("All Weights"); }

        private string _weight = "All Weights";

        /// <summary>
        /// DataBinding for filtering Drones by Weight
        /// </summary>
        public string Weight
        {
            get => _weight;
            set
            {
                _weight = value;
                CollectionView.Refresh();
            }
        }

        #endregion

        #region Group Drones

        public enum Groups { None, Status }

        private Groups _groupBy = Groups.None;

        /// <summary>
        /// DataBinding for listing Drones in Groups
        /// </summary>
        public Groups GroupBy
        {
            get => _groupBy;
            set
            {
                _groupBy = value;
                PropertyChanged(this, new PropertyChangedEventArgs("GroupBy"));
                CollectionView.GroupDescriptions.Clear();
                if (value != Groups.None)
                    CollectionView.GroupDescriptions.Add(new PropertyGroupDescription(value.ToString()));
            }
        }

        /// <summary>
        /// Cycles through all Group categories
        /// </summary>
        public void NextGroup()
        {
            Groups[] groups = Enum.GetValues<Groups>();
            GroupBy = groups[(Array.IndexOf(groups, _groupBy) + 1) % groups.Length];
        }

        #endregion

        public IEnumerable<uint> Stations { get { return bl.ListStations().Select(s => s.ID); } }

        /// <summary>
        /// DataBinding for selected Drone
        /// </summary>
        public PO.Drone SelectedDrone { get; set; }

        #region Control Visibility

        public enum WindowState { Add, Update }
        public WindowState State { get; set; }

        public Visibility UpdateVisibility { 
            get
            {
                if (State == WindowState.Update) return Visibility.Visible;
                else return Visibility.Collapsed;
            } }
        public Visibility AddVisibility
        {
            get
            {
                if (State == WindowState.Add) return Visibility.Visible;
                else return Visibility.Collapsed;
            }
        }
        #endregion

        /// <summary>
        /// Creates new Drone and modifies UI (ObservableCollection) accordingly.
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="model"></param>
        /// <param name="weight"></param>
        /// <param name="stationID"></param>
        public void Add(uint ID, string model, WeightCategories weight, uint stationID)
        {
            bl.AddDrone(ID, model, weight, stationID);
            _collection.Add(new PO.Drone(ID, bl));
        }

        /// <summary>
        /// Prevents user from exiting Drone List if any Simulators aren't pending cancellation.
        /// </summary>
        public uint SimulatorCount { get; set; }
    }
}
