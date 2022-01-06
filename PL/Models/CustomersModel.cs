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
    public class CustomersModel
    {
        private IBL bl;

        private ObservableCollection<PO.Customer> _collection = new ObservableCollection<PO.Customer>();

        public CustomersModel(IBL bl, int? customerID = null)
        {
            this.bl = bl;
            if (customerID == null) IsAdmin = true;
            else
            {
                SelectedCustomer = new PO.Customer(customerID.Value, bl);
                IsAdmin = false;
            }
            foreach (CustomerList customer in bl.ListCustomers()) _collection.Add(new PO.Customer(customer.ID, bl));
        }

        public ObservableCollection<PO.Customer> Collection { get { return _collection; } }
        public PO.Customer SelectedCustomer { get; set; }
        public enum WindowState { Add, Update }
        public WindowState State { get; set; }
        public bool IsAdmin { init; get; }

        public bool PasswordVisible { get; set; }

        public void Add(int ID, string name, string phone, double longitude, double latitude, string password = "")
        {
            bl.AddCustomer(ID, name, phone, longitude, latitude, password);
            _collection.Add(new PO.Customer(ID, bl));
        }

    }
}
