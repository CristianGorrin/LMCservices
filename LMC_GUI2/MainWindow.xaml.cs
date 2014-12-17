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

using System.Data;

using Interface;
using LogicController;
using System.Threading;

namespace LMC_GUI2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private LogicController.Controller controller;

        // Tab controller
        private int tabIndex;
        private int subTabIndex;

        public MainWindow()
        {
            InitializeComponent();
            this.controller = new Controller();
            this.tabIndex = 0;
            this.subTabIndex = 0;
        }

        #region Logon Window
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
        #region Tap changed
        private void tbc_main_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(e.OriginalSource is TabControl))
                return;
            
            Mouse.OverrideCursor = Cursors.Wait;

            TabControl tabCon;
            TabControl tab;
            
            try
            {
                tabCon = (TabControl)sender;
                tab = (TabControl)tabCon.SelectedContent;
            }
            catch (Exception)
            {
                Mouse.OverrideCursor = Cursors.Arrow;
                return;
            }


            if (tab == null || tabCon == null)
            {
                Mouse.OverrideCursor = Cursors.Arrow;
                return;
            }

            // TODO clean up 

            switch (tabCon.SelectedIndex)
            {
                case 0:
                    // Ordrer
                    switch (tab.SelectedIndex)
	                {
                        case 0:
                            if (this.tabIndex == 0 && this.subTabIndex == 0)
                            {
                                break;
                            }
                            else
                            {
                                CleanUp();

                                this.dgv_u_orders.ItemsSource = this.controller.GetOrdersCompanyAndPrivet().AsDataView();
                                this.tabIndex = 0;
                                this.subTabIndex = 0;
                            }
                            break;
                        case 1:
                            if (this.tabIndex == 0 && this.subTabIndex == 1)
                            {
                                break;
                            }
                            else
                            {
                                CleanUp();

                                this.dgv_p_orders.ItemsSource = this.controller.GetOrdersPrivet().DefaultView;
                                this.tabIndex = 0;
                                this.subTabIndex = 1;
                                
                                this.cmb_p_orders_worker.ItemsSource = this.controller.ListOfWorkers();
                                this.cmb_p_orders_customer.ItemsSource = this.controller.ListOfPrivateCustomers();
                                
                            }
                            break;
                        case 2:
                            if (this.tabIndex == 0 && this.subTabIndex == 2)
                            {
                                break;
                            }
                            else
                            {
                                CleanUp();

                                this.tabIndex = 0;
                                this.subTabIndex = 2;

                                this.dgv_c_orders.ItemsSource = this.controller.GetOrdersCompany().AsDataView();

                                this.cmb_c_orders_worker.ItemsSource = this.controller.ListOfWorkers();
                                this.cmb_c_orders_customer.ItemsSource = this.controller.ListOfCompanyCustomers();
                            }
                            break;
		                default:
                            throw new ArgumentOutOfRangeException("Sub tab index");
	                }
                    break;
                case 1:
                    //Acc
                        if (this.tabIndex == 1 && this.subTabIndex == 0)
                        {
                            break;
                        }
                        else
                        {
                            CleanUp();

                            this.tabIndex = 1;
                            this.subTabIndex = 0;
                        }
                    break;
                case 2:
                    // Regninger
                    switch (tab.SelectedIndex)
                    {
                        case 0:
                            if (this.tabIndex == 2 && this.subTabIndex == 0)
                            {
                                break;
                            }
                            else
                            {
                                CleanUp();

                                this.tabIndex = 2;
                                this.subTabIndex = 0;

                                this.cbm_p_invoices_customers.ItemsSource = this.controller.ListOfPrivateCustomersForInvoice();
                            }
                            break;
                        case 1:
                            if (this.tabIndex == 2 && this.subTabIndex == 1)
                            {
                                break;
                            }
                            else
                            {
                                CleanUp();

                                this.tabIndex = 2;
                                this.subTabIndex = 1;

                                this.cbm_c_invoices_customers.ItemsSource = this.controller.ListOfCompanyCustomersForInvoice();
                            }
                            break;
                        case 2:
                            if (this.tabIndex == 2 && this.subTabIndex == 2)
                            {
                                break;
                            }
                            else
                            {
                                CleanUp();

                                this.tabIndex = 2;
                                this.subTabIndex = 2;

                                this.dgv_np_invoices.ItemsSource = this.controller.GetInvoice().AsDataView();
                            }
                            break;
                        default:
                            throw new ArgumentOutOfRangeException("Sub tab index");
                    }
                    break;
                case 3:
                    // Kunder
                    switch (tab.SelectedIndex)
                    {
                        case 0:
                            if (this.tabIndex == 3 && this.subTabIndex == 0)
                            {
                                break;
                            }
                            else
                            {
                                CleanUp();

                                this.tabIndex = 3;
                                this.subTabIndex = 0;

                                this.dgv_p_customers.ItemsSource = this.controller.GetCustomersPrivet().AsDataView();
                            }
                            break;
                        case 1:
                            if (this.tabIndex == 3 && this.subTabIndex == 1)
                            {
                                break;
                            }
                            else
                            {
                                CleanUp();

                                this.tabIndex = 3;
                                this.subTabIndex = 1;

                                this.dgv_c_customers.ItemsSource = this.controller.GetCustomersCompany().AsDataView();
                            }
                            break;
                        default:
                            throw new ArgumentOutOfRangeException("Sub tab index");
                    }
                    break;
                case 4:
                    // Ansatte
                    if (this.tabIndex == 4 && this.subTabIndex == 0)
                    {
                        break;
                    }
                    else
                    {
                        CleanUp();

                        this.tabIndex = 4;
                        this.subTabIndex = 0;

                        this.dgv_workers.ItemsSource = this.controller.GetWorkers().AsDataView();
                    }
                    break;
                case 5:
                    // Adselinger
                    if (this.tabIndex == 5 && this.subTabIndex == 0)
                    {
                        break;
                    }
                    else
                    {
                        CleanUp();

                        this.tabIndex = 5;
                        this.subTabIndex = 0;

                        this.dgv_departments.ItemsSource = this.controller.GetDepartments().AsDataView();
                        this.cmb_departments_head.ItemsSource = this.controller.ListOfWorkers();
                    }
                    break;
                case 6:
                    // Bogføring
                    break;
                default:
                    throw new ArgumentOutOfRangeException("TabCon index");
            }

            Mouse.OverrideCursor = Cursors.Arrow;
        }
        #endregion

        #region Logon server
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Logon();

            MetroWindow loadingWindow = null;

            Thread newWindowThread = new Thread(new ThreadStart(() =>
            {
                // Create the Window
                loadingWindow = makeLoadingWindow();

                //show the Window
                loadingWindow.Show();
                // Start the Dispatcher Processing
                System.Windows.Threading.Dispatcher.Run();
            }));
            // Set the apartment state
            newWindowThread.SetApartmentState(ApartmentState.STA);
            // Make the thread a background thread
            newWindowThread.IsBackground = true;
            // Start the thread
            newWindowThread.Start();

            // wit for the newWindowThread to start up 
            Thread.Sleep(1000);

            this.controller.FildPostNo();
            this.dgv_u_orders.ItemsSource = this.controller.GetOrdersCompanyAndPrivet().AsDataView();

            bool? loaded = null;
            try
            {
                loadingWindow.Dispatcher.Invoke(() => { loaded = loadingWindow.IsLoaded; });
                loadingWindow.Dispatcher.Invoke(() => { loadingWindow.Close(); });
            }
            catch (Exception)
            {
                Environment.Exit(0);
            }


            if (loaded == false)
                Environment.Exit(0);
        }

        private MetroWindow makeLoadingWindow()
        {
            var loadingWindow = new MetroWindow();
            loadingWindow.Title = "Loading...";
            loadingWindow.Height = 200;
            loadingWindow.Width = 400;
            loadingWindow.ShowCloseButton = false;
            loadingWindow.Topmost = true;
            loadingWindow.Background = Brushes.DarkGreen;

            loadingWindow.ShowInTaskbar = false;
            loadingWindow.EnableDWMDropShadow = true;
            loadingWindow.WindowTransitionsEnabled = false;

            var progressRing = new MahApps.Metro.Controls.ProgressRing();
            progressRing.IsActive = true;
            progressRing.Foreground = Brushes.WhiteSmoke;

            loadingWindow.Content = progressRing;
            loadingWindow.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            loadingWindow.ResizeMode = System.Windows.ResizeMode.NoResize;
            return loadingWindow;
        }

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            Logon();
        }
        #endregion

        #region Order privet
        private void dgv_p_orders_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (!(sender is DataGrid))
                return;

            DataGrid senderItem = null;
            DataRowView selectedItem = null;
            DataRow row = null;

            try
            {
                senderItem = (DataGrid)sender;
                selectedItem = (DataRowView)senderItem.SelectedItem;
                row = (DataRow)selectedItem.Row;
            }
            catch (Exception)
            {
                return;
            }

            this.txt_p_orders_id.Text = row.ItemArray[5].ToString();
            this.cmb_p_orders_customer.Text = row.ItemArray[2].ToString();
            this.cmb_p_orders_worker.Text = row.ItemArray[0].ToString();
            this.txt_p_orders_houruse.Text = row.ItemArray[4].ToString();
            this.txt_p_orders_paidhour.Text = row.ItemArray[6].ToString();
            this.txt_p_orders_description.Text = row.ItemArray[3].ToString();
            this.dat_p_orders_startdate.SelectedDate = (DateTime)row.ItemArray[7];
        }

        private void btn_p_orders_remove_Click(object sender, RoutedEventArgs e)
        {
            if (this.dgv_p_orders.SelectedIndex == -1)
                return;

            Mouse.OverrideCursor = Cursors.Wait;

            int id;

            try
            {
                id = Convert.ToInt32(this.txt_p_orders_id.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Ordrenr. er ikke gyldigt");
                Mouse.OverrideCursor = Cursors.Arrow;
                return;
            }

            var deleteOk = MessageBox.Show("Er du sikker på at du vil slette ordre nr. " + id.ToString() + "?","Fjern order", MessageBoxButton.YesNo);
            if (deleteOk != MessageBoxResult.Yes)
            {
                Mouse.OverrideCursor = Cursors.Arrow;
                return;
            }

            int[] inUse;
            bool ok = this.controller.PrivetOrdersRemove(id, out inUse);
            
            if (inUse != null)
            {
                string message = string.Empty;

                message += "Kan ikke slette ordren da den bruges i følgende regning(er): ";

                message += inUse[0].ToString();
                for (int i = 1; i < inUse.Length - 1; i++)
                {
                    message += ", " + inUse[i].ToString();
                }

                if (inUse.Length > 1)
                {
                    message += ", " + inUse[inUse.Length - 1].ToString() + ".";
                }
                else
                {
                    message += ".";
                }

                MessageBox.Show(message);
                Mouse.OverrideCursor = Cursors.Arrow;
                return;
            }

            if (!ok)
            {
                MessageBox.Show("Kan ikke finde en ordre med ID " + id.ToString());
                Mouse.OverrideCursor = Cursors.Arrow;
                return;
            }

            this.dgv_p_orders.ItemsSource = this.controller.GetOrdersPrivet().DefaultView;
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void btn_p_orders_add_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            bool ok = true;
            string messege = string.Empty;

            int createById = -1;
            int customerId = -1;
            double hourUse = -1;
            double paidHour = -1;
            
            try 
	        {
                string temp = string.Empty;

                foreach (char item in this.cmb_p_orders_customer.Text)
                {
                    int tempInt;

                    if (int.TryParse(item.ToString(), out tempInt))
                    {
                        temp += item;
                    }
                    else if (item == '-')
                    {
                        break;
                    }
                }

                customerId = Convert.ToInt32(temp);
	        }
	        catch (Exception)
	        {
                ok = false;
                messege += "Kunde" + Environment.NewLine;
	        }

            try
            {
                string temp = string.Empty;

                foreach (char item in this.cmb_p_orders_worker.Text)
                {
                    int tempInt;

                    if (int.TryParse(item.ToString(), out tempInt))
                    {
                        temp += item;
                    }
                    else if (item == '-')
                    {
                        break;
                    }
                }

                createById = Convert.ToInt32(temp);
            }
            catch (Exception)
            {
                ok = false;
                messege += "Oprettet af" + Environment.NewLine;
            }

            try
            {
                hourUse = Convert.ToDouble(this.txt_p_orders_houruse.Text);
            }
            catch (Exception)
            {
                if (this.txt_p_orders_houruse.Text == string.Empty)
                {
                    hourUse = 0D;
                }
                else
                {
                    ok = false;
                    messege += "Timer brugt" + Environment.NewLine;
                }
            }

            if (hourUse > 24)
            {
                ok = false;
                messege += "Timer brugt der ikke er mere end 24 timer om dagen" + Environment.NewLine;
            }

            try
            {
                paidHour = Convert.ToDouble(this.txt_p_orders_paidhour.Text);
            }
            catch (Exception)
            {
                if (this.txt_p_orders_paidhour.Text == string.Empty)
                {
                    paidHour = 0D;
                }
                else
                {
                    ok = false;
                    messege += "Timeløn" + Environment.NewLine;
                }
            }

            if (this.txt_p_orders_description.Text == string.Empty)
            {
                ok = false;
                messege += "Opgavebeskrivelse" + Environment.NewLine;
            }

            if (this.dat_p_orders_startdate.SelectedDate == null)
            {
                ok = false;
                messege += "Vælg en start dato" + Environment.NewLine;
            }

            if (!ok)
	        {
                MessageBox.Show("Kan ikke tilføje/opdatere den nye ordre:" + Environment.NewLine + messege);
                Mouse.OverrideCursor = Cursors.Arrow;
                return;
	        }

            bool selectNew = false;

            if (this.txt_p_orders_id.Text == string.Empty)
            {
                // Add new order
                this.controller.PrivetOrdersAdd(createById, customerId, this.txt_p_orders_description.Text,
                    hourUse, paidHour, 1, (DateTime)this.dat_p_orders_startdate.SelectedDate);

                selectNew = true;
            }
            else
            {
                // Update order
                int id = -1;
                if (!int.TryParse(this.txt_p_orders_id.Text, out id))
                {
                    MessageBox.Show("Ordrenr. er ikke gyldigt");
                    Mouse.OverrideCursor = Cursors.Arrow;
                    return;
                }

                this.controller.PrivetOrdersUpdate(id, createById, customerId, this.txt_p_orders_description.Text,
                    hourUse, paidHour, 1, (DateTime)this.dat_p_orders_startdate.SelectedDate);
            }

            int selectNow = this.dgv_p_orders.SelectedIndex;

            this.dgv_p_orders.ItemsSource = this.controller.GetOrdersPrivet().DefaultView;

            if (selectNew)
                this.dgv_p_orders.SelectedIndex = this.dgv_p_orders.Items.Count - 1;
            else
                this.dgv_p_orders.SelectedIndex = selectNow;

            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void btn_p_orders_clear_Click(object sender, RoutedEventArgs e)
        {
            PrivetClearOrder();
        }

        private void txt_p_orders_id_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            PrivetClearOrder();
        }

        private void txt_p_orders_id_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try 
	            {
                    FindIdOrdersPrivet(Convert.ToInt32(this.txt_p_orders_id.Text));
	            }
	            catch (Exception)
	            {
	            }
            }
        }

        private void btn_p_orders_search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FindIdOrdersPrivet(Convert.ToInt32(this.txt_p_orders_id.Text));
            }
            catch (Exception)
            {
            }
        }

        private void FindIdOrdersPrivet(int id)
        {
            var items = (ItemCollection)this.dgv_p_orders.Items;
                
            for (int i = 0; i < items.Count; i++)
            {
                var view = (DataRowView)items[i];
                if (view.Row.ItemArray[5].ToString() == id.ToString())
                {
                    this.dgv_p_orders.SelectedIndex = i;
                    break;
                }
            }
        }

        private void PrivetClearOrder()
        {
            this.txt_p_orders_id.Text = string.Empty;
            this.cmb_p_orders_customer.Text = string.Empty;
            this.cmb_p_orders_worker.Text = string.Empty;
            this.txt_p_orders_houruse.Text = string.Empty;
            this.txt_p_orders_paidhour.Text = string.Empty;
            this.txt_p_orders_description.Text = string.Empty;
            this.dat_p_orders_startdate.Text = string.Empty;

            this.dgv_p_orders.SelectedIndex = -1;
        }
        #endregion

        #region Order company
        private void dgv_c_orders_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (!(sender is DataGrid))
                return;

            DataGrid senderItem = null;
            DataRowView selectedItem = null;
            DataRow row = null;

            try
            {
                senderItem = (DataGrid)sender;
                selectedItem = (DataRowView)senderItem.SelectedItem;
                row = (DataRow)selectedItem.Row;
            }
            catch (Exception)
            {
                return;
            }

            this.txt_c_orders_id.Text = row.ItemArray[5].ToString();
            this.cmb_c_orders_customer.Text = row.ItemArray[2].ToString();
            this.cmb_c_orders_worker.Text = row.ItemArray[0].ToString();
            this.txt_c_orders_houruse.Text = row.ItemArray[4].ToString();
            this.txt_c_orders_paidhour.Text = row.ItemArray[6].ToString();
            this.txt_c_orders_description.Text = row.ItemArray[3].ToString();
            this.dat_c_orders_startdate.SelectedDate = (DateTime)row.ItemArray[7];
        }

        private void txt_c_orders_id_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            CompanyClearOrder();
        }

        private void btn_c_orders_remove_Click(object sender, RoutedEventArgs e)
        {
            if (this.dgv_p_orders.SelectedIndex == -1)
                return;

            int id;

            try
            {
                id = Convert.ToInt32(this.txt_c_orders_id.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Ordrenr. er ikke gyldigt");
                return;
            }

            var deleteOk = MessageBox.Show("Er du sikker på at du vil slette ordre nr. " + id.ToString() + "?", "Fjern order", MessageBoxButton.YesNo);
            if (deleteOk != MessageBoxResult.Yes)
            {
                return;
            }

            Mouse.OverrideCursor = Cursors.Wait;

            int[] inUse;
            bool ok = this.controller.CompanyOrdersRemove(id, out inUse);

            if (inUse != null)
            {
                string message = string.Empty;

                message += "Kan ikke slette ordren da den bruges i følgende regning(er): ";

                message += inUse[0].ToString();
                for (int i = 1; i < inUse.Length - 1; i++)
                {
                    message += ", " + inUse[i].ToString();
                }

                if (inUse.Length > 1)
                {
                    message += ", " + inUse[inUse.Length - 1].ToString() + ".";
                }
                else
                {
                    message += ".";
                }

                MessageBox.Show(message);
                Mouse.OverrideCursor = Cursors.Arrow;
                return;
            }

            if (!ok)
            {
                MessageBox.Show("Kan ikke finde en ordre med ID " + id.ToString());
                Mouse.OverrideCursor = Cursors.Arrow;
                return;
            }

            this.dgv_c_orders.ItemsSource = this.controller.GetOrdersCompany().DefaultView;
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void btn_c_orders_clear_Click(object sender, RoutedEventArgs e)
        {
            CompanyClearOrder();
        }

        private void btn_c_orders_add_Click(object sender, RoutedEventArgs e)
        {
            bool ok = true;
            string messege = string.Empty;

            int createById = -1;
            int customerId = -1;
            double hourUse = -1;
            double paidHour = -1;

            try
            {
                string temp = string.Empty;

                foreach (char item in this.cmb_c_orders_customer.Text)
                {
                    int tempInt;

                    if (int.TryParse(item.ToString(), out tempInt))
                    {
                        temp += item;
                    }
                    else if (item == '-')
                    {
                        break;
                    }
                }

                customerId = Convert.ToInt32(temp);
            }
            catch (Exception)
            {
                ok = false;
                messege += "Kunde" + Environment.NewLine;
            }

            try
            {
                string temp = string.Empty;

                foreach (char item in this.cmb_c_orders_worker.Text)
                {
                    int tempInt;

                    if (int.TryParse(item.ToString(), out tempInt))
                    {
                        temp += item;
                    }
                    else if (item == '-')
                    {
                        break;
                    }
                }

                createById = Convert.ToInt32(temp);
            }
            catch (Exception)
            {
                ok = false;
                messege += "Oprettet af" + Environment.NewLine;
            }

            try
            {
                hourUse = Convert.ToDouble(this.txt_c_orders_houruse.Text);
            }
            catch (Exception)
            {
                if (this.txt_c_orders_houruse.Text == string.Empty)
                {
                    hourUse = 0D;
                }
                else
                {
                    ok = false;
                    messege += "Timer brugt" + Environment.NewLine;
                }
            }

            if (hourUse > 24)
            {
                ok = false;
                messege += "Timer brugt der ikke er mere end 24 timer om dagen" + Environment.NewLine;
            }

            try
            {
                paidHour = Convert.ToDouble(this.txt_c_orders_paidhour.Text);
            }
            catch (Exception)
            {
                if (this.txt_c_orders_paidhour.Text == string.Empty)
                {
                    paidHour = 0D;
                }
                else
                {
                    ok = false;
                    messege += "Timeløn" + Environment.NewLine;
                }
            }

            if (this.txt_c_orders_description.Text == string.Empty)
            {
                ok = false;
                messege += "Opgavebeskrivelse" + Environment.NewLine;
            }

            if (this.dat_c_orders_startdate.SelectedDate == null)
            {
                ok = false;
                messege += "Vælg en start dato" + Environment.NewLine;
            }

            if (!ok)
            {
                MessageBox.Show("Kan ikke tilføje/opdatere den nye ordre:" + Environment.NewLine + messege);
                return;
            }

            bool selectNew = false;

            Mouse.OverrideCursor = Cursors.Wait;

            if (this.txt_c_orders_id.Text == string.Empty)
            {
                // Add new order
                this.controller.CompanyOrdersAdd(createById, customerId, this.txt_c_orders_description.Text,
                    hourUse, paidHour, 1, (DateTime)this.dat_c_orders_startdate.SelectedDate);

                selectNew = true;
            }
            else
            {
                // Update order
                int id = -1;
                if (!int.TryParse(this.txt_c_orders_id.Text, out id))
                {
                    MessageBox.Show("Ordrenr. er ikke gyldigt");
                    Mouse.OverrideCursor = Cursors.Arrow;
                    return;
                }

                this.controller.CompanyOrdersUpdate(id, createById, customerId, this.txt_c_orders_description.Text,
                    hourUse, paidHour, 1, (DateTime)this.dat_c_orders_startdate.SelectedDate);
            }

            int selectNow = this.dgv_c_orders.SelectedIndex;

            this.dgv_c_orders.ItemsSource = this.controller.GetOrdersCompany().DefaultView;

            if (selectNew)
                this.dgv_c_orders.SelectedIndex = this.dgv_c_orders.Items.Count - 1;
            else
                this.dgv_c_orders.SelectedIndex = selectNow;

            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void btn_c_orders_search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FindIdOrdersCompany(Convert.ToInt32(this.txt_c_orders_id.Text));
            }
            catch (Exception)
            {
                return;
            }
        }

        private void txt_c_orders_id_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    FindIdOrdersCompany(Convert.ToInt32(this.txt_c_orders_id.Text));
                }
                catch (Exception)
                {
                    return;
                }
            }
        }

        private void FindIdOrdersCompany(int id)
        {
            var items = (ItemCollection)this.dgv_c_orders.Items;

            for (int i = 0; i < items.Count; i++)
            {
                var view = (DataRowView)items[i];
                if (view.Row.ItemArray[5].ToString() == id.ToString())
                {
                    this.dgv_c_orders.SelectedIndex = i;
                    break;
                }
            }
        }

        private void CompanyClearOrder()
        {
            this.txt_c_orders_id.Text = string.Empty;
            this.cmb_c_orders_customer.Text = string.Empty;
            this.cmb_c_orders_worker.Text = string.Empty;
            this.txt_c_orders_houruse.Text = string.Empty;
            this.txt_c_orders_paidhour.Text = string.Empty;
            this.txt_c_orders_description.Text = string.Empty;
            this.dat_c_orders_startdate.Text = string.Empty;

            this.dgv_c_orders.SelectedIndex = -1;
        }
        #endregion

        #region Order upcoming
        private void dgv_u_orders_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (!(sender is DataGrid))
                return;

            DataGrid senderItem = null;
            DataRowView selectedItem = null;
            DataRow row = null;

            try
            {
                senderItem = (DataGrid)sender;
                selectedItem = (DataRowView)senderItem.SelectedItem;
                row = (DataRow)selectedItem.Row;
            }
            catch (Exception)
            {
                return;
            }

            this.txt_u_orders_id.Text = "#" + row.ItemArray[0].ToString() + " - " + row.ItemArray[2].ToString();
            this.txt_u_orders_customer.Text = row.ItemArray[1].ToString();
            this.txt_u_orders_worker.Text = row.ItemArray[3].ToString();
            this.dat_u_orders_startdate.SelectedDate = (DateTime)row.ItemArray[4];
            this.txt_u_orders_description.Text = row.ItemArray[5].ToString();
        }

        private void btn_u_orders_clear_Click(object sender, RoutedEventArgs e)
        {
            OrderClearUpcoming();
        }

        private void txt_u_orders_id_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            OrderClearUpcoming();
        }

        private void txt_u_orders_id_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                FindOrderUpcoming();
            }
        }

        private void btn_u_orders_search_Click(object sender, RoutedEventArgs e)
        {
            FindOrderUpcoming();
        }

        private void FindOrderUpcoming()
        {
            string temp = string.Empty;

            foreach (char item in this.txt_u_orders_id.Text)
            {
                int dispose;

                if (int.TryParse(item.ToString(), out dispose))
                {
                    temp += item;
                }
                else if (item == '-')
                {
                    break;
                }
            }

            var items = (ItemCollection)this.dgv_u_orders.Items;

            for (int i = 0; i < items.Count; i++)
            {
                var view = (DataRowView)items[i];
                if (view.Row.ItemArray[0].ToString() == temp)
                {
                    this.dgv_u_orders.SelectedIndex = i;
                    break;
                }
            }
        }

        private void OrderClearUpcoming()
        {
            this.txt_u_orders_id.Text = string.Empty;
            this.txt_u_orders_customer.Text = string.Empty;
            this.txt_u_orders_worker.Text = string.Empty;
            this.dat_u_orders_startdate.Text = string.Empty;
            this.txt_u_orders_description.Text = string.Empty;

            this.dgv_u_orders.SelectedIndex = -1;
        }
        #endregion

        #region Customers privet
        private void dgv_p_customers_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (!(sender is DataGrid))
                return;

            DataGrid senderItem = null;
            DataRowView selectedItem = null;
            DataRow row = null;

            try
            {
                senderItem = (DataGrid)sender;
                selectedItem = (DataRowView)senderItem.SelectedItem;
                row = (DataRow)selectedItem.Row;
            }
            catch (Exception)
            {
                return;
            }

            this.txt_p_customers_id.Text = row.ItemArray[7].ToString();
            this.txt_p_customers_name.Text = row.ItemArray[3].ToString();
            this.txt_p_customers_surname.Text = row.ItemArray[8].ToString();
            this.txt_p_customers_address.Text = row.ItemArray[2].ToString();
            this.txt_p_customers_postno.Text = row.ItemArray[5].ToString() + @" / " + row.ItemArray[6].ToString();
            this.txt_p_customers_phoneno.Text = row.ItemArray[4].ToString();
            this.txt_p_customers_altphoneno.Text = row.ItemArray[0].ToString();
            this.txt_p_customers_email.Text = row.ItemArray[1].ToString();
        }

        private void btn_p_customers_search_Click(object sender, RoutedEventArgs e)
        {
            FindCustomerPrivait();
        }

        private void txt_p_customers_id_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                FindCustomerPrivait();
            }
        }

        private void FindCustomerPrivait()
        {
            for (int i = 0; i < this.dgv_p_customers.Items.Count; i++)
            {
                var row = (DataRowView)this.dgv_p_customers.Items[i];

                if (row.Row.ItemArray[7].ToString() == this.txt_p_customers_id.Text)
                {
                    this.dgv_p_customers.SelectedIndex = i;
                    break;
                }
            }
        }

        private void txt_p_customers_id_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            ClearPrivetCustomer();
        }

        private void btn_p_customers_clear_Click(object sender, RoutedEventArgs e)
        {
            ClearPrivetCustomer();
        }

        private void txt_p_customers_postno_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            AotoCompledPostNumber();
        }

        private void btn_p_customers_add_Click(object sender, RoutedEventArgs e)
        {
            string message = string.Empty;
            bool ok = true;

            if (this.txt_p_customers_name.Text == string.Empty)
            {
                message += "Kunde skal have et fornavn" + Environment.NewLine; 
                ok = false;
            }

            if (this.txt_p_customers_surname.Text == string.Empty)
            {
                message += "Kunde skal have efternavn" + Environment.NewLine;
                ok = false;
            }

            if (this.txt_p_customers_address.Text == string.Empty)
            {
                message += "Kunde skal have en adresse" + Environment.NewLine;
                ok = false;
            }

            if (this.txt_p_customers_postno.Text == string.Empty)
            {
                message += @"Kunde skal have postnummer/by" + Environment.NewLine;
                ok = false;
            }

            string postNumber = string.Empty;

            foreach (char item in this.txt_p_customers_postno.Text)
            {
                int tempInt;
                if (int.TryParse(item.ToString(), out tempInt))
                {
                    postNumber += item;
                }
                else if (item == '/' || item == ' ')
                {
                    break;
                }
            }
            try
            {
                if (!this.controller.TestPostNo(Convert.ToInt32(postNumber)))
                {
                    throw new ArgumentException("there is no zip for the post number");
                }
            }
            catch (Exception)
            {
                message += "Der er ingen byer med postnr.: " + this.txt_p_customers_postno.Text + Environment.NewLine;
                ok = false;
            }


            if (this.txt_p_customers_phoneno.Text == string.Empty)
            {
                message += "Kunde skal have et tlfnr" + Environment.NewLine;
                ok = false;
            }

            if (!ok)
            {
                MessageBox.Show("Kundes data bliver ikke gemt, da: " + message);
                return;
            }

            bool selectNew = false;

            Mouse.OverrideCursor = Cursors.Wait;

            if (this.txt_p_customers_id.Text == string.Empty)
            {
                // add new
                if(!this.controller.PrivateCustomerAdd(this.txt_p_customers_name.Text, this.txt_p_customers_surname.Text,
                    this.txt_p_customers_address.Text, Convert.ToInt32(postNumber), this.txt_p_customers_phoneno.Text, this.txt_p_customers_altphoneno.Text,
                    this.txt_p_customers_email.Text))
                {
                    MessageBox.Show("Kunden blev ikke gemt til database");
                    Mouse.OverrideCursor = Cursors.Arrow;
                    return;
                }
                else
                {
                    selectNew = true;
                }
            }
            else
            {
                int temp;
                if (!int.TryParse(this.txt_p_customers_id.Text, out temp))
                {
                    MessageBox.Show("Kunde ID er ikke gyldigt: " + this.txt_p_customers_id.Text);
                    Mouse.OverrideCursor = Cursors.Arrow;
                    return;
                }

                if (!this.controller.PrivateCustomerUpdate(Convert.ToInt32(this.txt_p_customers_id.Text), this.txt_p_customers_name.Text, this.txt_p_customers_surname.Text,
                    this.txt_p_customers_address.Text, Convert.ToInt32(postNumber), this.txt_p_customers_phoneno.Text, this.txt_p_customers_altphoneno.Text,
                    this.txt_p_customers_email.Text))
                {
                    MessageBox.Show("Kunden blev ikke gemt til database");
                    Mouse.OverrideCursor = Cursors.Arrow;
                    return;
                }
            }

            this.dgv_p_customers.ItemsSource = this.controller.GetCustomersPrivet().AsDataView();

            if (selectNew)
                this.dgv_p_customers.SelectedIndex = this.dgv_p_customers.Items.Count - 1;

            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void btn_p_customers_remove_Click(object sender, RoutedEventArgs e)
        {
            if (this.dgv_p_customers.SelectedIndex == -1)
                return;

            var result = MessageBox.Show("Vil du slette kunde: #" + this.txt_p_customers_id.Text + " " + this.txt_p_customers_name.Text 
                + " " + this.txt_p_customers_surname.Text + "?", "Fjern", MessageBoxButton.OKCancel);

            if (result != MessageBoxResult.OK)
                return;

            int id;

            Mouse.OverrideCursor = Cursors.Wait;
            
            if (int.TryParse(this.txt_p_customers_id.Text, out id))
            {
                if (this.controller.PrivateCustomersDelete(id))
                {
                    this.dgv_p_customers.SelectedIndex = -1;
                    this.dgv_p_customers.ItemsSource = this.controller.GetCustomersPrivet().AsDataView();
                    ClearPrivetCustomer();
                }
            }

            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void ClearPrivetCustomer()
        {
            this.txt_p_customers_id.Text = "";
            this.txt_p_customers_name.Text = "";
            this.txt_p_customers_surname.Text = "";
            this.txt_p_customers_address.Text = "";
            this.txt_p_customers_postno.Text = "";
            this.txt_p_customers_phoneno.Text = "";
            this.txt_p_customers_altphoneno.Text = "";
            this.txt_p_customers_email.Text = "";

            this.dgv_p_customers.SelectedIndex = -1;
        }

        private void AotoCompledPostNumber()
        {
            string temp = string.Empty;
            int ignore;
            foreach (char item in this.txt_p_customers_postno.Text)
            {
                if (int.TryParse(item.ToString(), out ignore))
                {
                    temp += item;
                }
                else if (item == ' ' || item == '/')
	            {
                    break;
	            }
            }

            if (temp == string.Empty)
                return;

            this.txt_p_customers_postno.Text = this.controller.PostGetInfo(Convert.ToInt32(temp));
        }
        #endregion

        #region Customers company
        private void btn_c_customers_add_Click(object sender, RoutedEventArgs e)
        {
            string messages = string.Empty;
            bool ok = true;

            int cvr = -1;
            if (!int.TryParse(this.txt_c_customers_cvr.Text, out cvr))
            {
                messages += "CVR skal indtastes" + Environment.NewLine;
                ok = false;
            }

            if (this.txt_c_customers_name.Text == string.Empty)
            {
                messages += "Firmanavn skal indtastes" + Environment.NewLine;
                ok = false;
            }

            if (this.txt_c_customers_address.Text == string.Empty)
            {
                messages += "Adressen skal indtastes" + Environment.NewLine;
                ok = false;
            }

            string postNumber = string.Empty;
            foreach (char item in this.txt_c_customers_postno.Text)
	        {
                int temp = -1;
                if (int.TryParse(item.ToString(), out temp))
                {
                    postNumber += item;
                }
                else if (item == ' ' || item == '/')
                {
                    break;
                }
	        }

            try
            {
                if (!this.controller.TestPostNo(Convert.ToInt32(postNumber)))
                {
                    ok = false;
                    messages += "Postnummer er ikke gyldigt" + Environment.NewLine;
                }
            }
            catch (Exception)
            {
                messages = "Indtast et postnummer" + Environment.NewLine;
                ok = false;
            }

            if (this.txt_c_customers_phoneno.Text == string.Empty)
            {
                ok = false;
                messages += "Indtast et telefonnummer" + Environment.NewLine;
            }

            if (!ok)
            {
                MessageBox.Show("Data blev ikke gemt da: " + messages);
                return;
            }

            bool selectNew = false;
            int selectedIndex = -1;

            if (this.txt_c_customers_id.Text == string.Empty)
            {
                // add new
                if(!this.controller.CompanyCustomerAdd(cvr, this.txt_c_customers_name.Text, this.txt_c_customers_address.Text,
                        Convert.ToInt32(postNumber), this.txt_c_customers_contactperson.Text, this.txt_c_customers_phoneno.Text,
                        this.txt_c_customers_altphoneno.Text, this.txt_c_customers_email.Text))
                    MessageBox.Show("Data blev ikke gemt til databasen");

                selectNew = true;

            }
            else
            {
                // update
                if (this.dgv_c_customers.SelectedIndex == -1)
                    return;
                

                if(!this.controller.CompanyCustomerUpdate(Convert.ToInt32(this.txt_c_customers_id.Text), cvr, this.txt_c_customers_name.Text, this.txt_c_customers_address.Text,
                        Convert.ToInt32(postNumber), this.txt_c_customers_contactperson.Text, this.txt_c_customers_phoneno.Text,
                        this.txt_c_customers_altphoneno.Text, this.txt_c_customers_email.Text))
                    MessageBox.Show("Data blev ikke gemt til databasen");

                selectedIndex = this.dgv_c_customers.SelectedIndex;
            }


            this.dgv_c_customers.ItemsSource = this.controller.GetCustomersCompany().AsDataView();

            if (selectNew)
                this.dgv_c_customers.SelectedIndex = this.dgv_c_customers.Items.Count - 1;
            else
                this.dgv_c_customers.SelectedIndex = selectedIndex;
        }

        private void txt_c_customers_postno_LostFocus(object sender, RoutedEventArgs e)
        {
            string postNumber = string.Empty;
            foreach (char item in this.txt_c_customers_postno.Text)
            {
                int temp;

                if (int.TryParse(item.ToString(), out temp))
                {
                    postNumber += item;
                }
                else if(item == ' ' || item == '/')
                {
                    break;
                }
            }

            if (postNumber != string.Empty)
            {
                this.txt_c_customers_postno.Text = this.controller.PostGetInfo(Convert.ToInt32(postNumber));
            }
        }

        private void btn_c_customers_clear_Click(object sender, RoutedEventArgs e)
        {
            ClearCompanyCustomer();
        }

        private void btn_c_customers_search_Click(object sender, RoutedEventArgs e)
        {
            FindCustomerCompany();
        }

        private void btn_c_customers_remove_Click(object sender, RoutedEventArgs e)
        {
            if (this.dgv_c_customers.SelectedIndex == -1)
                return;

            int id;

            if (int.TryParse(this.txt_c_customers_id.Text, out id))
            {
                var result = MessageBox.Show("Vil du slette kunde: #" + id.ToString() + " - " + this.txt_c_customers_name.Text + "?", "Fjern",
                    MessageBoxButton.YesNo);

                bool ok = false;

                if (result == MessageBoxResult.Yes)
                    ok = this.controller.CompanyCustomerRemove(id);
                else
                    return;

                if (!ok)
                {
                    MessageBox.Show("Kunne ikke slette kunden");
                    return;
                }
            }

            this.dgv_c_customers.ItemsSource = this.controller.GetCustomersCompany().AsDataView();

            this.dgv_c_customers.SelectedIndex = -1;
            ClearCompanyCustomer();
        }

        private void txt_c_customers_id_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            ClearCompanyCustomer();
        }

        private void txt_c_customers_id_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                FindCustomerCompany();
            }
        }

        private void dgv_c_customers_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (!(sender is DataGrid))
                return;

            DataGrid senderItem = null;
            DataRowView selectedItem = null;
            DataRow row = null;

            try
            {
                senderItem = (DataGrid)sender;
                selectedItem = (DataRowView)senderItem.SelectedItem;
                row = (DataRow)selectedItem.Row;
            }
            catch (Exception)
            {
                return;
            }

            this.txt_c_customers_id.Text = row.ItemArray[2].ToString();
            this.txt_c_customers_name.Text = row.ItemArray[6].ToString();
            this.txt_c_customers_cvr.Text = row.ItemArray[4].ToString();
            this.txt_c_customers_address.Text = row.ItemArray[0].ToString();
            this.txt_c_customers_postno.Text = row.ItemArray[8].ToString() + @" / " + row.ItemArray[9].ToString();
            this.txt_c_customers_phoneno.Text = row.ItemArray[7].ToString();
            this.txt_c_customers_altphoneno.Text = row.ItemArray[1].ToString();
            this.txt_c_customers_email.Text = row.ItemArray[5].ToString();
            this.txt_c_customers_contactperson.Text = row.ItemArray[3].ToString();
        }

        private void ClearCompanyCustomer()
        {
            this.txt_c_customers_id.Text = string.Empty;
            this.txt_c_customers_cvr.Text = string.Empty;
            this.txt_c_customers_name.Text = string.Empty;
            this.txt_c_customers_address.Text = string.Empty;
            this.txt_c_customers_postno.Text = string.Empty;
            this.txt_c_customers_contactperson.Text = string.Empty;
            this.txt_c_customers_phoneno.Text = string.Empty;
            this.txt_c_customers_altphoneno.Text = string.Empty;
            this.txt_c_customers_email.Text = string.Empty;
        }

        private void FindCustomerCompany()
        {
            int id = -1;

            if (!int.TryParse(this.txt_c_customers_id.Text, out id))
                return;

            for (int i = 0; i < this.dgv_c_customers.Items.Count; i++)
            {
                var row = (DataRowView)this.dgv_c_customers.Items[i];

                if (row.Row.ItemArray[2].ToString() == this.txt_c_customers_id.Text)
                {
                    this.dgv_c_customers.SelectedIndex = i;
                    break;
                }
            }
        }
        #endregion

        #region Workers
        private void dgv_workers_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (!(sender is DataGrid))
                return;

            DataGrid senderItem = null;
            DataRowView selectedItem = null;
            DataRow row = null;

            try
            {
                senderItem = (DataGrid)sender;
                selectedItem = (DataRowView)senderItem.SelectedItem;
                row = (DataRow)selectedItem.Row;
            }
            catch (Exception)
            {
                return;
            }

            this.txt_workers_id.Text = row.ItemArray[8].ToString();
            this.txt_workers_name.Text = row.ItemArray[3].ToString();
            this.txt_workers_surname.Text = row.ItemArray[9].ToString();
            this.txt_workers_address.Text = row.ItemArray[0].ToString();
            this.txt_workers_postno.Text = this.controller.PostGetInfo((int)row.ItemArray[5]);
            this.txt_workers_phoneno.Text = row.ItemArray[4].ToString();
            this.txt_workers_altphoneno.Text = row.ItemArray[1].ToString();
            this.txt_workers_email.Text = row.ItemArray[2].ToString();
        }
        private void btn_workers_clear_Click(object sender, RoutedEventArgs e)
        {
            ClearWorker();
        }

        private void txt_workers_id_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            ClearWorker();
        }

        private void txt_workers_postno_LostFocus(object sender, RoutedEventArgs e)
        {
            string postNumber = string.Empty;

            foreach (char item in this.txt_workers_postno.Text)
            {
                int temp = -1;

                if (int.TryParse(item.ToString(), out temp))
                {
                    postNumber += item;
                }
                else if (item == ' ' || item == '/')
                {
                    break;
                }
            }

            if (postNumber == string.Empty)
                return;

            this.txt_workers_postno.Text = this.controller.PostGetInfo(Convert.ToInt32(postNumber));
        }

        private void txt_workers_id_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                FindWorker();
            }
        }

        private void btn_workers_search_Click(object sender, RoutedEventArgs e)
        {
            FindWorker();
        }

        private void btn_workers_remove_Click(object sender, RoutedEventArgs e)
        {
            if (this.dgv_workers.SelectedIndex == -1)
                return;

            var result = MessageBox.Show("Vil du slette ansat nr: #" + this.txt_workers_id.Text + " - " + this.txt_workers_name.Text
                + " " + this.txt_workers_surname.Text + "?", "Fjern", MessageBoxButton.YesNo);

            if (result != MessageBoxResult.Yes)
                return;

            Mouse.OverrideCursor = Cursors.Wait;

            if (!this.controller.WorkersRemove(Convert.ToInt32(this.txt_workers_id.Text)))
                MessageBox.Show("Den ansatte blev ikke fjernet fra database");

            this.dgv_workers.ItemsSource = this.controller.GetWorkers().AsDataView();
   
            ClearWorker();

            Mouse.OverrideCursor = Cursors.Arrow;
        }
        
        private void btn_workers_add_Click(object sender, RoutedEventArgs e)
        {
            bool ok = true;
            string messges = string.Empty;

            if (this.txt_workers_name.Text == string.Empty)
            {
                ok = false;
                messges += "Indtast et fornavn" + Environment.NewLine;
            }

            if (this.txt_workers_surname.Text == string.Empty)
            {
                ok = false;
                messges += "Indtast et efternavn" + Environment.NewLine;
            }

            if (this.txt_workers_address.Text == string.Empty)
            {
                ok = false;
                messges += "Indtast en adresse" + Environment.NewLine;
            }

            string postNo = string.Empty;
            if (this.txt_workers_postno.Text == string.Empty)
            {
                ok = false;
                messges += "Indtast et postnummer";
            }
            else
	        {
                foreach (char item in this.txt_workers_postno.Text)
                {
                    int temp = -1;
                    if (int.TryParse(item.ToString(), out temp))
                    {
                        postNo += item;
                    }
                    else if(item == ' ' || item == '/')
                    {
                        break;
                    }
                }

                try
                {
                    if (!this.controller.TestPostNo(Convert.ToInt32(postNo)))
                    {
                        ok = false;
                        messges += "Postnummer er ikke gyldigt" + Environment.NewLine;
                    }
                }
                catch (Exception)
                {
                    ok = false;
                    messges += "Postnummer er ikke gyldigt" + Environment.NewLine;
                }
	        }

            if (this.txt_workers_phoneno.Text == string.Empty)
            {
                ok = false;
                messges += "Indtast et telefonnummer";
            }

            if (!ok)
            {
                MessageBox.Show("Den ansatte blev ikke gemt da: " + messges);
                return;
            }

            bool selectedNew = false;

            Mouse.OverrideCursor = Cursors.Wait;

            if (this.txt_workers_id.Text == string.Empty)
            {
                // add new
                if(!this.controller.WorkerAdd(this.txt_workers_name.Text, this.txt_workers_surname.Text, this.txt_workers_address.Text,
                    Convert.ToInt32(postNo), this.txt_workers_phoneno.Text, this.txt_workers_altphoneno.Text, this.txt_workers_email.Text))
                    MessageBox.Show("Den ansatte blev ikke gemt");

                selectedNew = true;
            }
            else
            {
                // update
                if (this.dgv_workers.SelectedIndex == -1)
                {
                    Mouse.OverrideCursor = Cursors.Arrow;
                    return;
                }

                if (!this.controller.WorkerUpdate(Convert.ToInt32(this.txt_workers_id.Text) ,this.txt_workers_name.Text, this.txt_workers_surname.Text, this.txt_workers_address.Text,
                        Convert.ToInt32(postNo), this.txt_workers_phoneno.Text, this.txt_workers_altphoneno.Text, this.txt_workers_email.Text))
                    MessageBox.Show("Den ansatte blev ikke gemt");
            }

            int index = this.dgv_workers.SelectedIndex;

            this.dgv_workers.ItemsSource = this.controller.GetWorkers().AsDataView();

            if (selectedNew == true)
            {
                this.dgv_workers.SelectedIndex = this.dgv_workers.Items.Count - 1;
            }
            else
            {
                this.dgv_workers.SelectedIndex = index;
            }

            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void FindWorker()
        {
            int id = -1;

            if (!int.TryParse(this.txt_workers_id.Text, out id))
                return;

            var items = (ItemCollection)this.dgv_workers.Items;

            for (int i = 0; i < items.Count; i++)
            {
                var view = (DataRowView)items[i];
                if (view.Row.ItemArray[8].ToString() == id.ToString())
                {
                    this.dgv_workers.SelectedIndex = i;
                    break;
                }
            }
        }

        private void ClearWorker()
        {
            this.txt_workers_id.Text = string.Empty;
            this.txt_workers_name.Text = string.Empty;
            this.txt_workers_surname.Text = string.Empty;
            this.txt_workers_address.Text = string.Empty;
            this.txt_workers_postno.Text = string.Empty;
            this.txt_workers_phoneno.Text = string.Empty;
            this.txt_workers_altphoneno.Text = string.Empty;
            this.txt_workers_email.Text = string.Empty;

            this.dgv_workers.SelectedIndex = -1;
        }
        #endregion

        #region Departments
        private void dgv_departments_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (!(sender is DataGrid))
                return;

            DataGrid senderItem = null;
            DataRowView selectedItem = null;
            DataRow row = null;

            try
            {
                senderItem = (DataGrid)sender;
                selectedItem = (DataRowView)senderItem.SelectedItem;
                row = (DataRow)selectedItem.Row;
            }
            catch (Exception)
            {
                return;
            }

            this.txt_departments_number.Text = row.ItemArray[0].ToString();
            this.txt_departments_name.Text = row.ItemArray[1].ToString();
            this.txt_departments_cvr.Text = row.ItemArray[2].ToString();
            this.cmb_departments_head.Text = row.ItemArray[3].ToString();
            this.txt_departments_address.Text = row.ItemArray[4].ToString();
            this.txt_departments_postno.Text = row.ItemArray[5].ToString() + @" / " + row.ItemArray[6].ToString();
            this.txt_departments_phoneno.Text = row.ItemArray[7].ToString();
            this.txt_departments_altphoneno.Text = row.ItemArray[8].ToString();
            this.txt_departments_email.Text = row.ItemArray[9].ToString();
        }

        private void txt_departments_postno_LostFocus(object sender, RoutedEventArgs e)
        {
            string postNumber = string.Empty;

            foreach (char item in this.txt_departments_postno.Text)
            {
                int temp = -1;

                if (int.TryParse(item.ToString(), out temp))
                {
                    postNumber += item;
                }
                else if (item == ' ' || item == '/')
                {
                    break;
                }
            }

            if (postNumber == string.Empty)
                return;

            this.txt_departments_postno.Text = this.controller.PostGetInfo(Convert.ToInt32(postNumber));
        }

        private void txt_departments_number_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            ClaerDepartment();
        }

        private void btn_departments_clear_Click(object sender, RoutedEventArgs e)
        {
            ClaerDepartment();
        }

        private void btn_departments_remove_Click(object sender, RoutedEventArgs e)
        {
            if (this.dgv_departments.SelectedIndex == -1)
                return;

            var result = MessageBox.Show("Vil du slette afdeling nr.: #" + this.txt_departments_number.Text, "Fjern", MessageBoxButton.YesNo);

            if (result != MessageBoxResult.Yes)
                return;

            Mouse.OverrideCursor = Cursors.Wait;

            if (!this.controller.DepartmentsRemove(Convert.ToInt32(this.txt_departments_number.Text)))
                MessageBox.Show("Afdelingen blev ikke fjernet");

            ClaerDepartment();

            this.dgv_departments.ItemsSource = this.controller.GetDepartments().AsDataView();

            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void btn_departments_add_Click(object sender, RoutedEventArgs e)
        {
            bool ok = true;
            string message = string.Empty;

            if (this.txt_departments_name.Text == string.Empty)
            {
                ok = false;
                message += "Indtast et navn" + Environment.NewLine;
            }

            int cvrNo = -1;
            if (this.txt_departments_cvr.Text == string.Empty)
            {
                ok = false;
                message += "Indtast CVR-nr." + Environment.NewLine;
            }
            else
            {
                try
                {
                    cvrNo = Convert.ToInt32(this.txt_departments_cvr.Text);
                }
                catch (Exception)
                {
                    ok = false;
                    message += "Postnummer er ikke gyldigt";
                }
            }

            int departmentHead = -1;
            if (this.cmb_departments_head.Text == string.Empty)
            {
                ok = false;
                message += "Vælg en afdelingsleder" + Environment.NewLine;
            }   
            else
            {
                string temp = string.Empty;
                foreach (char item in this.cmb_departments_head.Text)
	            {
                    int tempInt;
                    if (int.TryParse(item.ToString(), out tempInt))
                    {
                        temp += item;
                    }
                    else if (item == ' ' || item == '-')
                    {
                        break;
                    }
	            }

                try
                {
                    departmentHead = Convert.ToInt32(temp);
                }
                catch (Exception)
                {
                    ok = false;
                    message += "Vælg en gyldig afdelingsleder";
                }
            }

            if (this.txt_departments_address.Text == string.Empty)
            {
                ok = false;
                message += "Indtast en adresse" + Environment.NewLine;
            }

            int postNumber = -1;

            if (this.txt_departments_postno.Text == string.Empty)
            {
                ok = false;
                message += "Vælg et postnummer" + Environment.NewLine;
            }
            else
            {
                string tempPostNumber = string.Empty;

                foreach (char item in this.txt_departments_postno.Text)
                {
                    int temp;

                    if (int.TryParse(item.ToString(), out temp))
                    {
                        tempPostNumber += item;
                    }
                    else if (item == ' ' || item == '/')
                    {
                        break;
                    }
                }

                try
                {
                    postNumber = Convert.ToInt32(tempPostNumber);
                }
                catch (Exception)
                {
                    ok = false;
                    message += "Postnummer er ikke gyldigt" + Environment.NewLine;
                }
            }

            if (this.txt_departments_phoneno.Text == string.Empty)
            {
                ok = false;
                message += "Indtast et tlf.nr.";
            }

            if (!ok)
            {
                MessageBox.Show("Kan ikke gemme info: " + message);
                return;
            }

            bool selectNew = false;
            int index = this.dgv_departments.SelectedIndex;

            Mouse.OverrideCursor = Cursors.Wait;

            if (this.txt_departments_number.Text == string.Empty)
            {
                // add new
                if (!this.controller.DepartmentsAdd(this.txt_departments_address.Text, this.txt_departments_altphoneno.Text,
                    this.txt_departments_name.Text, cvrNo, departmentHead, this.txt_departments_email.Text, this.txt_departments_phoneno.Text,
                    postNumber))
                    MessageBox.Show("Kan ikke tilføje den nye afdeling");

                selectNew = true;
            }
            else
            {
                // update
                if (this.dgv_departments.SelectedIndex == -1)
                {
                    Mouse.OverrideCursor = Cursors.Arrow;
                    return;
                }

                if (!this.controller.DepartmentsUpdate(Convert.ToInt32(this.txt_departments_number.Text), this.txt_departments_address.Text, this.txt_departments_altphoneno.Text,
                    this.txt_departments_name.Text, cvrNo, departmentHead, this.txt_departments_email.Text, this.txt_departments_phoneno.Text,
                    postNumber))
                    MessageBox.Show("Kunne ikke opdatere afdeling");
            }

            this.dgv_departments.ItemsSource = this.controller.GetDepartments().AsDataView();

            if (selectNew)
            {
                this.dgv_departments.SelectedIndex = this.dgv_departments.Items.Count - 1;
            }
            else
            {
                this.dgv_departments.SelectedIndex = index;
            }

            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void ClaerDepartment()
        {
            this.txt_departments_number.Text = "";
            this.txt_departments_name.Text = "";
            this.txt_departments_cvr.Text = "";
            this.cmb_departments_head.Text = "";
            this.txt_departments_address.Text = "";
            this.txt_departments_postno.Text = "";
            this.txt_departments_phoneno.Text = "";
            this.txt_departments_altphoneno.Text = "";
            this.txt_departments_email.Text = "";

            this.dgv_departments.SelectedIndex = -1;
        }
        #endregion

        #region Invoices Private
        private void cbm_p_invoices_customers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.cbm_p_invoices_customers.SelectedIndex == -1)
                return;

            Mouse.OverrideCursor = Cursors.Wait;
            string temp = string.Empty;

            foreach (char item in this.cbm_p_invoices_customers.SelectedItem.ToString())
            {
                int ignore;
                if (int.TryParse(item.ToString(), out ignore))
                {
                    temp += item;
                }
                else if (item == ' ' || item == '-')
                {
                    break;
                } 
            }

            try
            {
                this.dgv_p_invoices.ItemsSource = this.controller.GetCustomersPrivetForInvoices(Convert.ToInt32(temp)).AsDataView();
            }
            catch (Exception)
            {
            }

            this.dgv_p_invoices_orders.ItemsSource = null;

            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private DataTable DataTabelForInvoices()
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("Ordrenr", typeof(int));
            dataTable.Columns.Add("Opgave", typeof(string));
            dataTable.Columns.Add("Dato", typeof(DateTime));
            dataTable.Columns.Add("Timer brugt", typeof(double));
            dataTable.Columns.Add("Timeløn", typeof(double));

            return dataTable;
        }

        private void dgv_p_invoices_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (!(sender is DataGrid))
                return;

            if (dgv_p_invoices_orders.Items.Count > 20)
            {
                MessageBox.Show("Der må kun være 20 ordre pr. regning");
                return;
            }

            var dataGrid = (DataGrid)sender;

            if (dataGrid.SelectedItem != null)
            {
                DataTable dataTableInvoices = DataTabelForInvoices();
                DataTable dataTableOrders = DataTabelForInvoices();

                foreach (DataRowView item in this.dgv_p_invoices_orders.Items)
                {
                    dataTableInvoices.Rows.Add(((DataRow)item.Row).ItemArray);
                }

                var moving = (DataRow)((DataRowView)dataGrid.SelectedItem).Row;

                dataTableInvoices.Rows.Add(moving.ItemArray);

                dgv_p_invoices_orders.ItemsSource = dataTableInvoices.AsDataView();

                foreach (DataRowView item in this.dgv_p_invoices.Items)
                {
                    if (item != dataGrid.SelectedItem)
                    {
                        dataTableOrders.Rows.Add(((DataRow)item.Row).ItemArray);
                    }
                }

                this.dgv_p_invoices.ItemsSource = dataTableOrders.AsDataView();
            }
        }

        private void dgv_p_invoices_orders_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (!(sender is DataGrid))
                return;

            var dataGrid = (DataGrid)sender;

            if (dataGrid.SelectedIndex == -1)
                return;

            DataTable dataTableInvoices = DataTabelForInvoices();
            DataTable dataTableOrders = DataTabelForInvoices();

            foreach (DataRowView item in this.dgv_p_invoices.Items)
            {
                dataTableOrders.Rows.Add(((DataRow)item.Row).ItemArray);
            }

            var moving = (DataRow)((DataRowView)dataGrid.SelectedItem).Row;

            dataTableOrders.Rows.Add(moving.ItemArray);

            this.dgv_p_invoices.ItemsSource = dataTableOrders.AsDataView();

            foreach (DataRowView item in this.dgv_p_invoices_orders.Items)
            {
                if (item != dataGrid.SelectedItem)
                {
                    dataTableInvoices.Rows.Add(((DataRow)item.Row).ItemArray);
                }
            }

            this.dgv_p_invoices_orders.ItemsSource = dataTableInvoices.AsDataView();
        }

        private void ContextMenu_dgv_p_invoices_Click(object sender, RoutedEventArgs e)
        {
            if (this.dgv_p_invoices.SelectedItems.Count < 1)
                return;

            if (this.dgv_p_invoices.SelectedItems.Count + this.dgv_p_invoices_orders.Items.Count > 20)
            {
                MessageBox.Show("Der er kun plads til 20 ordrer på en regning");
                return;
            }

            var oderes = DataTabelForInvoices();
            var oderesSelected = DataTabelForInvoices();

            var moving = new List<DataRow>();
            var listDeleteingIndex = new List<int>();


            foreach (DataRowView item in this.dgv_p_invoices.SelectedItems)
            {
                moving.Add((DataRow)item.Row);
                listDeleteingIndex.Add((int)((DataRow)item.Row).ItemArray[0]);
            }

            foreach (DataRowView item in this.dgv_p_invoices.Items)
            {
                int id = (int)((DataRow)item.Row).ItemArray[0];
                
                if (!listDeleteingIndex.Contains(id))
                {
                    oderes.Rows.Add(((DataRow)item.Row).ItemArray);
                }
            }

            foreach (DataRowView item in dgv_p_invoices_orders.Items)
            {
                oderesSelected.Rows.Add(((DataRow)item.Row).ItemArray);
            }

            foreach (var item in moving)
            {
                oderesSelected.Rows.Add(item.ItemArray);
            }

            dgv_p_invoices_orders.ItemsSource = oderesSelected.AsDataView();
            dgv_p_invoices.ItemsSource = oderes.AsDataView();
        }

        private void menu_dgv_p_invoices_orders_Click(object sender, RoutedEventArgs e)
        {
            if (this.dgv_p_invoices_orders.SelectedItems.Count < 1)
                return;

            var oderes = DataTabelForInvoices();
            var oderesSelected = DataTabelForInvoices();

            var moving = new List<DataRow>();
            var listDeleteingIndex = new List<int>();

            foreach (DataRowView item in this.dgv_p_invoices_orders.SelectedItems)
            {
                moving.Add((DataRow)item.Row);
                listDeleteingIndex.Add((int)((DataRow)item.Row).ItemArray[0]);
            }

            foreach (DataRowView item in this.dgv_p_invoices_orders.Items)
            {
                int id = (int)((DataRow)item.Row).ItemArray[0];

                if (!listDeleteingIndex.Contains(id))
                {
                    oderesSelected.Rows.Add(((DataRow)item.Row).ItemArray);
                }
            }

            foreach (DataRowView item in dgv_p_invoices.Items)
            {
                oderes.Rows.Add(((DataRow)item.Row).ItemArray);
            }

            foreach (var item in moving)
            {
                oderes.Rows.Add(item.ItemArray);
            }

            dgv_p_invoices_orders.ItemsSource = oderesSelected.AsDataView();
            dgv_p_invoices.ItemsSource = oderes.AsDataView();
        }

        private void btn_p_invoices_add_Click(object sender, RoutedEventArgs e)
        {
            if (this.dgv_p_invoices_orders.Items.Count < 1)
            {
                MessageBox.Show("Vælg ordrer for at lave en regning");
                return;
            }

            Mouse.OverrideCursor = Cursors.Wait;

            var departments = this.controller.ListOfDepartments();
            var acc = this.controller.ListOfBankAcc();

            Mouse.OverrideCursor = Cursors.Arrow;


            var window = new WpfAddInvoice(acc, departments);
            window.ShowDialog();

            if (!window.Ok)
                return;

            Mouse.OverrideCursor = Cursors.Wait;
            var orders = new List<int?>();

            foreach (DataRowView item in this.dgv_p_invoices_orders.Items)
            {
                orders.Add((int)(((DataRow)item.Row).ItemArray[0]));
            }

            for (int i = orders.Count; i < 20; i++)
            {
                orders.Add(null);
            }

            string customersId = string.Empty;

            foreach (char item in cbm_p_invoices_customers.Text)
	        {
                int temp;
		        if(int.TryParse(item.ToString(), out temp))
                {
                    customersId += item;
                }
                else if (item == ' ' || item == '-')
	            {
		            break;
	            }
	        }

            int? newInvoiceId;

            this.controller.CreateInvoicePrivate(orders.ToArray(), Convert.ToInt32(customersId),Convert.ToInt32(window.Bank),
                Convert.ToInt32(window.Department), window.DaysToPaid, out newInvoiceId);

            Mouse.OverrideCursor = Cursors.Arrow;

            if (newInvoiceId == null || newInvoiceId == -1)
            {
                MessageBox.Show("Der opstod en fejl, kunne ikke lave regningen");
            }
            else
            {
                this.dgv_p_invoices_orders.ItemsSource = null;

                if (this.dgv_p_invoices.Items.Count < 1)
                {
                    var dleleteingItem = (string)this.cbm_p_invoices_customers.SelectedItem;
                    this.cbm_p_invoices_customers.SelectedIndex = -1;
                    var list = (List<string>)this.cbm_p_invoices_customers.ItemsSource;
                    list.Remove(dleleteingItem);

                    this.cbm_p_invoices_customers.ItemsSource = null;
                    this.cbm_p_invoices_customers.ItemsSource = list;
                }
            }
        }

        private void dgv_p_invoices_orders_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
                this.dgv_p_invoices.SelectAll();
        }

        private void dgv_p_invoices_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
                this.dgv_p_invoices_orders.SelectAll();
        }

        private void btn_p_invoices_clear_Click(object sender, RoutedEventArgs e)
        {
            this.cbm_p_invoices_customers.SelectedIndex = -1;
            this.dgv_p_invoices.ItemsSource = null;
            this.dgv_p_invoices_orders.ItemsSource = null;
        }
        #endregion

        #region Invoices Company
        private void cbm_c_invoices_customers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.cbm_c_invoices_customers.SelectedIndex == -1)
                return;

            Mouse.OverrideCursor = Cursors.Wait;
            string temp = string.Empty;

            foreach (char item in this.cbm_c_invoices_customers.SelectedItem.ToString())
            {
                int ignore;
                if (int.TryParse(item.ToString(), out ignore))
                {
                    temp += item;
                }
                else if (item == ' ' || item == '-')
                {
                    break;
                }
            }

            try
            {
                this.dgv_c_invoices.ItemsSource = this.controller.GetCompanyCustomerstForInvoices(Convert.ToInt32(temp)).AsDataView();
            }
            catch (Exception)
            {
            }

            this.dgv_c_invoices_orders.ItemsSource = null;

            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void dgv_c_invoices_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (!(sender is DataGrid))
                return;

            if (dgv_p_invoices_orders.Items.Count > 20)
            {
                MessageBox.Show("Der må kun være 20 ordrer pr. regning");
                return;
            }

            var dataGrid = (DataGrid)sender;

            if (dataGrid.SelectedItem != null)
            {
                DataTable dataTableInvoices = DataTabelForInvoices();
                DataTable dataTableOrders = DataTabelForInvoices();

                foreach (DataRowView item in this.dgv_c_invoices_orders.Items)
                {
                    dataTableInvoices.Rows.Add(((DataRow)item.Row).ItemArray);
                }

                var moving = (DataRow)((DataRowView)dataGrid.SelectedItem).Row;

                dataTableInvoices.Rows.Add(moving.ItemArray);

                dgv_c_invoices_orders.ItemsSource = dataTableInvoices.AsDataView();

                foreach (DataRowView item in this.dgv_c_invoices.Items)
                {
                    if (item != dataGrid.SelectedItem)
                    {
                        dataTableOrders.Rows.Add(((DataRow)item.Row).ItemArray);
                    }
                }

                this.dgv_c_invoices.ItemsSource = dataTableOrders.AsDataView();
            }
        }

        private void ContextMenu_dgv_c_invoices_Click(object sender, RoutedEventArgs e)
        {
            if (this.dgv_c_invoices.SelectedItems.Count < 1)
                return;

            if (this.dgv_c_invoices.SelectedItems.Count + this.dgv_c_invoices_orders.Items.Count > 20)
            {
                MessageBox.Show("Der er kun plads til 20 ordrer på en regning");
                return;
            }

            var oderes = DataTabelForInvoices();
            var oderesSelected = DataTabelForInvoices();

            var moving = new List<DataRow>();
            var listDeleteingIndex = new List<int>();


            foreach (DataRowView item in this.dgv_c_invoices.SelectedItems)
            {
                moving.Add((DataRow)item.Row);
                listDeleteingIndex.Add((int)((DataRow)item.Row).ItemArray[0]);
            }

            foreach (DataRowView item in this.dgv_c_invoices.Items)
            {
                int id = (int)((DataRow)item.Row).ItemArray[0];

                if (!listDeleteingIndex.Contains(id))
                {
                    oderes.Rows.Add(((DataRow)item.Row).ItemArray);
                }
            }

            foreach (DataRowView item in dgv_c_invoices_orders.Items)
            {
                oderesSelected.Rows.Add(((DataRow)item.Row).ItemArray);
            }

            foreach (var item in moving)
            {
                oderesSelected.Rows.Add(item.ItemArray);
            }

            dgv_c_invoices_orders.ItemsSource = oderesSelected.AsDataView();
            dgv_c_invoices.ItemsSource = oderes.AsDataView();
        }

        private void dgv_c_invoices_orders_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (!(sender is DataGrid))
                return;

            var dataGrid = (DataGrid)sender;

            if (dataGrid.SelectedIndex == -1)
                return;

            DataTable dataTableInvoices = DataTabelForInvoices();
            DataTable dataTableOrders = DataTabelForInvoices();

            foreach (DataRowView item in this.dgv_c_invoices.Items)
            {
                dataTableOrders.Rows.Add(((DataRow)item.Row).ItemArray);
            }

            var moving = (DataRow)((DataRowView)dataGrid.SelectedItem).Row;

            dataTableOrders.Rows.Add(moving.ItemArray);

            this.dgv_c_invoices.ItemsSource = dataTableOrders.AsDataView();

            foreach (DataRowView item in this.dgv_c_invoices_orders.Items)
            {
                if (item != dataGrid.SelectedItem)
                {
                    dataTableInvoices.Rows.Add(((DataRow)item.Row).ItemArray);
                }
            }

            this.dgv_c_invoices_orders.ItemsSource = dataTableInvoices.AsDataView();
        }

        private void menu_dgv_c_invoices_orders_Click(object sender, RoutedEventArgs e)
        {
            if (this.dgv_c_invoices_orders.SelectedItems.Count < 1)
                return;

            var oderes = DataTabelForInvoices();
            var oderesSelected = DataTabelForInvoices();

            var moving = new List<DataRow>();
            var listDeleteingIndex = new List<int>();

            foreach (DataRowView item in this.dgv_c_invoices_orders.SelectedItems)
            {
                moving.Add((DataRow)item.Row);
                listDeleteingIndex.Add((int)((DataRow)item.Row).ItemArray[0]);
            }

            foreach (DataRowView item in this.dgv_c_invoices_orders.Items)
            {
                int id = (int)((DataRow)item.Row).ItemArray[0];

                if (!listDeleteingIndex.Contains(id))
                {
                    oderesSelected.Rows.Add(((DataRow)item.Row).ItemArray);
                }
            }

            foreach (DataRowView item in dgv_c_invoices.Items)
            {
                oderes.Rows.Add(((DataRow)item.Row).ItemArray);
            }

            foreach (var item in moving)
            {
                oderes.Rows.Add(item.ItemArray);
            }

            dgv_c_invoices_orders.ItemsSource = oderesSelected.AsDataView();
            dgv_c_invoices.ItemsSource = oderes.AsDataView();
        }

        private void dgv_c_invoices_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
                this.dgv_c_invoices.SelectAll();
        }

        private void dgv_c_invoices_orders_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
                this.dgv_c_invoices_orders.SelectAll();
        }

        private void btn_c_invoices_clear_Click(object sender, RoutedEventArgs e)
        {
            this.cbm_c_invoices_customers.SelectedIndex = -1;
            this.dgv_c_invoices.ItemsSource = null;
            this.dgv_c_invoices_orders.ItemsSource = null;
        }

        private void btn_c_invoices_add_Click(object sender, RoutedEventArgs e)
        {
            if (this.dgv_c_invoices_orders.Items.Count < 1)
            {
                MessageBox.Show("Vælg ordrer for at lave en regning");
                return;
            }

            Mouse.OverrideCursor = Cursors.Wait;

            var departments = this.controller.ListOfDepartments();
            var acc = this.controller.ListOfBankAcc();

            Mouse.OverrideCursor = Cursors.Arrow;

            var window = new WpfAddInvoice(acc, departments);
            window.ShowDialog();

            if (!window.Ok)
                return;

            Mouse.OverrideCursor = Cursors.Wait;
            var orders = new List<int?>();
            
            foreach (DataRowView item in this.dgv_c_invoices_orders.Items)
            {
                orders.Add((int)(((DataRow)item.Row).ItemArray[0]));
            }

            for (int i = orders.Count; i < 20; i++)
            {
                orders.Add(null);
            }

            string customersId = string.Empty;

            foreach (char item in cbm_c_invoices_customers.Text)
            {
                int temp;
                if (int.TryParse(item.ToString(), out temp))
                {
                    customersId += item;
                }
                else if (item == ' ' || item == '-')
                {
                    break;
                }
            }

            int? newInvoiceId;

            this.controller.CreateInvoiceCompany(orders.ToArray(), Convert.ToInt32(customersId), Convert.ToInt32(window.Bank),
                Convert.ToInt32(window.Department), window.DaysToPaid, out newInvoiceId);

            Mouse.OverrideCursor = Cursors.Arrow;

            if (newInvoiceId == null || newInvoiceId == -1)
            {
                MessageBox.Show("Der opstod en fejl, kunne ikke lave en regning");
            }
            else
            {
                this.dgv_c_invoices_orders.ItemsSource = null;

                if (this.dgv_c_invoices.Items.Count < 1)
                {
                    var dleleteingItem = (string)this.cbm_c_invoices_customers.SelectedItem;
                    this.cbm_c_invoices_customers.SelectedIndex = -1;
                    var list = (List<string>)this.cbm_c_invoices_customers.ItemsSource;
                    list.Remove(dleleteingItem);

                    this.cbm_c_invoices_customers.ItemsSource = null;
                    this.cbm_c_invoices_customers.ItemsSource = list;
                }
            }
        }
        #endregion

        #region Invoices not paid
        private void dgv_np_invoices_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (((DataGrid)sender).SelectedIndex == -1)
                return;

            Mouse.OverrideCursor = Cursors.Wait;

            this.txt_np_invoices_id.Text = ((DataRowView)((DataGrid)sender).SelectedItem).Row.ItemArray[0].ToString();

            var list = new List<string>();


            var row = ((DataRowView)((DataGrid)sender).SelectedItem).Row.ItemArray;

            var id = string.Empty;

            for (int i = 2; i < row[0].ToString().Length; i++)
			{
			    id += row[0].ToString()[i];
			}

            for (int i = 2; i < row.Length; i++)
            {
                if (row[0].ToString()[0] == 'C')
                {
                    if (row[i].ToString() != string.Empty)
                    {
                        list.Add(this.controller.FindOrdersInfo('C', Convert.ToInt32(row[i].ToString())));
                    }
                }
                else if (row[0].ToString()[0] == 'P')
                {
                    if (row[i].ToString() != string.Empty)
                    {
                        list.Add(this.controller.FindOrdersInfo('P', Convert.ToInt32(row[i].ToString())));
                    }
                }
                else
                {
                    throw new ArgumentException();
                }
            }

            var dataTable = new DataTable();
            dataTable.Columns.Add("Beskrivelse", typeof(string));

            foreach (var item in list)
            {
                dataTable.Rows.Add(item);
            }

            this.dgv_np_invoices_orders.ItemsSource = dataTable.AsDataView();
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void txt_np_invoices_id_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            ClearInvoices();
        }

        private void btn_np_invoices_clear_Click(object sender, RoutedEventArgs e)
        {
            ClearInvoices();
        }

        private void ClearInvoices()
        {
            this.dgv_np_invoices_orders.ItemsSource = null;
            this.txt_np_invoices_id.Text = string.Empty;
            this.dgv_np_invoices.SelectedIndex = -1;
        }

        private void btn_np_invoices_search_Click(object sender, RoutedEventArgs e)
        {
            FindInvoices();
        }

        private void txt_np_invoices_id_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                FindInvoices();
            }
        }

        private void FindInvoices()
        {
            if (txt_np_invoices_id.Text == string.Empty)
                return;

            int index = 0;
            bool found = false;
            foreach (DataRowView item in this.dgv_np_invoices.ItemsSource)
	        {
                if (item.Row.ItemArray[0].ToString() == txt_np_invoices_id.Text.ToUpper())
                {
                    found = true;
                    break;
                }

                index++;
	        }

            if (found)
            {
                this.dgv_np_invoices.SelectedIndex = index;
            }
            else
            {
                ClearInvoices();
            }

            this.dgv_np_invoices_orders.Focus();
        }

        private void btn_np_invoices_remove_Click(object sender, RoutedEventArgs e)
        {
            if (dgv_np_invoices.SelectedIndex == -1)
                return;

            var result = MessageBox.Show("Vil du slette regning nr. " + txt_np_invoices_id.Text + "?", "Slet regningen", MessageBoxButton.YesNo);

            if (result != MessageBoxResult.Yes)
                return;

            Mouse.OverrideCursor = Cursors.Wait;

            string id = string.Empty;

            for (int i = 2; i < this.txt_np_invoices_id.Text.Length; i++)
            {
                id += this.txt_np_invoices_id.Text[i];
            }

            if (this.controller.DelelteInvoice(this.txt_np_invoices_id.Text[0], Convert.ToInt32(id)))
            {
                var dataTable = new DataTable();
                
                foreach (var item in this.dgv_np_invoices.Columns)
	            {
                    dataTable.Columns.Add(item.Header.ToString(), typeof(string));
	            }
                
                foreach (DataRowView item in this.dgv_np_invoices.ItemsSource)
                {
                    if (item.Row.ItemArray[0].ToString() != this.txt_np_invoices_id.Text.ToUpper())
                    {
                        dataTable.Rows.Add(item.Row.ItemArray);
                    }
                }

                this.dgv_np_invoices.ItemsSource = dataTable.AsDataView();
            }
            else
            {
                MessageBox.Show("Kunne ikke slette fra databasen");
            }

            ClearInvoices();
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void btn_np_invoices_paid_Click(object sender, RoutedEventArgs e)
        {
            if (this.dgv_np_invoices.SelectedIndex == -1)
                return;

            Mouse.OverrideCursor = Cursors.Wait;

            var row = (DataRowView)dgv_np_invoices.SelectedItem;

            string id = string.Empty;

            for (int i = 2; i < row.Row.ItemArray[0].ToString().Length; i++)
            {
                id += row.Row.ItemArray[0].ToString()[i];
            }

            if (this.controller.PiadInvoice(row.Row.ItemArray[0].ToString()[0], Convert.ToInt32(id)))
            {
                var dataTable = new DataTable();

                foreach (var item in this.dgv_np_invoices.Columns)
                {
                    dataTable.Columns.Add(item.Header.ToString(), typeof(string));
                }

                foreach (DataRowView item in this.dgv_np_invoices.ItemsSource)
                {
                    if (item.Row.ItemArray[0].ToString() != row.Row.ItemArray[0].ToString())
                    {
                        dataTable.Rows.Add(item.Row.ItemArray);
                    }
                }

                this.dgv_np_invoices.ItemsSource = dataTable.AsDataView();
                ClearInvoices();
            }
            else
            {
                MessageBox.Show("Regningen blev ikke opdateret");
            }

            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void btn_np_invoices_export_Click(object sender, RoutedEventArgs e)
        {
            if (this.dgv_np_invoices.Items.Count < 1)
            {
                MessageBox.Show("Vælg ordrer for at lave en regning");
                return;
            }

            Mouse.OverrideCursor = Cursors.Wait;

            var departments = this.controller.ListOfDepartments();
            var acc = this.controller.ListOfBankAcc();

            Mouse.OverrideCursor = Cursors.Arrow;

            var window = new WpfAddInvoice(acc, departments);
            window.ShowDialog();

            if (!window.Ok)
                return;

            Mouse.OverrideCursor = Cursors.Wait;

            var orders = new List<int?>();

            var row = (DataRow)((DataRowView)this.dgv_np_invoices.SelectedItem).Row;

            for (int i = 2; i < row.ItemArray.Length; i++)
            {
                if (row.ItemArray[i] != null)
                {
                    orders.Add(Convert.ToInt32(row.ItemArray[i].ToString()));
                }
                else
                {
                    orders.Add(null);
                }
            }

            bool ok = false;
            var customersId = string.Empty;
            for (int i = 0; i < orders.Count; i++)
            {
                int? ignore;
                if (orders[i] != null)
                {
                    if (this.txt_np_invoices_id.Text[0] == 'C')
                    {
                        ok = this.controller.CreateInvoiceCompany(orders.ToArray<int?>(), this.controller.FindCompanyCustomerBaseOrder((int)orders[i]).CompanyCustomersNo,
                            Convert.ToInt32(window.Bank), Convert.ToInt32(window.Department), window.DaysToPaid, out ignore);
                    }
                    else if (this.txt_np_invoices_id.Text[0] == 'P')
                    {
                        ok = this.controller.CreateInvoicePrivate(orders.ToArray<int?>(), this.controller.FindPrivateCustomerBaseOrder((int)orders[i]).PrivateCustomersNo,
                            Convert.ToInt32(window.Bank), Convert.ToInt32(window.Department), window.DaysToPaid, out ignore);
                    }
                    else
                    {
                        throw new ArgumentException();
                    }
                    break;
                }
            }

            if (!ok)
            {
                MessageBox.Show("Kunne ikke lave regningen");
            }

            Mouse.OverrideCursor = Cursors.Arrow;
        }
        #endregion
        #endregion

        private void CleanUp()
        {
            switch (this.tabIndex)
            {
                case 0:
                    switch (this.subTabIndex)
	                {
                        case 0:
                            this.controller.CleanUpPrivetOrders();
                            this.controller.CleanUpCompanyOrders();

                            this.dgv_u_orders.ItemsSource = null;
                            break;
                        case 1:
                            this.controller.CleanUpPrivetOrders();

                            this.cmb_p_orders_worker.ItemsSource = null;
                            this.cmb_p_orders_customer.ItemsSource = null;
                            break;
                        case 2:
                            this.controller.CleanUpCompanyOrders();
                            
                            this.cmb_c_orders_worker.ItemsSource = null;
                            this.cmb_c_orders_customer.ItemsSource = null;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException("CleanUp sup tab Orders");
	                }
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    switch (this.subTabIndex)
	                {
                        case 0:
                            this.controller.CleanUpPrivateCustomers();
                            this.dgv_p_customers.ItemsSource = null;
                            break;
                        case 1:
                            this.controller.CleanUpCompanyCustomers();
                            this.dgv_c_customers.ItemsSource = null;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException("CleanUp sup tab customer");
	                }
                    break;
                case 4:
                    this.dgv_workers.ItemsSource = null;
                    break;
                case 5:
                    this.controller.CleanUpDepartments();
                    this.dgv_departments.ItemsSource = null;
                    this.cmb_departments_head.ItemsSource = null;
                    break;
                case 6:
                    switch (this.subTabIndex)
	                {
                        case 0:
                            this.dgv_p_invoices.ItemsSource = null;
                            this.dgv_p_invoices_orders.ItemsSource = null;

                            this.cbm_p_invoices_customers.ItemsSource = null;
                            break;
                        case 1:
                            this.dgv_c_invoices.ItemsSource = null;
                            this.dgv_c_invoices_orders.ItemsSource = null;

                            this.cbm_c_invoices_customers.ItemsSource = null;
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
		                default:
                            throw new ArgumentOutOfRangeException("Cleanup invoices");
	                }
                    break;
                default:
                    throw new ArgumentOutOfRangeException("CleanUp");
            }
        }
    }
}
