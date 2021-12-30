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
    /// Interaction logic for UserSignInPage.xaml
    /// </summary>
    public partial class UserSignInPage : Page
    {
        BlApi.IBL bl;
        public UserSignInPage(BlApi.IBL bl)
        {
            this.bl = bl;

            InitializeComponent();
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.AddCustomer(int.Parse(ID_input.Text), Name_input.Text,Phone_input.Text,double.Parse(Longitude_input.Text), double.Parse(Latitude_input.Text));
                NavigationService.GoBack();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
