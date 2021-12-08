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
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for DisplayDroneList.xaml
    /// </summary>
    
    public partial class DisplayDroneList : Window
    {
        private IBL.IBL bl;
        public DisplayDroneList(IBL.IBL bl)
        {
            this.bl = bl;

            InitializeComponent();

            DroneListView.ItemsSource = bl.ListDrones();

            StatusSelector.ItemsSource = Enum.GetValues(typeof(IBL.BO.DroneStatuses));
            WeightSelector.ItemsSource = Enum.GetValues(typeof(IBL.BO.WeightCategories));
        }

        private IEnumerable<IBL.BO.DroneStatuses> selectedStatuses = new List<IBL.BO.DroneStatuses>();
        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedStatuses = e.AddedItems.Cast<IBL.BO.DroneStatuses>();
            RefreshDroneList();
        }

        private IEnumerable<IBL.BO.WeightCategories> selectedWeights = new List<IBL.BO.WeightCategories>();
        private void WeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedWeights = e.AddedItems.Cast<IBL.BO.WeightCategories>();
            RefreshDroneList();
        }

        private void RefreshDroneList()
        {
            DroneListView.ItemsSource = bl.ListDronesFiltered(drone => (selectedStatuses.Count() > 0 ? selectedStatuses.Contains(drone.Status) : true) && (selectedWeights.Count() > 0 ? selectedWeights.Contains(drone.Weight) : true));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new AddDroneWindow(bl).Show();
        }
    }
}
