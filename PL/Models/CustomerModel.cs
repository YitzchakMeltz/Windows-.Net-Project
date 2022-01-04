using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Models
{
    class CustomerModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //private int id;
        public CustomerModel(int id)
        {
            this.ID = ID;
        }

        public int ID { 
            get => ID;
            set => ID = value;
        }

        public int Name
        {
            get => Name;
            set => Name = value;
        }
    }
}
