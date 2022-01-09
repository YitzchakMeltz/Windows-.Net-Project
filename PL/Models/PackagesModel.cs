using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Models
{
    public class PackagesModel
    {
        private IBL bl;

        public PackagesModel(IBL bl)
        {
            this.bl = bl;
            IsAdmin = true;
            foreach (PackageList p in bl.ListPackages()) _collection.Add(new PO.Package(p.ID, bl));
        }

        public PackagesModel(IBL bl, int ID) : this(bl)
        {
            IsAdmin = false;
            SenderID = ID;
            State = WindowState.Add;
        }

        private ObservableCollection<PO.Package> _collection = new ObservableCollection<PO.Package>();

        
        public ObservableCollection<PO.Package> Collection => _collection;
        public IEnumerable<int> Customers => bl.ListCustomers().Select(c => c.ID);

        public PO.Package SelectedPackage { get; set; }
        public enum WindowState { Add, Update }
        public WindowState State { get; set; }

        public Boolean IsAdmin { init; get; }
        public int SenderID { init; get; }
        public System.Windows.Visibility AddVisibility {
            get
            {
                if (State == WindowState.Add) return System.Windows.Visibility.Visible;
                else return System.Windows.Visibility.Collapsed;
            }
        }
        public System.Windows.Visibility UpdateVisibility { 
            get
            {
                if (State == WindowState.Update) return System.Windows.Visibility.Visible;
                else return System.Windows.Visibility.Collapsed;
            }
        }

        public void Add(int senderID, int receiverID, WeightCategories weight, Priorities priority)
        {
            _collection.Add(new PO.Package(bl.AddPackage(senderID, receiverID, weight, priority), bl));
        }

        public void Remove()
        {
            bl.DeletePackage(SelectedPackage.ID);
            _collection.Remove(new PO.Package(SelectedPackage.ID, bl));
        }
    }
}
