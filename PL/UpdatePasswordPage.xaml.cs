﻿using System;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for UpdatePasswordPage.xaml
    /// </summary>
    public partial class UpdatePasswordPage : Page
    {
        //private enum PasswordState { Visible, Hidden }

        //private PasswordState passwordState = PasswordState.Hidden;

        private uint ID;

        public UpdatePasswordPage(uint ID)
        {
            this.ID = ID;

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
            return;
        }

        public bool Check_Strong_Security(PasswordBox p)
        {
            Regex regex = new Regex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[-+_!@#$%^&*., ?]).+$");
            if (p.Password.Length < 8) throw new Exception("Password must be at least 8 characters!");
            if (!regex.IsMatch(p.Password)) throw new Exception("Password must contain at least 1 upper & lowercase letter and a special character!");

            return true;
        }
    }
}
