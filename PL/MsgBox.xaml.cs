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
            {
                msgbox_icon.Source = new BitmapImage(new Uri(@"\icons\check.png", UriKind.Relative));
                OkButton.Content = "Ok, great!";
            }
            else if(type.Equals("Question"))
            {
                msgbox_icon.Source = new BitmapImage(new Uri(@"\icons\question.png", UriKind.Relative));
                OkButton.Content = "Yup";
            }
        }

        public static Boolean? Show(string type, string msg)
        {
            MsgBox msgbox = new MsgBox(type, msg);
            msgbox.Owner = Application.Current.MainWindow;
            return msgbox.ShowDialog();
        }

        private void Ok_Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }
    }
}
