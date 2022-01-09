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
    /// Interaction logic for PackagePage.xaml
    /// </summary>
    public partial class PackagePage : Page
    {
        public PackagePage(PackagesModel model)
        {
            InitializeComponent();

            DataContext = model;

            if ((DataContext as PackagesModel).UpdateVisibility == Visibility.Visible)
            {
                SenderID_input.IsEnabled = false;
                SenderID_input.Foreground = Brushes.Gray;

                ReceiverID_input.IsEnabled = false;
                ReceiverID_input.Foreground = Brushes.Gray;

                WeightSelector.IsEnabled = false;
                WeightSelector.Foreground = Brushes.Gray;

                PrioritySelector.IsEnabled = false;
                PrioritySelector.Foreground = Brushes.Gray;
            }
        }

        private void Delete_Package_Button_Click(object sender, RoutedEventArgs e)
        {
            MsgBox.Show("Error", "Not Implemented Yet!");
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int senderID;
                if (int.TryParse(SenderID_input.Text.Replace("Sender ID: ",""), out senderID) == false)
                    throw new BO.InvalidManeuver("Inputted Sender ID is not valid");
                int receiverID;
                if (int.TryParse(ReceiverID_input.Text, out receiverID) == false)
                    throw new BO.InvalidManeuver("Inputted Receiver ID is not valid");
                (DataContext as PackagesModel).Add(senderID, receiverID, (BO.WeightCategories)WeightSelector.SelectedItem, (BO.Priorities)PrioritySelector.SelectedItem);
                MsgBox.Show("Success", "Package Succesfully Added");
                NavigationService.GoBack();
            }
            catch (Exception exception)
            {
                MsgBox.Show("Error", exception.Message);
            }
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
