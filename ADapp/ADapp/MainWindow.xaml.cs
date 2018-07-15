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
using System.DirectoryServices;

namespace ADapp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string userid;
        private string password;
        private string computerName;
        private ADAddress domainList;
        private int domainID;
        public MainWindow(string u, string p, ADAddress a)
        {
            InitializeComponent();
            userid = u;
            password = p;
            domainList = a;
            this.Visibility = Visibility.Visible;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            domainID = Domain_Combo.SelectedIndex;
        }

        private void ADCsearch_b_Click(object sender, RoutedEventArgs e)
        {
            computerName = ADCsearch.Text;
            try
            {
                using (DirectoryEntry entry = new DirectoryEntry("LDAP://CN=" + computerName + domainList.domains[domainID].DNcomputer,
                                                             userid, password, AuthenticationTypes.Secure))
                {
                    OS.Text = entry.Properties["OperatingSystem"].Value.ToString() + " " + entry.Properties["OperatingSystemVersion"].Value.ToString();
                    ManagedBy.Text = entry.Properties["ManagedBy"].Value.ToString();
                }
            } catch(Exception ex)
            {
                System.Windows.MessageBox.Show("An exception was thrown because:\n" + ex.Message);
            }
        }
    }
}
