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
        public bool DepartmentsRemove(int id)
        {
            var rdg = new RDGs.RDGtblDepartment(this.session.ConnectionString);

            try
            {
                rdg.Delete(id);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
        public bool DepartmentsUpdate(int deparment, string address, string altPhoneNo, string companyName, int cvrNo,
            int deparmentHead, string email, string phoneNo, int zip)
        {
            var rdg = new RDGs.RDGtblDepartment(this.session.ConnectionString);

            try
            {
                rdg.Update(new InterfaceAdaptor.Department()
                {
                    Deparment = deparment,
                    Active = true,
                    Address = address,
                    AltPhoneNo = altPhoneNo,
                    CompanyName = companyName,
                    CvrNo = cvrNo,
                    DeparmentHead = new InterfaceAdaptor.Worker() { WorkNo = deparmentHead },
                    Email = email,
                    PostNo = this.postNumbers.GetAtPostNumber(zip),
                    PhoneNo = phoneNo,
                });
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
        public bool DepartmentsAdd(string address, string altPhoneno, string companyName, int cvrNo,
            int deparmentHead, string email, string phoneNo, int zip)
        {
            var rdg = new RDGs.RDGtblDepartment(this.session.ConnectionString);

            try
            {
                rdg.Add(new InterfaceAdaptor.Department()
                {
                    Active = true,
                    Address = address,
                    AltPhoneNo = altPhoneno,
                    CompanyName = companyName,
                    CvrNo = cvrNo,
                    DeparmentHead = new InterfaceAdaptor.Worker() { WorkNo = deparmentHead },
                    Email = email,
                    PostNo = this.postNumbers.GetAtPostNumber(zip),
                    PhoneNo = phoneNo,
                });
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }


        private void Filddepartments(bool? active)
        {
            this.departments.Clear();

            var rdg = new RDGs.RDGtblDepartment(this.session.ConnectionString);

            foreach (var item in rdg.Get(active))
            {
                this.departments.Add(item);
            }
        }

        public List<string> ListOfDepartments()
        {
            var list = new List<string>();

            var rdg = new RDGs.RDGtblDepartment(this.session.ConnectionString);

            foreach (var item in rdg.Get(true))
            {
                list.Add("#" + item.Deparment + " " + item.CompanyName);
            }

            return list;
        }
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

        private void FildWorkers(bool? active)
        {
            this.workers.Clear();

            var rgd = new RDGs.RDGtblWorkers(this.session.ConnectionString);

            foreach (var item in rgd.Get(active))
            {
                this.workers.Add(item);
            }
        }


        public bool WorkersRemove(int id)
        {
            var rdg = new RDGs.RDGtblWorkers(this.session.ConnectionString);

            try
            {
                rdg.Delete(id);
            }
            catch (Exception)
            {
                return false;
            }

            this.workers.RemoveAtWorkerId(id);
            return true;
        }

        public bool WorkerAdd(string name, string surname, string address, int zip,
            string phoneNo, string altPhoneNo, string email)
        {
            int workerStatusId = -1;

            try
            {
                var rdgWorkerStatus = new RDGs.RDGtblWorkerStatus(this.session.ConnectionString);
                workerStatusId = rdgWorkerStatus.NextId;
                rdgWorkerStatus.Add(new InterfaceAdaptor.WorkerStatus()
                {
                    Staus = "temp" // TODO add worker status to the program
                });
            }
            catch (Exception)
            {
                return false;
            }

            if (workerStatusId == -1)
                return false;

            var rdg = new RDGs.RDGtblWorkers(this.session.ConnectionString);
            try
            {
                rdg.Add(new InterfaceAdaptor.Worker()
                {
                    Active = true,
                    Address = address,
                    AltPhoneNo = altPhoneNo,
                    Email = email,
                    Name = name,
                    PhoneNo = phoneNo,
                    PostNo = postNumbers.GetAtPostNumber(zip),
                    Surname = surname,
                    WorkerStatus = new InterfaceAdaptor.WorkerStatus() { StautsNo = workerStatusId },
                });
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool WorkerUpdate(int id, string name, string surname, string address, int zip,
            string phoneNo, string altPhoneNo, string email)
        {
            var rdg = new RDGs.RDGtblWorkers(this.session.ConnectionString);

            try
            {
                rdg.Update(new InterfaceAdaptor.Worker()
                {
                    Active = true,
                    Address = address,
                    AltPhoneNo = altPhoneNo,
                    Email = email,
                    Name = name,
                    PhoneNo = phoneNo,
                    PostNo = this.postNumbers.GetAtPostNumber(zip),
                    Surname = surname,
                    WorkerStatus = null, // TODO add worker status to the program
                    WorkNo = id
                });
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
        #endregion

        #region companyCustomers
        private void FildCompanyCustomers(bool? active) 
        {
            this.companyCustomers.Clear();

            var rdg = new RDGs.RDGtblCompanyCustomers(this.session.ConnectionString);
            foreach (var item in rdg.Get(active))
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

        public List<string> ListOfCompanyCustomersForInvoice()
        {
            var list = new List<string>();
            var filter = new List<int>();

            var rdg = new RDGs.RGDtblCompanyOrders(this.session.ConnectionString);

            string temp = string.Empty;

            foreach (var item in rdg.Get(false))
            {
                if (!filter.Contains(item.Customer.CompanyCustomersNo))
                {
                    temp = "#";
                    if (item.Customer.CompanyCustomersNo < 10)
                    {
                        temp += "00" + item.Customer.CompanyCustomersNo;
                    }
                    else if (item.Customer.CompanyCustomersNo < 100)
                    {
                        temp += "0" + item.Customer.CompanyCustomersNo;
                    }
                    else
                    {
                        temp += item.Customer.CompanyCustomersNo;
                    }

                    temp += " - " + item.Customer.Name;

                    list.Add(temp);

                    filter.Add(item.Customer.CompanyCustomersNo);
                }
            }

            list.Sort();

            return list;
        }

        public bool CompanyCustomerRemove(int id)
        {
            var rdg = new RDGs.RDGtblCompanyCustomers(this.session.ConnectionString);

            try
            {
                rdg.Delete(id);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool CompanyCustomerAdd(int cvr, string name, string address, int zip,
            string contactperson, string phoneno, string altphoneno, string email)
        {
            var rdg = new RDGs.RDGtblCompanyCustomers(this.session.ConnectionString);

            try
            {
                rdg.Add(new InterfaceAdaptor.CompanyCustomer()
                {
                    Active = true,
                    Address = address,
                    AltPhoneNo = altphoneno,
                    ContactPerson = contactperson,
                    CvrNo = cvr,
                    Email = email,
                    Name = name,
                    PhoneNo = phoneno,
                    PostNo = this.postNumbers.GetAtPostNumber(zip)
                });
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool CompanyCustomerUpdate(int id, int cvr, string name, string address, int zip,
            string contactperson, string phoneno, string altphoneno, string email)
        {
            var rdg = new RDGs.RDGtblCompanyCustomers(this.session.ConnectionString);
            
            try
            {
                rdg.Update(new InterfaceAdaptor.CompanyCustomer()
                {
                    CompanyCustomersNo = id,
                    Active = true,
                    Address = address,
                    AltPhoneNo = altphoneno,
                    ContactPerson = contactperson,
                    CvrNo = cvr,
                    Email = email,
                    Name = name,
                    PhoneNo = phoneno,
                    PostNo = this.postNumbers.GetAtPostNumber(zip)
                });
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public DataTable GetCompanyCustomerstForInvoices(int id)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("Order nr", typeof(int));
            dataTable.Columns.Add("Opgave", typeof(string));
            dataTable.Columns.Add("Dato", typeof(DateTime));
            dataTable.Columns.Add("Timer brugt", typeof(double));
            dataTable.Columns.Add("Time løn", typeof(double));

            var rdg = new RDGs.RGDtblCompanyOrders(this.session.ConnectionString);

            foreach (var item in rdg.GetBaseCustomer(id, false))
            {
                dataTable.Rows.Add(
                    item.InvoiceNo,
                    item.DescriptionTask,
                    item.CreateDate,
                    item.HoutsUse,
                    item.PaidHour);
            }

            return dataTable;
        }

        public Interface.IcompanyCustomer FindCompanyCustomer(int id)
        {
            var rdg = new RDGs.RDGtblCompanyCustomers(this.session.ConnectionString);

            try
            {
                return rdg.Find(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Interface.IcompanyCustomer FindCompanyCustomerBaseOrder(int orderId)
        {
            var rdg = new RDGs.RGDtblCompanyOrders(this.session.ConnectionString);
            return rdg.Find(orderId).Customer;
        }

        #endregion

        #region privateCustomers
        public Interface.IprivetCustomer FindPrivateCustomer(int id)
        {
            var rdg = new RDGs.RDGtblPrivateCustomers(this.session.ConnectionString);

            try
            {
                return rdg.Find(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Interface.IprivetCustomer FindPrivateCustomerBaseOrder(int orderId)
        {
            var rdg = new RDGs.RDGtblPrivetOrders(this.session.ConnectionString);
            return rdg.Find(orderId).Customer;
        }

        private void FildPrivateCustomers(bool? activet)
        {
            this.privateCustomers.Clear();

            var rdg = new RDGs.RDGtblPrivateCustomers(this.session.ConnectionString);
            foreach (var item in rdg.Get(activet))
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

        public List<string> ListOfPrivateCustomersForInvoice()
        {
            var list = new List<string>();
            var filter = new List<int>();

            var rdg = new RDGs.RDGtblPrivetOrders(this.session.ConnectionString);

            string temp = string.Empty;

            foreach (var item in rdg.Get(false))
            {
                if (!filter.Contains(item.Customer.PrivateCustomersNo))
                {
                    temp = "#";
                    if (item.Customer.PrivateCustomersNo < 10)
                    {
                        temp += "00" + item.Customer.PrivateCustomersNo;
                    }
                    else if (item.Customer.PrivateCustomersNo < 100)
                    {
                        temp += "0" + item.Customer.PrivateCustomersNo;
                    }
                    else
                    {
                        temp += item.Customer.PrivateCustomersNo;
                    }

                    temp += " - " + item.Customer.Name + " " + item.Customer.Surname;
                    
                    list.Add(temp);

                    filter.Add(item.Customer.PrivateCustomersNo);
                }
            }

            list.Sort();

            return list;
        }

        public bool PrivateCustomersDelete(int id)
        {
            var rdg = new RDGs.RDGtblPrivateCustomers(this.session.ConnectionString);

            try
            {
                rdg.Delete(id);
            }
            catch (Exception)
            {
                return false;
            }

            this.privateCustomers.RemoveAtId(id);

            return true;
        }

        public bool PrivateCustomerAdd(string name, string surname, string address, int postNumber, string phoneNo,
            string altPhoneNo, string email)
        {
            var rdg = new RDGs.RDGtblPrivateCustomers(this.session.ConnectionString);

            try
            {
                rdg.Add(new InterfaceAdaptor.PrivetCustomer()
                {
                    Active = true,
                    AltPhoneNo = altPhoneNo,
                    Email = email,
                    HomeAddress = address,
                    Name = name,
                    PhoneNo = phoneNo,
                    PostNo = this.postNumbers.GetAtPostNumber(postNumber),
                    Surname = surname
                });
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool PrivateCustomerUpdate(int id, string name, string surname, string address, int zip, string phoneNo,
            string altPhoneNo, string email)
        {
            var rdg = new RDGs.RDGtblPrivateCustomers(this.session.ConnectionString);
            try
            {
                rdg.Update(new InterfaceAdaptor.PrivetCustomer()
                {
                    Active = true,
                    AltPhoneNo = altPhoneNo,
                    Email = email,
                    HomeAddress = address,
                    Name = name,
                    PhoneNo = phoneNo,
                    PostNo = this.postNumbers.GetAtPostNumber(zip),
                    PrivateCustomersNo = id,
                    Surname = surname
                });
            }
            catch (Exception)
            {
                return false;
            }
           
            return true;
        }

        public DataTable GetCustomersPrivetForInvoices(int id)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("Order nr", typeof(int));
            dataTable.Columns.Add("Opgave", typeof(string));
            dataTable.Columns.Add("Dato", typeof(DateTime));
            dataTable.Columns.Add("Timer brugt", typeof(double));
            dataTable.Columns.Add("Time løn", typeof(double));
            
            var rdg = new RDGs.RDGtblPrivetOrders(this.session.ConnectionString);

            foreach (var item in rdg.GetBaseCustomer(id, false))
            {
                dataTable.Rows.Add(
                    item.InvoiceNo,
                    item.DescriptionTask,
                    item.CreateDate,
                    item.HourUse,
                    item.PaidHour);
            } 

            return dataTable;
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
        public void FildPostNo()
        {
            this.postNumbers.Clear();

            var rdg = new RDGs.RDGtblPostNo(this.session.ConnectionString);

            foreach (Interface.IpostNo item in rdg.Get())
            {
                this.postNumbers.Add(item);
            }
        }

        public bool TestPostNo(int number)
        {
            return this.postNumbers.Validate(number);
        }

        public string PostGetInfo(int postNumber)
        {
            var obj = this.postNumbers.GetAtPostNumber(postNumber);

            if (obj != null)
                return obj.PostNumber.ToString() + @" / " + obj.City;
            else
                return string.Empty;
        }
        #endregion

        #region AccBank
        public List<string> ListOfBankAcc()
        {
            var list = new List<string>();
            
            var rdg = new RDGs.RDGtblBankAccounts(this.session.ConnectionString);

            foreach (var item in rdg.Get())
	        {
                list.Add("#" + item.Id + " - " + item.AccountName);
	        }

            return list;
        }
        #endregion

        #region Invoice
        public string FindOrdersInfo(char type, int id)
        {
            if (type == 'C')
            {
                var rdg = new RDGs.RGDtblCompanyOrders(this.session.ConnectionString);
                return rdg.Find(id).DescriptionTask;
            }
            else if (type == 'P')
            {
                var rdg = new RDGs.RDGtblPrivetOrders(this.session.ConnectionString);
                return rdg.Find(id).DescriptionTask;
            }
            else
	        {
                throw new ArgumentException("Type");
	        }
        }

        public bool DelelteInvoice(char type, int id)
        {
            if (type == 'C')
            {
                var rdg = new RDGs.RDGtblInvoiceCompany(this.session.ConnectionString);

                try
                {
                    rdg.Delete(id);
                }
                catch (Exception)
                {

                    return false;
                }

                return true;
            }
            else if (type == 'P')
            {
                var rdg = new RDGs.RDGtblInvoicePrivet(this.session.ConnectionString);

                try
                {
                    rdg.Delete(id);
                }
                catch (Exception)
                {
                    return false;
                }

                return true;
            }
            else
            {
                throw new ArgumentException("Type");
            }
        }

        public bool PiadInvoice(char type, int id)
        {
            if (type == 'C')
            {
                try
                {
                    var rdg = new RDGs.RDGtblInvoiceCompany(this.session.ConnectionString);

                    rdg.UpdateActive(id, false);
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else if (type == 'P')
            {
                try
                {
                    var rdg = new RDGs.RDGtblInvoicePrivet(this.session.ConnectionString);
                    rdg.UpdateActive(id, false);
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                throw new ArgumentException();
            }

            return true;
        }
        #endregion

        #region Get data tables
        public DataTable GetOrdersPrivet()
        {
            FildPrivetOrders(false);
            var dataTable = this.privetOrders.AsDataTable();
            
            dataTable.Columns[0].ColumnName = "Oprettet af";
            dataTable.Columns[1].ColumnName = "Oprettelsesdato";
            dataTable.Columns[2].ColumnName = "Kunde";
            dataTable.Columns[5].ColumnName = "Beskrivelse";
            dataTable.Columns[6].ColumnName = "Timer brugt";
            dataTable.Columns[7].ColumnName = "Ordrenr";
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
            dataTable.Columns[1].ColumnName = "Oprettelsesdato";
            dataTable.Columns[2].ColumnName = "Kunde";
            dataTable.Columns[5].ColumnName = "Beskrivelse";
            dataTable.Columns[6].ColumnName = "Timer brugt";
            dataTable.Columns[7].ColumnName = "Ordrenr";
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

            dataTable.Columns.Add("Ordrenr", typeof(int));
            dataTable.Columns.Add("Kunde", typeof(string));
            dataTable.Columns.Add("Kundetype", typeof(string));
            dataTable.Columns.Add("Oprettet af", typeof(string));
            dataTable.Columns.Add("Startdato", typeof(DateTime));
            dataTable.Columns.Add("Beskrivelse", typeof(string));

            foreach (DataRow item in privet.Rows)
            {
                if (DateIsAfterToday((DateTime)item.ItemArray[7]))
                {
                    dataTable.Rows.Add(new object[6] {
                    item.ItemArray[5],
                    item.ItemArray[2],
                    "Privat",
                    item.ItemArray[0],
                    item.ItemArray[7],
                    item.ItemArray[3],
                    });
                }
            }

            foreach (DataRow item in company.Rows)
            {
                if (DateIsAfterToday((DateTime)item.ItemArray[7]))
                {
                    dataTable.Rows.Add(new object[6] {
                    item.ItemArray[5],
                    item.ItemArray[2],
                    "Firma",
                    item.ItemArray[0],
                    item.ItemArray[7],
                    item.ItemArray[3],
                    });
                }
            }

            return dataTable;
        }

        public DataTable GetCustomersCompany()
        {
            FildCompanyCustomers(true);

            var dataTable =  this.companyCustomers.AsDataTable();

            dataTable.Columns[0].ColumnName = "Addresse";
            dataTable.Columns[1].ColumnName = "Tlf nr 2";
            dataTable.Columns[3].ColumnName = "Kundenr";
            dataTable.Columns[4].ColumnName = "Kontaktperson";
            dataTable.Columns[5].ColumnName = "CVR-nr";
            dataTable.Columns[6].ColumnName = "Email";
            dataTable.Columns[7].ColumnName = "Firmanavn";
            dataTable.Columns[8].ColumnName = "Tlf";
            dataTable.Columns[9].ColumnName = "Post Nr";
            dataTable.Columns[10].ColumnName = "By";

            dataTable.Columns.Remove("Active");
            return dataTable;
        }

        public DataTable GetCustomersPrivet()
        {
            FildPrivateCustomers(true);

            var dataTable = this.privateCustomers.AsDataTable();

            dataTable.Columns[1].ColumnName = "Tlf nr 2";
            dataTable.Columns[2].ColumnName = "Email";
            dataTable.Columns[3].ColumnName = "Addresse";
            dataTable.Columns[4].ColumnName = "Fornavn";
            dataTable.Columns[5].ColumnName = "Tlf nr";
            dataTable.Columns[6].ColumnName = "Postnr";
            dataTable.Columns[7].ColumnName = "By";
            dataTable.Columns[8].ColumnName = "Kundenr";
            dataTable.Columns[9].ColumnName = "Efternavn";

            dataTable.Columns.Remove("Active");

            return dataTable;
        }

        public DataTable GetWorkers()
        {
            FildWorkers(true);

            var dataTable = this.workers.AsDataTable();

            dataTable.Columns[1].ColumnName = "Addresse";
            dataTable.Columns[2].ColumnName = "Tlf nr 2";
            dataTable.Columns[3].ColumnName = "Email";
            dataTable.Columns[4].ColumnName = "Fornavn";
            dataTable.Columns[5].ColumnName = "Tlf nr";
            dataTable.Columns[6].ColumnName = "Postnr";
            dataTable.Columns[7].ColumnName = "By";
            dataTable.Columns[8].ColumnName = "Status";
            dataTable.Columns[9].ColumnName = "Id";
            dataTable.Columns[10].ColumnName = "Efternavn";


            dataTable.Columns.Remove("Active");

            return dataTable;
        }

        public DataTable GetDepartments()
        {
            Filddepartments(true);

            var dataTable = this.departments.AsDataTable();

            dataTable.Columns[1].ColumnName = "Afdelingsnavn";
            dataTable.Columns[2].ColumnName = "CVR-nr";
            dataTable.Columns[3].ColumnName = "Afdelingsleder";
            dataTable.Columns[4].ColumnName = "Adresse";
            dataTable.Columns[5].ColumnName = "Postnr";
            dataTable.Columns[6].ColumnName = "By";
            dataTable.Columns[7].ColumnName = "Tlf nr";
            dataTable.Columns[8].ColumnName = "Tlf nr2";
            dataTable.Columns[9].ColumnName = "Email";

            dataTable.Columns.Remove("Active");

            return dataTable;
        }

        public DataTable GetInvoice()
        {
            var dataTable = new DataTable();

            dataTable.Columns.Add("Faktura nr", typeof(string));
            dataTable.Columns.Add("Kunde", typeof(string));
            dataTable.Columns.Add("Orders nr 1", typeof(string));
            dataTable.Columns.Add("Orders nr 2", typeof(string));
            dataTable.Columns.Add("Orders nr 3", typeof(string));
            dataTable.Columns.Add("Orders nr 4", typeof(string));
            dataTable.Columns.Add("Orders nr 5", typeof(string));
            dataTable.Columns.Add("Orders nr 6", typeof(string));
            dataTable.Columns.Add("Orders nr 7", typeof(string));
            dataTable.Columns.Add("Orders nr 8", typeof(string));
            dataTable.Columns.Add("Orders nr 9", typeof(string));
            dataTable.Columns.Add("Orders nr 10", typeof(string));
            dataTable.Columns.Add("Orders nr 11", typeof(string));
            dataTable.Columns.Add("Orders nr 12", typeof(string));
            dataTable.Columns.Add("Orders nr 13", typeof(string));
            dataTable.Columns.Add("Orders nr 14", typeof(string));
            dataTable.Columns.Add("Orders nr 15", typeof(string));
            dataTable.Columns.Add("Orders nr 16", typeof(string));
            dataTable.Columns.Add("Orders nr 17", typeof(string));
            dataTable.Columns.Add("Orders nr 18", typeof(string));
            dataTable.Columns.Add("Orders nr 19", typeof(string));
            dataTable.Columns.Add("Orders nr 20", typeof(string));

            var rdgCustomersC = new RDGs.RDGtblCompanyCustomers(this.session.ConnectionString);
            var invoiceCompany = new RDGs.RDGtblInvoiceCompany(this.session.ConnectionString).Get(true);
            foreach (var item in invoiceCompany)
            {
                var customers = rdgCustomersC.Find((int)item.Order[0]);

                dataTable.Rows.Add(
                    "C-" + item.Id,
                    "#" + customers.CompanyCustomersNo + ": " + customers.Name,
                    item.Order[0].ToString(),
                    item.Order[1].ToString(),
                    item.Order[2].ToString(),
                    item.Order[3].ToString(),
                    item.Order[4].ToString(),
                    item.Order[5].ToString(),
                    item.Order[6].ToString(),
                    item.Order[7].ToString(),
                    item.Order[8].ToString(),
                    item.Order[9].ToString(),                    
                    item.Order[10].ToString(),
                    item.Order[11].ToString(),
                    item.Order[12].ToString(),
                    item.Order[13].ToString(),
                    item.Order[14].ToString(),                    
                    item.Order[15].ToString(),
                    item.Order[16].ToString(),
                    item.Order[17].ToString(),
                    item.Order[18].ToString(),
                    item.Order[19].ToString());
            }

            var rdgCustomersP = new RDGs.RDGtblPrivateCustomers(this.session.ConnectionString);
            var invoicePrivet = new RDGs.RDGtblInvoicePrivet(this.session.ConnectionString).Get(true);

            foreach (var item in invoicePrivet)
            {
                var customers = rdgCustomersP.Find((int)item.Order[0]);

                dataTable.Rows.Add(
                    "P-" + item.Id,
                    "#" + customers.PrivateCustomersNo + ": " + customers.Name + " " + customers.Surname,
                    item.Order[0].ToString(),
                    item.Order[1].ToString(),
                    item.Order[2].ToString(),
                    item.Order[3].ToString(),
                    item.Order[4].ToString(),
                    item.Order[5].ToString(),
                    item.Order[6].ToString(),
                    item.Order[7].ToString(),
                    item.Order[8].ToString(),
                    item.Order[9].ToString(),
                    item.Order[10].ToString(),
                    item.Order[11].ToString(),
                    item.Order[12].ToString(),
                    item.Order[13].ToString(),
                    item.Order[14].ToString(),
                    item.Order[15].ToString(),
                    item.Order[16].ToString(),
                    item.Order[17].ToString(),
                    item.Order[18].ToString(),
                    item.Order[19].ToString());
            }

            return dataTable;
        }

        /// <summary>
        /// Testing ignores house and less
        /// </summary>
        /// <param name="date">test base on</param>
        /// <returns>if date is gather then now returns true else false</returns>
        public static bool DateIsAfterToday(DateTime date)
        {
            if (date.Year > DateTime.Now.Year)
            {
                return true;
            }
            else if (date.Year == DateTime.Now.Year)
            {
                if (date.Month > DateTime.Now.Month)
                {
                    return true;
                }
                else if (date.Month == DateTime.Now.Month)
                {
                    if (date.Day >= DateTime.Now.Day)
                    {
                        return true;
                    }
                }
            }

            return false;
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
