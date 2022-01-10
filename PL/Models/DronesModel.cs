using System;
using BO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using System.Windows;
using System.Windows.Data;
using System.ComponentModel;

namespace PL.Models
{
    public class DronesModel
    {
        private IBL bl;
        public DronesModel(IBL bl)
        {
            this.bl = bl;

            //ObservableCollection<PO.Drone> collection = new ObservableCollection<PO.Drone>();
            foreach (DroneList drone in bl.ListDrones()) _collection.Add(new PO.Drone(drone.ID, bl));

            collectionView = CollectionViewSource.GetDefaultView(_collection);
            collectionView.Filter = (d) =>
            {
                if ((Status == "All Statuses" || (d as PO.Drone).Status.ToString() == Status) &&
                    (Weight == "All Weights" || (d as PO.Drone).Weight.ToString() == Weight)) return true;
                else return false;
            };
        }


        public ObservableCollection<PO.Drone> _collection = new ObservableCollection<PO.Drone>();
        private ICollectionView collectionView;
        public IEnumerable<string> Statuses { get => Enum.GetValues<BO.DroneStatuses>().Select(s => s.ToString()).Prepend("All Statuses"); }
        private string _status = "All Statuses";
        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                collectionView.Refresh();
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
                collectionView.Refresh();
            }
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

        public ObservableCollection<PO.Drone> Collection => _collection;
        public PO.Drone SelectedDrone { get; set; }
        public IEnumerable<int> Stations { get { return bl.ListStations().Select(s => s.ID); } }

        public void Add(int ID, string model, WeightCategories weight, int stationID)
        {
            bl.AddDrone(ID, model, weight, stationID);
            _collection.Add(new PO.Drone(ID, bl));
            collectionView.Refresh();
        }

    }
}
