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
    /// Interaction logic for CustomerPackageListPage.xaml
    /// </summary>
    public partial class CustomerPackageListPage : Page
    {
        BlApi.IBL bl;
        public CustomerPackageListPage(BlApi.IBL bl, Customer customer)
        {
            this.bl = bl;

            InitializeComponent();

            HeaderTitle.Text = "Customer ID: " + customer.ID.ToString();

            SentListView.ItemsSource = customer.Outgoing;
            ReceivedListView.ItemsSource = customer.Incoming;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
