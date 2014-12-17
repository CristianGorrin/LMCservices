using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Office.Core;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Drawing;

using System.Text.RegularExpressions;

using Interface;

using System.Diagnostics;

using System.IO;

namespace ExcelAPI
{
    public class CreateInvoice <T>
    {
        //Excel interface
        private Excel.Application xlApp;
        private Excel._Workbook xlWorkbook;
        private Excel._Worksheet xlSheet;

        //Input
        private List<T> orders;
        private Interface.IcompanyCustomer companyCustomer;
        private Interface.IprivetCustomer privateCustomer;
        private Interface.IbankAccounts bank;
        private Interface.Idepartment dep;
        private int daysToPay;
        private string fakturaNo;

        private const int rowsOffset = 17;
        private const int columnsOffset = 2;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input">Input count cannot be above 17. If it is so it will be ignored.</param>       
        /*
         * Private Orders
         */
        public CreateInvoice(List<T> input, Interface.IprivetCustomer customer, Interface.IbankAccounts bank, 
            Interface.Idepartment dep, int daysToPay, string fakturaNo)
        {
            this.orders = input;

            if (!(input is List<Interface.IprivetOrder>))
                throw new ArgumentException("Not of right type");
            
            this.privateCustomer = customer;
            this.bank = bank;
            this.daysToPay = daysToPay;
            this.fakturaNo = "P-" + fakturaNo;
            this.dep = dep;
        }
        //  Interface.Iworker worker, Interface.Idepartment dep, 

        /*
         * Company orders
         */
        public CreateInvoice(List<T> input, Interface.IcompanyCustomer customer, Interface.IbankAccounts bank,
            Interface.Idepartment dep, int daysToPay, string fakturaNo)
        {
            this.orders = input;

            if (!(input is List<Interface.IcompanyOrder>))
                throw new ArgumentException("Not of right type");

            this.companyCustomer = customer;
            this.bank = bank;
            this.daysToPay = daysToPay;
            this.fakturaNo = "C-" + fakturaNo;
            this.dep = dep;
        }

        /*
         * Start Excel
         */
        public void StartExcel()
        {            
            //Start Excel and get Application object.
            xlApp = new Excel.Application();
            xlApp.Visible = false;

            //Create Excel Workbook.
            xlWorkbook = (Excel._Workbook)(xlApp.Workbooks.Add(Missing.Value));
            xlSheet = (Excel._Worksheet)xlWorkbook.ActiveSheet;

            PageLayout();

            InsertPicture();

            MergeColumns();

            FormatCells();

            ConstantText();

            InputCustomerData();

            InputCompanyInfo();

            InputInvoiceInfo();

            InputOrders(rowsOffset, columnsOffset);

            Totals();

            xlApp.ActiveWindow.View = Excel.XlWindowView.xlPageLayoutView;
            xlApp.WindowState = Excel.XlWindowState.xlMaximized;
            xlApp.Visible = true;
        }

        /*
         * Page layout for printing
         */
        private void PageLayout()
        {
            xlSheet.PageSetup.CenterVertically = false;
            xlSheet.PageSetup.CenterHorizontally = true;
            xlSheet.PageSetup.TopMargin = xlApp.CentimetersToPoints(0.50);
            xlSheet.PageSetup.LeftMargin = xlApp.CentimetersToPoints(0.50);
            xlSheet.PageSetup.RightMargin = xlApp.CentimetersToPoints(0.50);
            xlSheet.PageSetup.BottomMargin = xlApp.CentimetersToPoints(0.50);
            xlSheet.PageSetup.Orientation = Excel.XlPageOrientation.xlLandscape;
        }

        /*
         * Insert Picture
         */
        private bool InsertPicture()
        {
            if (File.Exists(@"./tempLogoLMC.PNG"))
            {            
                xlSheet.Shapes.AddPicture(@"./tempLogoLMC.PNG",
                    Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 0, 0, 440, 105);

                return true;
            } 
            else return false;
        }

        /*
         * Merging
         */
        private void MergeColumns() 
        {
            //Merge columns M-O (rows 1-13)
            for (int i = 1; i <= 13; i++)
            {
                xlSheet.get_Range("M" + i, "O" + i).Merge(Type.Missing);
            }

            //Merge columns C-E (rows 9-14)
            for (int i = 9; i <= 14; i++)
            {
                xlSheet.get_Range("C" + i, "E" + i).Merge(Type.Missing);
            }

            //Merge main cells - columns B-F, G-H, I-J, K-L, M-N (rows 16-33) and border them
            for (int i = 16; i <= 33; i++)
            {
                xlSheet.get_Range("B" + i, "G" + i).Merge(Type.Missing);
                xlSheet.get_Range("B" + i, "G" + i).BorderAround(Type.Missing);
                xlSheet.get_Range("H" + i, "I" + i).Merge(Type.Missing);
                xlSheet.get_Range("H" + i, "I" + i).BorderAround(Type.Missing);
                xlSheet.get_Range("J" + i, "K" + i).Merge(Type.Missing);
                xlSheet.get_Range("J" + i, "K" + i).BorderAround(Type.Missing);
                xlSheet.get_Range("L" + i, "M" + i).Merge(Type.Missing);
                xlSheet.get_Range("L" + i, "M" + i).BorderAround(Type.Missing);
                xlSheet.get_Range("N" + i, "O" + i).Merge(Type.Missing);
                xlSheet.get_Range("N" + i, "O" + i).BorderAround(Type.Missing);
            }

            //Totals - merge and border
            for (int i = 34; i <= 36; i++)
            {
                xlSheet.get_Range("L" + i, "M" + i).Merge(Type.Missing);
                xlSheet.get_Range("L" + i, "M" + i).BorderAround(Type.Missing);
                xlSheet.get_Range("L" + i, "M" + i).Borders.Weight = Excel.XlBorderWeight.xlMedium;
                xlSheet.get_Range("N" + i, "O" + i).Merge(Type.Missing);
                xlSheet.get_Range("N" + i, "O" + i).BorderAround(Type.Missing);
                xlSheet.get_Range("N" + i, "O" + i).Borders.Weight = Excel.XlBorderWeight.xlMedium;
            }
            xlSheet.get_Range("L38", "O38").Merge(Type.Missing);
        }

        /*
         * Formating
         */
        private void FormatCells() 
        {
            //Format and alignment
            //Company info
            xlSheet.get_Range("L1", "L13").Font.Bold = true;
            xlSheet.get_Range("O10", "O10").Font.Bold = true;
            xlSheet.get_Range("M10", "M10").Font.Bold = true;
            xlSheet.get_Range("L1", "O13").HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            //Customer info
            xlSheet.get_Range("A9", "A14").Font.Bold = true;
            xlSheet.get_Range("C8", "C8").Font.Bold = true;
            xlSheet.get_Range("C9", "E14").HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;

            //Format and alignment  headers           
            xlSheet.get_Range("B16", "N16").Font.Bold = true;
            xlSheet.get_Range("B16", "N16").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Style
            xlSheet.get_Range("B16", "N16").Interior.Color = Excel.XlRgbColor.rgbGrey;
            xlSheet.get_Range("B16", "N16").Font.Color = Excel.XlRgbColor.rgbWhite;

            //Format and alignment totals and "Beløb"         
            xlSheet.get_Range("L34", "L38").Font.Bold = true;
            xlSheet.get_Range("L34", "N36").HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            xlSheet.get_Range("N17", "N36").NumberFormat = "#,##0.00 kr";
            xlSheet.get_Range("L38", "O38").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            xlSheet.get_Range("H17", "O33").HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
        }

        /*
         * Constant text
         */
        private void ConstantText()
        {
            //Company information
            xlSheet.Cells[1, 12] = "CVR NR.:";
            xlSheet.Cells[2, 12] = "BANK:";
            xlSheet.Cells[3, 12] = "REG NR.:";
            xlSheet.Cells[4, 12] = "KONTO NR.:";            
            xlSheet.Cells[6, 12] = "MOBIL NR.:";
            xlSheet.Cells[7, 12] = "E-MAIL:";

            //Invoice data            
            xlSheet.Cells[11, 12] = "FAKTURA";
            xlSheet.Cells[12, 12] = "DATO";

            //Customer information
            xlSheet.Cells[8, 3] = "KUNDE";
            xlSheet.Cells[9, 1] = "Att.:";

            if (this.companyCustomer != null)
            xlSheet.Cells[10, 1] = "Firmanavn:";

            xlSheet.Cells[11, 1] = "Adresse:";
            xlSheet.Cells[12, 1] = "Postnr. & by:";
            xlSheet.Cells[13, 1] = "Telefonnr.:";
            xlSheet.Cells[14, 1] = "E-mail:";

            //Headers
            xlSheet.Cells[16, 2] = "BESKRIVELSE";
            xlSheet.Cells[16, 8] = "DATO";
            xlSheet.Cells[16, 10] = "TIMER";
            xlSheet.Cells[16, 12] = "SATS";
            xlSheet.Cells[16, 14] = "BELØB";

            //Total text
            xlSheet.Cells[34, 12] = "SUBTOTAL";
            xlSheet.Cells[35, 12] = "MOMS";
            xlSheet.Cells[36, 12] = "I ALT";

            //Misc text
            xlSheet.Cells[35, 2] = "Hvis der er spørgsmål til denne faktura, bedes De venligst kontakte os via tlf.nr. eller e-mail.";
            xlSheet.Cells[38, 12] = "TAK FORDI DE HANDLEDE MED OS!";
        }

        /*
         * Customer data
         */
        private void InputCustomerData()
        {
            if (this.privateCustomer != null)
            {
                //Insert customer information
                xlSheet.Cells[9, 3] = this.privateCustomer.Name + " " + this.privateCustomer.Surname; // Insert customer name
                xlSheet.Cells[10, 3] = ""; // Insert company name
                xlSheet.Cells[11, 3] = this.privateCustomer.HomeAddress; // Insert address
                xlSheet.Cells[12, 3] = this.privateCustomer.PostNo.PostNumber.ToString() + ", " + this.privateCustomer.PostNo.City; // Insert postno. & city
                xlSheet.Cells[13, 3] = this.privateCustomer.PhoneNo.ToString(); // Insert telephone no.
                xlSheet.Cells[14, 3] = this.privateCustomer.Email; // Insert e-mail
            }
            else if (this.companyCustomer != null)
            {
                //Insert customer information
                xlSheet.Cells[9, 3] = this.companyCustomer.ContactPerson; // Insert customer name
                xlSheet.Cells[10, 3] = this.companyCustomer.Name; // Insert company name
                xlSheet.Cells[11, 3] = this.companyCustomer.Address; // Insert address
                xlSheet.Cells[12, 3] = this.companyCustomer.PostNo.PostNumber.ToString() + ", " + this.companyCustomer.PostNo.City; // Insert postno. & city
                xlSheet.Cells[13, 3] = this.companyCustomer.PhoneNo.ToString(); // Insert telephone no.
                xlSheet.Cells[14, 3] = this.companyCustomer.Email; // Insert e-mail
            }
            else
            {
                throw new ArgumentNullException("No customers!");
            }
        }

        /*
         * Company info
         */
        private void InputCompanyInfo()
        {
            //Insert company info
            xlSheet.Cells[1, 13] = this.dep.CvrNo.ToString(); // CVR nr.
            xlSheet.Cells[2, 13] = this.bank.Bank; // Insert bank
            xlSheet.Cells[3, 13] = this.bank.RegNo.ToString(); // Insert bank registration number
            xlSheet.Cells[4, 13] = this.bank.AccountNo; // Insert bank account number
            //Department info
            xlSheet.Cells[5, 13] = this.dep.DeparmentHead.Name + " " + this.dep.DeparmentHead.Surname; // Department head
            xlSheet.Cells[6, 13] = this.dep.PhoneNo; // Insert mobile number
            xlSheet.Cells[7, 13] = this.dep.Email; // Insert personal e-mail
        }

        /*
         * Invoice info
         */
        private void InputInvoiceInfo()
        {
            var payDate = DateTime.Now;
            payDate = payDate.AddDays(this.daysToPay);
            //Insert invoice data
            xlSheet.Cells[10, 13] = "Indbetales inden d. " + payDate.ToShortDateString(); // Insert DaysToPaid.
            xlSheet.Cells[11, 13] = this.fakturaNo; // Insert Invoice no.
            xlSheet.Cells[12, 13] = @" " + DateTime.Now.ToShortDateString(); // Insert date
        }

        /*
         * Orders
         */
        private void InputOrders(int rowsOffset, int columnsOffset)
        {
            int rows = 0;

            if (this.privateCustomer != null)
            {
                foreach (Interface.IprivetOrder item in this.orders)
                {
                    if (item == null)
                        break;

                    xlSheet.Cells[rowsOffset + rows, columnsOffset] = item.DescriptionTask;
                    xlSheet.Cells[rowsOffset + rows, columnsOffset + 6] = item.TaskDate.ToShortDateString();
                    xlSheet.Cells[rowsOffset + rows, columnsOffset + 8] = item.HourUse.ToString();
                    xlSheet.Cells[rowsOffset + rows, columnsOffset + 10] = item.PaidHour.ToString();
                    xlSheet.Cells[rowsOffset + rows, columnsOffset + 12] = (item.HourUse * item.PaidHour).ToString();

                    rows++;
                }
            }
            else if (this.companyCustomer != null)
            {
                foreach (Interface.IcompanyOrder item in this.orders)
                {
                    if (item == null)
                        break;

                    xlSheet.Cells[rowsOffset + rows, columnsOffset] = item.DescriptionTask;
                    xlSheet.Cells[rowsOffset + rows, columnsOffset + 6] = item.TaskDate.ToShortDateString();
                    xlSheet.Cells[rowsOffset + rows, columnsOffset + 8] = item.HoutsUse.ToString();
                    xlSheet.Cells[rowsOffset + rows, columnsOffset + 10] = item.PaidHour.ToString();
                    xlSheet.Cells[rowsOffset + rows, columnsOffset + 12] = (item.HoutsUse * item.PaidHour).ToString();

                    rows++;
                }
            }
            else
            {
                throw new ArgumentNullException("No customers!");
            }
        }

        /*
         * Totals
         */
        private void Totals()
        {
            //Total
            double total = 0;
            if (this.privateCustomer != null)
            {
                foreach (Interface.IprivetOrder item in this.orders)
                {
                    if (item == null)
                        break;

                    total += item.PaidHour * item.HourUse;
                }
            }
            else if (this.companyCustomer != null)
            {
                foreach (Interface.IcompanyOrder item in this.orders)
                {
                    if (item == null)
                        break;

                    total += item.PaidHour * item.HoutsUse;
                }
            }
            else
            {
                throw new ArgumentNullException("No customers!");
            }   
            double moms =  total / 4;

            xlSheet.Cells[34, 14] = total - moms; // SUBTOTAL
            xlSheet.Cells[35, 14] = moms.ToString(); // MOMS
            xlSheet.Cells[36, 14] = total.ToString(); // I ALT
        }
    }
}
