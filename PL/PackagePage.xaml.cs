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
    /// Interaction logic for PackagePage.xaml
    /// </summary>
    public partial class PackagePage : Page
    {
        private enum State { Add, Update }
        private State windowState = State.Add;
        private Boolean isUser;

        BlApi.IBL bl;
        public PackagePage(BlApi.IBL bl, Boolean isUser)
        {
            this.bl = bl;

            InitializeComponent();

            WeightSelector.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));

            PrioritySelector.ItemsSource = Enum.GetValues(typeof(BO.Priorities));
        }

        public PackagePage(BlApi.IBL bl, Package package, Boolean isUser) : this(bl, isUser)
        {
            windowState = State.Update;

            SenderID_input.Text = package.Sender.ID.ToString();
            SenderID_input.IsEnabled = false;
            SenderID_input.Foreground = Brushes.Gray;

            ReceiverID_input.Text = package.Receiver.ID.ToString();

            WeightSelector.SelectedItem = package.Weight;
            WeightSelector.IsEnabled = false;
            WeightSelector.Foreground = Brushes.Gray;

            PrioritySelector.SelectedItem = package.Weight;
            PrioritySelector.IsEnabled = false;
            PrioritySelector.Foreground = Brushes.Gray;

            DeletePackageButton.Visibility = Visibility.Visible;

            AddButton.Content = "Update";
        }

        public PackagePage(BlApi.IBL bl, String SenderID, Boolean isUser) : this(bl, isUser)
        {
            SenderID_input.Text = SenderID;
            SenderID_input.IsEnabled = false;
            SenderID_input.Foreground = Brushes.Gray;
        }

        private void Delete_Package_Button_Click(object sender, RoutedEventArgs e)
        {
            MsgBox.Show("Error", "Not Implemented Yet!");
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            switch(windowState)
            {
                case State.Add:
                    try
                    {
                        int senderID;
                        if (int.TryParse(SenderID_input.Text.Replace("Sender ID: ",""), out senderID) == false)
                            throw new BO.InvalidManeuver("Inputted Sender ID is not valid");
                        int receiverID;
                        if (int.TryParse(ReceiverID_input.Text, out receiverID) == false)
                            throw new BO.InvalidManeuver("Inputted Receiver ID is not valid");
                        bl.AddPackage(senderID, receiverID, (BO.WeightCategories)WeightSelector.SelectedItem, ((BO.Priorities)PrioritySelector.SelectedItem));
                        MsgBox.Show("Success", "Package Succesfully Added");
                        NavigationService.GoBack();
                    }
                    catch (Exception exception)
                    {
                        MsgBox.Show("Error", exception.Message);
                    }
                    break;
            }
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
