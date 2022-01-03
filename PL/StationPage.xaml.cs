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

            TotalSlots_input.Text = station.AvailableChargingSlots.ToString();

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
            switch (windowState)
            {
                case State.Add:
                    try
                    {
                        int stationID;
                        if (int.TryParse(ID_input.Text, out stationID) == false)
                            throw new BO.InvalidManeuver("Inputted Customer ID is not valid.");
                        double longitude;
                        if (double.TryParse(Longitude_input.Text, out longitude) == false)
                            throw new BO.InvalidManeuver("Inputted Longitude is not valid.");
                        double latitude;
                        if (double.TryParse(Latitude_input.Text, out latitude) == false)
                            throw new BO.InvalidManeuver("Inputted Latitude is not valid.");
                        bl.AddStation(stationID, Name_input.Text, longitude, latitude, int.Parse(TotalSlots_input.Text));
                        MsgBox.Show("Success", "Station Added Succesfully");
                        NavigationService.GoBack();
                    }
                    catch (Exception exception)
                    {
                        MsgBox.Show("Error", exception.Message);
                    }
                    break;
                case State.Update:
                    try
                    {
                        bl.UpdateStation(int.Parse(ID_input.Text), Name_input.Text, int.Parse(TotalSlots_input.Text));
                        MsgBox.Show("Success", "Customer Succesfully Updated");
                    }
                    catch (Exception exception)
                    {
                        MsgBox.Show("Error", exception.Message);
                    }
                    break;
            }
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
