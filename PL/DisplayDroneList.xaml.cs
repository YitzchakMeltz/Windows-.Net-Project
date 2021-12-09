using System;
using System.Collections.Generic;
using IBL.BO;
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

            StatusSelector.ItemsSource = Enum.GetValues<IBL.BO.DroneStatuses>().Select(s => s.ToString()).Prepend("");
            WeightSelector.ItemsSource = Enum.GetValues<IBL.BO.WeightCategories>().Select(w => w.ToString()).Prepend("");
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DroneListView.ItemsSource = bl.ListDronesFiltered(drone => (StatusSelector.SelectedItem is "" or null || drone.Status == Enum.Parse<IBL.BO.DroneStatuses>((string)StatusSelector.SelectedItem)) && (WeightSelector.SelectedItem is "" or null || drone.Weight == Enum.Parse<IBL.BO.WeightCategories>((string)WeightSelector.SelectedItem)));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new AddDroneWindow(bl).Show();
        }
    }
}
