using BO;
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
    /// Interaction logic for StationPage.xaml
    /// </summary>
    public partial class StationPage : Page
    {
        private enum State { Add, Update }
        private State windowState = State.Add;

        BlApi.IBL bl;
        public StationPage(BlApi.IBL bl)
        {
            this.bl = bl;

            InitializeComponent();
        }

        public StationPage(BlApi.IBL bl, BaseStation station) : this(bl)
        {
            windowState = State.Update;

            ID_input.Text = station.ID.ToString();
            ID_input.IsEnabled = false;
            ID_input.Foreground = Brushes.Gray;

            Name_input.Text = station.Name;

            Available_input.Text = station.AvailableChargingSlots.ToString();
            Available_input.IsEnabled = false;
            Available_input.Foreground = Brushes.Gray;

            Longitude_input.Text = station.Location.ToString();
            Longitude_input.IsEnabled = false;
            Longitude_input.Foreground = Brushes.Gray;

            Latitude_input.Visibility = Visibility.Collapsed;

            ViewDronesButton.Visibility = Visibility.Visible;

            ButtonGrid.SetValue(Grid.RowProperty, 11);

            AddButton.Content = "Update";
        }

        private void View_Drones_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CustomerPackageListPage(bl, bl.GetCustomer(int.Parse(ID_input.Text))));
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
