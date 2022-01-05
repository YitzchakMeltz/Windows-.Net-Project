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
    /// Interaction logic for AddDronePage.xaml
    /// </summary>
    public partial class AddDronePage : Page
    {
        public enum State { Add, Update }

        public AddDronePage(DroneListModel drones, State windowsState)
        {
            DataContext = drones;

            InitializeComponent();

            //StationIDSelector.ItemsSource = bl.ListStations().Select(station => station.ID);
            if (windowsState == State.Add) return;

            ChargeButton.Visibility = Visibility.Visible;
            ReleaseButton.Visibility = Visibility.Visible;
            AssignButton.Visibility = Visibility.Visible;
            CollectButton.Visibility = Visibility.Visible;
            DeliverButton.Visibility = Visibility.Visible;
            DroneStatus_output.Visibility = Visibility.Visible;
            DroneBattery_output.Visibility = Visibility.Visible;

            ButtonGrid.SetValue(Grid.RowProperty, 11);

            DroneID_input.IsEnabled = false;
            DroneID_input.Foreground = Brushes.Gray;

            WeightSelector.IsEnabled = false;
            WeightSelector.Foreground = Brushes.Gray;

            DroneLocation_output.Foreground = Brushes.Gray;

            DroneStatus_output.Foreground = Brushes.Gray;

            DroneBattery_output.Foreground = Brushes.Gray;

            DroneLocation_output.Visibility = Visibility.Visible;

            StationIDSelector.Visibility = Visibility.Hidden;
            StationIDSelectorPlaceholder.Visibility = Visibility.Hidden;

            ActionButtonsGridLeft.Background = new SolidColorBrush(Color.FromArgb(0x4d, 0x4d, 0x4d, 0x4d));
            ActionButtonsGridCenter.Background = new SolidColorBrush(Color.FromArgb(0x4d, 0x4d, 0x4d, 0x4d));
            ActionButtonsGridRight.Background = new SolidColorBrush(Color.FromArgb(0x4d, 0x4d, 0x4d, 0x4d));

            ButtonGrid.Visibility = Visibility.Collapsed;
            CancelUpdateButton.Visibility = Visibility.Visible;
            BackButton.Visibility = Visibility.Visible;
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                if (DroneID_input.Text != "" && int.Parse(DroneID_input.Text) >= 0 && DroneModel_input.Text != "" &&
                    StationIDSelector.SelectedItem != null && WeightSelector.SelectedItem != null)
                {
                    (DataContext as DroneListModel).Add(int.Parse(DroneID_input.Text), DroneModel_input.Text,
                        (BO.WeightCategories)WeightSelector.SelectedItem, (int)StationIDSelector.SelectedItem);
                    MsgBox.Show("Success", "Drone successfully added.");
                    NavigationService.GoBack();
                }
                else
                {
                    MsgBox.Show("Error", "Drone could not be added.");
                }
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

        private void Deliver_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                (DataContext as DroneListModel).SelectedDrone.Deliver();
                MsgBox.Show("Success", "The Drone has successfully delivered the package.");
            }
            catch (Exception exception)
            {
                MsgBox.Show("Error", exception.Message);
            }
        }

        private void Collect_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                (DataContext as DroneListModel).SelectedDrone.Collect();
                MsgBox.Show("Success", "The Drone has successfully collected the package.");
            }
            catch (Exception exception)
            {
                MsgBox.Show("Error", exception.Message);
            }
        }

        private void Assign_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                (DataContext as DroneListModel).SelectedDrone.Assign();
                MsgBox.Show("Success", "The Drone has successfully been assigned a package.");
            }
            catch (Exception exception)
            {
                MsgBox.Show("Error", exception.Message);
            }
        }

        private void Release_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                (DataContext as DroneListModel).SelectedDrone.Release();
                MsgBox.Show("Success", "The Drone has been released from charging.");
            }
            catch (Exception exception)
            {
                MsgBox.Show("Error", exception.Message);
            }
        }

        private void Charge_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                (DataContext as DroneListModel).SelectedDrone.Charge();
                MsgBox.Show("Success", "The Drone has successfully been sent to charging.");
            }
            catch (Exception exception)
            { 
                MsgBox.Show("Error", exception.Message); 
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
