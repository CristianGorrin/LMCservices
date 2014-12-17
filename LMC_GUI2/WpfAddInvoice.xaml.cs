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

namespace LMC_GUI2
{
    /// <summary>
    /// Interaction logic for WpfAddInvoice.xaml
    /// </summary>
    public partial class WpfAddInvoice : MetroWindow
    {
        private bool ok = false;

        public WpfAddInvoice(List<string> bank, List<string> department)
        {
            InitializeComponent();

            this.cmb_Bank.ItemsSource = bank;
            this.cmb_Department.ItemsSource = department;
        }

        public bool Ok { get { return this.ok; } }

        
        
        public int DaysToPaid
        {
            get
            {
                return Convert.ToInt32(this.txt_DaysToPaid.Text);
            }
        }

        public string Bank
        {
            get
            {
                string temp = string.Empty;

                foreach (char item in this.cmb_Bank.SelectedItem.ToString())
                {
                    int igonre;
                    if (int.TryParse(item.ToString(), out igonre))
                    {
                        temp += item;
                    }
                    else
                    {
                        if (item == ' ' || item == '-' )
                        {
                            break;
                        }
                    }
                }

                return temp;
            }
        }

        public string Department
        {
            get
            {
                string temp = string.Empty;

                foreach (char item in this.cmb_Department.SelectedItem.ToString())
                {
                    int igonre;
                    if (int.TryParse(item.ToString(), out igonre))
                    {
                        temp += item;
                    }
                    else
                    {
                        if (item == ' ' || item == '-')
                        {
                            break;
                        }
                    }
                }

                return temp;
            }
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_ok_Click(object sender, RoutedEventArgs e)
        {
            if (cmb_Bank.SelectedIndex == -1 || cmb_Department.SelectedIndex == -1)
            {
                MessageBox.Show("Alle felter skal være udfyldt");
                return;
            }

            try
            {
                Convert.ToInt32(txt_DaysToPaid.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Betalingsdato skal være et gyldigt tal");
                return;
            }

            this.ok = true;
            this.Close();
        }
    }
}
