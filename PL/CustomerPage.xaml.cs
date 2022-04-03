using BO;
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
    /// Interaction logic for CustomerPage.xaml
    /// </summary>
    public partial class CustomerPage : Page
    {
        private String originalName, originalPhone;
        public CustomerPage(CustomersModel model)
        {
            InitializeComponent();

            DataContext = model;
            if (model.State == CustomersModel.WindowState.Add) return;

            ButtonGrid.SetValue(Grid.RowProperty, 10);

            ViewPackagesButton.Visibility = Visibility.Visible;

            Phone_input.SetValue(Grid.RowProperty, 6);
            Phone_placeholder.SetValue(Grid.RowProperty, 6);

            Longitude_input.SetValue(Grid.RowProperty, 7);
            Longitude_placeholder.SetValue(Grid.RowProperty, 7);

            Latitude_input.SetValue(Grid.RowProperty, 8);
            Latitude_placeholder.SetValue(Grid.RowProperty, 8);

            originalName = (DataContext as CustomersModel).SelectedCustomer.Name;
            originalPhone = (DataContext as CustomersModel).SelectedCustomer.Phone;
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Revert_Button_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as CustomersModel).SelectedCustomer.Name = originalName;
            (DataContext as CustomersModel).SelectedCustomer.Phone = originalPhone;
        }

        private void View_Packages_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CustomerPackageListPage(DataContext as CustomersModel));
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            if ((DataContext as CustomersModel).State == CustomersModel.WindowState.Add)
                try
                {
                    uint customerID;
                    if (uint.TryParse(ID_input.Text, out customerID) == false)
                        throw new BO.InvalidManeuver("Inputted Customer ID is not valid.");
                    double longitude;
                    if (double.TryParse(Longitude_input.Text, out longitude) == false)
                        throw new BO.InvalidManeuver("Inputted Longitude is not valid.");
                    double latitude;
                    if (double.TryParse(Latitude_input.Text, out latitude) == false)
                        throw new BO.InvalidManeuver("Inputted Latitude is not valid.");
                    (DataContext as CustomersModel).Add(customerID, Name_input.Text, Phone_input.Text, longitude, latitude, Password_input.Password);
                    MsgBox.Show("Success", "Customer Succesfully Added");
                    NavigationService.GoBack();
                }
                catch (Exception exception)
                {
                    if (exception is SecurityError)
                        Reset_Password_Box();
                    MsgBox.Show("Error", exception.Message);
                }
        }

        private void Logout_Button_Click(object sender, RoutedEventArgs e)
        {
                if (MsgBox.Show("Question", "Are you sure you want to log out?").Value)
                {

                    while (NavigationService.CanGoBack)
                    {
                        NavigationService.GoBack();
                    }
                }
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
            switch ((DataContext as CustomersModel).PasswordVisible)
            {
                case Visibility.Collapsed:
                    (DataContext as CustomersModel).PasswordVisible = Visibility.Visible;
                    VisiblePassword_input.Text = Password_input.Password;
                    VisibilityIcon.Source = new BitmapImage(new Uri(@"\icons\visible.png", UriKind.Relative));
                    break;
                case Visibility.Visible:
                    (DataContext as CustomersModel).PasswordVisible = Visibility.Collapsed;
                    Password_input.Password = VisiblePassword_input.Text;
                    VisiblePassword_input.Text = "";
                    VisibilityIcon.Source = new BitmapImage(new Uri(@"\icons\hidden.png", UriKind.Relative));
                    break;
            }
        }

        private void Change_Password_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UpdatePasswordPage((DataContext as CustomersModel).SelectedCustomer));
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Reset_Password_Box()
        {
            Password_input.Password = "";

            //clear plaintext password from memory for security reasons
            VisiblePassword_input.Text = "";

            Password_input.Visibility = Visibility.Visible;
            VisiblePassword_input.Visibility = Visibility.Collapsed;

            VisibilityIcon.Source = new BitmapImage(new Uri(@"\icons\hidden.png", UriKind.Relative));
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = false;
            e.Handled = true;
            MsgBox.Show("Error", "Copying & Pasting passwords has been disabled for security reasons");
        }
    }
}
