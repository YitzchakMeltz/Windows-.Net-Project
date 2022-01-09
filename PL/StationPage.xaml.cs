using BO;
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
    /// Interaction logic for StationPage.xaml
    /// </summary>
    public partial class StationPage : Page
    {
        private String originalName, originalTotalSlots;
        public StationPage(StationsModel model)
        {
            InitializeComponent();

            DataContext = model;

            ButtonGrid.SetValue(Grid.RowProperty, 11);

            originalName = (DataContext as StationsModel).SelectedStation.Name;
            originalTotalSlots = (DataContext as StationsModel).SelectedStation.AvailableChargingSlots.ToString();
        }

        private void View_Drones_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StationDroneListPage(DataContext as StationsModel));
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
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
                (DataContext as StationsModel).Add(stationID, Name_input.Text, longitude, latitude, int.Parse(TotalSlots_input.Text));
                MsgBox.Show("Success", "Station Added Succesfully");
                NavigationService.GoBack();
            }
            catch (Exception exception)
            {
                MsgBox.Show("Error", exception.Message);
            }
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Revert_Button_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as StationsModel).SelectedStation.Name = originalName;
            (DataContext as StationsModel).SelectedStation.AvailableChargingSlots = uint.Parse(originalTotalSlots);
        }
    }
}
