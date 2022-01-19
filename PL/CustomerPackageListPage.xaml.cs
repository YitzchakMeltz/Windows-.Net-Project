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
    /// Interaction logic for CustomerPackageListPage.xaml
    /// </summary>
    public partial class CustomerPackageListPage : Page
    {
        public CustomerPackageListPage(CustomersModel model)
        {
            InitializeComponent();

            DataContext = model;

            if((DataContext as CustomersModel).AdminVisibility == Visibility.Visible)
            {
                AddPackageButton.Visibility = Visibility.Collapsed;
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Add_Package_Button_Click(object sender, RoutedEventArgs e)
        {
            if((DataContext as CustomersModel).AdminVisibility == Visibility.Collapsed)
            {
                NavigationService.Navigate(new PackagePage(new PackagesModel((DataContext as CustomersModel).bl, 
                    (DataContext as CustomersModel).SelectedCustomer))); //HeaderTitle.Text.Replace("Customer","Sender"), isUser));
            }
        }
    }
}
