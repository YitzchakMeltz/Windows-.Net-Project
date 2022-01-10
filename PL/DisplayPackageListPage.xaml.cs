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
    /// Interaction logic for DisplayPackageListPage.xaml
    /// </summary>
    public partial class DisplayPackageListPage : Page
    {
        public DisplayPackageListPage(PackagesModel model)
        {
            InitializeComponent();

            DataContext = model;
        }

        private void Add_Package_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as PackagesModel).SelectedPackage = null;
            (DataContext as PackagesModel).State = PackagesModel.WindowState.Add;
            NavigationService.Navigate(new PackagePage(DataContext as PackagesModel));
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void PackageListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (PackageListView.SelectedValue is not null)
            {
                (DataContext as PackagesModel).State = PackagesModel.WindowState.Update;
                NavigationService.Navigate(new PackagePage(DataContext as PackagesModel));
            }
        }

        private void Group_Button_Click(object sender, RoutedEventArgs e)
        {
            GroupingWindow gp = new GroupingWindow("Package");
            gp.Owner = Application.Current.MainWindow;
            gp.ShowDialog();
        }
    }
}
