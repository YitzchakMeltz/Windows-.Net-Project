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
    /// Interaction logic for LogInPage.xaml
    /// </summary>
    public partial class LogInPage : Page
    {
        BlApi.IBL bl;
        public LogInPage(BlApi.IBL bl)
        {
            this.bl = bl;

            InitializeComponent();
        }

        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            try 
            {
                if (bl.Login(int.Parse(ID_input.Text), System.Text.Encoding.UTF8.GetBytes(Password_input.Text)))
                {
                    NavigationService.Navigate(new CustomerPage(bl, bl.GetCustomer(int.Parse(ID_input.Text)), true));
                }
                else throw new BO.InvalidManeuver("Invalid credentials.");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
