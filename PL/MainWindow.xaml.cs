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
using BlApi;
using BO;
using PL.Models;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
  
    public partial class MainWindow : Window
    {
        static BlApi.IBL bl = BlApi.BlFactory.GetBl();

        public MainWindow()
        { 
            InitializeComponent();
            mainFrame.Content = new WelcomePage(mainFrame, bl);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Page page;
            page = mainFrame.Content as DisplayDroneListPage;
            page ??= mainFrame.Content as DronePage;
            if (page != null && (page.DataContext as DronesModel).SimulatorCount != 0)
            {
                e.Cancel = !MsgBox.Show("Question", "A simulator is running.\nAre you sure you want to exit?").Value;
            }
        }
    }
}
