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
    /// Interaction logic for ManagerMenuPage.xaml
    /// </summary>
    public partial class ManagerMenuPage : Page
    {
        BlApi.IBL bl;
        public ManagerMenuPage(BlApi.IBL bl)
        {
            this.bl = bl;
            InitializeComponent();
        }

        private void Drones_Menu_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DisplayDroneListPage(new Models.DronesModel(bl)));
        }

        private void Stations_Menu_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DisplayStationListPage(new Models.StationsModel(bl)));
        }

        private void Customers_Menu_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DisplayCustomerListPage(new Models.CustomersModel(bl, null)));
        }

        private void Packages_Menu_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DisplayPackageListPage(new Models.PackagesModel(bl)));
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
