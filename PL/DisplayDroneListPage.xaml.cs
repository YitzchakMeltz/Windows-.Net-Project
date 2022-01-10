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

        /*private void FilterItems()
        {
            //DataContext = new DronesModel(bl, DronesModel.WindowState.Update);
            //DroneListView.ItemsSource = bl.ListDronesFiltered(drone => (StatusSelector.SelectedItem is "All Statuses" or null || drone.Status == Enum.Parse<BO.DroneStatuses>((string)StatusSelector.SelectedItem)) && (WeightSelector.SelectedItem is "All Weights" or null || drone.Weight == Enum.Parse<BO.WeightCategories>((string)WeightSelector.SelectedItem)));
            //DroneListView.Items.Refresh();
        }
        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterItems();
        }*/

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as DronesModel).SelectedDrone = null;
            (DataContext as DronesModel).State = DronesModel.WindowState.Add;
            NavigationService.Navigate(new AddDronePage(DataContext as DronesModel));
        }

        private void DroneListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DroneListView.SelectedValue is not null)
            {
                (DataContext as DronesModel).State = DronesModel.WindowState.Update;
                NavigationService.Navigate(new AddDronePage(DataContext as DronesModel));
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
<<<<<<< Updated upstream

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            FilterItems();
        }

        private void Group_Button_Click(object sender, RoutedEventArgs e)
        {
            if (GroupedText.Text == "None")
                GroupedText.Text = "Skimmer";
            else
                GroupedText.Text = "None";
        }
=======
>>>>>>> Stashed changes
    }
}