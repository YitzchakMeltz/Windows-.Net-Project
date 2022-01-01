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
    /// Interaction logic for AddDronePage.xaml
    /// </summary>
    public partial class AddDronePage : Page
    {
        private enum State { Add, Update }
        private State windowState = State.Add;

        BlApi.IBL bl;

        public AddDronePage(BlApi.IBL bl)
        {
            this.bl = bl;

            InitializeComponent();

            WeightSelector.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));

            StationIDSelector.ItemsSource = bl.ListStations().Select(station => station.ID);
        }

        public AddDronePage(BlApi.IBL bl, Drone drone) : this(bl)
        {
            windowState = State.Update;

            ChargeButton.Visibility = Visibility.Visible;
            ReleaseButton.Visibility = Visibility.Visible;
            AssignButton.Visibility = Visibility.Visible;
            CollectButton.Visibility = Visibility.Visible;
            DeliverButton.Visibility = Visibility.Visible;
            DroneStatus_output.Visibility = Visibility.Visible;
            DroneBattery_output.Visibility = Visibility.Visible;

            ButtonGrid.SetValue(Grid.RowProperty, 11);

            DroneStatus_output.Text = drone.Status.ToString();

            DroneBattery_output.Text = Math.Round(drone.Battery, 2).ToString() + "%";

            DroneID_input.IsEnabled = false;
            DroneID_input.Text = drone.ID.ToString();

            DroneModel_input.Text = drone.Model;

            WeightSelector.IsEnabled = false;
            WeightSelector.SelectedItem = drone.Weight;
            WeightSelector.Foreground = Brushes.Gray;

            DroneLocation_output.Visibility = Visibility.Visible;
            DroneLocation_output.Text = drone.Location.ToString();

            StationIDSelector.Visibility = Visibility.Hidden;
            StationIDSelectorPlaceholder.Visibility = Visibility.Hidden;

            //StationIDSelector.IsEnabled = false;
            //StationIDSelector.ItemsSource = new Location[] { drone.Location };
            //StationIDSelector.SelectedIndex = 0;
            //StationIDSelector.Foreground = Brushes.Gray;

            CancelButton.Content = "Close";
            AddButton.Content = "Update";
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (windowState)
                {
                    case State.Add:
                        if (DroneID_input.Text != "" && int.Parse(DroneID_input.Text) >= 0 && DroneModel_input.Text != "" &&
                            StationIDSelector.SelectedItem != null && WeightSelector.SelectedItem != null)
                        {
                            bl.AddDrone(int.Parse(DroneID_input.Text), DroneModel_input.Text,
                                (BO.WeightCategories)WeightSelector.SelectedItem, (int)StationIDSelector.SelectedItem);
                            MessageBox.Show("Drone successfully added.", "Success", MessageBoxButton.OK);
                            NavigationService.GoBack();
                        }
                        else
                        {
                            MessageBox.Show("Drone could not be added.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        break;

                    case State.Update:
                        if (DroneModel_input.Text != "")
                        {
                            bl.UpdateDrone(int.Parse(DroneID_input.Text), DroneModel_input.Text);
                            MessageBox.Show("Dronw was successfully updated.", "Success", MessageBoxButton.OK);
                        }
                        else
                        {
                            MessageBox.Show("Drone could not be updated. (The drone model is probably empty).", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        break;
                } 
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                bl.DeliverPackage(int.Parse(DroneID_input.Text));
                DroneStatus_output.Text = bl.GetDrone(int.Parse(DroneID_input.Text)).Status.ToString();
                DroneLocation_output.Text = bl.GetDrone(int.Parse(DroneID_input.Text)).Location.ToString();
                MessageBox.Show("The Drone has successfully delivered the package.", "Success");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Collect_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.CollectPackage(int.Parse(DroneID_input.Text));
                DroneBattery_output.Text = Math.Round(bl.GetDrone(int.Parse(DroneID_input.Text)).Battery, 2).ToString() + "%";
                DroneLocation_output.Text = bl.GetDrone(int.Parse(DroneID_input.Text)).Location.ToString();
                MessageBox.Show("The Drone has successfully collected the package.", "Success");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Assign_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.AssignPackageToDrone(int.Parse(DroneID_input.Text));
                DroneStatus_output.Text = bl.GetDrone(int.Parse(DroneID_input.Text)).Status.ToString();
                MessageBox.Show("The Drone has successfully been assigned a package.", "Success");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Release_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.ReleaseDrone(int.Parse(DroneID_input.Text));
                DroneBattery_output.Text = Math.Round(bl.GetDrone(int.Parse(DroneID_input.Text)).Battery, 2).ToString() + "%";
                DroneStatus_output.Text = bl.GetDrone(int.Parse(DroneID_input.Text)).Status.ToString();
                MessageBox.Show("The Drone has been released from charging.", "Success");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Charge_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.ChargeDrone(int.Parse(DroneID_input.Text));
                DroneStatus_output.Text = bl.GetDrone(int.Parse(DroneID_input.Text)).Status.ToString();
                MessageBox.Show("The Drone has successfully been sent to charging.", "Success");
            }
            catch (Exception exception)
            { 
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); 
            }
        }
    }
}
