using IBL.BO;
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

        IBL.IBL bl;
        Frame mainFrame;

        public AddDronePage(Frame f, IBL.IBL bl)
        {
            this.bl = bl;
            this.mainFrame = f;

            InitializeComponent();

            WeightSelector.ItemsSource = Enum.GetValues(typeof(IBL.BO.WeightCategories));

            StationIDSelector.ItemsSource = bl.ListStations().Select(station => station.ID);
        }

        public AddDronePage(Frame f, IBL.IBL bl, Drone drone) : this(f, bl)
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

            StationIDSelector.IsEnabled = false;
            StationIDSelector.ItemsSource = new Location[] { drone.Location };
            StationIDSelector.SelectedIndex = 0;
            StationIDSelector.Foreground = Brushes.Gray;

            AddButton.Content = "Update";
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DroneID_input.Text != "" && DroneModel_input.Text != "" &&
                               StationIDSelector.SelectedItem != null && WeightSelector.SelectedItem != null)
                {
                    switch (windowState)
                    {
                        case State.Add:
                            bl.AddDrone(int.Parse(DroneID_input.Text), DroneModel_input.Text,
                                (IBL.BO.WeightCategories)WeightSelector.SelectedItem, (int)StationIDSelector.SelectedItem);
                            break;

                        case State.Update:
                            bl.UpdateDrone(int.Parse(DroneID_input.Text), DroneModel_input.Text);
                            break;
                    }

                    mainFrame.Content = new DisplayDroneListPage(bl, mainFrame);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new DisplayDroneListPage(bl, mainFrame);
        }

        private void Deliver_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.DeliverPackage(int.Parse(DroneID_input.Text));
                DroneStatus_output.Text = bl.GetDrone(int.Parse(DroneID_input.Text)).Status.ToString();
                StationIDSelector.ItemsSource = new Location[] { bl.GetDrone(int.Parse(DroneID_input.Text)).Location };
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
                StationIDSelector.ItemsSource = new Location[] { bl.GetDrone(int.Parse(DroneID_input.Text)).Location };
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
                bl.ReleaseDrone(int.Parse(DroneID_input.Text), 60);
                DroneBattery_output.Text = Math.Round(bl.GetDrone(int.Parse(DroneID_input.Text)).Battery, 2).ToString() + "%";
                DroneStatus_output.Text = bl.GetDrone(int.Parse(DroneID_input.Text)).Status.ToString();
                MessageBox.Show("The Drone has been released from charging for 60 minutes.", "Success");
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
