using BO;
using PL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Windows.Controls.Primitives;

namespace PL
{
    /// <summary>
    /// Interaction logic for DronePage.xaml
    /// </summary>
    public partial class DronePage : Page
    {
        public DronePage(DronesModel drones)
        {
            DataContext = drones;

            InitializeComponent();

            if (drones.State == DronesModel.WindowState.Add) return;

            ButtonGrid.SetValue(Grid.RowProperty, 11);

            DroneID_input.IsEnabled = false;
            DroneID_input.Foreground = Brushes.Gray;

            WeightSelector.IsEnabled = false;
            WeightSelector.Foreground = Brushes.Gray;

            DroneLocation_output.Foreground = Brushes.Gray;

            DroneStatus_output.Foreground = Brushes.Gray;

            DroneBattery_output.Foreground = Brushes.Gray;

            ActionButtonsGridLeft.Background = new SolidColorBrush(Color.FromArgb(0x4d, 0x4d, 0x4d, 0x4d));
            ActionButtonsGridCenter.Background = new SolidColorBrush(Color.FromArgb(0x4d, 0x4d, 0x4d, 0x4d));
            ActionButtonsGridRight.Background = new SolidColorBrush(Color.FromArgb(0x4d, 0x4d, 0x4d, 0x4d));
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                if (DroneID_input.Text != "" && int.Parse(DroneID_input.Text) >= 0 && DroneModel_input.Text != "" &&
                    StationIDSelector.SelectedItem != null && WeightSelector.SelectedItem != null)
                {
                    (DataContext as DronesModel).Add(uint.Parse(DroneID_input.Text), DroneModel_input.Text,
                        (BO.WeightCategories)WeightSelector.SelectedItem, (uint)StationIDSelector.SelectedItem);
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
                (DataContext as DronesModel).SelectedDrone.Deliver();
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
                (DataContext as DronesModel).SelectedDrone.Collect();
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
                (DataContext as DronesModel).SelectedDrone.Assign();
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
                (DataContext as DronesModel).SelectedDrone.Release();
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
                (DataContext as DronesModel).SelectedDrone.Charge();
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

        //private BackgroundWorker worker;
        //private volatile bool simulator = false;
        private void Toggle_Simulate(object sender, RoutedEventArgs e)
        {
            if ((sender as ToggleButton).IsChecked.Value)
            {
                (DataContext as DronesModel).SelectedDrone.Simulate(Worker_RunWorkerCompleted);
                loading_animation.Visibility = Visibility.Visible;
            }
            /*simulator = true;
            worker = new BackgroundWorker();

            worker.DoWork += ((sender, e) =>
            {
                //worker.ReportProgress(0);
                // Call BL Simulate function here
                (DataContext as DronesModel).SelectedDrone.Simulate(() => worker.ReportProgress(0), () => simulator);
                //Thread.Sleep(30000);
                //worker.ReportProgress(70);
                //(DataContext as PO.Drone).Simulate(() => SimulatorToggleButton.IsEnabled);
            });

            worker.ProgressChanged += Worker_ProgressChanged;

            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;

            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;

            worker.RunWorkerAsync();
        }*/
            else
            {
                (DataContext as DronesModel).SelectedDrone.StopSimulator();
                SimulatorToggleButton.IsEnabled = false;
                MsgBox.Show("Info", "Stopped Work");
            }
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SimulatorToggleButton.IsChecked = false;
            SimulatorToggleButton.IsEnabled = true;
            loading_animation.Visibility = Visibility.Collapsed;
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //(DataContext as DronesModel).SelectedDrone.Reload();
            /*if (e.ProgressPercentage == 0)
            {
                loading_animation.Visibility = Visibility.Visible;
                MsgBox.Show("Info", "Started Work");
            }
            else
            {
                MsgBox.Show("Info", "Finished Work");
            }*/
        }
    }
}
