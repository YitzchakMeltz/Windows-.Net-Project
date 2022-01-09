using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Models
{
    public class StationsModel
    {
        private IBL bl;
        public StationsModel(IBL bl)
        {
            this.bl = bl;
            foreach (BaseStationList s in bl.ListStations()) _collection.Add(new PO.Station(s.ID, bl));
        }

        private ObservableCollection<PO.Station> _collection = new ObservableCollection<PO.Station>();
        public ObservableCollection<PO.Station> Collection => _collection;

        public PO.Station SelectedStation { get; set; }

        public enum WindowState { Add, Update }
        public WindowState State { get; set; }

        public void Add(int ID, string name, double latitude, double longitude, int chargeSlots)
        {
            bl.AddStation(ID, name, latitude, longitude, chargeSlots);
            _collection.Add(new PO.Station(ID, bl));
        }
    }
}
