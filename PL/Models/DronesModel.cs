using System;
using BO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using System.Windows;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace PL.Models
{
    public class DronesModel : INotifyPropertyChanged
    {
        private IBL bl;
        public event PropertyChangedEventHandler PropertyChanged;
        public DronesModel(IBL bl)
        {
            this.bl = bl;

            foreach (DroneList drone in bl.ListDrones()) _collection.Add(new PO.Drone(drone.ID, bl));

            CollectionView = CollectionViewSource.GetDefaultView(_collection);
            CollectionView.Filter += (o) =>
            {
                if ((Status == "All Statuses" || (o as PO.Drone).Status.ToString() == Status) &&
                    (Weight == "All Weights" || (o as PO.Drone).Weight.ToString() == Weight)) return true;
                else return false;
            };
        }


        private ObservableCollection<PO.Drone> _collection = new ObservableCollection<PO.Drone>();
        public ICollectionView CollectionView { init; get; }
        public IEnumerable<string> Statuses { get => Enum.GetValues<BO.DroneStatuses>().Select(s => s.ToString()).Prepend("All Statuses"); }
        private string _status = "All Statuses";
        public string Status {
            get => _status;
            set
            {
                _status = value;
                CollectionView.Refresh();
            }
        }
        public IEnumerable<string> Weights { get => Enum.GetValues<BO.WeightCategories>().Select(w => w.ToString()).Prepend("All Weights"); }
        private string _weight = "All Weights";
        public string Weight
        {
            get => _weight;
            set
            {
                _weight = value;
                CollectionView.Refresh();
            }
        }
        public enum Groups { None, Status }
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

        public PO.Drone SelectedDrone { get; set; }
        public IEnumerable<int> Stations { get { return bl.ListStations().Select(s => s.ID); } }

        public void Add(int ID, string model, WeightCategories weight, int stationID)
        {
            bl.AddDrone(ID, model, weight, stationID);
            _collection.Add(new PO.Drone(ID, bl));
        }

    }
}
