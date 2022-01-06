using System;
using BO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using System.Windows;

namespace PL.Models
{
    public class DroneListModel
    {
        private IBL bl;
        public DroneListModel(IEnumerable<DroneList> e, IBL bl, WindowState state)
        {
            this.bl = bl;
            State = state;
            foreach (DroneList drone in e) _collection.Add(new PO.Drone(drone.ID, bl));
        }

        private ObservableCollection<PO.Drone> _collection = new ObservableCollection<PO.Drone>();
        public IEnumerable<string> Statuses { get => Enum.GetValues<BO.DroneStatuses>().Select(s => s.ToString()).Prepend("All Statuses"); }
        public IEnumerable<string> Weights { get => Enum.GetValues<BO.WeightCategories>().Select(w => w.ToString()).Prepend("All Weights"); }

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

        public ObservableCollection<PO.Drone> Collection { get { return _collection; } }
        public PO.Drone SelectedDrone { get; set; }
        public IEnumerable<int> Stations { get { return bl.ListStations().Select(s => s.ID); } }

        public void Add(int ID, string model, WeightCategories weight, int stationID)
        {
            bl.AddDrone(ID, model, weight, stationID);
            _collection.Add(new PO.Drone(ID, bl));
        }

    }
}
