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
using System.Collections.ObjectModel;
using System.Windows.Shapes;
using PL.Models;
using BlApi;

namespace PL
{
    /// <summary>
    /// Interaction logic for DisplayDroneListPage.xaml
    /// </summary>
    public partial class DisplayDroneListPage : Page
    {
        public DisplayDroneListPage(DronesModel model)
        {
            InitializeComponent();

            DataContext = model;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as DronesModel).SelectedDrone = null;
            (DataContext as DronesModel).State = DronesModel.WindowState.Add;
            NavigationService.Navigate(new DronePage(DataContext as DronesModel));
        }

        private void DroneListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DroneListView.SelectedValue is not null)
            {
                (DataContext as DronesModel).State = DronesModel.WindowState.Update;
                NavigationService.Navigate(new DronePage(DataContext as DronesModel));
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if ((DataContext as DronesModel).SimulatorCount == 0)
                NavigationService.GoBack();
            else
                MsgBox.Show("Error", "Cannot exit while simulator is running");
        }

        private void Group_Button_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as DronesModel).NextGroup();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            (DataContext as DronesModel).CollectionView.Refresh();
        }
    }
}