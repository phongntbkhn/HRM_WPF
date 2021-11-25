using DataLayer.Data;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace HrmApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {

        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
               // Create the startup window
               Login wnd = new Login();
            wnd.Show();
        }
    }
}
