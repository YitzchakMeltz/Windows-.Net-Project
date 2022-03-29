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
using System.Text.RegularExpressions;
using BlApi;

namespace PL
{
    /// <summary>
    /// Interaction logic for UpdatePasswordPage.xaml
    /// </summary>
    public partial class UpdatePasswordPage : Page
    {
        PO.Customer customer;

        public UpdatePasswordPage(PO.Customer customer)
        {
            this.customer = customer;

            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Handle_Placeholder(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            TextBlock passwordPlaceholder = OldPasswordPlaceholder;
            Button visibilityButton = OldVisibilityButton;

            switch (passwordBox.Name)
            {
                case "N1Password_input":
                    passwordPlaceholder = N1PasswordPlaceholder;
                    visibilityButton = N1VisibilityButton;
                    break;

                case "N2Password_input":
                    passwordPlaceholder = N2PasswordPlaceholder;
                    visibilityButton= N2VisibilityButton;
                    break;
            }

            if (passwordBox.SecurePassword.Length == 0)
            {
                passwordPlaceholder.Visibility = Visibility.Visible;
            }
            else
            {
                passwordPlaceholder.Visibility = Visibility.Hidden;
                visibilityButton.IsEnabled = true;
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
            TextBox textBox = OldVisiblePassword_input;
            Image icon = OldVisibilityIcon;
            PasswordBox passwordBox = OldPassword_input;

            switch ((sender as Button).Name)
            {
                case "N1VisibilityButton":
                    textBox = N1VisiblePassword_input;
                    icon = N1VisibilityIcon;
                    passwordBox = N1Password_input;
                    break;

                case "N2VisibilityButton":
                    textBox = N2VisiblePassword_input;
                    icon = N2VisibilityIcon;
                    passwordBox = N2Password_input;
                    break;
            }

            switch (textBox.Visibility)
            {
                case Visibility.Collapsed:
                    textBox.Visibility = Visibility.Visible;
                    textBox.Text = passwordBox.Password;

                    passwordBox.Visibility = Visibility.Collapsed;
                    icon.Source = new BitmapImage(new Uri(@"\icons\visible.png", UriKind.Relative));
                    break;
                case Visibility.Visible:
                    textBox.Visibility = Visibility.Collapsed;
                    passwordBox.Password = textBox.Text;
                    textBox.Text = "";    //clear plaintext password from memory for security reasons

                    passwordBox.Visibility = Visibility.Visible;
                    icon.Source = new BitmapImage(new Uri(@"\icons\hidden.png", UriKind.Relative));
                    break;
            }
        }

        private void Change_Password_Button_Click(object sender, RoutedEventArgs e)
        {
            if (OldVisiblePassword_input.Visibility == Visibility.Visible)
            {
                OldPassword_input.Password = OldVisiblePassword_input.Text;
                OldVisiblePassword_input.Text = "";    //clear plaintext password from memory for security reasons
            }
            if (N1VisiblePassword_input.Visibility == Visibility.Visible)
            {
                N1Password_input.Password = N1VisiblePassword_input.Text;
                N1VisiblePassword_input.Text = "";    //clear plaintext password from memory for security reasons
            }
            if (N2VisiblePassword_input.Visibility == Visibility.Visible)
            {
                N2Password_input.Password = N2VisiblePassword_input.Text;
                N2VisiblePassword_input.Text = "";    //clear plaintext password from memory for security reasons
            }

            try
            {
                customer.UpdatePassword(new string[] { OldPassword_input.Password, N1Password_input.Password, N2Password_input.Password });
                MsgBox.Show("Success", "Password Updated Succesfully");
                NavigationService.GoBack();

            }
            catch (Exception exception)
            {
                MsgBox.Show("Error", exception.Message);
            }
        }
        private void CommandBinding_CanExecutePaste(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = false;
            e.Handled = true;
            MsgBox.Show("Error", "Copying & Pasting passwords has been disabled for security reasons");
        }
    }
}
