using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SqlClient = System.Data.SqlClient;

namespace RDGs
{
    public class RDGtblPrivetOrders
    {
        private string connectionString;

        public RDGtblPrivetOrders(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Interface.IprivetOrder> GetBaseCustomer(int id, bool? paid)
        {
            var list = new List<Interface.IprivetOrder>();

            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext(this.connectionString))
            {
                IQueryable<tblPrivetOrder> privetOrders;

                if (paid == null)
                {
                    privetOrders = from tblPrivetOrder in dbContext.tblPrivetOrders
                                   where tblPrivetOrder.customers == id
                                   select tblPrivetOrder;
                }
                else
                {
                    privetOrders = from tblPrivetOrder in dbContext.tblPrivetOrders
                                   where tblPrivetOrder.customers == id && tblPrivetOrder.paid == paid
                                   select tblPrivetOrder;
                }

                foreach (var item in privetOrders)
                {
                    list.Add(tblPrivetOrderToPrivetOrder(item));
                }
            }

            return list;
        }


        public List<Interface.IprivetOrder> Get(bool? paid)
        {
            var list = new List<Interface.IprivetOrder>();

            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext(this.connectionString))
            {
                IQueryable<tblPrivetOrder> privetOrders;

                if (paid == null)
                {
                    privetOrders = from tblPrivetOrder in dbContext.tblPrivetOrders
                                   select tblPrivetOrder;
                }
                else
                {
                    privetOrders = from tblPrivetOrder in dbContext.tblPrivetOrders
                                   where tblPrivetOrder.paid == paid
                                   select tblPrivetOrder;
                }

                foreach (var item in privetOrders)
                {
                    list.Add(tblPrivetOrderToPrivetOrder(item));
                }
            }

            return list;
        }

        public InterfaceAdaptor.PrivetOrder tblPrivetOrderToPrivetOrder(tblPrivetOrder item)
        {
            return new InterfaceAdaptor.PrivetOrder()
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
                        StautsNo = item.tblWorker.tblWorkerStatus.statusNo,
                        Staus = item.tblWorker.tblWorkerStatus.status
                    },
                    WorkNo = item.tblWorker.workNo
                },
                CreateDate = (DateTime)item.createdDate,
                Customer = new InterfaceAdaptor.PrivetCustomer()
                {
                    Active = (bool)item.tblPrivateCustomer.active,
                    AltPhoneNo = item.tblPrivateCustomer.altPhoneNo,
                    Email = item.tblPrivateCustomer.email,
                    HomeAddress = item.tblPrivateCustomer.homeAddress,
                    Name = item.tblPrivateCustomer.name,
                    PhoneNo = item.tblPrivateCustomer.phoneNo,
                    PostNo = new InterfaceAdaptor.PostNo()
                    {
                        City = item.tblPrivateCustomer.tblPostNo.city,
                        Id = item.tblPrivateCustomer.tblPostNo.ID,
                        PostNumber = item.tblPrivateCustomer.tblPostNo.postNo
                    },
                    PrivateCustomersNo = item.tblPrivateCustomer.privateCustomersNo,
                    Surname = item.tblPrivateCustomer.surname
                },
                DateSendBill = item.dateSendBill,
                DaysToPaid = (int)item.daysToPaid,
                DescriptionTask = item.descriptionTask,
                HourUse = Convert.ToDouble(item.hoursUse),
                InvoiceNo = item.invoiceNo,
                Paid = (bool)item.paid,
                PaidHour = Convert.ToDouble(item.paidHour),
                PaidToAcc = item.paidToACC,
                TaskDate = item.taskDate,
            };
        }

        public Interface.IprivetOrder Find(int id)
        {
            Interface.IprivetOrder privetOrder = null;

            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext(this.connectionString))
            {
                var privetOrderFound = dbContext.tblPrivetOrders.SingleOrDefault(
                    x => x.invoiceNo == id);

                privetOrder = new InterfaceAdaptor.PrivetOrder()
                {
                    CreateBy = new InterfaceAdaptor.Worker()
                    {
                        Active = (bool)privetOrderFound.tblWorker.active,
                        Address = privetOrderFound.tblWorker.homeAddress,
                        AltPhoneNo = privetOrderFound.tblWorker.altPhoneNo,
                        Email = privetOrderFound.tblWorker.email,
                        Name = privetOrderFound.tblWorker.name,
                        PhoneNo = privetOrderFound.tblWorker.phoneNo,
                        PostNo = new InterfaceAdaptor.PostNo()
                        {
                            City = privetOrderFound.tblWorker.tblPostNo.city,
                            Id = privetOrderFound.tblWorker.tblPostNo.ID,
                            PostNumber = privetOrderFound.tblWorker.tblPostNo.postNo
                        },
                        Surname = privetOrderFound.tblWorker.surname,
                        WorkerStatus = new InterfaceAdaptor.WorkerStatus()
                        {
                            StautsNo = privetOrderFound.tblWorker.tblWorkerStatus.statusNo,
                            Staus = privetOrderFound.tblWorker.tblWorkerStatus.status
                        },
                        WorkNo = privetOrderFound.tblWorker.workNo
                    },
                    CreateDate = (DateTime)privetOrderFound.createdDate,
                    Customer = new InterfaceAdaptor.PrivetCustomer()
                    {
                        Active = (bool)privetOrderFound.tblPrivateCustomer.active,
                        AltPhoneNo = privetOrderFound.tblPrivateCustomer.altPhoneNo,
                        Email = privetOrderFound.tblPrivateCustomer.email,
                        HomeAddress = privetOrderFound.tblPrivateCustomer.homeAddress,
                        Name = privetOrderFound.tblPrivateCustomer.name,
                        PhoneNo = privetOrderFound.tblPrivateCustomer.phoneNo,
                        PostNo = new InterfaceAdaptor.PostNo()
                        {
                            City = privetOrderFound.tblPrivateCustomer.tblPostNo.city,
                            Id = privetOrderFound.tblPrivateCustomer.tblPostNo.ID,
                            PostNumber = privetOrderFound.tblPrivateCustomer.tblPostNo.postNo
                        },
                        PrivateCustomersNo = privetOrderFound.tblPrivateCustomer.privateCustomersNo,
                        Surname = privetOrderFound.tblPrivateCustomer.surname
                    },
                    DateSendBill = privetOrderFound.dateSendBill,
                    DaysToPaid = (int)privetOrderFound.daysToPaid,
                    DescriptionTask = privetOrderFound.descriptionTask,
                    HourUse = Convert.ToDouble(privetOrderFound.hoursUse),
                    InvoiceNo = privetOrderFound.invoiceNo,
                    Paid = (bool)privetOrderFound.paid,
                    PaidHour = Convert.ToDouble(privetOrderFound.paidHour),
                    PaidToAcc = privetOrderFound.paidToACC,
                    TaskDate = privetOrderFound.taskDate,
                };
            }

            return privetOrder;
        }

        public void Add(Interface.IprivetOrder privetOrder)
        {
            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext(this.connectionString))
            {
                var newPrivetOrderFound = new tblPrivetOrder()
                {
                    createBy = privetOrder.CreateBy.WorkNo,
                    createdDate = privetOrder.CreateDate,
                    customers = privetOrder.Customer.PrivateCustomersNo,
                    dateSendBill = privetOrder.DateSendBill,
                    daysToPaid = privetOrder.DaysToPaid,
                    descriptionTask = privetOrder.DescriptionTask,
                    hoursUse = Convert.ToDecimal(privetOrder.HourUse),
                    paid = privetOrder.Paid,
                    paidHour = Convert.ToDecimal(privetOrder.PaidHour),
                    paidToACC = privetOrder.PaidToAcc,
                    taskDate = privetOrder.TaskDate,
                };
                
                if (newPrivetOrderFound.dateSendBill != null)
                {
                    if (newPrivetOrderFound.dateSendBill.Value.Year == 1)
                    {
                        newPrivetOrderFound.dateSendBill = null;
                    }
                }
                

                dbContext.tblPrivetOrders.InsertOnSubmit(newPrivetOrderFound);
                dbContext.SubmitChanges();
            }
        }


        public void Update(Interface.IprivetOrder privetOrder)
        {
            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext(this.connectionString))
            {
                var privetOrderUpteing = dbContext.tblPrivetOrders.SingleOrDefault(
                    x => x.invoiceNo == privetOrder.InvoiceNo);

                if (privetOrder.CreateBy != null)
                {
                    privetOrderUpteing.createBy = privetOrder.CreateBy.WorkNo;
                }

                if (privetOrder.CreateDate != new DateTime())
                {
                    privetOrderUpteing.createdDate = privetOrder.CreateDate;
                }

                if (privetOrder.Customer != null)
                {
                    privetOrderUpteing.customers = privetOrder.Customer.PrivateCustomersNo;
                }

                if (privetOrder.DateSendBill != new DateTime())
                {
                    privetOrderUpteing.dateSendBill = privetOrder.DateSendBill;
                }

                if (privetOrder.DaysToPaid != -1)
                {
                    privetOrderUpteing.daysToPaid = privetOrder.DaysToPaid;
                }

                if (privetOrder.PaidToAcc != -1)
                {
                    privetOrderUpteing.paidToACC = privetOrder.PaidToAcc;
                }

                if (privetOrder.DescriptionTask != null)
                {
                    privetOrderUpteing.descriptionTask = privetOrder.DescriptionTask;
                }

                if (privetOrder.HourUse != -1)
                {
                    privetOrderUpteing.hoursUse = Convert.ToDecimal(privetOrder.HourUse);
                }

                privetOrderUpteing.paid = privetOrder.Paid;

                if (privetOrder.PaidHour != -1)
                {
                    privetOrderUpteing.paidHour = Convert.ToDecimal(privetOrder.PaidHour);
                }
                if (privetOrder.TaskDate != new DateTime())
                {
                    privetOrderUpteing.taskDate = privetOrder.TaskDate;
                }

                dbContext.SubmitChanges();
            }
        }

        public void Delete(int id)
        {
            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext(this.connectionString))
            {
                var deleingItem = dbContext.tblPrivetOrders.SingleOrDefault(
                    x => x.invoiceNo == id);

                var deleteInfo = new StringBuilder();
                deleteInfo.Append("[tblPrivetOrders] { ");
                deleteInfo.Append("invoiceNo = " + deleingItem.invoiceNo.ToString() + ", ");
                deleteInfo.Append("taskDate = " + deleingItem.taskDate.ToString() + ", ");
                deleteInfo.Append("descriptionTask = " + deleingItem.descriptionTask + ", ");
                deleteInfo.Append("daysToPaid = " + deleingItem.daysToPaid.ToString() + ", ");
                deleteInfo.Append("hoursUse = " + deleingItem.hoursUse.ToString() + ", ");
                deleteInfo.Append("paidHour = " + deleingItem.paidHour.ToString() + ", ");
                deleteInfo.Append("createBy = " + deleingItem.createBy.ToString() + ", ");
                deleteInfo.Append("paidToACC = " + deleingItem.paidToACC.ToString() + ", ");
                deleteInfo.Append("customers = " + deleingItem.customers.ToString() + ", ");
                deleteInfo.Append("paid = ");
                if ((bool)deleingItem.paid) { deleteInfo.Append("1"); } else { deleteInfo.Append("0"); }
                deleteInfo.Append(" }");

                dbContext.tblPrivetOrders.DeleteOnSubmit(deleingItem);
                dbContext.tblDeleteItems.InsertOnSubmit(new tblDeleteItem()
                {
                    deleteDate = DateTime.Now,
                    itemInfo = deleteInfo.ToString(),
                    restored = false
                });

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
                var cmd = new SqlClient.SqlCommand(@"SELECT IDENT_CURRENT ('[tblPrivetOrders]')", conn);

                conn.Open();

                decimal result = (decimal)cmd.ExecuteScalar();

                conn.Close();

                return Convert.ToInt32(result) + 1;
            }
        }
    }
}
