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
    public class StationsModel : INotifyPropertyChanged
    {
        private IBL bl;
        public event PropertyChangedEventHandler PropertyChanged;
        public StationsModel(IBL bl)
        {
            this.bl = bl;

            //==================================================================
            // Code before multi-threading was added
            //bl.ListStations().ToList().ForEach(station => _collection.Add(new PO.Station(station.ID, bl)));

            // Code after multi-threading was added
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += ((sender, e) => { bl.ListStations().ToList().
                ForEach(station => _collection.
                Add(new PO.Station(station.ID, bl))); });

            worker.RunWorkerAsync();
            //==================================================================

            CollectionView = CollectionViewSource.GetDefaultView(_collection);
        }

        private ObservableCollection<PO.Station> _collection = new ObservableCollection<PO.Station>();
        public ICollectionView CollectionView { init; get; }
        public enum Groups { None, AvailableChargingSlots, IsAvailable }
        private Groups _groupBy = Groups.None;

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
        public void NextGroup()
        {
            Groups[] groups = Enum.GetValues<Groups>();
            GroupBy = groups[(Array.IndexOf(groups, _groupBy) + 1) % groups.Length];
        }

        public PO.Station SelectedStation { get; set; }

        public enum WindowState { Add, Update }
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

        public void Add(int ID, string name, double latitude, double longitude, int chargeSlots)
        {
            bl.AddStation(ID, name, latitude, longitude, chargeSlots);
            _collection.Add(new PO.Station(ID, bl));
        }
    }
}
