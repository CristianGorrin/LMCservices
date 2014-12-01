using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDGs
{
    public class RDGtblPrivetOrders
    {
        public List<InterfaceAdaptor.PrivetOrder> Get(bool? paid)
        {
            var list = new List<InterfaceAdaptor.PrivetOrder>();

            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext())
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
                    list.Add(new InterfaceAdaptor.PrivetOrder()
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
                        DateSendBill = (DateTime)item.dateSendBill,
                        DaysToPaid = (int)item.daysToPaid,
                        DescriptionTask = item.descriptionTask,
                        HourUse = Convert.ToDouble(item.hoursUse),
                        InvoiceNo = item.invoiceNo,
                        Paid = (bool)item.paid,
                        PaidHour = Convert.ToDouble(item.paidHour),
                        PaidToAcc = item.paidToACC,
                        TaskDate = item.taskDate,
                    });
                }
            }

            return list;
        }

        public InterfaceAdaptor.PrivetOrder Find(int id)
        {
            InterfaceAdaptor.PrivetOrder privetOrder = null;

            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext())
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
                    DateSendBill = (DateTime)privetOrderFound.dateSendBill,
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

        public void Add(InterfaceAdaptor.PrivetOrder privetOrder)
        {
            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext())
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

                dbContext.tblPrivetOrders.InsertOnSubmit(newPrivetOrderFound);
                dbContext.SubmitChanges();
            }
        }


        public void Update(InterfaceAdaptor.PrivetOrder privetOrder)
        {
            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext())
            {
                var privetOrderUpteing = dbContext.tblPrivetOrders.SingleOrDefault(
                    x => x.invoiceNo == privetOrder.InvoiceNo);

                privetOrderUpteing.createBy = privetOrder.CreateBy.WorkNo;
                privetOrderUpteing.createdDate = privetOrder.CreateDate;
                privetOrderUpteing.customers = privetOrder.Customer.PrivateCustomersNo;
                privetOrderUpteing.dateSendBill = privetOrder.DateSendBill;
                privetOrderUpteing.daysToPaid = privetOrder.DaysToPaid;
                privetOrderUpteing.descriptionTask = privetOrder.DescriptionTask;
                privetOrderUpteing.hoursUse = Convert.ToDecimal(privetOrder.HourUse);
                privetOrderUpteing.paid = privetOrder.Paid;
                privetOrderUpteing.paidHour = Convert.ToDecimal(privetOrder.PaidHour);
                privetOrderUpteing.taskDate = privetOrder.TaskDate;

                dbContext.SubmitChanges();
            }
        }

        public void Delete(int id)
        {
            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext())
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
                int id;

                using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext())
                {
                    id = dbContext.tblPrivetOrders.Max(x => x.invoiceNo);
                    id++;
                }

                return id;
            }
        }
    }
}
