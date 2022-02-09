using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Data;

namespace PL.Models
{
    /// <summary>
    /// Represents the Model that controls the Stations List Page and its selected Station
    /// </summary>
    public class StationsModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private IBL bl;

        /// <summary>
        /// Collection of all Stations.
        /// </summary>
        private ObservableCollection<PO.Station> _collection = new ObservableCollection<PO.Station>();

        public StationsModel(IBL bl)
        {
            this.bl = bl;

            //==================================================================
            // Code before multi-threading was added
            //bl.ListStations().ToList().ForEach(station => _collection.Add(new PO.Station(station.ID, bl)));

            // Code after multi-threading was added
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.ProgressChanged += addStation;
            worker.DoWork += (sender, e) => { bl.ListStations().ToList().
                ForEach(station => worker.ReportProgress(0, station.ID)); };

            worker.RunWorkerAsync();
            //==================================================================

            CollectionView = CollectionViewSource.GetDefaultView(_collection);
        }

        /// <summary>
        /// Progress report handler for the constructor's BackgroundWorker. Enables BackgroundWorker to add Stations to the Observable Collection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addStation(object sender, ProgressChangedEventArgs e)
        {
            _collection.Add(new PO.Station((uint)e.UserState, bl));
        }

        /// <summary>
        /// DataBinding Collection View with Filter (see constructor)
        /// </summary>
        public ICollectionView CollectionView { init; get; }

        #region Group Stations
        public enum Groups { None, AvailableChargingSlots, IsAvailable }

        private Groups _groupBy = Groups.None;

        /// <summary>
        /// DataBinding for listing Stations in Groups.
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
        /// Cycles through all Group categories.
        /// </summary>
        public void NextGroup()
        {
            Groups[] groups = Enum.GetValues<Groups>();
            GroupBy = groups[(Array.IndexOf(groups, _groupBy) + 1) % groups.Length];
        }
        #endregion

        /// <summary>
        /// DataBinding for selected Package
        /// </summary>
        public PO.Station SelectedStation { get; set; }

        #region Control Visibility
        public enum WindowState { Add, Update }

        /// <summary>
        /// Represents window state for DataBinding to determine which controls should be visible
        /// </summary>
        public WindowState State { get; set; }

        public Visibility AdminVisibility { init; get; }

        public Visibility UserVisibility
        {
            get
            {
                if (AdminVisibility == Visibility.Visible) return Visibility.Collapsed;
                else return Visibility.Visible;
            }
        }

        /// <summary>
        /// Converts Window State to Visibility for DataBinding
        /// </summary>
        public Visibility UpdateVisibility
        {
            get
            {
                if (State == WindowState.Update) return Visibility.Visible;
                else return Visibility.Collapsed;
            }
        }
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
        /// Creates new Station and modifies UI (ObservableCollection) accordingly.
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="name"></param>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="chargeSlots"></param>
        public void Add(uint ID, string name, double latitude, double longitude, uint chargeSlots)
        {
            bl.AddStation(ID, name, latitude, longitude, chargeSlots);
            _collection.Add(new PO.Station(ID, bl));
        }
    }
}
