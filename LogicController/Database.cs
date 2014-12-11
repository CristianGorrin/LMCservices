using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;

using Customers;
using Orders;
using PostNo;
using Company;

using RDGs;


namespace LogicController
{
    public partial class Controller
    {
        private Departments departments;
        private Workers workers;
        private CompanyCustomers companyCustomers;
        private PrivateCustomers privateCustomers;
        private CompanyOrders companyOrders;
        private PrivetOrders privetOrders;
        private PostNumbers postNumbers;


        #region departments

        #endregion

        #region workers
        public List<string> ListOfWorkers()
        {
            List<string> list = new List<string>();
            var rgd = new RDGs.RDGtblWorkers(this.session.ConnectionString);

            foreach (var item in rgd.Get(true))
            {
                list.Add("#" + item.WorkNo.ToString() + " - " + item.Name + " " + item.Surname);
            }

            return list;
        }
        #endregion

        #region companyCustomers
        private void FildCompanyCustomers(bool? paid) 
        {
            var rdg = new RDGs.RDGtblCompanyCustomers(this.session.ConnectionString);
            foreach (var item in rdg.Get(paid))
            {
                this.companyCustomers.Add(item);
            }
        }

        public List<string> ListOfCompanyCustomers()
        {
            var list = new List<string>();

            var rdg = new RDGs.RDGtblCompanyCustomers(this.session.ConnectionString);

            foreach (var item in rdg.Get(true))
            {
                list.Add("#" + item.CompanyCustomersNo.ToString() + " - " + item.Name);
            }

            return list;
        }
        #endregion

        #region privateCustomers
        private void FildPrivateCustomers(bool? paid)
        {
            this.privateCustomers.Clear();

            var rdg = new RDGs.RDGtblPrivateCustomers(this.session.ConnectionString);
            foreach (var item in rdg.Get(paid))
            {
                this.privateCustomers.Add(item);
            }
        }

        public List<string> ListOfPrivateCustomers()
        {
            var list = new List<string>();

            var rdg = new RDGs.RDGtblPrivateCustomers(this.session.ConnectionString);

            foreach (var item in rdg.Get(true))
            {
                list.Add("#" + item.PrivateCustomersNo + " - " + item.Name + " " + item.Surname);
            }

            return list;
        }
        #endregion

        #region companyOrders
        private void FildCompanyOrders(bool? paid)
        {
            this.companyOrders.Clear();

            var rdg = new RDGs.RGDtblCompanyOrders(this.session.ConnectionString);
            foreach (var item in rdg.Get(paid))
            {
                this.companyOrders.Add(item);
            }
        }

        public bool CompanyOrdersRemove(int id, out int[] inUseId)
        {
            var rdg = new RDGs.RGDtblCompanyOrders(this.session.ConnectionString);
            inUseId = null;

            try
            {
                rdg.Delete(id);
            }
            catch (Exception)
            {
                var invoicePrivet = new RDGs.RDGtblInvoiceCompany(this.session.ConnectionString);
                inUseId = invoicePrivet.OrdersInUse(id);
                return false;
            }

            this.companyOrders.RemoveAtId(id);
            return true;
        }

        public bool CompanyOrdersAdd(int createById, int customerId, string descriptionTask, double hourUse, 
            double paidHour, int toAcc, DateTime taskDate)
        {
            var rdg = new RDGs.RGDtblCompanyOrders(this.session.ConnectionString);

            var newOrder = new InterfaceAdaptor.CompanyOrder()
            {
                CreateBy = new InterfaceAdaptor.Worker() {  WorkNo = createById },
                CreateDate = DateTime.Now,
                Customer = new InterfaceAdaptor.CompanyCustomer() {  CompanyCustomersNo = customerId },
                DescriptionTask = descriptionTask,
                HoutsUse = hourUse,
                Paid = false,
                PaidHour = paidHour,
                PaidToAcc = toAcc,
                TaskDate = taskDate
            };

            try
            {
                rdg.Add(newOrder);
            }
            catch (Exception)
            {
                return false;
            }

            this.companyOrders.Add(rdg.Find(rdg.NextId - 1));

            return true;
        }

        public bool CompanyOrdersUpdate(int orderId, int createById, int customerId, string descriptionTask, double hourUse,
            double paidHour, int toAcc, DateTime taskDate)
        {
            var rdg = new RDGs.RGDtblCompanyOrders(this.session.ConnectionString);

            var newOrder = new InterfaceAdaptor.CompanyOrder()
            {
                CreateBy = new InterfaceAdaptor.Worker() { WorkNo = createById },
                Customer = new InterfaceAdaptor.CompanyCustomer() { CompanyCustomersNo = customerId },
                DescriptionTask = descriptionTask,
                HoutsUse = hourUse,
                InvoiceNo = orderId,
                PaidHour = paidHour,
                PaidToAcc = toAcc,
                TaskDate = taskDate,
            };
            try
            {
                rdg.Update(newOrder);
            }
            catch (Exception)
            {
                return false;   
            }

            for (int i = 0; i < this.privetOrders.Count; i++)
            {
                if (this.privetOrders.GetAt(i).InvoiceNo == orderId)
                {
                    this.companyOrders.Update(rdg.Find(newOrder.InvoiceNo), i);
                    break;
                }
            }

            return true;
        }
        #endregion
        
        #region privetOrders
        private void FildPrivetOrders(bool? paid)
        {
            this.privetOrders.Clear();

            var rdg = new RDGs.RDGtblPrivetOrders(this.session.ConnectionString);
            foreach (var item in rdg.Get(paid))
            {
                this.privetOrders.Add(item);
            }
        }

        public bool PrivetOrdersRemove(int id, out int[] inUseId)
        {
            var rdg = new RDGs.RDGtblPrivetOrders(this.session.ConnectionString);
            inUseId = null;

            try
            {
                rdg.Delete(id);
            }
            catch (Exception)
            {
                var invoicePrivet = new RDGs.RDGtblInvoicePrivet(this.session.ConnectionString);
                inUseId = invoicePrivet.OrdersInUse(id);
                return false;
            }

            this.privetOrders.RemoveAtId(id);
            return true;
        }

        public bool PrivetOrdersAdd(int createById, int customerId, string descriptionTask, double hourUse, 
            double paidHour, int toAcc, DateTime taskDate)
        {
            var rdg = new RDGs.RDGtblPrivetOrders(this.session.ConnectionString);

            var newOrder = new InterfaceAdaptor.PrivetOrder() 
            {
                CreateBy = new InterfaceAdaptor.Worker() { WorkNo = createById },
                CreateDate = DateTime.Now,
                Customer = new InterfaceAdaptor.PrivetCustomer() { PrivateCustomersNo = customerId },
                DescriptionTask = descriptionTask,
                HourUse = hourUse,
                Paid = false,
                PaidHour = paidHour,
                PaidToAcc = toAcc,
                TaskDate = taskDate
            };
            
            try
            {
                rdg.Add(newOrder);
            }
            catch (Exception)
            {
                return false;
            }

            this.privetOrders.Add(rdg.Find(rdg.NextId - 1));

            return true;
        }

        public bool PrivetOrdersUpdate(int orderId, int createById, int customerId, string descriptionTask, double hourUse,
            double paidHour, int toAcc, DateTime taskDate)
        {
            var rdg = new RDGs.RDGtblPrivetOrders(this.session.ConnectionString);

            var newOrder = new InterfaceAdaptor.PrivetOrder() 
            {
                InvoiceNo = orderId,
                CreateBy = new InterfaceAdaptor.Worker() { WorkNo = createById },
                CreateDate = DateTime.Now,
                Customer = new InterfaceAdaptor.PrivetCustomer() { PrivateCustomersNo = customerId },
                DescriptionTask = descriptionTask,
                HourUse = hourUse,
                Paid = false,
                PaidHour = paidHour,
                PaidToAcc = toAcc,
                TaskDate = taskDate
            };

            try
            {
                rdg.Update(newOrder);
            }
            catch (Exception)
            {
                return false;
            }

            for (int i = 0; i < this.privetOrders.Count; i++)
			{
			    if (this.privetOrders.GetAt(i).InvoiceNo == orderId)
	            {
                    this.privetOrders.Update(rdg.Find(newOrder.InvoiceNo), i);
                    break;
	            }
			}

            return true;
        }
        #endregion

        #region postNumbers
        
        #endregion

        #region Get data tables
        public DataTable GetOrdersPrivet()
        {
            FildPrivetOrders(false);
            var dataTable = this.privetOrders.AsDataTable();
            
            dataTable.Columns[0].ColumnName = "Oprettet af";
            dataTable.Columns[1].ColumnName = "Oprettet Dato";
            dataTable.Columns[2].ColumnName = "Kunde";
            dataTable.Columns[5].ColumnName = "Beskrivelse";
            dataTable.Columns[6].ColumnName = "Timer brugt";
            dataTable.Columns[7].ColumnName = "Order nr";
            dataTable.Columns[9].ColumnName = "Timeløn";
            dataTable.Columns[11].ColumnName = "Start dato";

            // Remove 3:Date Bill Send 8:Paid 10:Paid to Acc
            dataTable.Columns.Remove("Paid to Acc");
            dataTable.Columns.Remove("Paid");
            dataTable.Columns.Remove("Date Bill Send");
            dataTable.Columns.Remove("Days to Paid");
            return dataTable;
        }

        public DataTable GetOrdersCompany()
        {
            FildCompanyOrders(false);
            var dataTable = this.companyOrders.AsDataTable();

            dataTable.Columns[0].ColumnName = "Oprettet af";
            dataTable.Columns[1].ColumnName = "Oprettet Dato";
            dataTable.Columns[2].ColumnName = "Kunde";
            dataTable.Columns[5].ColumnName = "Beskrivelse";
            dataTable.Columns[6].ColumnName = "Timer brugt";
            dataTable.Columns[7].ColumnName = "Order nr";
            dataTable.Columns[9].ColumnName = "Timeløn";
            dataTable.Columns[11].ColumnName = "Start dato";

            // Remove 3:Date Bill Send 8:Paid 10:Paid to Acc
            dataTable.Columns.Remove("Paid to Acc");
            dataTable.Columns.Remove("Paid");
            dataTable.Columns.Remove("Date Bill Send");
            dataTable.Columns.Remove("Days to Pay");
            return dataTable;
        }

        public DataTable GetOrdersCompanyAndPrivet()
        {
            var privet = GetOrdersPrivet();
            var company = GetOrdersCompany();

            var dataTable = new DataTable();

            dataTable.Columns.Add("Order Nr", typeof(int));
            dataTable.Columns.Add("Kunde", typeof(string));
            dataTable.Columns.Add("Kunde type", typeof(string));
            dataTable.Columns.Add("Oprettet af", typeof(string));
            dataTable.Columns.Add("Start Dato", typeof(DateTime));
            dataTable.Columns.Add("Beskrivelse", typeof(string));


            return null;
        }

        #endregion

        #region Clean Up
        public void CleanUpDepartments()
        {
            this.departments.Clear();
        }

        public void CleanUpWorkers()
        {
            this.workers.Clear();
        }
        public void CleanUpCompanyCustomers()
        {
            this.companyCustomers.Clear();
        }
        public void CleanUpPrivateCustomers()
        {
            this.privateCustomers.Clear();
        }
        public void CleanUpCompanyOrders()
        {
            this.companyOrders.Clear();
        }

        public void CleanUpPrivetOrders()
        {
            this.privetOrders.Clear();
        }

        public void CleanUpPostNumbers()
        {
            this.postNumbers.Clear();
        }
        #endregion
    }
}
