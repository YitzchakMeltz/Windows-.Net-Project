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

namespace PL
{
    /// <summary>
    /// Interaction logic for DisplayCustomerListPage.xaml
    /// </summary>
    public partial class DisplayCustomerListPage : Page
    {
        BlApi.IBL bl;
        public DisplayCustomerListPage(BlApi.IBL bl)
        {
            this.bl = bl;

            InitializeComponent();

            CustomerListView.ItemsSource = bl.ListCustomers();
            
        }

        private void Add_Customer_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void CustomerListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (CustomerListView.SelectedValue is not null)
                NavigationService.Navigate(new UserSignInPage(bl, bl.GetCustomer((int)CustomerListView.SelectedValue), false));
        }
    }
}
