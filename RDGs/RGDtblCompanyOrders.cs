using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SqlClient = System.Data.SqlClient;


namespace RDGs
{
    public class RGDtblCompanyOrders
    {
        private string connectionString;

        public RGDtblCompanyOrders(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Interface.IcompanyOrder> Get(bool? paid)
        {
            var list = new List<Interface.IcompanyOrder>();

            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext(this.connectionString))
            {
                IQueryable<tblCompanyOrder> companyOrders;

                if (paid == null)
                {
                    companyOrders = from tblCompanyOrder in dbContext.tblCompanyOrders
                                    select tblCompanyOrder;
                }
                else
                {
                    companyOrders = from tblCompanyOrder in dbContext.tblCompanyOrders
                                    where tblCompanyOrder.paid == paid
                                    select tblCompanyOrder;
                }

                foreach (var item in companyOrders)
                {
                    list.Add(new InterfaceAdaptor.CompanyOrder()
                    {
                        CreateBy = new InterfaceAdaptor.Worker()
                        {
                            Active = (bool)item.tblWorker.active,
                            Address = item.tblWorker.homeAddress,
                            AltPhoneNo = item.tblWorker.altPhoneNo,
                            Email = item.tblWorker.email,
                            Name = item.tblWorker.name,
                            PhoneNo = item.tblWorker.phoneNo,
                            PostNo = new InterfaceAdaptor.PostNo()
                            {
                                City = item.tblWorker.tblPostNo.city,
                                Id = item.tblWorker.tblPostNo.ID,
                                PostNumber = item.tblWorker.tblPostNo.postNo
                            },
                            Surname = item.tblWorker.surname,
                            WorkerStatus = new InterfaceAdaptor.WorkerStatus()
                            {
                                Staus = item.tblWorker.tblWorkerStatus.status,
                                StautsNo = item.tblWorker.tblWorkerStatus.statusNo
                            },
                            WorkNo = item.tblWorker.workNo,
                        },
                        CreateDate = (DateTime)item.createdDate,
                        Customer = new InterfaceAdaptor.CompanyCustomer()
                        {
                            Active = (bool)item.tblCompanyCustomer.active,
                            Address = item.tblCompanyCustomer._address,
                            AltPhoneNo = item.tblCompanyCustomer.altPhoneNo,
                            CompanyCustomersNo = item.tblCompanyCustomer.companyCustomersNo,
                            ContactPerson = item.tblCompanyCustomer.companyContactPerson,
                            CvrNo = item.tblCompanyCustomer.cvrNo,
                            Email = item.tblCompanyCustomer.email,
                            Name = item.tblCompanyCustomer.companyName,
                            PhoneNo = item.tblCompanyCustomer.phoneNo,
                            PostNo = new InterfaceAdaptor.PostNo()
                            {
                                City = item.tblCompanyCustomer.tblPostNo.city,
                                Id = item.tblCompanyCustomer.tblPostNo.ID,
                                PostNumber = item.tblCompanyCustomer.tblPostNo.postNo
                            },
                        },
                        DateSendBill = (DateTime)item.dateSendBill,
                        DaysToPaid = (int)item.daysToPaid,
                        DescriptionTask = item.descriptionTask,
                        HoutsUse = (int)item.hoursUse,
                        InvoiceNo = item.invoiceNo,
                        Paid = (bool)item.paid,
                        PaidHour = (int)item.paidHour,
                        PaidToAcc = (int)item.paidToACC,
                        TaskDate = (DateTime)item.taskDate
                    });
                }
            }

            return list;
        }

        public Interface.IcompanyOrder Find(int id)
        {
            Interface.IcompanyOrder companyOrder = null;

            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext(this.connectionString))
            {
                var companyOrderFound = dbContext.tblCompanyOrders.SingleOrDefault(
                    x => x.invoiceNo == id);

                companyOrder = new InterfaceAdaptor.CompanyOrder()
                {
                    CreateBy = new InterfaceAdaptor.Worker()
                    {
                        Active = (bool)companyOrderFound.tblWorker.active,
                        Address = companyOrderFound.tblWorker.homeAddress,
                        AltPhoneNo = companyOrderFound.tblWorker.altPhoneNo,
                        Email = companyOrderFound.tblWorker.email,
                        Name = companyOrderFound.tblWorker.name,
                        PhoneNo = companyOrderFound.tblWorker.phoneNo,
                        PostNo = new InterfaceAdaptor.PostNo()
                        {
                            City = companyOrderFound.tblWorker.tblPostNo.city,
                            Id = companyOrderFound.tblWorker.tblPostNo.ID,
                            PostNumber = companyOrderFound.tblWorker.tblPostNo.postNo
                        },
                        Surname = companyOrderFound.tblWorker.surname,
                        WorkerStatus = new InterfaceAdaptor.WorkerStatus()
                        {
                            Staus = companyOrderFound.tblWorker.tblWorkerStatus.status,
                            StautsNo = companyOrderFound.tblWorker.tblWorkerStatus.statusNo
                        },
                        WorkNo = companyOrderFound.tblWorker.workNo,
                    },
                    CreateDate = (DateTime)companyOrderFound.createdDate,
                    Customer = new InterfaceAdaptor.CompanyCustomer()
                    {
                        Active = (bool)companyOrderFound.tblCompanyCustomer.active,
                        Address = companyOrderFound.tblCompanyCustomer._address,
                        AltPhoneNo = companyOrderFound.tblCompanyCustomer.altPhoneNo,
                        CompanyCustomersNo = companyOrderFound.tblCompanyCustomer.companyCustomersNo,
                        ContactPerson = companyOrderFound.tblCompanyCustomer.companyContactPerson,
                        CvrNo = companyOrderFound.tblCompanyCustomer.cvrNo,
                        Email = companyOrderFound.tblCompanyCustomer.email,
                        Name = companyOrderFound.tblCompanyCustomer.companyName,
                        PhoneNo = companyOrderFound.tblCompanyCustomer.phoneNo,
                        PostNo = new InterfaceAdaptor.PostNo()
                        {
                            City = companyOrderFound.tblCompanyCustomer.tblPostNo.city,
                            Id = companyOrderFound.tblCompanyCustomer.tblPostNo.ID,
                            PostNumber = companyOrderFound.tblCompanyCustomer.tblPostNo.postNo
                        },
                    },
                    DateSendBill = (DateTime)companyOrderFound.dateSendBill,
                    DaysToPaid = (int)companyOrderFound.daysToPaid,
                    DescriptionTask = companyOrderFound.descriptionTask,
                    HoutsUse = (double)companyOrderFound.hoursUse,
                    InvoiceNo = companyOrderFound.invoiceNo,
                    Paid = (bool)companyOrderFound.paid,
                    PaidHour = (double)companyOrderFound.paidHour,
                    PaidToAcc = (int)companyOrderFound.paidToACC,
                    TaskDate = (DateTime)companyOrderFound.taskDate
                };
            }

            return companyOrder;
        }

        public void Add(Interface.IcompanyOrder companyOrder)
        {
            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext(this.connectionString))
            {
                var newCompanyOrder = new tblCompanyOrder()
                {
                    createBy = companyOrder.CreateBy.WorkNo,
                    createdDate = companyOrder.CreateDate,
                    customer = companyOrder.Customer.CompanyCustomersNo,
                    dateSendBill = companyOrder.DateSendBill,
                    daysToPaid = companyOrder.DaysToPaid,
                    descriptionTask = companyOrder.DescriptionTask,
                    hoursUse = Convert.ToDecimal(companyOrder.HoutsUse),
                    paid = companyOrder.Paid,
                    paidHour = Convert.ToDecimal(companyOrder.PaidHour),
                    paidToACC = companyOrder.PaidToAcc,
                    taskDate = companyOrder.TaskDate,
                };

                dbContext.tblCompanyOrders.InsertOnSubmit(newCompanyOrder);
                dbContext.SubmitChanges();
            }
        }

        public void Update(Interface.IcompanyOrder companyOrder)
        {
            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext(this.connectionString))
            {
                var companyOrderUpdateing = dbContext.tblCompanyOrders.SingleOrDefault(
                    x => x.invoiceNo == companyOrder.InvoiceNo);

                companyOrderUpdateing.createBy = companyOrder.CreateBy.WorkNo;
                companyOrderUpdateing.createdDate = companyOrder.CreateDate;
                companyOrderUpdateing.customer = companyOrder.Customer.CompanyCustomersNo;
                companyOrderUpdateing.dateSendBill = companyOrder.DateSendBill;
                companyOrderUpdateing.daysToPaid = companyOrder.DaysToPaid;
                companyOrderUpdateing.descriptionTask = companyOrder.DescriptionTask;
                companyOrderUpdateing.hoursUse = Convert.ToDecimal(companyOrder.HoutsUse);
                companyOrderUpdateing.paid = companyOrder.Paid;
                companyOrderUpdateing.paidHour = Convert.ToDecimal(companyOrder.PaidHour);
                companyOrderUpdateing.paidToACC = companyOrder.PaidToAcc;
                companyOrderUpdateing.taskDate = companyOrder.TaskDate;

                dbContext.SubmitChanges();
            }
        }

        public void Delete(int id)
        {
            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext(this.connectionString))
            {
                var companyOrderDeleteing = dbContext.tblCompanyOrders.SingleOrDefault(
                    x => x.invoiceNo == id);

                var deleteInfo = new StringBuilder();
                deleteInfo.Append("[tblCompanyOrders] { ");
                deleteInfo.Append("invoiceNo = " + companyOrderDeleteing.ToString() + ", ");
                deleteInfo.Append("createdDate = " + companyOrderDeleteing.createdDate.ToString() + ", ");
                deleteInfo.Append("taskDate = " + companyOrderDeleteing.taskDate.ToString() + ", ");
                deleteInfo.Append("descriptionTask = " + companyOrderDeleteing.descriptionTask + ", ");
                deleteInfo.Append("dateSendBill = " + companyOrderDeleteing.descriptionTask + ", ");
                deleteInfo.Append("daysToPaid = " + companyOrderDeleteing.daysToPaid.ToString() + ", ");
                deleteInfo.Append("hoursUse = " + companyOrderDeleteing.hoursUse.ToString() + ", ");
                deleteInfo.Append("paidHour = " + companyOrderDeleteing.paidHour.ToString() + ", ");
                deleteInfo.Append("createBy = " + companyOrderDeleteing.createBy.ToString() + ", ");
                deleteInfo.Append("paidToACC = " + companyOrderDeleteing.paidToACC.ToString() + ", ");
                deleteInfo.Append("customer = " + companyOrderDeleteing.customer.ToString() + ", ");
                deleteInfo.Append("paid = ");
                if ((bool)companyOrderDeleteing.paid) { deleteInfo.Append("1"); } else { deleteInfo.Append("0"); }
                deleteInfo.Append(" }");

                dbContext.tblDeleteItems.InsertOnSubmit(new tblDeleteItem()
                {
                    deleteDate = DateTime.Now,
                    itemInfo = deleteInfo.ToString(),
                    restored = false
                });

                dbContext.tblCompanyOrders.DeleteOnSubmit(companyOrderDeleteing);

                dbContext.SubmitChanges();
            }
        }

        public int NextId
        {
            get
            {
                string connString = string.Empty;
                using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext(this.connectionString))
                {
                    connString = dbContext.Connection.ConnectionString;
                }

                var conn = new SqlClient.SqlConnection(connString);
                var cmd = new SqlClient.SqlCommand(@"SELECT IDENT_CURRENT ('[tblCompanyOrders]')", conn);

                conn.Open();

                decimal result = (decimal)cmd.ExecuteScalar();

                conn.Close();

                return Convert.ToInt32(result) + 1;
            }
        }
    }
}
