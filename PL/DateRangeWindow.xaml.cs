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
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for DateRangeWindow.xaml
    /// </summary>
    public partial class DateRangeWindow : Window
    {
        public DateRangeWindow(PackagesModel model)
        {
            InitializeComponent();
            DataContext = model;
        }

        public static bool Show(PackagesModel model)
        {
            return new DateRangeWindow(model) { Owner = Application.Current.MainWindow }.ShowDialog().Value;
        }
        private void Ok_Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
