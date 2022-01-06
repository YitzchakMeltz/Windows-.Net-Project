using PL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for DisplayCustomerListPage.xaml
    /// </summary>
    public partial class DisplayCustomerListPage : Page
    {
        public DisplayCustomerListPage(CustomersModel model)
        {
            InitializeComponent();

            DataContext = model;
        }

        private void Add_Customer_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as CustomersModel).State = CustomersModel.WindowState.Add;
            (DataContext as CustomersModel).SelectedCustomer = null;
            NavigationService.Navigate(new CustomerPage(DataContext as CustomersModel));
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void CustomerListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (CustomerListView.SelectedValue is not null)
            {
                (DataContext as CustomersModel).State = CustomersModel.WindowState.Update;
                NavigationService.Navigate(new CustomerPage(DataContext as CustomersModel));
            }
        }
    }
}
