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
    /// Interaction logic for CustomerPage.xaml
    /// </summary>
    public partial class CustomerPage : Page
    {
        private enum State { Add, Update }
        private State windowState = State.Add;
        private bool isUser;

        private enum PasswordState { Visible, Hidden }
        private PasswordState passwordState = PasswordState.Hidden;

        BlApi.IBL bl;

        public CustomerPage(BlApi.IBL bl)
        {
            this.bl = bl;

            InitializeComponent();
        }

        public CustomerPage(BlApi.IBL bl, Customer customer, Boolean isUser) : this(bl)
        {
            windowState = State.Update;
            this.isUser = isUser;

            AddButton.Content = "Update";

            ButtonGrid.SetValue(Grid.RowProperty, 10);

            if (isUser)
            {
                User_image.Visibility = Visibility.Hidden;

                Welcome_msg.Visibility = Visibility.Visible;
                Welcome_msg.Text = "Welcome Back " + customer.Name + "!";

                LogoutButton.Visibility = Visibility.Visible;
            }

            ViewPackagesButton.Visibility = Visibility.Visible;

            ID_input.Text = customer.ID.ToString();
            ID_input.IsEnabled = false;
            ID_input.Foreground = Brushes.Gray;

            Password_input.Visibility = Visibility.Collapsed;

            Phone_input.SetValue(Grid.RowProperty, 6);
            Phone_placeholder.SetValue(Grid.RowProperty, 6);

            Longitude_input.SetValue(Grid.RowProperty, 7);
            Longitude_placeholder.SetValue(Grid.RowProperty, 7);

            Latitude_input.SetValue(Grid.RowProperty, 8);
            Latitude_placeholder.SetValue(Grid.RowProperty, 8);

            Name_input.Text = customer.Name;

            Phone_input.Text = customer.Phone;

            Longitude_input.Text = customer.Location.ToString();
            Longitude_input.IsEnabled = false;
            Longitude_input.Foreground = Brushes.Gray;

            Latitude_input.Visibility = Visibility.Hidden;
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            if(!isUser)
            {
                NavigationService.GoBack();
            }
        }

        private void View_Packages_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CustomerPackageListPage(bl, bl.GetCustomer(int.Parse(ID_input.Text))));
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            switch (windowState)
            {
                case State.Add:
                    try
                    {
                        int customerID;
                        if (int.TryParse(ID_input.Text, out customerID) == false)
                            throw new BO.InvalidManeuver("Inputted Customer ID is not valid.");
                        double longitude;
                        if (double.TryParse(Longitude_input.Text, out longitude) == false)
                            throw new BO.InvalidManeuver("Inputted Longitude is not valid.");
                        double latitude;
                        if (double.TryParse(Latitude_input.Text, out latitude) == false)
                            throw new BO.InvalidManeuver("Inputted Latitude is not valid.");
                        bl.AddCustomer(customerID, Name_input.Text, Phone_input.Text, longitude, latitude, Password_input.Password);
                        NavigationService.GoBack();
                    }
                    catch (Exception exception)
                    {
                        MsgBox.Show("Error", exception.Message);
                    }
                    break;
                case State.Update:
                    try
                    {
                        bl.UpdateCustomer(int.Parse(ID_input.Text), Name_input.Text, Phone_input.Text);
                        MsgBox.Show("Success", "Customer Succesfully Updated");
                    }
                    catch (Exception exception)
                    {
                        MsgBox.Show("Error", exception.Message);
                    }
                    break;
            }
        }

        private void Logout_Button_Click(object sender, RoutedEventArgs e)
        {
            if (isUser)
            {
                while (NavigationService.CanGoBack)
                {
                    NavigationService.GoBack();
                }
            }
            else NavigationService.GoBack();
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
            switch (passwordState)
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
                    VisiblePassword_input.Text = "";

                    Password_input.Visibility = Visibility.Visible;
                    VisibilityIcon.Source = new BitmapImage(new Uri(@"\icons\hidden.png", UriKind.Relative));
                    passwordState = PasswordState.Hidden;
                    break;
            }
        }
    }
}
