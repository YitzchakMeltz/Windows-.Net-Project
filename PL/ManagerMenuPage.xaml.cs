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
            NavigationService.Navigate(new DisplayDroneListPage(bl));
        }

        private void Stations_Menu_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DisplayStationListPage(bl));
        }

        private void Customers_Menu_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DisplayCustomerListPage(bl));
        }

        private void Packages_Menu_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DisplayPackageListPage(bl, false));
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
