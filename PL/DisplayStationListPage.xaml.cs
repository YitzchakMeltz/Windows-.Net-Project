using PL.Models;
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
    /// Interaction logic for DisplayStationListPage.xaml
    /// </summary>
    public partial class DisplayStationListPage : Page
    {
        public DisplayStationListPage(StationsModel model)
        {
            InitializeComponent();

            DataContext = model;
        }
        private void Add_Station_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as StationsModel).State = StationsModel.WindowState.Add;
            (DataContext as StationsModel).SelectedStation = null;
            NavigationService.Navigate(new StationPage(DataContext as StationsModel));
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void StationListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (StationListView.SelectedValue is not null)
            {
                (DataContext as StationsModel).State = StationsModel.WindowState.Update;
                NavigationService.Navigate(new StationPage(DataContext as StationsModel));
            }
        }

        private void Group_Button_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as StationsModel).NextGroup();
            /*GroupingWindow gp = new GroupingWindow("Station");
            gp.Owner = Application.Current.MainWindow;
            gp.ShowDialog();*/
        }
    }
    
}
