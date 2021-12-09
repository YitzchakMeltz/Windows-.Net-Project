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
    /// Interaction logic for WelcomePage.xaml
    /// </summary>
    public partial class WelcomePage : Page
    {
        Frame mainFrame;
        IBL.IBL bl;
        public WelcomePage(Frame f, IBL.IBL bl)
        {
            InitializeComponent();
            this.mainFrame = f;
            this.bl = bl;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Content =  new DisplayDroneListPage(bl, mainFrame);
        }
    }
}
