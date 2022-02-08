using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Text;
using System.Threading.Tasks;

namespace PL.Models
{
    /// <summary>
    /// Represents the Model that controls the Customers List Page and its selected Customer
    /// </summary>
    public class CustomersModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public IBL bl;

        /// <summary>
        /// Collection of all Customers
        /// </summary>
        private ObservableCollection<PO.Customer> _collection = new ObservableCollection<PO.Customer>();

        public CustomersModel(IBL bl, uint? customerID = null)
        {
            this.bl = bl;
            PasswordVisible = System.Windows.Visibility.Collapsed;
            if (customerID == null) AdminVisibility = Visibility.Visible;
            else
            {
                State = WindowState.Update;
                SelectedCustomer = new PO.Customer(customerID.Value, bl);
                AdminVisibility = Visibility.Collapsed;
            }

            //==================================================================
            // Code before multi-threading was added
            //bl.ListCustomers().ToList().ForEach(customer => _collection.Add(new PO.Customer(customer.ID, bl)));

            // Code after multi-threading was added
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.ProgressChanged += addCustomer;
            worker.DoWork += (sender, e) => { bl.ListCustomers().ToList().
                            ForEach(customer => worker.ReportProgress(0, customer.ID)); };

            worker.RunWorkerAsync();
            //==================================================================
        }

        /// <summary>
        /// Progress report handler for the constructor's BackgroundWorker. Enables BackgroundWorker to add Customers to the Observable Collection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addCustomer(object sender, ProgressChangedEventArgs e)
        {
            _collection.Add(new PO.Customer((uint)e.UserState, bl));
        }

        /// <summary>
        /// Constructor for Customer of specific package
        /// </summary>
        /// <param name="bl"></param>
        /// <param name="package"></param>
        public CustomersModel(IBL bl, PackageCustomer customer) : this(bl)
        {
            SelectedCustomer = new PO.Customer(customer.ID, bl);
            State = WindowState.Update;
        }

        /// <summary>
        /// Public access to Observable Collection. DataBinding.
        /// </summary>
        public ObservableCollection<PO.Customer> Collection => _collection;

        /// <summary>
        /// DataBinding for selected Customer
        /// </summary>
        public PO.Customer SelectedCustomer { get; set; }

        #region Control Visibility
        public enum WindowState { Add, Update }

        /// <summary>
        /// Represents window state for DataBinding to determine which controls should be visible
        /// </summary>
        public WindowState State { get; set; }

        /// <summary>
        /// Represents admin visibility to determine extra controls to be visible
        /// </summary>
        public Visibility AdminVisibility { init; get; }
        public Visibility UserVisibility { 
            get
            {
                if (AdminVisibility == Visibility.Visible) return Visibility.Collapsed;
                else return Visibility.Visible;
            } }

        /// <summary>
        /// Converts Update State to Visibility.Visible
        /// </summary>
        public Visibility UpdateVisibility
        {
            get
            {
                if (State == WindowState.Update) return Visibility.Visible;
                else return Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Converts Add State to Visibility.Visible
        /// </summary>
        public Visibility AddVisibility
        {
            get
            {
                if (State == WindowState.Add) return Visibility.Visible;
                else return Visibility.Collapsed;
            }
        }

        private Visibility _passwordVisible;

        /// <summary>
        /// DataBinding alerts UI to show or hide Password
        /// </summary>
        public Visibility PasswordVisible
        {
            get { return _passwordVisible; }
            set
            {
                _passwordVisible = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("PasswordHidden"));
                    PropertyChanged(this, new PropertyChangedEventArgs("PasswordVisible"));
                }
            }
        }
        public Visibility PasswordHidden
        {
            get
            {
                if (PasswordVisible == Visibility.Visible || State == WindowState.Update) return Visibility.Collapsed;
                else return Visibility.Visible;
            }
        }
        #endregion

        /// <summary>
        /// Creates new Customer and modifies UI (ObservableCollection) accordingly.
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <param name="password"></param>
        public void Add(uint ID, string name, string phone, double longitude, double latitude, string password = "")
        {
            bl.AddCustomer(ID, name, phone, longitude, latitude, password);
            _collection.Add(new PO.Customer(ID, bl));
        }

    }
}
