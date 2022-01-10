using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PL.Models
{
    public class PackagesModel : INotifyPropertyChanged
    {
        private IBL bl;
        public event PropertyChangedEventHandler PropertyChanged;
        public PackagesModel(IBL bl)
        {
            this.bl = bl;
            IsAdmin = true;
            foreach (PackageList p in bl.ListPackages()) _collection.Add(new PO.Package(p.ID, bl));

            CollectionView = CollectionViewSource.GetDefaultView(_collection);
            CollectionView.Filter += (o) =>
            {
                if ((Status == "All Statuses" || (o as PO.Package).Status.ToString() == Status) &&
                    (StartDate == DateTime.MinValue || (o as PO.Package).Creation >= StartDate) && 
                    (EndDate == DateTime.MinValue || (o as PO.Package).Creation <= EndDate)) return true;
                else return false;
            };
        }

        public PackagesModel(IBL bl, PO.Customer customer) : this(bl)
        {
            IsAdmin = false;
            SenderID = customer.ID;
            _collection.CollectionChanged += (object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs args) => customer.PackagesChanged();
            State = WindowState.Add;
        }

        private ObservableCollection<PO.Package> _collection = new ObservableCollection<PO.Package>();
        public ICollectionView CollectionView { init; get; }
        public IEnumerable<string> Statuses { get => Enum.GetValues<BO.Statuses>().Select(s => s.ToString()).Prepend("All Statuses"); }
        private string _status = "All Statuses";
        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                CollectionView.Refresh();
            }
        }

        public IEnumerable<string> DateChoices => new string[] { "All Dates", "Select Dates" };
        private string _dateChoice = "All Dates";
        public string DateChoice {
            get => _dateChoice;
            set 
            {
                if (value == "All Dates")
                {
                    _dateChoice = value;
                    StartDate = DateTime.MinValue;
                    EndDate = DateTime.MaxValue;
                }
                else
                {
                    if (DateRangeWindow.Show(this))
                        _dateChoice = value;
                    else DateChoice = "All Dates";
                }
                PropertyChanged(this, new PropertyChangedEventArgs("DateChoice"));
            }
        }
        private DateTime _startDate = DateTime.MinValue;
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                CollectionView.Refresh();
            }
        }
        private DateTime _endDate = DateTime.MaxValue;
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                CollectionView.Refresh();
            }
        }

        public enum Groups { None, Sender, Receiver }
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
                    CollectionView.GroupDescriptions.Add(new PropertyGroupDescription($"{value.ToString()}.ID"));
            }
        }
        public void NextGroup()
        {
            Groups[] groups = Enum.GetValues<Groups>();
            GroupBy = groups[(Array.IndexOf(groups, _groupBy) + 1) % groups.Length];
        }
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
            _collection.Remove(SelectedPackage);
        }
    }
}
