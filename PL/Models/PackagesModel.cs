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
    /// <summary>
    /// Represents the Model that controls the Packages List Page and its selected Package
    /// </summary>
    public class PackagesModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public IBL bl;

        /// <summary>
        /// Collection of all Packages
        /// </summary>
        private ObservableCollection<PO.Package> _collection = new ObservableCollection<PO.Package>();

        public PackagesModel(IBL bl)
        {
            this.bl = bl;
            IsAdmin = true;


            //==================================================================
            // Code before multi-threading was added
            //bl.ListPackages().ToList().ForEach(package => _collection.Add(new PO.Package(package.ID, bl)));

            // Code after multi-threading was added
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.ProgressChanged += addPackage;
            worker.DoWork += (sender, e) => { bl.ListPackages().ToList().
                            ForEach(package => worker.ReportProgress(0, package.ID)); };

            worker.RunWorkerAsync();
            //==================================================================

            CollectionView = CollectionViewSource.GetDefaultView(_collection);
            CollectionView.Filter += (o) =>
            {
                if ((Status == "All Statuses" || (o as PO.Package).Status.ToString() == Status) &&
                    ((o as PO.Package).Creation >= StartDate) && 
                    ((o as PO.Package).Creation <= EndDate)) return true;
                else return false;
            };
        }

        /// <summary>
        /// Progress report handler for the constructor's BackgroundWorker. Enables BackgroundWorker to add Packages to the Observable Collection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addPackage(object sender, ProgressChangedEventArgs e)
        {
            _collection.Add(new PO.Package((uint)e.UserState, bl));
        }

        /// <summary>
        /// Constructor for Package of specific Customer
        /// </summary>
        /// <param name="bl"></param>
        /// <param name="customer"></param>
        public PackagesModel(IBL bl, PO.Customer customer) : this(bl)
        {
            IsAdmin = false;
            SenderID = customer.ID;
            _collection.CollectionChanged += (object sender, NotifyCollectionChangedEventArgs args) => customer.PackagesChanged();
            State = WindowState.Add;
        }

        /// <summary>
        /// DataBinding Collection View with Filter (see constructor)
        /// </summary>
        public ICollectionView CollectionView { init; get; }

        #region Filter by Status
        public IEnumerable<string> Statuses { get => Enum.GetValues<BO.Statuses>().Select(s => s.ToString()).Prepend("All Statuses"); }

        private string _status = "All Statuses";

        /// <summary>
        /// DataBinding for filtering Packages by Status
        /// </summary>
        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                CollectionView.Refresh();
            }
        }
        #endregion

        #region Filter by Date

        public IEnumerable<string> DateChoices => new string[] { "All Dates", "Select Dates" };

        private string _dateChoice = "All Dates";

        /// <summary>
        /// DataBinding for filtering Packages by Date
        /// </summary>
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
                    else _dateChoice = "All Dates";
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

        #endregion

        #region Group Packages
        public enum Groups { None, Sender, Receiver }

        private Groups _groupBy = Groups.None;

        /// <summary>
        /// DataBinding for listing Packages in Groups
        /// </summary>
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

        /// <summary>
        /// Cycles through all Group categories
        /// </summary>
        public void NextGroup()
        {
            Groups[] groups = Enum.GetValues<Groups>();
            GroupBy = groups[(Array.IndexOf(groups, _groupBy) + 1) % groups.Length];
        }
        #endregion

        public IEnumerable<uint> Customers => bl.ListCustomers().Select(c => c.ID);

        /// <summary>
        /// DataBinding for selected Package
        /// </summary>
        public PO.Package SelectedPackage { get; set; }

        #region Control Visibility
        public enum WindowState { Add, Update }

        /// <summary>
        /// Represents window state for DataBinding to determine which controls should be visible
        /// </summary>
        public WindowState State { get; set; }

        public bool IsAdmin { init; get; }

        /// <summary>
        /// Binds Sender Customer ID when a customer creates a package (it must be sent from himself)
        /// </summary>
        public uint SenderID { init; get; }

        /// <summary>
        /// Converts Window State to Visibility for DataBinding
        /// </summary>
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
        #endregion

        /// <summary>
        /// Creates new Package and modifies UI (ObservableCollection) accordingly.
        /// </summary>
        /// <param name="senderID"></param>
        /// <param name="receiverID"></param>
        /// <param name="weight"></param>
        /// <param name="priority"></param>
        public void Add(uint senderID, uint receiverID, WeightCategories weight, Priorities priority)
        {
            _collection.Add(new PO.Package(bl.AddPackage(senderID, receiverID, weight, priority), bl));
        }

        /// <summary>
        /// Removes Package and modifies UI (ObservableCollection) accordingly.
        /// </summary>
        public void Remove()
        {
            bl.DeletePackage(SelectedPackage.ID);
            _collection.Remove(SelectedPackage);
        }
    }
}
