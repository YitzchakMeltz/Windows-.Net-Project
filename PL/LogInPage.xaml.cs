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
    /// Interaction logic for LogInPage.xaml
    /// </summary>
    public partial class LogInPage : Page
    {
        private enum PasswordState { Visible, Hidden }

        private PasswordState passwordState = PasswordState.Hidden;

        BlApi.IBL bl;
        public LogInPage(BlApi.IBL bl, bool admin = false)
        {
            this.bl = bl;

            DataContext = new LoginModel(admin);

            InitializeComponent();
        }

        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            if (passwordState == PasswordState.Visible)
            {
                Password_input.Password = VisiblePassword_input.Text;
                VisiblePassword_input.Text = "";    //clear plaintext password from memory for security reasons
            }

            try 
            {
                uint customerID;
                if (ID_input.Text == "Administrator") customerID = 0;

                if (uint.TryParse(ID_input.Text, out customerID) == false)
                    throw new BO.InvalidManeuver("Inputted ID is not valid.");
                if (bl.Login(customerID, System.Text.Encoding.UTF8.GetBytes(Password_input.Password)))
                {
                    if (customerID == 0) NavigationService.Navigate(new ManagerMenuPage(bl));
                    NavigationService.Navigate(new CustomerPage(new Models.CustomersModel(bl, uint.Parse(ID_input.Text))));
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

        private void Handle_CapsLock(object sender, RoutedEventArgs e)
        {
            if (Keyboard.IsKeyToggled(Key.CapsLock))
            {
                CapsLockMsg.Visibility = Visibility.Visible;
            }
            else
            {
                CapsLockMsg.Visibility = Visibility.Collapsed;
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
                    VisiblePassword_input.Text = "";    //clear plaintext password from memory for security reasons

                    Password_input.Visibility = Visibility.Visible;
                    VisibilityIcon.Source = new BitmapImage(new Uri(@"\icons\hidden.png", UriKind.Relative));
                    passwordState = PasswordState.Hidden;
                    break;
            }
        }

    }
}
