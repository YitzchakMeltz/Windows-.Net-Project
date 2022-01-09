using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.PO
{
    public class Station : INotifyPropertyChanged
    {
        private IBL bl;
        public Station(int ID, IBL bl)
        {
            this.ID = ID;
            this.bl = bl;
        }

        public int ID { init; get; }

        public string Name {
            get => bl.GetStation(ID).Name;
            set
            {
                bl.UpdateStation(ID, name: value);
                PropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }
        public uint AvailableChargingSlots
        {
            get => bl.GetStation(ID).AvailableChargingSlots;
            set {
                bl.UpdateStation(ID, totalChargeStation: (int?)value);
                PropertyChanged(this, new PropertyChangedEventArgs("AvailableChargingSlots"));
            }
        }
        public Location Location => bl.GetStation(ID).Location;
        public List<ChargingDrone> ChargingDrones { init; get; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
