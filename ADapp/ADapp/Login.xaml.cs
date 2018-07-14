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
using System.Xml.Serialization;
using System.IO;

namespace ADapp
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Login : Window
    {
        private ADAddress adAddr;
        public Login()
        {
            InitializeComponent();
            readXML();
        }

        private void readXML()
        {
            try
            {
                using (TextReader reader = new StreamReader(Directory.GetCurrentDirectory() + "\\adaddress.xml"))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ADAddress));
                    adAddr = (ADAddress)serializer.Deserialize(reader);
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.Message);
                Application.Current.Shutdown();
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainProg = new MainWindow(Userid.Text, Password.Password.ToString());
            this.Visibility = Visibility.Hidden;
        }

        private void Userid_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
