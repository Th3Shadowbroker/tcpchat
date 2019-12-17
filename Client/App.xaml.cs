using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Client
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                new MainWindow().Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to establish a connection to 127.0.0.1:3305");
                Application.Current.Shutdown();
            }
        }
    }
}
