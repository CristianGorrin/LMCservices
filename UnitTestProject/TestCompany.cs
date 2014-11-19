using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Company;
using Interface;
using API;

namespace UnitTestProject
{
    [TestClass]
    public class TestCompany
    {
        [TestMethod]
        public void WorkerStatus()
        {
            var workerStatus = new Company.WorkerStatus("testStaus", 1);
            Assert.AreNotEqual(null, workerStatus);

            // testing get
            Assert.AreEqual( "testStaus", workerStatus.Staus);
            Assert.AreEqual(1, workerStatus.StautsNo);

            // testing set
            workerStatus.Staus = "newStaus";
            Assert.AreEqual("newStaus", workerStatus.Staus);
        }

        [TestMethod]
        public void Department()
        {
            var department = new Company.Department(new TestingCompany.DepartmentTest());

            // testing get
            Assert.AreEqual(true, department.Active);
            Assert.AreEqual("testAddress", department.Address);
            Assert.AreEqual("testAltPhoneNo", department.AltPhoneNo);
            Assert.AreEqual("testCompanyName", department.CompanyName);
            Assert.AreEqual(1, department.CvrNo);
            Assert.AreEqual(2, department.Deparment);
            Assert.AreEqual(new TestingCompany.WorkerTest().WorkNo, department.DeparmentHead.WorkNo);
            Assert.AreEqual("testEmail", department.Email);
            Assert.AreEqual("testPhoneNo", department.PhoneNo);
            Assert.AreEqual(new TestingCompany.PostNumberTest().Id, department.PostNo.Id);

            // testing set
            department.Active = false;
            Assert.AreEqual(false, department.Active);
            department.Address = "newTestAddress";
            Assert.AreEqual("newTestAddress", department.Address);
            department.AltPhoneNo = "newTestAltPhoneNo";
            Assert.AreEqual("newTestAltPhoneNo", department.AltPhoneNo);
            department.CompanyName = "newTestCompanyName";
            Assert.AreEqual("newTestCompanyName", department.CompanyName);
            department.CvrNo = 12;
            Assert.AreEqual(12, department.CvrNo);
            department.DeparmentHead = new Company.Worker(false, "asd", "asd", "asd", "asd", "asd", new TestingCompany.PostNumberTest(), "asd", new TestingCompany.WorkerStatusTest(), 1);
            Assert.AreEqual(department.DeparmentHead.WorkNo, new TestingCompany.WorkerTest().WorkNo);
            department.Email = "newTestEmail";
            Assert.AreEqual("newTestEmail", department.Email);
            department.PhoneNo = "newTestPhoneNo";
            Assert.AreEqual("newTestPhoneNo", department.PhoneNo);
            department.PostNo = new TestingCompany.PostNumberTest();
            Assert.AreEqual(department.PostNo.Id, new TestingCompany.PostNumberTest().Id);
        }
        
        [TestMethod]
        public void Worker()
        {
            var worker = new Company.Worker(new TestingCompany.WorkerTest());

            // testing get
            Assert.AreEqual(true, worker.Active);
            Assert.AreEqual("testAddress", worker.Address);
            Assert.AreEqual("testAltPhoneNo", worker.AltPhoneNo);
            Assert.AreEqual("testEmail", worker.Email);
            Assert.AreEqual("testName", worker.Name);
            Assert.AreEqual("testPhoneNo", worker.PhoneNo);
            Assert.AreEqual(new TestingCompany.PostNumberTest().Id, worker.PostNo.Id);
            Assert.AreEqual("testSurname", worker.Surname);
            Assert.AreEqual(new TestingCompany.WorkerStatusTest().StautsNo, worker.WorkerStatus.StautsNo);
            Assert.AreEqual(1, worker.WorkNo);

            // testing get
            worker.Active = false;
            Assert.AreEqual(false, worker.Active);
            worker.Address = "newTestAddress";
            Assert.AreEqual("newTestAddress", worker.Address);
            worker.AltPhoneNo = "newTestAltPhoneNo";
            Assert.AreEqual("newTestAltPhoneNo", worker.AltPhoneNo);
            worker.Email = "newTestEamil";
            Assert.AreEqual("newTestEamil", worker.Email);
            worker.Name = "newTestName";
            Assert.AreEqual("newTestName", worker.Name);
            worker.PhoneNo = "newTestPhoneNo";
            Assert.AreEqual("newTestPhoneNo", worker.PhoneNo);
            worker.PostNo = new TestingCompany.PostNumberTest();
            Assert.AreEqual(new TestingCompany.PostNumberTest().Id, worker.PostNo.Id);
            worker.Surname = "newTestSurname";
            Assert.AreEqual("newTestSurname", worker.Surname);
            worker.WorkerStatus = new TestingCompany.WorkerStatusTest();
            Assert.AreEqual(new TestingCompany.WorkerStatusTest().StautsNo, worker.WorkerStatus.StautsNo);
        }
    }

    #region  Only for testing
    public class TestingCompany
    {
        public class PostNumberTest : Interface.IpostNo
        {
            public string City { get { return "testCity"; } }
            public int Id { get { return 1; } }
            public int PostNumber { get { return 6300; } }
        }

        public class WorkerStatusTest : Company.WorkerStatus
        {
            public WorkerStatusTest()
                : base("testStaus", 1)
            {
            }
        }

        public class WorkerTest : Company.Worker
        {
            public WorkerTest()
                : base(true, "testAddress", "testAltPhoneNo", "testEmail", "testName",
                "testPhoneNo", new TestingCompany.PostNumberTest(), "testSurname", new TestingCompany.WorkerStatusTest(), 1)
            {
            }
        }

        public class DepartmentTest : Company.Department
        {
            public DepartmentTest()
                : base(true, "testAddress", "testAltPhoneNo", "testCompanyName", 1,
                2, new WorkerTest(), "testEmail", "testPhoneNo", new TestingCompany.PostNumberTest())
            {
            }
        }
    }
    #endregion
}
