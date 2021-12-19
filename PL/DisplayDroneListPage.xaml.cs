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
    /// Interaction logic for DisplayDroneListPage.xaml
    /// </summary>
    public partial class DisplayDroneListPage : Page
    {
        IBL.IBL bl;
        public DisplayDroneListPage(IBL.IBL bl)//, Frame f)
        {
            this.bl = bl;

            InitializeComponent();

            DroneListView.ItemsSource = bl.ListDrones();

            StatusSelector.ItemsSource = Enum.GetValues<IBL.BO.DroneStatuses>().Select(s => s.ToString()).Prepend("All Statuses");
            WeightSelector.ItemsSource = Enum.GetValues<IBL.BO.WeightCategories>().Select(w => w.ToString()).Prepend("All Weights");
        }

        private void FilterItems()
        {
            DroneListView.ItemsSource = bl.ListDronesFiltered(drone => (StatusSelector.SelectedItem is "All Statuses" or null || drone.Status == Enum.Parse<IBL.BO.DroneStatuses>((string)StatusSelector.SelectedItem)) && (WeightSelector.SelectedItem is "All Weights" or null || drone.Weight == Enum.Parse<IBL.BO.WeightCategories>((string)WeightSelector.SelectedItem)));
            DroneListView.Items.Refresh();
        }
        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterItems();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddDronePage(bl));
        }

        private void DroneListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DroneListView.SelectedValue is not null)
                NavigationService.Navigate(new AddDronePage(bl, bl.GetDrone((int)DroneListView.SelectedValue)));
        }

        private void Home_Button_Click(object sender, RoutedEventArgs e)
        {
            //mainFrame.Content = new WelcomePage(mainFrame, bl);
            NavigationService.GoBack();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            FilterItems();
        }
    }
}
