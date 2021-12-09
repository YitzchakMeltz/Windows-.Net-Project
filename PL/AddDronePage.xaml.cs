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

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            if(DroneID_input.Text != "" && DroneID_input.Text != "" && 
                StationIDSelector.SelectedItem != null && WeightSelector.SelectedItem != null)
            {
                bl.AddDrone(int.Parse(DroneID_input.Text), DroneModel_input.Text,
                    (IBL.BO.WeightCategories)WeightSelector.SelectedItem, (int)StationIDSelector.SelectedItem);

                mainFrame.Content = new DisplayDroneListPage(bl, mainFrame);
            }
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new DisplayDroneListPage(bl, mainFrame);
        }
    }
}
