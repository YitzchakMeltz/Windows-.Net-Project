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
    /// Interaction logic for UpdatePasswordPage.xaml
    /// </summary>
    public partial class UpdatePasswordPage : Page
    {
        private enum PasswordState { Visible, Hidden }

        private PasswordState passwordState = PasswordState.Hidden;

        private uint ID;

        public UpdatePasswordPage()
        {
            InitializeComponent();
        }

        public UpdatePasswordPage(uint ID)
        {
            this.ID = ID;
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

        private void Password_input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.CapsLock && e.IsToggled)
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
            TextBox textBox;
            TextBox placeholder;
            PasswordBox passwordBox;

            switch ((sender as Button).Name)
            {
                case "OldVisibilityButton":
                    textBox = OldVisiblePassword_input;
                    passwordBox = OldPassword_input;
                    break;

                case "N1VisibilityButton":
                    textBox = N1VisiblePassword_input;
                    passwordBox = N1Password_input;
                    break;

                case "N2VisibilityButton":
                    textBox = N2VisiblePassword_input;
                    passwordBox = N2Password_input;
                    break;
            }

            switch (passwordState)
            {
                case PasswordState.Hidden:
                    textBox.Visibility = Visibility.Visible;
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
