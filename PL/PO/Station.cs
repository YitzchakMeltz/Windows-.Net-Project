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
        public enum Availability { Unavailable, Available }
        public Station(uint ID, IBL bl)
        {
            this.ID = ID;
            this.bl = bl;
        }

        public uint ID { init; get; }

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
                bl.UpdateStation(ID, totalChargeStation: value);
                PropertyChanged(this, new PropertyChangedEventArgs("AvailableChargingSlots"));
            }
        }

        public Availability IsAvailable 
        {
            get 
            { 
                if(bl.GetStation(ID).AvailableChargingSlots > 0)
                    return Availability.Available;

                return Availability.Unavailable;
            }
        }


        public Location Location => bl.GetStation(ID).Location;
        public List<ChargingDrone> ChargingDrones => bl.GetStation(ID).ChargingDrones;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
