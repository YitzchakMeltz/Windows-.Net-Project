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
    /// Interaction logic for MsgBox.xaml
    /// </summary>
    public partial class MsgBox : Window
    {
        public MsgBox(String type, String msg)
        {
            InitializeComponent();

            this.Title = type;
            msg_text.Text = msg;

            if (type.Equals("Success"))
                msgbox_icon.Source = new BitmapImage(new Uri(@"\icons\check.png", UriKind.Relative));
        }

        public static void Show(string type, string msg)
        {
            new MsgBox(type, msg).ShowDialog();
        }

        private void Ok_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
