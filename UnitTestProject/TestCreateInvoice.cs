using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using RDGs;
using TestingExcelAPI;
using Interface;

namespace UnitTestProject
{
    /// <summary>
    /// Summary description for TestCreateInvoice
    /// </summary>
    [TestClass]
    public class TestCreateInvoice
    {


        public TestCreateInvoice()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void CreateInvoice()
        {
            var list = new List<Interface.IprivetOrder>();

            for (int i = 0; i < 20; i++)
            {
                list.Add(new PrivateOrder());
            }

            var test = new CreateInvoice<Interface.IprivetOrder>(list, new PrivateCustomer(), new BankAcc(), new Department(), 10, "789465132");
            test.StartExcel();
        }

        class BankAcc : Interface.IbankAccounts
        {
            public int Id { get { return 10; } }
            public string Bank { get { return "Danske Bank"; } }
            public string AccountName { get { return "TestAccName"; } }
            public int RegNo { get { return 3540; } }
            public string AccountNo { get { return "DK156541321"; } }
            public double Balance { get { return 3000D; } }
        }

        class PrivateOrder : Interface.IprivetOrder
        {
            public Iworker CreateBy
            { get { throw new NotImplementedException("Not going to be implemented."); } set { throw new NotImplementedException("Not going to be implemented."); } }
            public DateTime CreateDate { get { return DateTime.Now; } }
            public IprivetCustomer Customer { get { return new PrivateCustomer(); } set { throw new NotImplementedException("Not going to be implemented."); } }
            public DateTime? DateSendBill { get { return DateTime.Now; } }
            public int DaysToPaid { get { return 8; } }
            public string DescriptionTask { get { return "Hækkeklip"; } }
            public double HourUse { get { return 20; } }
            public int InvoiceNo { get { return 123564; } }
            public bool Paid { get { return false; } }
            public double PaidHour { get { return 140; } }
            public int PaidToAcc { get { return 2800; } }
            public DateTime TaskDate { get { return new DateTime(2014, 7, 22); } }
        }

        class PrivateCustomer : Interface.IprivetCustomer
        {
            public bool Active { get { return true; } }
            public string AltPhoneNo { get { throw new NotImplementedException("Not going to be implemented."); } }
            public string Email { get { return "test@test.test"; } }
            public string HomeAddress { get { return "Testvej 1"; } }
            public string Name { get { return "TestName"; } }
            public string PhoneNo { get { return "12345678"; } }
            public IpostNo PostNo { get { return new PostNo(); } set { throw new NotImplementedException("Not going to be implemented."); } }
            public int PrivateCustomersNo { get { return 2; } }
            public string Surname { get { return "TestSurname"; } }
        }

        class PostNo : Interface.IpostNo
        {
            public string City { get { return "Odense C"; } }
            public int Id { get { return 1; } }
            public int PostNumber { get { return 5000; } }
        }

        class Department : Interface.Idepartment
        {
            public bool Active { get { throw new NotImplementedException("Not going to be implemented."); } }
            public string Address { get { throw new NotImplementedException("Not going to be implemented."); } }
            public string AltPhoneNo { get { throw new NotImplementedException("Not going to be implemented."); } }
            public string CompanyName { get { throw new NotImplementedException("Not going to be implemented."); } }
            public int CvrNo { get { return 35562508; } }
            public int Deparment { get { throw new NotImplementedException("Not going to be implemented."); } }
            public Iworker DeparmentHead { get { return new Worker(); } }
            public string Email { get { return "luismiguel-6300@hotmail.com"; } }
            public string PhoneNo { get { return "52232442"; } }
            public IpostNo PostNo { get { throw new NotImplementedException("Not going to be implemented."); } }
        }

        class Worker : Interface.Iworker
        {
            public bool Active { get { throw new NotImplementedException("Not going to be implemented."); } }
            public string Address { get { throw new NotImplementedException("Not going to be implemented."); } }
            public string AltPhoneNo { get { throw new NotImplementedException("Not going to be implemented."); } }
            public string Email { get { throw new NotImplementedException("Not going to be implemented."); } }
            public string Name { get { return "Luis Miguel"; } }
            public string PhoneNo { get { throw new NotImplementedException("Not going to be implemented."); } }
            public IpostNo PostNo { get { throw new NotImplementedException("Not going to be implemented."); } }
            public string Surname { get { return "C. Gorrin"; } }
            public IworkerStatus WorkerStatus { get { throw new NotImplementedException("Not going to be implemented."); } set { throw new NotImplementedException("Not going to be implemented."); } }
            public int WorkNo { get { throw new NotImplementedException("Not going to be implemented."); } }
        }

    }
}
