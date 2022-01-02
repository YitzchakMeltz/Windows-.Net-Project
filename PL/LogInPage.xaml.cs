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
        private enum PasswordState {Visible, Hidden}
        private PasswordState passwordState = PasswordState.Hidden;

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
                if (bl.Login(int.Parse(ID_input.Text), System.Text.Encoding.UTF8.GetBytes(Password_input.Password)))
                {
                    NavigationService.Navigate(new CustomerPage(bl, bl.GetCustomer(int.Parse(ID_input.Text)), true));
                }
                else throw new BO.InvalidManeuver("Invalid credentials.");
            }
            catch (Exception exception)
            {
                MsgBox.Show("Error", exception.Message);
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Handle_Placeholder(object sender, RoutedEventArgs e)
        {
            if (Password_input.SecurePassword.Length == 0)
            {
                PasswordPlaceholder.Visibility = Visibility.Visible;
            }
            else
            {
                PasswordPlaceholder.Visibility = Visibility.Hidden;
                VisibilityButton.IsEnabled = true;
            }
        }

        private void Visibility_Click(object sender, RoutedEventArgs e)
        {
            switch(passwordState)
            {
                case PasswordState.Hidden:
                    VisiblePassword_input.Visibility = Visibility.Visible;
                    VisiblePassword_input.Text = Password_input.Password;

                    Password_input.Visibility = Visibility.Collapsed;
                    VisibilityIcon.Source = new BitmapImage(new Uri(@"\icons\visible.png", UriKind.Relative));
                    passwordState = PasswordState.Visible;
                    break;
                case PasswordState.Visible:
                    VisiblePassword_input.Visibility = Visibility.Collapsed;
                    Password_input.Password = VisiblePassword_input.Text;

                    Password_input.Visibility = Visibility.Visible;
                    VisibilityIcon.Source = new BitmapImage(new Uri(@"\icons\hidden.png", UriKind.Relative));
                    passwordState = PasswordState.Hidden;
                    break;
            }   
        }
    }
}
