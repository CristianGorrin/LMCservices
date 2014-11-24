using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Orders;
using Interface;

namespace UnitTestProject
{
    [TestClass]
    public class TestOrders
    {
        [TestMethod]
        public void PrivetOrder()
        {
            var dateTime = DateTime.Now;
            var privetOrder = new Orders.PrivetOrder(new TestingOrder.Worker1(), dateTime, new TestingOrder.CustomerPrivet1(),
                dateTime, 5, "testDescriptionTask", 5.5D, 55, false, 200, 1594564564, dateTime);

            // Testing get
            Assert.AreEqual(new TestingOrder.Worker1().WorkNo, privetOrder.CreateBy.WorkNo);
            Assert.AreEqual(dateTime, privetOrder.CreateDate);
            Assert.AreEqual(new TestingOrder.CustomerPrivet1().PrivateCustomersNo, privetOrder.Customer.PrivateCustomersNo);
            Assert.AreEqual(dateTime, privetOrder.DateSendBill);
            Assert.AreEqual(5, privetOrder.DaysToPaid);
            Assert.AreEqual("testDescriptionTask", privetOrder.DescriptionTask);
            Assert.AreEqual(5.5D, privetOrder.HourUse);
            Assert.AreEqual(55, privetOrder.InvoiceNo);
            Assert.AreEqual(false, privetOrder.Paid);
            Assert.AreEqual(200, privetOrder.PaidHour);
            Assert.AreEqual(1594564564, privetOrder.PaidToAcc);
            Assert.AreEqual(dateTime, privetOrder.TaskDate);
            
            // Testing Set
            dateTime = dateTime.AddDays(2);
            privetOrder.CreateBy = new TestingOrder.Worker2();
            privetOrder.CreateDate = dateTime;
            privetOrder.Customer = new TestingOrder.CustomerPrivet2();
            privetOrder.DateSendBill = dateTime;
            privetOrder.DaysToPaid = 25;
            privetOrder.DescriptionTask = "newTestDescriptionTask";
            privetOrder.HourUse = 4.6D;
            privetOrder.Paid = true;
            privetOrder.PaidHour = 230D;
            privetOrder.PaidToAcc = 8512;
            privetOrder.TaskDate = dateTime;

            var createBy = (Interface.Iworker)new PrivateObject(privetOrder).GetField("crateBy");
            Assert.AreEqual(new TestingOrder.Worker2().WorkNo, createBy.WorkNo);
            var createDate = (DateTime)new PrivateObject(privetOrder).GetField("createdDate");
            Assert.AreEqual(dateTime, createDate);
            var customer = (Interface.IprivetCustomer)new PrivateObject(privetOrder).GetField("customer");
            Assert.AreEqual(new TestingOrder.CustomerPrivet2().PrivateCustomersNo, customer.PrivateCustomersNo);
            var dateSendBill = (DateTime)new PrivateObject(privetOrder).GetField("dateSendBill");
            Assert.AreEqual(dateTime, dateSendBill);
            var daysToPaid = (int)new PrivateObject(privetOrder).GetField("daysToPaid");
            Assert.AreEqual(25, daysToPaid);
            var descriptionTask = (string)new PrivateObject(privetOrder).GetField("descriptionTask");
            Assert.AreEqual("newTestDescriptionTask", descriptionTask);
            var hoursUse = (double)new PrivateObject(privetOrder).GetField("hoursUse");
            Assert.AreEqual(4.6D, hoursUse);
            var paid = (bool)new PrivateObject(privetOrder).GetField("paid");
            Assert.AreEqual(true, paid);
            var paidHour = (double)new PrivateObject(privetOrder).GetField("paidHour");
            Assert.AreEqual(230D, paidHour);
            var paidToAcc = (int)new PrivateObject(privetOrder).GetField("paidToAcc");
            Assert.AreEqual(8512, paidToAcc);
            var taskDate = (DateTime)new PrivateObject(privetOrder).GetField("taskDate");
            Assert.AreEqual(dateTime, taskDate);
        }

        [TestMethod]
        public void CompanyOrdersd()
        {
            var dateTime = DateTime.Now;
            var companyOrdersd = new Orders.CompanyOrder(new TestingOrder.Worker1(), dateTime, 
                new TestingOrder.CustomerCompany1(), dateTime, 5, "testDescriptonTask",
                65D, 15, false, 320.5D, 1564, dateTime);

            // testing get
            Assert.AreEqual(new TestingOrder.Worker1().WorkNo, companyOrdersd.CreateBy.WorkNo);
            Assert.AreEqual(dateTime, companyOrdersd.CreateDate);
            Assert.AreEqual(new TestingOrder.CustomerCompany1().CompanyCustomersNo, companyOrdersd.Customer.CompanyCustomersNo);
            Assert.AreEqual(dateTime, companyOrdersd.DateSendBill);
            Assert.AreEqual(5, companyOrdersd.DaysToPaid);
            Assert.AreEqual("testDescriptonTask", companyOrdersd.DescriptionTask);
            Assert.AreEqual(65D, companyOrdersd.HoutsUse);
            Assert.AreEqual(15, companyOrdersd.InvoiceNo);
            Assert.AreEqual(false, companyOrdersd.Paid);
            Assert.AreEqual(320.5D, companyOrdersd.PaidHour);
            Assert.AreEqual(1564, companyOrdersd.PaidToAcc);
            Assert.AreEqual(dateTime, companyOrdersd.TaskDate);

            dateTime = dateTime.AddDays(5);

            // testing set
            companyOrdersd.CreateBy = new TestingOrder.Worker2();
            companyOrdersd.CreateDate = dateTime;
            companyOrdersd.Customer = new TestingOrder.CustomerCompany2();
            companyOrdersd.DateSendBill = dateTime;
            companyOrdersd.DaysToPaid = 43;
            companyOrdersd.DescriptionTask = "newTestDescriptonTask";
            companyOrdersd.HoutsUse = 16D;
            companyOrdersd.Paid = true;
            companyOrdersd.PaidHour = 465D;
            companyOrdersd.PaidToAcc = 648641;
            companyOrdersd.TaskDate = dateTime;

            var crateBy = (Interface.Iworker)new PrivateObject(companyOrdersd).GetField("crateBy");
            Assert.AreEqual(new TestingOrder.Worker2().WorkNo, crateBy.WorkNo);
            var createdDate = (DateTime)new PrivateObject(companyOrdersd).GetField("createdDate");
            Assert.AreEqual(dateTime, createdDate);
            var customer = (Interface.IcompanyCustomer)new PrivateObject(companyOrdersd).GetField("customer");
            Assert.AreEqual(new TestingOrder.CustomerCompany2().CompanyCustomersNo, customer.CompanyCustomersNo);
            var dateSenndBill = (DateTime)new PrivateObject(companyOrdersd).GetField("dateSenndBill");
            Assert.AreEqual(dateTime, dateSenndBill);
            var daysToPaid = (int)new PrivateObject(companyOrdersd).GetField("daysToPaid");
            Assert.AreEqual(43, daysToPaid);
            var descriptionTask = (string)new PrivateObject(companyOrdersd).GetField("descriptionTask");
            Assert.AreEqual("newTestDescriptonTask", descriptionTask);
            var hoursUse = (double)new PrivateObject(companyOrdersd).GetField("hoursUse");
            Assert.AreEqual(16D, hoursUse);
            var paid = (bool)new PrivateObject(companyOrdersd).GetField("paid");
            Assert.AreEqual(true, paid);
            var paidHour = (double)new PrivateObject(companyOrdersd).GetField("paidHour");
            Assert.AreEqual(465D, paidHour);
            var paidToAcc = (int)new PrivateObject(companyOrdersd).GetField("paidToAcc");
            Assert.AreEqual(648641, paidToAcc);
            var taskDate = (DateTime)new PrivateObject(companyOrdersd).GetField("taskDate");
        }

        public class TestingOrder
        {
            public class Worker1 : Interface.Iworker
            {
                public bool Active { get { return true; } }
                public string Address { get { return "workerAddress"; } }
                public string AltPhoneNo { get { return "workerAltPhoneNo"; } }
                public string Email { get { return "workerEmail"; } }
                public string Name { get { return "workerName"; } }
                public string PhoneNo { get { return "workerPhoneNo"; } }
                public IpostNo PostNo { get { return new PostNo(); } }
                public string Surname { get { return "workerSurname"; } }
                public IworkerStatus WorkerStatus { get { return new WorkerStatus(); } set { throw new NotImplementedException(); } }
                public int WorkNo { get { return 1; } }
            }

            public class Worker2 : Interface.Iworker
            {
                public bool Active { get { return false; } }
                public string Address { get { return "workerAddress2"; } }
                public string AltPhoneNo { get { return "workerAltPhoneNo2"; } }
                public string Email { get { return "workerEmail"; } }
                public string Name { get { return "workerName2"; } }
                public string PhoneNo { get { return "workerPhoneNo2"; } }
                public IpostNo PostNo { get { return new PostNo(); } }
                public string Surname { get { return "workerSurname2"; } }
                public IworkerStatus WorkerStatus { get { return new WorkerStatus(); } set { throw new NotImplementedException(); } }
                public int WorkNo { get { return 2; } }
            }

            public class PostNo : Interface.IpostNo
            {
                public string City { get { return "postNoCity"; } }
                public int Id { get { return 1; } }
                public int PostNumber { get { return 6300; } }
            }

            public class WorkerStatus : Interface.IworkerStatus
            {
                public string Staus { get { return "workerStatusStaus"; } }
                public int StautsNo { get { return 1; } }
            }

            public class CustomerPrivet1 : Interface.IprivetCustomer
            {
                public bool Active { get { return true; } }
                public string AltPhoneNo { get { return "CustomerPrivetAltPhoneNo"; } }
                public string Email { get { return "CustomerPrivetEmail"; } }
                public string HomeAddress { get { return "CustomerPrivetHomeAddress"; } }
                public string Name { get { return "CustomerPrivetName"; } }
                public string PhoneNo { get { return "CustomerPrivetPhoneNo"; } }
                public IpostNo PostNo { get { return new TestingOrder.PostNo(); } set { throw new NotImplementedException(); } }
                public int PrivateCustomersNo { get { return 1; } }
                public string Surname { get { return "CustomerPrivetSurname"; } }
            }

            public class CustomerPrivet2 : Interface.IprivetCustomer
            {
                public bool Active { get { return false; } }
                public string AltPhoneNo { get { return "CustomerPrivetAltPhoneNo2"; } }
                public string Email { get { return "CustomerPrivetEmail2"; } }
                public string HomeAddress { get { return "CustomerPrivetHomeAddress2"; } }
                public string Name { get { return "CustomerPrivetName2"; } }
                public string PhoneNo { get { return "CustomerPrivetPhoneNo2"; } }
                public IpostNo PostNo { get { return new TestingOrder.PostNo(); } set { throw new NotImplementedException(); } }
                public int PrivateCustomersNo { get { return 1; } }
                public string Surname { get { return "CustomerPrivetSurname2"; } }
            }

            public class CustomerCompany1 : Interface.IcompanyCustomer
            {
                public string Address { get { return "testAddress"; } }
                public string AltPhoneNo { get { return "testAltPhoneNo"; } }
                public bool Active { get { return true; } }
                public int CompanyCustomersNo { get { return 16; } }
                public string ContactPerson { get { return "testConractPerson"; } }
                public int CvrNo { get { return 12564; } }
                public string Email { get { return "testEmail"; } }
                public string Name { get { return "testName"; } }
                public string PhoneNo { get { return "testPhoneNo"; } }
                public IpostNo PostNo { get { return new PostNo(); } set { throw new NotImplementedException(); } }
            }

            public class CustomerCompany2 : Interface.IcompanyCustomer
            {
                public string Address { get { return "testAddress2"; } }
                public string AltPhoneNo { get { return "testAlrPhoneNo2"; } }
                public bool Active { get { return false; } }
                public int CompanyCustomersNo { get { return 164; } }
                public string ContactPerson { get { return "testContactPerson"; } }
                public int CvrNo { get { return 8451; } }
                public string Email { get { return "testEmail2"; } }
                public string Name { get { return "testName2"; } }
                public string PhoneNo { get { return "testPhoneNo"; } }
                public IpostNo PostNo { get { return new PostNo(); } set { throw new NotImplementedException(); } }
            }
        }
    }
}
