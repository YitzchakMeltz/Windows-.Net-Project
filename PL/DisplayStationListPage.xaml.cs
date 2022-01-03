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
    /// Interaction logic for DisplayStationListPage.xaml
    /// </summary>
    public partial class DisplayStationListPage : Page
    {
        BlApi.IBL bl;
        public DisplayStationListPage(BlApi.IBL bl)
        {
            this.bl = bl;

            InitializeComponent();

            StationListView.ItemsSource = bl.ListStations();
        }
        private void Add_Station_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void StationListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (StationListView.SelectedValue is not null)
                NavigationService.Navigate(new CustomerPage(bl, bl.GetCustomer((int)StationListView.SelectedValue), false));
        }
    }
}
