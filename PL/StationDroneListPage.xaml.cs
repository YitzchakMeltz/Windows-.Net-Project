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
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for StationDroneListPage.xaml
    /// </summary>
    public partial class StationDroneListPage : Page
    {
        BlApi.IBL bl;
        public StationDroneListPage(BlApi.IBL bl, BaseStation station)
        {
            this.bl = bl;

            InitializeComponent();

            HeaderTitle.Text = "Station ID: " + station.ID.ToString();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
