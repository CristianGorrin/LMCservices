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
using MahApps.Metro.Controls;

using System.Data.SqlClient;

namespace LMC_GUI2
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : MetroWindow
    {
        private bool exit;
        private bool save;

        private string serverName;
        private bool integratedSecurity;
        private string userName;
        private string pass;

        public LoginWindow()
        {
            InitializeComponent();

            this.exit = true;
            this.save = false;
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.cmb_login_servertype.Items.Add("Windows Integrated Security");
            this.cmb_login_servertype.Items.Add("SQL Server Login");

            this.txt_login_servername.Text = this.serverName;

            if (this.integratedSecurity)
            {
                this.txt_login_password.IsEnabled = false;
                this.txt_login_username.IsEnabled = false;

                this.cmb_login_servertype.SelectedItem = "Windows Integrated Security";
            }
            else
            {
                this.txt_login_password.Text = this.pass;
                this.txt_login_username.Text = this.userName;

                this.cmb_login_servertype.SelectedItem = "SQL Server Login";
            }
        }

        private void cmb_login_servertype_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((string)this.cmb_login_servertype.SelectedItem == "Windows Integrated Security")
            {
                this.txt_login_password.IsEnabled = false;
                this.txt_login_username.IsEnabled = false;
            }
            else
            {
                this.txt_login_password.IsEnabled = true;
                this.txt_login_username.IsEnabled = true;
            }
        }

        private void btn_login_exit_Click(object sender, RoutedEventArgs e)
        {
            this.exit = true;
            this.Close();
        }

        private void btn_login_cancel_Click(object sender, RoutedEventArgs e)
        {
            if (TestServerConnection(BulidConnectionStringFromFilds()))
            {
                this.save = false;
                this.exit = false;
            }
            else
            {
                MessageBox.Show("Der kan ikke oprettes en forbindelse til serveren");
                return;
            }
            
            this.Close();
        }

        private void btn_login_login_Click(object sender, RoutedEventArgs e)
        {
            bool ok = TestServerConnection(BulidConnectionStringFromGUI());

            if (ok)
            {
                this.save = true;
                this.exit = false;

                this.serverName = this.txt_login_servername.Text;

                if ((string)this.cmb_login_servertype.SelectedItem == "Windows Integrated Security")
                {
                    this.integratedSecurity = true;
                }
                else
                {
                    this.integratedSecurity = false;
                }
                this.userName = this.txt_login_username.Text;
                this.pass = this.txt_login_password.Text;

                this.Close();
            }
            else
            {
                MessageBox.Show("Der kan ikke oprettes en forbindelse til serveren");
            }
        }

        public bool Exit { get { return this.exit; } }
        public bool Save { get { return this.save; } }


        public string ServerName { get { return  this.serverName; } set { this.serverName = value;} }
        public bool IntegratedSecurity { get { return this.integratedSecurity; } set { this.integratedSecurity = value; } }
        public string UserName { get { return this.userName; } set { this.userName = value; } }
        public string Pass { get { return this.pass; } set { this.pass = value; } }

        private bool TestServerConnection(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }
            }
        }

        private string BulidConnectionStringFromGUI()
        {
            string connection = string.Empty;

            connection += "Data Source=" + this.txt_login_servername.Text + ";";
            connection += "Initial Catalog=LMCdatabase;";
            connection += "Connect Timeout=10;";
            connection += "Integrated Security=";
            if ((string)this.cmb_login_servertype.SelectedItem == "Windows Integrated Security") 
            {
                connection += "True"; 
            }
            else
            {
                connection += "False;";
                connection += "User Id=" + this.txt_login_username.Text + ";";
                connection += "Password=" + this.txt_login_password.Text + ";";
            }
            
            connection += ";";
            connection += "Encrypt=False;";
            connection += "TrustServerCertificate=False";

            return connection;
        }
        private string BulidConnectionStringFromFilds()
        {
            string connection = string.Empty;

            connection += "Data Source=" + this.serverName + ";";
            connection += "Initial Catalog=LMCdatabase;";
            connection += "Connect Timeout=10;";
            connection += "Integrated Security=";
            if (this.integratedSecurity)
            {
                connection += "True";
            }
            else
            {
                connection += "False;";
                connection += "User Id=" + this.userName + ";";
                connection += "Password=" + this.pass + ";";
            }

            connection += ";";
            connection += "Encrypt=False;";
            connection += "TrustServerCertificate=False";

            return connection;
        }
    }
}
