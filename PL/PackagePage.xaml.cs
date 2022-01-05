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
    /// Interaction logic for PackagePage.xaml
    /// </summary>
    public partial class PackagePage : Page
    {
        private enum State { Add, Update }
        private State windowState = State.Add;

        BlApi.IBL bl;
        public PackagePage(BlApi.IBL bl)
        {
            this.bl = bl;

            InitializeComponent();

            WeightSelector.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));

            StationSelector.ItemsSource = bl.ListStations().Select(station => station.ID);
        }

        public PackagePage(BlApi.IBL bl, Package package) : this(bl)
        {
            windowState = State.Update;

            SenderID_input.Text = package.Sender.ID.ToString();
            SenderID_input.IsEnabled = false;
            SenderID_input.Foreground = Brushes.Gray;

            ReceiverID_input.Text = package.Receiver.ID.ToString();

            WeightSelector.SelectedItem = package.Weight;
            WeightSelector.IsEnabled = false;
            WeightSelector.Foreground = Brushes.Gray;

            StationSelector.SelectedItem = package.Weight;
            StationSelector.IsEnabled = false;
            StationSelector.Foreground = Brushes.Gray;

            DeletePackageButton.Visibility = Visibility.Visible;

            AddButton.Content = "Update";
        }
        private void Delete_Package_Button_Click(object sender, RoutedEventArgs e)
        {
            MsgBox.Show("Error", "Not Implemented Yet!");
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            MsgBox.Show("Error", "Not Implemented Yet!");
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
