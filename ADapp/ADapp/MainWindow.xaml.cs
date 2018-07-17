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
        private string username;
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
                    entry.Close();
                }
            } catch(Exception ex)
            {
                System.Windows.MessageBox.Show("An exception was thrown because:\n" + ex.Message);
            }
        }

        private void ADCsearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Usersearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Usersearch_b_Click(object sender, RoutedEventArgs e)
        {
            username = Usersearch.Text;
            try {
                using (DirectoryEntry entry = new DirectoryEntry("LDAP://CN=" + username + domainList.domains[domainID].DNuser, userid, password, AuthenticationTypes.Secure))
                {
                    //do something with user
                    displayname.Text = entry.Properties["DisplayName"].Value.ToString();
                }
            } catch(Exception ex)
            {
                System.Windows.MessageBox.Show("An exception was thrown because:\n" + ex.Message);
            }
        }

        //RESET USER PW
        private void Resetpw_b_Click(object sender, RoutedEventArgs e)
        {
            username = Usersearch.Text;
            try
            {
                using (DirectoryEntry entry = new DirectoryEntry("LDAP://CN=" + username + domainList.domains[domainID].DNuser, userid, password, AuthenticationTypes.Secure))
                {
                    //do something with user
                    entry.Invoke("SetPassword", new object[] { "Welcome1" });
                    entry.Properties["LockOutTime"].Value = 0;
                    entry.CommitChanges();

                    System.Windows.MessageBox.Show("Password has been reset");
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("An exception was thrown because:\n" + ex.Message);
            }
        }

        //UNLOCK ACC
        private void Unlockacc_b_Click(object sender, RoutedEventArgs e)
        {
            username = Usersearch.Text;
            try
            {
                using (DirectoryEntry entry = new DirectoryEntry("LDAP://CN=" + username + domainList.domains[domainID].DNuser, userid, password, AuthenticationTypes.Secure))
                {
                    //do something with user
                    entry.Properties["LockOutTime"].Value = 0;
                    entry.CommitChanges();

                    System.Windows.MessageBox.Show("Account unlocked");
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("An exception was thrown because:\n" + ex.Message);
            }
        }

        //CHANGE DESCRIP
        private void Changedescrip_b_Click(object sender, RoutedEventArgs e)
        {
            computerName = ADCsearch.Text;
            try
            {
                using (DirectoryEntry entry = new DirectoryEntry("LDAP://CN=" + computerName + domainList.domains[domainID].DNcomputer,
                                                             userid, password, AuthenticationTypes.Secure))
                {
                    string descrip = Description.Text;
                    entry.InvokeSet("description", descrip);   //Uncomment and modify this line if you want to play around
                    entry.CommitChanges();

                    System.Windows.MessageBox.Show("Successfully changed description");
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("An exception was thrown because:\n" + ex.Message);
            }
        }

        //ASSIGN ADM
        private void Assign_b_Click(object sender, RoutedEventArgs e)
        {
            computerName = ADCsearch.Text;
            try
            {
                using(DirectoryEntry entry = new DirectoryEntry("LDAP://CN=" + computerName + domainList.domains[domainID].DNcomputer,
                                                             userid, password, AuthenticationTypes.Secure))
                {
                    string useradm = useradm_assign.Text;
                    DirectoryEntry entry2 = new DirectoryEntry("LDAP://CN=" + useradm + domainList.domains[domainID].DNuser, userid, password, AuthenticationTypes.Secure);

                    entry.Properties["managedby"].Clear();
                    entry.Properties["managedby"].Add(entry2.Properties["distinguishedname"].Value);
                    //System.Windows.MessageBox.Show("An exception was thrown because:\n" + entry);
                    entry.CommitChanges();
                    entry.Close();

                    System.Windows.MessageBox.Show("Added " + useradm + " as admin for " + computerName);
                }
            }
            catch (System.DirectoryServices.DirectoryServicesCOMException ex)
            {
                System.Windows.MessageBox.Show("An exception was thrown because:\n" + ex.Message);
            }
        }

        //REMOVE ADM
        private void Remove_b_Click(object sender, RoutedEventArgs e)
        {
            computerName = ADCsearch.Text;
            try
            {
                using (DirectoryEntry entry = new DirectoryEntry("LDAP://CN=" + computerName + domainList.domains[domainID].DNcomputer,
                                                             userid, password, AuthenticationTypes.Secure))
                {
                    string useradm = useradm_remove.Text;
                    DirectoryEntry entry2 = new DirectoryEntry("LDAP://CN=" + useradm + domainList.domains[domainID].DNuser, userid, password, AuthenticationTypes.Secure);

                    entry.Properties["managedby"].Remove(entry2.Properties["distinguishedname"].Value);
                    entry.CommitChanges();

                    System.Windows.MessageBox.Show("Removed " + useradm + " as admin for " + computerName);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("An exception was thrown because:\n" + ex.Message);
            }
        }
    }
}
