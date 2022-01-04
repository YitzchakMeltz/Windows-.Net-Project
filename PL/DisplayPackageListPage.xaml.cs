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
    /// Interaction logic for DisplayPackageListPage.xaml
    /// </summary>
    public partial class DisplayPackageListPage : Page
    {
        BlApi.IBL bl;
        public DisplayPackageListPage(BlApi.IBL bl)
        {
            this.bl = bl;

            InitializeComponent();

            PackageListView.ItemsSource = bl.ListPackages();
        }

        private void Add_Package_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PackagePage(bl));
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void PackageListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (PackageListView.SelectedValue is not null)
                NavigationService.Navigate(new PackagePage(bl, bl.GetPackage((int)PackageListView.SelectedValue)));
        }
    }
}
