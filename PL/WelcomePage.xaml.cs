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
    /// Interaction logic for WelcomePage.xaml
    /// </summary>
    public partial class WelcomePage : Page
    {
        BlApi.IBL bl;
        public WelcomePage(Frame f, BlApi.IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DisplayDroneListPage(bl));
        }

        private void Manager_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ManagerMenuPage(bl));
        }

        private void New_Customer_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CustomerPage(new Models.CustomersModel(bl, null)));
        }

        private void Existing_Customer_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LogInPage(bl));
        }
    }
}
