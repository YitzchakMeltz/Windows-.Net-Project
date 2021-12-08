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

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IBL.BO.DroneStatuses droneStatuses = (IBL.BO.DroneStatuses)sender;
        }

        private void WeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IBL.BO.WeightCategories selectedWeight = (IBL.BO.WeightCategories)sender;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new AddDroneWindow(bl).Show();
        }
    }
}
