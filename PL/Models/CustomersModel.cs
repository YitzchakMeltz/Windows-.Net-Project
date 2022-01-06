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
    public class CustomersModel : INotifyPropertyChanged
    {
        private IBL bl;

        private ObservableCollection<PO.Customer> _collection = new ObservableCollection<PO.Customer>();

        public CustomersModel(IBL bl, int? customerID = null)
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
            foreach (CustomerList customer in bl.ListCustomers()) _collection.Add(new PO.Customer(customer.ID, bl));
        }

        public ObservableCollection<PO.Customer> Collection { get { return _collection; } }
        public PO.Customer SelectedCustomer { get; set; }
        public enum WindowState { Add, Update }
        public WindowState State { get; set; }
        public Visibility AdminVisibility { init; get; }
        /*public Visibility NewVisibility { 
            get {
                if (State == WindowState.Add && AdminVisibility == Visibility.Collapsed) return Visibility.Visible;
                else return Visibility.Collapsed;
            } }*/
        public Visibility UserVisibility { 
            get
            {
                if (AdminVisibility == Visibility.Visible) return Visibility.Collapsed;
                else return Visibility.Visible;
            } }
        public Visibility UpdateVisibility
        {
            get
            {
                if (State == WindowState.Update) return Visibility.Visible;
                else return Visibility.Collapsed;
            }
        }
        public Visibility AddVisibility
        {
            get
            {
                if (State == WindowState.Add) return Visibility.Visible;
                else return Visibility.Collapsed;
            }
        }

        private Visibility _passwordVisible;

        public event PropertyChangedEventHandler PropertyChanged;

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

        public void Add(int ID, string name, string phone, double longitude, double latitude, string password = "")
        {
            bl.AddCustomer(ID, name, phone, longitude, latitude, password);
            _collection.Add(new PO.Customer(ID, bl));
        }

    }
}
