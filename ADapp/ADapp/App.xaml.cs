using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ADapp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public string userid;
        public string password;
        private string distinguishedName;
        
        public App(string a, string b)
        {
            userid = a;
            password = b;
        }

        private void createConnection()
        {

        }
    }
}
