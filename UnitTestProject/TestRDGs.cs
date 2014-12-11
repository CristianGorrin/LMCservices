using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using RDGs;
using Interface;

namespace UnitTestProject
{
    // The tests is base on the values from testingValuesV1.sql

    [TestClass]
    public class TestRDGs
    {
        // this is the connection string for the database used in this test
        public const string connectionString = @"Data Source=(localdb)\ProjectsV12;Initial Catalog=LMCdatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False";

        [TestClass]
        public class TestRDGtblPostNo
        {
            

            [TestMethod]
            public void Get()
            {
                var tblPostNo = new RDGs.RDGtblPostNo(TestRDGs.connectionString);
                var list = tblPostNo.Get();

                Assert.AreEqual(591, list.Count);
            }

            [TestMethod]
            public void Find()
            {
                var tblPostNo = new RDGs.RDGtblPostNo(TestRDGs.connectionString);
                var postNo = tblPostNo.Find(17);

                Assert.AreEqual(2650, postNo.PostNumber);
                Assert.AreEqual("Hvidovre", postNo.City);

                postNo = tblPostNo.Find(56);
                Assert.AreEqual(3140, postNo.PostNumber);
                Assert.AreEqual("Ålsgårde", postNo.City);
            }

            [TestMethod]
            public void Update()
            {
                var tblPostNo = new RDGs.RDGtblPostNo(TestRDGs.connectionString);

                tblPostNo.Update(new PostNum() { City = "newCity", Id = 1, PostNumber = 9999 });

                var updatePostNo = tblPostNo.Find(1);
                Assert.AreEqual("newCity", updatePostNo.City);
                Assert.AreEqual(1, updatePostNo.Id);
                Assert.AreEqual(9999, updatePostNo.PostNumber);

                tblPostNo.Update(new PostNum() { City = "Frederiksberg", Id = 1, PostNumber = 2000 });

                updatePostNo = tblPostNo.Find(1);
                Assert.AreEqual("Frederiksberg", updatePostNo.City);
                Assert.AreEqual(1, updatePostNo.Id);
                Assert.AreEqual(2000, updatePostNo.PostNumber);
            }

            [TestMethod]
            public void Add()
            {
                var tblPostNo = new RDGs.RDGtblPostNo(TestRDGs.connectionString);

                var newPostNo = new PostNum() { City = "addNewCity", PostNumber = 50 };
                tblPostNo.Add(newPostNo);

                var result = tblPostNo.Find(tblPostNo.NextId - 1);

                Assert.AreEqual("addNewCity", result.City);
                Assert.AreEqual(50, result.PostNumber);
            }

            [TestMethod]
            public void Delete()
            {
                var tblPostNo = new RDGs.RDGtblPostNo(TestRDGs.connectionString);

                int id = tblPostNo.NextId - 1;

                tblPostNo.Delete(new PostNum() { Id = id });

                object findPostNo = null;

                try
                {
                    findPostNo = tblPostNo.Find(id);
                }
                catch (Exception)
                {
                }

                if (findPostNo != null)
                {
                    throw new AssertFailedException("The post number: " + id.ToString() + "has not been delete");
                }
            }

            class PostNum : Interface.IpostNo
            {
                public string City { get; set; }
                public int Id { get; set; }
                public int PostNumber { get; set; }
            }
        }

        [TestClass]
        public class TestRDGtblBankAccounts
        {
            [TestMethod]
            public void Get()
            {
                var bankAccounts = new RDGtblBankAccounts(TestRDGs.connectionString);

                var list = bankAccounts.Get();

                if (list.Count != 100)
                {
                    throw new AssertFailedException("RDGtblBankAccounts doesn't return expect amount");
                }
            }

            [TestMethod]
            public void Find()
            {
                var bankAccounts = new RDGtblBankAccounts(TestRDGs.connectionString);

                var bankAccFound = bankAccounts.Find(1);

                Assert.AreEqual(1, bankAccFound.Id);
                Assert.AreEqual("Luctus Incorporated", bankAccFound.Bank);
                Assert.AreEqual("consequat nec, mollis vitae, posuere", bankAccFound.AccountName);
                Assert.AreEqual(6515, bankAccFound.RegNo);
                Assert.AreEqual("PL76022421705935562898910716", bankAccFound.AccountNo);
                Assert.AreEqual(398.2200D, bankAccFound.Balance);

                bankAccFound = bankAccounts.Find(25);
                Assert.AreEqual(25, bankAccFound.Id);
                Assert.AreEqual("Tristique PC", bankAccFound.Bank);
                Assert.AreEqual("enim Mgravida sagittis", bankAccFound.AccountName);
                Assert.AreEqual(2749, bankAccFound.RegNo);
                Assert.AreEqual("RS50652018226709615068", bankAccFound.AccountNo);
                Assert.AreEqual(528.7300D, bankAccFound.Balance);
            }

            [TestMethod]
            public void Update()
            {
                var bankAccounts = new RDGtblBankAccounts(TestRDGs.connectionString);

                bankAccounts.Update(new BankAcc()
                {
                    AccountName = "newAccName",
                    Balance = 654.84D,
                    Id = 1,
                    AccountNo = "rdfvew16948516516156",
                    Bank = "newBank",
                    RegNo = 8564
                });


                var updateBankAcc = bankAccounts.Find(1);
                Assert.AreEqual("newAccName", updateBankAcc.AccountName);
                Assert.AreEqual(654.84D, updateBankAcc.Balance);
                Assert.AreEqual(1, updateBankAcc.Id);
                Assert.AreEqual("rdfvew16948516516156", updateBankAcc.AccountNo);
                Assert.AreEqual("newBank", updateBankAcc.Bank);
                Assert.AreEqual(8564, updateBankAcc.RegNo);

                bankAccounts.Update(new BankAcc() 
                {
                    AccountName = "consequat nec, mollis vitae, posuere",
                    AccountNo = "PL76022421705935562898910716",
                    Balance = 398.2200D,
                    Bank = "Luctus Incorporated",
                    Id = 1,
                    RegNo = 6515
                });

                updateBankAcc = bankAccounts.Find(1);
                Assert.AreEqual("consequat nec, mollis vitae, posuere", updateBankAcc.AccountName);
                Assert.AreEqual(398.2200D, updateBankAcc.Balance);
                Assert.AreEqual(1, updateBankAcc.Id);
                Assert.AreEqual("PL76022421705935562898910716", updateBankAcc.AccountNo);
                Assert.AreEqual("Luctus Incorporated", updateBankAcc.Bank);
                Assert.AreEqual(6515, updateBankAcc.RegNo);
            }

            [TestMethod]
            public void Add()
            {
                var bankAccounts = new RDGtblBankAccounts(TestRDGs.connectionString);

                bankAccounts.Add(new BankAcc()
                {
                    AccountName = "newAccName",
                    AccountNo = "newAccNo-486532",
                    RegNo = 4856,
                    Balance = 4184.4685D,
                    Bank = "newBank"
                });

                var bankAcc = bankAccounts.Find(bankAccounts.NextId - 1);
                Assert.AreEqual("newAccName", bankAcc.AccountName);
                Assert.AreEqual(4184.4685D, bankAcc.Balance);
                Assert.AreEqual("newAccNo-486532", bankAcc.AccountNo);
                Assert.AreEqual("newBank", bankAcc.Bank);
                Assert.AreEqual(4856, bankAcc.RegNo);
            }

            [TestMethod]
            public void Delete()
            {
                var bankAccounts = new RDGtblBankAccounts(TestRDGs.connectionString);

                int id = bankAccounts.NextId - 1;

                bankAccounts.Delete(id);

                object found = null;

                try
                {
                    found = bankAccounts.Find(id);
                }
                catch (Exception)
                {
                }

                if (found != null)
                {
                    throw new AssertFailedException("The bank acc hasn't been delete");
                }
            }

            class BankAcc : Interface.IbankAccounts
            {
                public int Id { get; set; }
                public string Bank { get; set; }
                public string AccountName { get; set; }
                public int RegNo { get; set; }
                public string AccountNo { get; set; }
                public double Balance { get; set; }
            }
        }

        [TestClass]
        public class TestRDGtblWorkers
        {
            [TestMethod]
            public void Get()
            {
                var workers = new RDGtblWorkers(TestRDGs.connectionString);

                var list = workers.Get(null);

                if (list.Count != 100)
                {
                    throw new AssertFailedException("dot's return expect amount");
                }

                list = workers.Get(true);
                if (list.Count != 47)
                {
                    throw new AssertFailedException("dot's return expect amount");
                }

                list = workers.Get(false);
                if (list.Count != 53)
                {
                    throw new AssertFailedException("dot's return expect amount");
                }
            }

            [TestMethod]
            public void Find()
            {
                var workers = new RDGtblWorkers(TestRDGs.connectionString);

                var worker = workers.Find(1);
                Assert.AreEqual(1, worker.WorkNo);
                Assert.AreEqual("Jack", worker.Name);
                Assert.AreEqual("Hahn", worker.Surname);
                Assert.AreEqual(60, worker.WorkerStatus.StautsNo);
                Assert.AreEqual("+4514420838", worker.PhoneNo);
                Assert.AreEqual("47 27 92 43", worker.AltPhoneNo);
                Assert.AreEqual("P.O. Box 968, 2362 A Rd.", worker.Address);
                Assert.AreEqual(72, worker.PostNo.Id);
                Assert.AreEqual("id.enim.Curabitur@vel.ca", worker.Email);
                Assert.AreEqual(true, worker.Active);

                worker = workers.Find(53);
                Assert.AreEqual(53, worker.WorkNo);
                Assert.AreEqual("Kasper", worker.Name);
                Assert.AreEqual("Harrington", worker.Surname);
                Assert.AreEqual(11, worker.WorkerStatus.StautsNo);
                Assert.AreEqual("+4573740728", worker.PhoneNo);
                Assert.AreEqual("97 54 14 96", worker.AltPhoneNo);
                Assert.AreEqual("P.O. Box 970, 9621 In, St.", worker.Address);
                Assert.AreEqual(84, worker.PostNo.Id);
                Assert.AreEqual("lectus@In.com", worker.Email);
                Assert.AreEqual(false, worker.Active);
            }

            [TestMethod]
            public void Add()
            {
                var workers = new RDGtblWorkers(TestRDGs.connectionString);

                int id = workers.NextId;

                workers.Add(new Worker()
                {
                    Active = true,
                    Address = "newAddress",
                    AltPhoneNo = "newAllPhone123456",
                    Email = "newEmail",
                    Name = "newName",
                    PhoneNo = "newPhone16516",
                    PostNo = new InterfaceAdaptor.PostNo() { Id = 15 },
                    Surname = "newSurname",
                    WorkerStatus = new InterfaceAdaptor.WorkerStatus() { StautsNo = 10 },
                });

                var worker = workers.Find(id);
                Assert.AreEqual(id, worker.WorkNo);
                Assert.AreEqual("newName", worker.Name);
                Assert.AreEqual("newSurname", worker.Surname);
                Assert.AreEqual(10, worker.WorkerStatus.StautsNo);
                Assert.AreEqual("newPhone16516", worker.PhoneNo);
                Assert.AreEqual("newAllPhone123456", worker.AltPhoneNo);
                Assert.AreEqual("newAddress", worker.Address);
                Assert.AreEqual(15, worker.PostNo.Id);
                Assert.AreEqual("newEmail", worker.Email);
                Assert.AreEqual(true, worker.Active);
            }

            [TestMethod]
            public void Update()
            {
                var workers = new RDGtblWorkers(TestRDGs.connectionString);

                workers.Update(new Worker
                {
                    Active = false,
                    Address = "newAddress",
                    AltPhoneNo = "newAltPhone",
                    Email = "newEmail",
                    Name = "newName",
                    PhoneNo = "newPhone",
                    PostNo = new InterfaceAdaptor.PostNo() { Id = 6 },
                    Surname = "newSurname",
                    WorkerStatus = new InterfaceAdaptor.WorkerStatus() { StautsNo = 5 },
                    WorkNo = 1
                });

                var worker = workers.Find(1);
                Assert.AreEqual(1, worker.WorkNo);
                Assert.AreEqual("newName", worker.Name);
                Assert.AreEqual("newSurname", worker.Surname);
                Assert.AreEqual(5, worker.WorkerStatus.StautsNo);
                Assert.AreEqual("newPhone", worker.PhoneNo);
                Assert.AreEqual("newAltPhone", worker.AltPhoneNo);
                Assert.AreEqual("newAddress", worker.Address);
                Assert.AreEqual(6, worker.PostNo.Id);
                Assert.AreEqual("newEmail", worker.Email);
                Assert.AreEqual(false, worker.Active);

                workers.Update(new Worker
                {
                    Active = true,
                    Address = "P.O. Box 968, 2362 A Rd.",
                    AltPhoneNo = "47 27 92 43",
                    Email = "id.enim.Curabitur@vel.ca",
                    Name = "Jack",
                    PhoneNo = "+4514420838",
                    PostNo = new InterfaceAdaptor.PostNo() { Id = 72 },
                    Surname = "Hahn",
                    WorkerStatus = new InterfaceAdaptor.WorkerStatus() { StautsNo = 60 },
                    WorkNo = 1
                });

                worker = workers.Find(1);
                Assert.AreEqual(1, worker.WorkNo);
                Assert.AreEqual("Jack", worker.Name);
                Assert.AreEqual("Hahn", worker.Surname);
                Assert.AreEqual(60, worker.WorkerStatus.StautsNo);
                Assert.AreEqual("+4514420838", worker.PhoneNo);
                Assert.AreEqual("47 27 92 43", worker.AltPhoneNo);
                Assert.AreEqual("P.O. Box 968, 2362 A Rd.", worker.Address);
                Assert.AreEqual(72, worker.PostNo.Id);
                Assert.AreEqual("id.enim.Curabitur@vel.ca", worker.Email);
                Assert.AreEqual(true, worker.Active);
            }

            [TestMethod]
            public void Delete()
            {
                var workers = new RDGtblWorkers(TestRDGs.connectionString);

                int id = workers.NextId - 1;

                workers.Delete(id);

                object worker = null;

                try
                {
                    worker = workers.Find(id);
                }
                catch (Exception)
                {
                }

                if (worker != null)
                {
                    throw new AssertFailedException("Worker wasn't delete");
                }
            }

            class Worker : Interface.Iworker
            {
                public bool Active { get; set; }
                public string Address { get; set; }
                public string AltPhoneNo { get; set; }
                public string Email { get; set; }
                public string Name { get; set; }
                public string PhoneNo { get; set; }
                public IpostNo PostNo { get; set; }
                public string Surname { get; set; }
                public IworkerStatus WorkerStatus { get; set; }
                public int WorkNo { get; set; }
            }
        }

        [TestClass]
        public class TestRDGtblCompanyCustomers
        {
            [TestMethod]
            public void Get()
            {
                var tblCompanyCustomers = new RDGtblCompanyCustomers(TestRDGs.connectionString);

                var list = tblCompanyCustomers.Get(null);
                Assert.AreEqual(100, list.Count);

                list = tblCompanyCustomers.Get(true);
                Assert.AreEqual(52, list.Count);

                list = tblCompanyCustomers.Get(false);
                Assert.AreEqual(48, list.Count);
            }

            [TestMethod]
            public void Find()
            {
                var tblCompanyCustomers = new RDGtblCompanyCustomers(TestRDGs.connectionString);

                var companyCustomers = tblCompanyCustomers.Find(1);

                Assert.AreEqual(1, companyCustomers.CompanyCustomersNo);
                Assert.AreEqual("Sem Ut Cursus Industries", companyCustomers.Name);
                Assert.AreEqual("Leo Suername", companyCustomers.ContactPerson);
                Assert.AreEqual(62988505, companyCustomers.CvrNo);
                Assert.AreEqual("+4577602112", companyCustomers.PhoneNo);
                Assert.AreEqual("45 35 20 10", companyCustomers.AltPhoneNo);
                Assert.AreEqual("3469 Sollicitudin Avenue", companyCustomers.Address);
                Assert.AreEqual(1, companyCustomers.PostNo.Id);
                Assert.AreEqual("faucibus.ut@mollisdui.net", companyCustomers.Email);
                Assert.AreEqual(false, companyCustomers.Active);
            }

            [TestMethod]
            public void Update()
            {
                var tblCompanyCustomers = new RDGtblCompanyCustomers(TestRDGs.connectionString);

                tblCompanyCustomers.Update(new CompanyCustomer());

                var companyCustomers = tblCompanyCustomers.Find(1);

                Assert.AreEqual(1, companyCustomers.CompanyCustomersNo);
                Assert.AreEqual("newName", companyCustomers.Name);
                Assert.AreEqual("newContactPerson", companyCustomers.ContactPerson);
                Assert.AreEqual(51655446, companyCustomers.CvrNo);
                Assert.AreEqual("newPhoneNo", companyCustomers.PhoneNo);
                Assert.AreEqual("newAltPhoneNo", companyCustomers.AltPhoneNo);
                Assert.AreEqual("newAddress", companyCustomers.Address);
                Assert.AreEqual(2, companyCustomers.PostNo.Id);
                Assert.AreEqual("newEmail", companyCustomers.Email);
                Assert.AreEqual(false, companyCustomers.Active);

                tblCompanyCustomers.Update(new InterfaceAdaptor.CompanyCustomer()
                {
                    Active = false,
                    Address = "3469 Sollicitudin Avenue",
                    AltPhoneNo = "45 35 20 10",
                    CompanyCustomersNo = 1,
                    ContactPerson = "Leo Suername",
                    CvrNo = 62988505,
                    Email = "faucibus.ut@mollisdui.net",
                    Name = "Sem Ut Cursus Industries",
                    PhoneNo = "+4577602112",
                    PostNo = new InterfaceAdaptor.PostNo() { Id = 1 }
                });

                companyCustomers = tblCompanyCustomers.Find(1);

                Assert.AreEqual(1, companyCustomers.CompanyCustomersNo);
                Assert.AreEqual("Sem Ut Cursus Industries", companyCustomers.Name);
                Assert.AreEqual("Leo Suername", companyCustomers.ContactPerson);
                Assert.AreEqual(62988505, companyCustomers.CvrNo);
                Assert.AreEqual("+4577602112", companyCustomers.PhoneNo);
                Assert.AreEqual("45 35 20 10", companyCustomers.AltPhoneNo);
                Assert.AreEqual("3469 Sollicitudin Avenue", companyCustomers.Address);
                Assert.AreEqual(1, companyCustomers.PostNo.Id);
                Assert.AreEqual("faucibus.ut@mollisdui.net", companyCustomers.Email);
                Assert.AreEqual(false, companyCustomers.Active);
            }

            [TestMethod]
            public void Add()
            {
                var tblCompanyCustomers = new RDGtblCompanyCustomers(TestRDGs.connectionString);

                tblCompanyCustomers.Add(new CompanyCustomer());

                var companyCustomers = tblCompanyCustomers.Find(tblCompanyCustomers.NextId - 1);

                Assert.AreEqual(tblCompanyCustomers.NextId - 1, companyCustomers.CompanyCustomersNo);
                Assert.AreEqual("newName", companyCustomers.Name);
                Assert.AreEqual("newContactPerson", companyCustomers.ContactPerson);
                Assert.AreEqual(51655446, companyCustomers.CvrNo);
                Assert.AreEqual("newPhoneNo", companyCustomers.PhoneNo);
                Assert.AreEqual("newAltPhoneNo", companyCustomers.AltPhoneNo);
                Assert.AreEqual("newAddress", companyCustomers.Address);
                Assert.AreEqual(2, companyCustomers.PostNo.Id);
                Assert.AreEqual("newEmail", companyCustomers.Email);
                Assert.AreEqual(false, companyCustomers.Active);
            }

            [TestMethod]
            public void Delete()
            {
                var tblCompanyCustomers = new RDGtblCompanyCustomers(TestRDGs.connectionString);

                tblCompanyCustomers.Delete(tblCompanyCustomers.NextId - 1);

                object obj = null;

                try
                {
                    obj = tblCompanyCustomers.Find(tblCompanyCustomers.NextId - 1);
                }
                catch (Exception)
                {
                }

                Assert.IsNull(obj);
            }

            class CompanyCustomer : Interface.IcompanyCustomer
            {
                public string Address { get { return "newAddress"; } }
                public string AltPhoneNo { get { return "newAltPhoneNo"; } }
                public bool Active { get { return false; } }
                public int CompanyCustomersNo { get { return 1; } }
                public string ContactPerson { get { return "newContactPerson"; } }
                public int CvrNo { get { return 51655446; } }
                public string Email { get { return "newEmail"; } }
                public string Name { get { return "newName"; } }
                public string PhoneNo { get { return "newPhoneNo"; } }
                public IpostNo PostNo { get { return new InterfaceAdaptor.PostNo() { Id = 2 }; } set { throw new NotImplementedException(); } }
            }
        }

        [TestClass]
        public class TestRGDtblCompanyOrders
        {
            [TestMethod]
            public void Get()
            {
                var tblCompanyCustomers = new RGDtblCompanyOrders(TestRDGs.connectionString);

                var list = tblCompanyCustomers.Get(null);

                Assert.AreEqual(100, list.Count);

                list = tblCompanyCustomers.Get(true);
                Assert.AreEqual(55, list.Count);

                list = tblCompanyCustomers.Get(false);
                Assert.AreEqual(45, list.Count);
            }

            [TestMethod]
            public void Find()
            {
                var tblCompanyCustomers = new RGDtblCompanyOrders(TestRDGs.connectionString);

                var Order = tblCompanyCustomers.Find(1);

                Assert.AreEqual(1, Order.InvoiceNo);
                Assert.AreEqual(49 , Order.CreateBy.WorkNo);
                Assert.AreEqual("23-03-2015", Order.CreateDate.ToShortDateString());
                Assert.AreEqual(1, Order.Customer.CompanyCustomersNo);
                Assert.AreEqual("26-01-2014", Order.DateSendBill.Value.ToShortDateString());
                Assert.AreEqual(11, Order.DaysToPaid);
                Assert.AreEqual("auctor ullamcorper, nisl arcu", Order.DescriptionTask);
                Assert.AreEqual(2.0D, Order.HoutsUse);
                Assert.AreEqual(true, Order.Paid);
                Assert.AreEqual(169.0D, Order.PaidHour);
                Assert.AreEqual(1, Order.PaidToAcc);
                Assert.AreEqual("15-08-2014", Order.TaskDate.ToShortDateString());
            }

            [TestMethod]
            public void Update()
            {
                var tblCompanyCustomers = new RGDtblCompanyOrders(TestRDGs.connectionString);

                tblCompanyCustomers.Update(new CompanyOrder());

                var Order = tblCompanyCustomers.Find(1);

                Assert.AreEqual(1, Order.InvoiceNo);
                Assert.AreEqual(15, Order.CreateBy.WorkNo);
                Assert.AreEqual("29-08-2013", Order.CreateDate.ToShortDateString());
                Assert.AreEqual(16, Order.Customer.CompanyCustomersNo);
                Assert.AreEqual("01-09-2013", Order.DateSendBill.Value.ToShortDateString());
                Assert.AreEqual(10, Order.DaysToPaid);
                Assert.AreEqual("newDescriptionTask", Order.DescriptionTask);
                Assert.AreEqual(3.5D, Order.HoutsUse);
                Assert.AreEqual(false, Order.Paid);
                Assert.AreEqual(350D, Order.PaidHour);
                Assert.AreEqual(5, Order.PaidToAcc);
                Assert.AreEqual("02-06-2013", Order.TaskDate.ToShortDateString());

                tblCompanyCustomers.Update(new InterfaceAdaptor.CompanyOrder() {
                    CreateBy = new InterfaceAdaptor.Worker() { WorkNo = 49 },
                    CreateDate = new DateTime(2015,3,23),
                    Customer = new InterfaceAdaptor.CompanyCustomer() { CompanyCustomersNo = 1 },
                    DateSendBill = new DateTime(2014,01,26),
                    DaysToPaid = 11,
                    DescriptionTask = "auctor ullamcorper, nisl arcu",
                    HoutsUse = 2.0D,
                    InvoiceNo = 1,
                    Paid = true,
                    PaidHour = 169.0D,
                    PaidToAcc = 1,
                    TaskDate = new DateTime(2014,8,15)
                });

                Order = tblCompanyCustomers.Find(1);

                Assert.AreEqual(1, Order.InvoiceNo);
                Assert.AreEqual(49, Order.CreateBy.WorkNo);
                Assert.AreEqual("23-03-2015", Order.CreateDate.ToShortDateString());
                Assert.AreEqual(1, Order.Customer.CompanyCustomersNo);
                Assert.AreEqual("26-01-2014", Order.DateSendBill.Value.ToShortDateString());
                Assert.AreEqual(11, Order.DaysToPaid);
                Assert.AreEqual("auctor ullamcorper, nisl arcu", Order.DescriptionTask);
                Assert.AreEqual(2.0D, Order.HoutsUse);
                Assert.AreEqual(true, Order.Paid);
                Assert.AreEqual(169.0D, Order.PaidHour);
                Assert.AreEqual(1, Order.PaidToAcc);
                Assert.AreEqual("15-08-2014", Order.TaskDate.ToShortDateString());
            }

            [TestMethod]
            public void Add()
            {
                var tblCompanyCustomers = new RGDtblCompanyOrders(TestRDGs.connectionString);

                tblCompanyCustomers.Add(new CompanyOrder());

                var Order = tblCompanyCustomers.Find(tblCompanyCustomers.NextId - 1);

                Assert.AreEqual(tblCompanyCustomers.NextId - 1, Order.InvoiceNo);
                Assert.AreEqual(15, Order.CreateBy.WorkNo);
                Assert.AreEqual("29-08-2013", Order.CreateDate.ToShortDateString());
                Assert.AreEqual(16, Order.Customer.CompanyCustomersNo);
                Assert.AreEqual("01-09-2013", Order.DateSendBill.Value.ToShortDateString());
                Assert.AreEqual(10, Order.DaysToPaid);
                Assert.AreEqual("newDescriptionTask", Order.DescriptionTask);
                Assert.AreEqual(3.5D, Order.HoutsUse);
                Assert.AreEqual(false, Order.Paid);
                Assert.AreEqual(350D, Order.PaidHour);
                Assert.AreEqual(5, Order.PaidToAcc);
                Assert.AreEqual("02-06-2013", Order.TaskDate.ToShortDateString());
            }

            [TestMethod]
            public void Delete()
            {
                var tblCompanyCustomers = new RGDtblCompanyOrders(TestRDGs.connectionString);

                tblCompanyCustomers.Delete(tblCompanyCustomers.NextId - 1);

                try
                {
                    tblCompanyCustomers.Find(101);
                    throw new AssertFailedException("tblCompanyCustomers 101 has not been delete");
                }
                catch (Exception) { }
            }

            class CompanyOrder : Interface.IcompanyOrder
            {
                public Iworker CreateBy { get { return new InterfaceAdaptor.Worker() { WorkNo = 15 }; } set { throw new NotImplementedException(); } }
                public DateTime CreateDate { get { return new DateTime(2013, 8, 29); } }
                public IcompanyCustomer Customer { get { return new InterfaceAdaptor.CompanyCustomer { CompanyCustomersNo = 16 }; } set { throw new NotImplementedException(); } }
                public DateTime? DateSendBill { get { return new DateTime(2013, 9, 1); } }
                public int DaysToPaid { get { return 10; } }
                public string DescriptionTask { get { return "newDescriptionTask"; } }
                public double HoutsUse { get { return 3.5D; } }
                public int InvoiceNo { get { return 1; } }
                public bool Paid { get { return false; } }
                public double PaidHour { get { return 350D; } }
                public int PaidToAcc { get { return 5; } }
                public DateTime TaskDate { get { return new DateTime(2013, 6, 2); } }
            }
        }

        [TestClass]
        public class TestRDGtblDepartment
        {
            [TestMethod]
            public void Get()
            {
                var tblDepartment = new RDGtblDepartment(TestRDGs.connectionString);

                var list = tblDepartment.Get(null);
                Assert.AreEqual(100, list.Count);

                list = tblDepartment.Get(true);
                Assert.AreEqual(50, list.Count);

                list = tblDepartment.Get(false);
                Assert.AreEqual(50, list.Count);

            }

            [TestMethod]
            public void Find()
            {
                var tblDepartment = new RDGtblDepartment(TestRDGs.connectionString);

                var department = tblDepartment.Find(1);

                Assert.AreEqual(false, department.Active);
                Assert.AreEqual("P.O. Box 608, 3272 Donec Rd.", department.Address);
                Assert.AreEqual("+45-266-616-5489", department.AltPhoneNo);
                Assert.AreEqual("Egestas Blandit Consulting", department.CompanyName);
                Assert.AreEqual(356108649, department.CvrNo);
                Assert.AreEqual(1, department.Deparment);
                Assert.AreEqual(32, department.DeparmentHead.WorkNo);
                Assert.AreEqual("Fusce.mi@pharetraQuisque.org", department.Email);
                Assert.AreEqual("436-5207", department.PhoneNo);
                Assert.AreEqual(88, department.PostNo.Id);
            }

            [TestMethod]
            public void Add()
            {
                var tblDepartment = new RDGtblDepartment(TestRDGs.connectionString);

                tblDepartment.Add(new Department());

                var department = tblDepartment.Find(tblDepartment.NextId - 1);

                Assert.AreEqual(true, department.Active);
                Assert.AreEqual("newAddress", department.Address);
                Assert.AreEqual("newAltPhoneNo", department.AltPhoneNo);
                Assert.AreEqual("newCompanyName", department.CompanyName);
                Assert.AreEqual(16585158, department.CvrNo);
                Assert.AreEqual(tblDepartment.NextId - 1, department.Deparment);
                Assert.AreEqual(21, department.DeparmentHead.WorkNo);
                Assert.AreEqual("newEmail", department.Email);
                Assert.AreEqual("newPhoneNo", department.PhoneNo);
                Assert.AreEqual(3, department.PostNo.Id);
            }

            [TestMethod]
            public void Delete()
            {
                var tblDepartment = new RDGtblDepartment(TestRDGs.connectionString);

                tblDepartment.Delete(tblDepartment.NextId - 1);

                object obj = null;

                try
                {
                    obj = tblDepartment.Find(tblDepartment.NextId - 1);
                }
                catch (Exception)
                {
                }

                Assert.IsNull(obj);
            }

            [TestMethod]
            public void Update()
            {
                var tblDepartment = new RDGtblDepartment(TestRDGs.connectionString);

                tblDepartment.Update(new Department());

                var department = tblDepartment.Find(1);

                Assert.AreEqual(true, department.Active);
                Assert.AreEqual("newAddress", department.Address);
                Assert.AreEqual("newAltPhoneNo", department.AltPhoneNo);
                Assert.AreEqual("newCompanyName", department.CompanyName);
                Assert.AreEqual(16585158, department.CvrNo);
                Assert.AreEqual(1, department.Deparment);
                Assert.AreEqual(21, department.DeparmentHead.WorkNo);
                Assert.AreEqual("newEmail", department.Email);
                Assert.AreEqual("newPhoneNo", department.PhoneNo);
                Assert.AreEqual(3, department.PostNo.Id);

                tblDepartment.Update(new InterfaceAdaptor.Department()
                {
                    Active = false,
                    Address = "P.O. Box 608, 3272 Donec Rd.",
                    AltPhoneNo = "+45-266-616-5489",
                    CompanyName = "Egestas Blandit Consulting",
                    CvrNo = 356108649,
                    Deparment = 1,
                    DeparmentHead = new InterfaceAdaptor.Worker() { WorkNo = 32 },
                    Email = "Fusce.mi@pharetraQuisque.org",
                    PhoneNo = "436-5207",
                    PostNo = new InterfaceAdaptor.PostNo() { Id = 88 }
                });

                department = tblDepartment.Find(1);

                Assert.AreEqual(false, department.Active);
                Assert.AreEqual("P.O. Box 608, 3272 Donec Rd.", department.Address);
                Assert.AreEqual("+45-266-616-5489", department.AltPhoneNo);
                Assert.AreEqual("Egestas Blandit Consulting", department.CompanyName);
                Assert.AreEqual(356108649, department.CvrNo);
                Assert.AreEqual(1, department.Deparment);
                Assert.AreEqual(32, department.DeparmentHead.WorkNo);
                Assert.AreEqual("Fusce.mi@pharetraQuisque.org", department.Email);
                Assert.AreEqual("436-5207", department.PhoneNo);
                Assert.AreEqual(88, department.PostNo.Id);
            }

            class Department : Interface.Idepartment
            {
                public bool Active { get { return true; } }
                public string Address { get { return "newAddress"; } }
                public string AltPhoneNo { get { return "newAltPhoneNo"; } }
                public string CompanyName { get { return "newCompanyName"; } }
                public int CvrNo { get { return 16585158; } }
                public int Deparment { get { return 1; } }
                public Iworker DeparmentHead { get { return new InterfaceAdaptor.Worker { WorkNo = 21 }; } }
                public string Email { get { return "newEmail"; } }
                public string PhoneNo { get { return "newPhoneNo"; } }
                public IpostNo PostNo { get { return new InterfaceAdaptor.PostNo() { Id = 3 }; } }
            }
        }

        [TestClass]
        public class TestRDGtblPrivateCustomers
        {
            [TestMethod]
            public void Get()
            {
                var tblPrivateCustomers = new RDGtblPrivateCustomers(TestRDGs.connectionString);

                var list = tblPrivateCustomers.Get(null);
                Assert.AreEqual(100, list.Count);

                list = tblPrivateCustomers.Get(true);
                Assert.AreEqual(59, list.Count);

                list = tblPrivateCustomers.Get(false);
                Assert.AreEqual(41, list.Count);
            }

            [TestMethod]
            public void Find()
            {
                var tblPrivateCustomers = new RDGtblPrivateCustomers(TestRDGs.connectionString);

                var privateCustomers = tblPrivateCustomers.Find(1);

                Assert.AreEqual(false, privateCustomers.Active);
                Assert.AreEqual("41 57 38 53", privateCustomers.AltPhoneNo);
                Assert.AreEqual("ligula.Nullam@Etiambibendum.co.uk", privateCustomers.Email);
                Assert.AreEqual("505-8050 Nulla. St.", privateCustomers.HomeAddress);
                Assert.AreEqual("Wang", privateCustomers.Name);
                Assert.AreEqual("+4557187129", privateCustomers.PhoneNo);
                Assert.AreEqual(1, privateCustomers.PostNo.Id);
                Assert.AreEqual(1, privateCustomers.PrivateCustomersNo);
                Assert.AreEqual("Stein", privateCustomers.Surname);
            }

            [TestMethod]
            public void Add()
            {
                var tblPrivateCustomers = new RDGtblPrivateCustomers(TestRDGs.connectionString);

                tblPrivateCustomers.Add(new PrivateCustomers());

                var privateCustomers = tblPrivateCustomers.Find(tblPrivateCustomers.NextId - 1);

                Assert.AreEqual(true, privateCustomers.Active);
                Assert.AreEqual("newAltPhoneNo", privateCustomers.AltPhoneNo);
                Assert.AreEqual("newEmail", privateCustomers.Email);
                Assert.AreEqual("newHomeAddress", privateCustomers.HomeAddress);
                Assert.AreEqual("newName", privateCustomers.Name);
                Assert.AreEqual("newPhoneNo", privateCustomers.PhoneNo);
                Assert.AreEqual(2, privateCustomers.PostNo.Id);
                Assert.AreEqual(tblPrivateCustomers.NextId - 1, privateCustomers.PrivateCustomersNo);
                Assert.AreEqual("newSurname", privateCustomers.Surname);
            }

            [TestMethod]
            public void Delete()
            {
                var tblPrivateCustomers = new RDGtblPrivateCustomers(TestRDGs.connectionString);

                tblPrivateCustomers.Delete(tblPrivateCustomers.NextId - 1);

                object obj = null;

                try
                {
                    obj = tblPrivateCustomers.Find(tblPrivateCustomers.NextId - 1);
                }
                catch (Exception)
                {
                }

                Assert.IsNull(obj);
            }

            [TestMethod]
            public void Update()
            {
                var tblPrivateCustomers = new RDGtblPrivateCustomers(TestRDGs.connectionString);

                tblPrivateCustomers.Update(new PrivateCustomers());

                var privateCustomers = tblPrivateCustomers.Find(1);

                Assert.AreEqual(true, privateCustomers.Active);
                Assert.AreEqual("newAltPhoneNo", privateCustomers.AltPhoneNo);
                Assert.AreEqual("newEmail", privateCustomers.Email);
                Assert.AreEqual("newHomeAddress", privateCustomers.HomeAddress);
                Assert.AreEqual("newName", privateCustomers.Name);
                Assert.AreEqual("newPhoneNo", privateCustomers.PhoneNo);
                Assert.AreEqual(2, privateCustomers.PostNo.Id);
                Assert.AreEqual(1, privateCustomers.PrivateCustomersNo);
                Assert.AreEqual("newSurname", privateCustomers.Surname);

                tblPrivateCustomers.Update(new InterfaceAdaptor.PrivetCustomer()
                {
                    Active = false,
                    AltPhoneNo = "41 57 38 53",
                    Email = "ligula.Nullam@Etiambibendum.co.uk",
                    HomeAddress = "505-8050 Nulla. St.",
                    Name = "Wang",
                    PhoneNo = "+4557187129",
                    PostNo = new InterfaceAdaptor.PostNo() {  Id = 1 },
                    PrivateCustomersNo = 1,
                    Surname = "Stein"
                });

                privateCustomers = tblPrivateCustomers.Find(1);

                Assert.AreEqual(false, privateCustomers.Active);
                Assert.AreEqual("41 57 38 53", privateCustomers.AltPhoneNo);
                Assert.AreEqual("ligula.Nullam@Etiambibendum.co.uk", privateCustomers.Email);
                Assert.AreEqual("505-8050 Nulla. St.", privateCustomers.HomeAddress);
                Assert.AreEqual("Wang", privateCustomers.Name);
                Assert.AreEqual("+4557187129", privateCustomers.PhoneNo);
                Assert.AreEqual(1, privateCustomers.PostNo.Id);
                Assert.AreEqual(1, privateCustomers.PrivateCustomersNo);
                Assert.AreEqual("Stein", privateCustomers.Surname);
            }

            class PrivateCustomers : Interface.IprivetCustomer
            {
                public bool Active { get { return true; } }
                public string AltPhoneNo { get { return "newAltPhoneNo"; } }
                public string Email { get { return "newEmail"; } }
                public string HomeAddress { get { return "newHomeAddress"; } }
                public string Name { get { return "newName"; } }
                public string PhoneNo { get { return "newPhoneNo"; } }
                public IpostNo PostNo { get { return new InterfaceAdaptor.PostNo() { Id = 2 }; } set { throw new NotFiniteNumberException(); } }
                public int PrivateCustomersNo { get { return 1; } }
                public string Surname { get { return "newSurname"; } }
            }
        }

        [TestClass]
        public class TestRDGtblPrivetOrders
        {
            [TestMethod]
            public void Get()
            {
                var tblPrivetOrders = new RDGtblPrivetOrders(TestRDGs.connectionString);

                var list = tblPrivetOrders.Get(null);
                Assert.AreEqual(100, list.Count);

                list = tblPrivetOrders.Get(true);
                Assert.AreEqual(46, list.Count);

                list = tblPrivetOrders.Get(false);
                Assert.AreEqual(54, list.Count);
            }

            [TestMethod]
            public void Find()
            {
                var tblPrivetOrders = new RDGtblPrivetOrders(TestRDGs.connectionString);

                var privetOrder = tblPrivetOrders.Find(1);

                Assert.AreEqual(48, privetOrder.CreateBy.WorkNo);
                Assert.AreEqual("24-11-2013", privetOrder.CreateDate.ToShortDateString());
                Assert.AreEqual(24, privetOrder.Customer.PrivateCustomersNo);
                Assert.AreEqual("10-06-2014", privetOrder.DateSendBill.Value.ToShortDateString());
                Assert.AreEqual(2, privetOrder.DaysToPaid);
                Assert.AreEqual("orci quis lectus.", privetOrder.DescriptionTask);
                Assert.AreEqual(10.0D, privetOrder.HourUse);
                Assert.AreEqual(1, privetOrder.InvoiceNo);
                Assert.AreEqual(true, privetOrder.Paid);
                Assert.AreEqual(419.0D, privetOrder.PaidHour);
                Assert.AreEqual(78, privetOrder.PaidToAcc);
                Assert.AreEqual("17-12-2013", privetOrder.TaskDate.ToShortDateString());
            }

            [TestMethod]
            public void Add()
            {
                var tblPrivetOrders = new RDGtblPrivetOrders(TestRDGs.connectionString);

                tblPrivetOrders.Add(new PrivetOrder());

                var privetOrder = tblPrivetOrders.Find(tblPrivetOrders.NextId - 1);

                Assert.AreEqual(10, privetOrder.CreateBy.WorkNo);
                Assert.AreEqual("20-05-2013", privetOrder.CreateDate.ToShortDateString());
                Assert.AreEqual(2, privetOrder.Customer.PrivateCustomersNo);
                Assert.AreEqual("20-06-2013", privetOrder.DateSendBill.Value.ToShortDateString());
                Assert.AreEqual(5, privetOrder.DaysToPaid);
                Assert.AreEqual("newDescriptionTask", privetOrder.DescriptionTask);
                Assert.AreEqual(5.8D, privetOrder.HourUse);
                Assert.AreEqual(tblPrivetOrders.NextId - 1, privetOrder.InvoiceNo);
                Assert.AreEqual(false, privetOrder.Paid);
                Assert.AreEqual(641D, privetOrder.PaidHour);
                Assert.AreEqual(11, privetOrder.PaidToAcc);
                Assert.AreEqual("20-07-2013", privetOrder.TaskDate.ToShortDateString());
            }

            [TestMethod]
            public void Delete()
            {
                var tblPrivetOrders = new RDGtblPrivetOrders(TestRDGs.connectionString);

                tblPrivetOrders.Delete(tblPrivetOrders.NextId - 1);
                object obj = null;

                try
                {
                    obj = tblPrivetOrders.Find(tblPrivetOrders.NextId - 1);

                }
                catch (Exception) { }

                Assert.IsNull(obj);
            }

            [TestMethod]
            public void Update()
            {
                var tblPrivetOrders = new RDGtblPrivetOrders(TestRDGs.connectionString);

                tblPrivetOrders.Update(new PrivetOrder());

                var privetOrder = tblPrivetOrders.Find(1);

                Assert.AreEqual(10, privetOrder.CreateBy.WorkNo);
                Assert.AreEqual("20-05-2013", privetOrder.CreateDate.ToShortDateString());
                Assert.AreEqual(2, privetOrder.Customer.PrivateCustomersNo);
                Assert.AreEqual("20-06-2013", privetOrder.DateSendBill.Value.ToShortDateString());
                Assert.AreEqual(5, privetOrder.DaysToPaid);
                Assert.AreEqual("newDescriptionTask", privetOrder.DescriptionTask);
                Assert.AreEqual(5.8D, privetOrder.HourUse);
                Assert.AreEqual(1, privetOrder.InvoiceNo);
                Assert.AreEqual(false, privetOrder.Paid);
                Assert.AreEqual(641D, privetOrder.PaidHour);
                Assert.AreEqual(11, privetOrder.PaidToAcc);
                Assert.AreEqual("20-07-2013", privetOrder.TaskDate.ToShortDateString());



                tblPrivetOrders.Update(new InterfaceAdaptor.PrivetOrder()
                {
                    CreateBy = new InterfaceAdaptor.Worker() { WorkNo = 48 },
                    CreateDate = new DateTime(2013, 11, 24),
                    Customer = new InterfaceAdaptor.PrivetCustomer() { PrivateCustomersNo = 24 },
                    DateSendBill = new DateTime(2014, 06, 10),
                    DaysToPaid = 2,
                    DescriptionTask = "orci quis lectus.",
                    HourUse = 10.0D,
                    InvoiceNo = 1,
                    Paid = true,
                    PaidHour = 419.0D,
                    PaidToAcc = 78,
                    TaskDate = new DateTime(2013, 12, 17)
                });

                privetOrder = tblPrivetOrders.Find(1);

                Assert.AreEqual(48, privetOrder.CreateBy.WorkNo);
                Assert.AreEqual("24-11-2013", privetOrder.CreateDate.ToShortDateString());
                Assert.AreEqual(24, privetOrder.Customer.PrivateCustomersNo);
                Assert.AreEqual("10-06-2014", privetOrder.DateSendBill.Value.ToShortDateString());
                Assert.AreEqual(2, privetOrder.DaysToPaid);
                Assert.AreEqual("orci quis lectus.", privetOrder.DescriptionTask);
                Assert.AreEqual(10.0D, privetOrder.HourUse);
                Assert.AreEqual(1, privetOrder.InvoiceNo);
                Assert.AreEqual(true, privetOrder.Paid);
                Assert.AreEqual(419.0D, privetOrder.PaidHour);
                Assert.AreEqual(78, privetOrder.PaidToAcc);
                Assert.AreEqual("17-12-2013", privetOrder.TaskDate.ToShortDateString());
            }

            class PrivetOrder : Interface.IprivetOrder
            {
                public Iworker CreateBy { get { return new InterfaceAdaptor.Worker() { WorkNo = 10 }; } set { throw new NotImplementedException(); } }
                public DateTime CreateDate { get { return new DateTime(2013, 5, 20); } }
                public IprivetCustomer Customer { get { return new InterfaceAdaptor.PrivetCustomer() { PrivateCustomersNo = 2 }; } set { throw new NotImplementedException(); } }
                public DateTime? DateSendBill { get { return new DateTime(2013, 6, 20); } }
                public int DaysToPaid { get { return 5; } }
                public string DescriptionTask { get { return "newDescriptionTask"; } }
                public double HourUse { get { return 5.8D; } }
                public int InvoiceNo { get { return 1; } }
                public bool Paid { get { return false; } }
                public double PaidHour { get { return 641D; } }
                public int PaidToAcc { get { return 11; } }
                public DateTime TaskDate { get { return new DateTime(2013,7,20); } }
            }
        }

        [TestClass]
        public class TestRDGtblWorkerStatus
        {
            [TestMethod]
            public void Get()
            {
                var workerStatus = new RDGtblWorkerStatus(TestRDGs.connectionString);

                var list = workerStatus.Get();
                Assert.AreEqual(100, list.Count);
            }

            [TestMethod]
            public void Find()
            {
                var workerStatus = new RDGtblWorkerStatus(TestRDGs.connectionString);

                var status = workerStatus.Find(1);
                Assert.AreEqual("Burundi", status.Staus);
                Assert.AreEqual(1, status.StautsNo);
            }

            [TestMethod]
            public void Add()
            {
                var workerStatus = new RDGtblWorkerStatus(TestRDGs.connectionString);

                workerStatus.Add(new WorkerStatus());

                var status = workerStatus.Find(workerStatus.NextId - 1);
                Assert.AreEqual("newStaus", status.Staus);
                Assert.AreEqual(workerStatus.NextId - 1, status.StautsNo);
            }

            [TestMethod]
            public void Delete()
            {
                var workerStatus = new RDGtblWorkerStatus(TestRDGs.connectionString);

                workerStatus.Delete(workerStatus.NextId - 1);

                object obj = null;

                try
                {
                    obj = workerStatus.Find(workerStatus.NextId - 1);
                }
                catch (Exception) { }

                Assert.IsNull(obj);
            }

            [TestMethod]
            public void Update()
            {
                var workerStatus = new RDGtblWorkerStatus(TestRDGs.connectionString);

                workerStatus.Update(new WorkerStatus());

                var status = workerStatus.Find(1);
                Assert.AreEqual("newStaus", status.Staus);
                Assert.AreEqual(1, status.StautsNo);

                workerStatus.Update(new InterfaceAdaptor.WorkerStatus()
                {
                    Staus = "Burundi",
                    StautsNo = 1
                });

                status = workerStatus.Find(1);
                Assert.AreEqual("Burundi", status.Staus);
                Assert.AreEqual(1, status.StautsNo);
            }

            class WorkerStatus : Interface.IworkerStatus
            {
                public string Staus { get { return "newStaus"; } }
                public int StautsNo { get { return 1; } }
            }
        }

        [TestClass]
        public class TestRDGtblInvoicePrivet
        {
            [TestMethod]
            public void Get()
            {
                var invoicePrivet = new RDGs.RDGtblInvoicePrivet(TestRDGs.connectionString);

                var list = invoicePrivet.Get(null);
                Assert.AreEqual(20, list.Count);

                list = invoicePrivet.Get(true);
                Assert.AreEqual(8, list.Count);
                
                list = invoicePrivet.Get(false);
                Assert.AreEqual(12, list.Count);
            }

            [TestMethod]
            public void Find()
            {
                var invoicePrivet = new RDGs.RDGtblInvoicePrivet(TestRDGs.connectionString);

                var found = invoicePrivet.Find(1);

                Assert.AreEqual(1, found.Id);
                Assert.AreEqual(false, found.Active);
                Assert.AreEqual(95, found.Order[0]);
                Assert.AreEqual(40, found.Order[1]);
                Assert.AreEqual(28, found.Order[2]);
                Assert.AreEqual(39, found.Order[3]);
                Assert.AreEqual(78, found.Order[4]);
                Assert.AreEqual(41, found.Order[5]);
                Assert.AreEqual(36, found.Order[6]);
                Assert.AreEqual(9, found.Order[7]);
                Assert.AreEqual(26, found.Order[8]);
                Assert.AreEqual(36, found.Order[9]);
                Assert.AreEqual(77, found.Order[10]);
                Assert.AreEqual(93, found.Order[11]);
                Assert.AreEqual(45, found.Order[12]);
                Assert.AreEqual(35, found.Order[13]);
                Assert.AreEqual(40, found.Order[14]);
                Assert.AreEqual(88, found.Order[15]);
                Assert.AreEqual(76, found.Order[16]);
                Assert.AreEqual(28, found.Order[17]);
                Assert.AreEqual(6, found.Order[18]);
                Assert.AreEqual(48, found.Order[19]);
            }
                
            [TestMethod]
            public void Update()
            {
                var invoicePrivet = new RDGs.RDGtblInvoicePrivet(TestRDGs.connectionString);

                invoicePrivet.Update(new InvoicePrivet()
                {
                    Active = true,
                    Id = 1,
                    Order = new int?[20] { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, null, null, 5, null, 7, 8, 9, 33, null }
                });

                var found = invoicePrivet.Find(1);

                Assert.AreEqual(1, found.Id);
                Assert.AreEqual(true, found.Active);
                Assert.AreEqual(10, found.Order[0]);
                Assert.AreEqual(11, found.Order[1]);
                Assert.AreEqual(12, found.Order[2]);
                Assert.AreEqual(13, found.Order[3]);
                Assert.AreEqual(14, found.Order[4]);
                Assert.AreEqual(15, found.Order[5]);
                Assert.AreEqual(16, found.Order[6]);
                Assert.AreEqual(17, found.Order[7]);
                Assert.AreEqual(18, found.Order[8]);
                Assert.AreEqual(19, found.Order[9]);
                Assert.AreEqual(20, found.Order[10]);
                Assert.AreEqual(null, found.Order[11]);
                Assert.AreEqual(null, found.Order[12]);
                Assert.AreEqual(5, found.Order[13]);
                Assert.AreEqual(null, found.Order[14]);
                Assert.AreEqual(7, found.Order[15]);
                Assert.AreEqual(8, found.Order[16]);
                Assert.AreEqual(9, found.Order[17]);
                Assert.AreEqual(33, found.Order[18]);
                Assert.AreEqual(null, found.Order[19]);

                invoicePrivet.Update(new InvoicePrivet()
                {
                    Active = false,
                    Id = 1,
                    Order = new int?[20] { 95, 40, 28, 39, 78, 41, 36, 9, 26, 36, 77, 93, 45, 35, 40, 88, 76, 28, 6, 48 }
                });

                found = invoicePrivet.Find(1);

                Assert.AreEqual(1, found.Id);
                Assert.AreEqual(false, found.Active);
                Assert.AreEqual(95, found.Order[0]);
                Assert.AreEqual(40, found.Order[1]);
                Assert.AreEqual(28, found.Order[2]);
                Assert.AreEqual(39, found.Order[3]);
                Assert.AreEqual(78, found.Order[4]);
                Assert.AreEqual(41, found.Order[5]);
                Assert.AreEqual(36, found.Order[6]);
                Assert.AreEqual(9, found.Order[7]);
                Assert.AreEqual(26, found.Order[8]);
                Assert.AreEqual(36, found.Order[9]);
                Assert.AreEqual(77, found.Order[10]);
                Assert.AreEqual(93, found.Order[11]);
                Assert.AreEqual(45, found.Order[12]);
                Assert.AreEqual(35, found.Order[13]);
                Assert.AreEqual(40, found.Order[14]);
                Assert.AreEqual(88, found.Order[15]);
                Assert.AreEqual(76, found.Order[16]);
                Assert.AreEqual(28, found.Order[17]);
                Assert.AreEqual(6, found.Order[18]);
                Assert.AreEqual(48, found.Order[19]);
            }
 
            [TestMethod]
            public void Add()
            {
                var invoicePrivet = new RDGs.RDGtblInvoicePrivet(TestRDGs.connectionString);

                invoicePrivet.Add(new InvoicePrivet()
                {
                    Active = false,
                    Id = 1,
                    Order = new int?[20] { 95, 40, 28, 39, 78, 41, 36, 9, 26, 36, 77, 93, 45, 35, 40, 88, 76, 28, 6, 48 }
                });

                var found = invoicePrivet.Find(invoicePrivet.NextId - 1);

                Assert.AreEqual(invoicePrivet.NextId - 1, found.Id);
                Assert.AreEqual(false, found.Active);
                Assert.AreEqual(95, found.Order[0]);
                Assert.AreEqual(40, found.Order[1]);
                Assert.AreEqual(28, found.Order[2]);
                Assert.AreEqual(39, found.Order[3]);
                Assert.AreEqual(78, found.Order[4]);
                Assert.AreEqual(41, found.Order[5]);
                Assert.AreEqual(36, found.Order[6]);
                Assert.AreEqual(9, found.Order[7]);
                Assert.AreEqual(26, found.Order[8]);
                Assert.AreEqual(36, found.Order[9]);
                Assert.AreEqual(77, found.Order[10]);
                Assert.AreEqual(93, found.Order[11]);
                Assert.AreEqual(45, found.Order[12]);
                Assert.AreEqual(35, found.Order[13]);
                Assert.AreEqual(40, found.Order[14]);
                Assert.AreEqual(88, found.Order[15]);
                Assert.AreEqual(76, found.Order[16]);
                Assert.AreEqual(28, found.Order[17]);
                Assert.AreEqual(6, found.Order[18]);
                Assert.AreEqual(48, found.Order[19]);
            }

            [TestMethod]
            public void Delete()
            {
                var invoicePrivet = new RDGs.RDGtblInvoicePrivet(TestRDGs.connectionString);

                invoicePrivet.Delete(invoicePrivet.NextId - 1);

                object obj = null;

                try
                {
                    obj = invoicePrivet.Find(invoicePrivet.NextId - 1);
                }
                catch (Exception) { }

                Assert.IsNull(obj);
            }

            
            private class InvoicePrivet : Interface.IinvoicePrivet
            {
                public int Id { get; set; }
                public bool Active { get; set; }
                public int?[] Order { get; set; }
            }
        }

        [TestClass]
        public class TestRDGtblInvoiceCompany
        {
            [TestMethod]
            public void Get()
            {
                var invoiceCompany = new RDGs.RDGtblInvoiceCompany(TestRDGs.connectionString);

                var list = invoiceCompany.Get(null);
                Assert.AreEqual(20, list.Count);

                list = invoiceCompany.Get(true);
                Assert.AreEqual(12, list.Count);

                list = invoiceCompany.Get(false);
                Assert.AreEqual(8, list.Count);
            }

            [TestMethod]
            public void Find()
            {
                var invoiceCompany = new RDGs.RDGtblInvoiceCompany(TestRDGs.connectionString);

                var found = invoiceCompany.Find(1);

                Assert.AreEqual(1, found.Id);
                Assert.AreEqual(true, found.Active);
                Assert.AreEqual(30, found.Order[0]);
                Assert.AreEqual(15, found.Order[1]);
                Assert.AreEqual(15, found.Order[2]);
                Assert.AreEqual(45, found.Order[3]);
                Assert.AreEqual(86, found.Order[4]);
                Assert.AreEqual(54, found.Order[5]);
                Assert.AreEqual(74, found.Order[6]);
                Assert.AreEqual(83, found.Order[7]);
                Assert.AreEqual(57, found.Order[8]);
                Assert.AreEqual(59, found.Order[9]);
                Assert.AreEqual(36, found.Order[10]);
                Assert.AreEqual(69, found.Order[11]);
                Assert.AreEqual(63, found.Order[12]);
                Assert.AreEqual(77, found.Order[13]);
                Assert.AreEqual(1, found.Order[14]);
                Assert.AreEqual(10, found.Order[15]);
                Assert.AreEqual(39, found.Order[16]);
                Assert.AreEqual(54, found.Order[17]);
                Assert.AreEqual(38, found.Order[18]);
                Assert.AreEqual(78, found.Order[19]);
            }

            [TestMethod]
            public void Update()
            {
                var invoiceCompany = new RDGs.RDGtblInvoiceCompany(TestRDGs.connectionString);

                invoiceCompany.Update(new InvoiceCompany()
                {
                    Active = true,
                    Id = 1,
                    Order = new int?[20] { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, null, null, 5, null, 7, 8, 9, 33, null }
                });

                var found = invoiceCompany.Find(1);

                Assert.AreEqual(1, found.Id);
                Assert.AreEqual(true, found.Active);
                Assert.AreEqual(10, found.Order[0]);
                Assert.AreEqual(11, found.Order[1]);
                Assert.AreEqual(12, found.Order[2]);
                Assert.AreEqual(13, found.Order[3]);
                Assert.AreEqual(14, found.Order[4]);
                Assert.AreEqual(15, found.Order[5]);
                Assert.AreEqual(16, found.Order[6]);
                Assert.AreEqual(17, found.Order[7]);
                Assert.AreEqual(18, found.Order[8]);
                Assert.AreEqual(19, found.Order[9]);
                Assert.AreEqual(20, found.Order[10]);
                Assert.AreEqual(null, found.Order[11]);
                Assert.AreEqual(null, found.Order[12]);
                Assert.AreEqual(5, found.Order[13]);
                Assert.AreEqual(null, found.Order[14]);
                Assert.AreEqual(7, found.Order[15]);
                Assert.AreEqual(8, found.Order[16]);
                Assert.AreEqual(9, found.Order[17]);
                Assert.AreEqual(33, found.Order[18]);
                Assert.AreEqual(null, found.Order[19]);

                invoiceCompany.Update(new InvoiceCompany()
                {
                    Active = true,
                    Id = 1,
                    Order = new int?[20] { 30,	15,	15,	45,	86,	54,	74,	83,	57,	59,	36,	69,	63,	77,	1,	10,	39,	54,	38,	78 }
                });

                found = invoiceCompany.Find(1);

                Assert.AreEqual(1, found.Id);
                Assert.AreEqual(true, found.Active);
                Assert.AreEqual(30, found.Order[0]);
                Assert.AreEqual(15, found.Order[1]);
                Assert.AreEqual(15, found.Order[2]);
                Assert.AreEqual(45, found.Order[3]);
                Assert.AreEqual(86, found.Order[4]);
                Assert.AreEqual(54, found.Order[5]);
                Assert.AreEqual(74, found.Order[6]);
                Assert.AreEqual(83, found.Order[7]);
                Assert.AreEqual(57, found.Order[8]);
                Assert.AreEqual(59, found.Order[9]);
                Assert.AreEqual(36, found.Order[10]);
                Assert.AreEqual(69, found.Order[11]);
                Assert.AreEqual(63, found.Order[12]);
                Assert.AreEqual(77, found.Order[13]);
                Assert.AreEqual(1, found.Order[14]);
                Assert.AreEqual(10, found.Order[15]);
                Assert.AreEqual(39, found.Order[16]);
                Assert.AreEqual(54, found.Order[17]);
                Assert.AreEqual(38, found.Order[18]);
                Assert.AreEqual(78, found.Order[19]);
            }

            [TestMethod]
            public void Add()
            {
                var invoiceCompany = new RDGs.RDGtblInvoiceCompany(TestRDGs.connectionString);

                invoiceCompany.Add(new InvoiceCompany()
                {
                    Active = true,
                    Id = 1,
                    Order = new int?[20] { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, null, null, 5, null, 7, 8, 9, 33, null }
                });

                var found = invoiceCompany.Find(invoiceCompany.NextId - 1);

                Assert.AreEqual(invoiceCompany.NextId - 1, found.Id);
                Assert.AreEqual(true, found.Active);
                Assert.AreEqual(10, found.Order[0]);
                Assert.AreEqual(11, found.Order[1]);
                Assert.AreEqual(12, found.Order[2]);
                Assert.AreEqual(13, found.Order[3]);
                Assert.AreEqual(14, found.Order[4]);
                Assert.AreEqual(15, found.Order[5]);
                Assert.AreEqual(16, found.Order[6]);
                Assert.AreEqual(17, found.Order[7]);
                Assert.AreEqual(18, found.Order[8]);
                Assert.AreEqual(19, found.Order[9]);
                Assert.AreEqual(20, found.Order[10]);
                Assert.AreEqual(null, found.Order[11]);
                Assert.AreEqual(null, found.Order[12]);
                Assert.AreEqual(5, found.Order[13]);
                Assert.AreEqual(null, found.Order[14]);
                Assert.AreEqual(7, found.Order[15]);
                Assert.AreEqual(8, found.Order[16]);
                Assert.AreEqual(9, found.Order[17]);
                Assert.AreEqual(33, found.Order[18]);
                Assert.AreEqual(null, found.Order[19]);
            }

            [TestMethod]
            public void Delete()
            {
                var invoiceCompany = new RDGs.RDGtblInvoiceCompany(TestRDGs.connectionString);

                invoiceCompany.Delete(invoiceCompany.NextId - 1);

                object obj = null;

                try
                {
                    obj = invoiceCompany.Find(invoiceCompany.NextId - 1);
                }
                catch (Exception) { }

                Assert.IsNull(obj);
            }

            class InvoiceCompany : Interface.IinvoiceCompany
            {
                public int Id { get; set; }
                public bool Active { get; set; }
                public int?[] Order { get; set; }
            }
        }
    }
}
