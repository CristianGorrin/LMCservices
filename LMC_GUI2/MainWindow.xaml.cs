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
using MahApps.Metro.Controls;

using Interface;
using LogicController;

namespace LMC_GUI2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private LogicController.Controller controller;

        public MainWindow()
        {
            InitializeComponent();
            this.controller = new Controller();
        }

        #region Method
        private void Logon()
        {
            var logon = new LMC_GUI2.LoginWindow();
            try
            {
                logon.ServerName = this.controller.Session.DbConnection.DataSource;
                logon.IntegratedSecurity = this.controller.Session.DbConnection.IntegratedSecurity;
                logon.UserName = this.controller.Session.DbConnection.UserName;
                logon.Pass = this.controller.Session.DbConnection.Pass;
            }
            catch (Exception)
            {
                logon.ServerName = "Server";
                logon.IntegratedSecurity = true;
            }
            

            logon.ShowDialog();

            if (logon.Exit)
            {
                Environment.Exit(0);
            }

            if (logon.Save)
            {
                this.controller.Session.DbConnection.DataSource = logon.ServerName;
                this.controller.Session.DbConnection.IntegratedSecurity = logon.IntegratedSecurity;
                this.controller.Session.DbConnection.UserName = logon.UserName;
                this.controller.Session.DbConnection.Pass = logon.Pass;
            }

            this.btn_login.Content = "Log på: " + this.controller.Session.DbConnection.DataSource;
        }
        #endregion


        #region Event handlers
        private void tbc_main_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // TODO tab controller
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Logon();
        }

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            Logon();
        }
        #endregion
    }
}
