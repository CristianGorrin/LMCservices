using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Interface;
using API;
using Customers;

namespace UnitTestProject
{
    [TestClass]
    public class TestCustomers
    {
        [TestMethod]
        public void PrivateCustomer()
        {
            Customers.PrivateCustomer privateCustomer = new PrivateCustomer(new TestingCustomers.TestPrivateCustomer());

            // Testing get
            Assert.AreEqual(true, privateCustomer.Active);
            Assert.AreEqual("testAltPhoneNo", privateCustomer.AltPhoneNo);
            Assert.AreEqual("testEmail", privateCustomer.Email);
            Assert.AreEqual("testHomeAddress", privateCustomer.HomeAddress);
            Assert.AreEqual("testName", privateCustomer.Name);
            Assert.AreEqual("testPhoneNo", privateCustomer.PhoneNo);
            Assert.AreEqual(new TestingCustomers.PostNumberTest().Id, privateCustomer.PostNo.Id);
            Assert.AreEqual(1, privateCustomer.PrivateCustomersNo);
            Assert.AreEqual("testName", privateCustomer.Name);

            // Testing set
            privateCustomer.Active = false;
            Assert.AreEqual(false, privateCustomer.Active);
            privateCustomer.AltPhoneNo = "NewTestAltPhoneNo";
            Assert.AreEqual("NewTestAltPhoneNo", privateCustomer.AltPhoneNo);
            privateCustomer.Email = "newTestEamil";
            Assert.AreEqual("newTestEamil", privateCustomer.Email);
            privateCustomer.HomeAddress = "newTestHomeAddress";
            Assert.AreEqual("newTestHomeAddress", privateCustomer.HomeAddress);
            privateCustomer.Name = "newTestName";
            Assert.AreEqual("newTestName", privateCustomer.Name);
            privateCustomer.PhoneNo = "newTestPhoneNo";
            Assert.AreEqual("newTestPhoneNo", privateCustomer.PhoneNo);
            privateCustomer.PostNo = new TestingCustomers.PostNumberTest();
            Assert.AreEqual(new TestingCustomers.PostNumberTest().Id, privateCustomer.PostNo.Id);
            privateCustomer.Name = "newTestName";
            Assert.AreEqual("newTestName", privateCustomer.Name);

        }

        [TestMethod]
        public void CompanyCustomer()
        {
            Customers.CompanyCustomer companyCustomer = new TestingCustomers.TestCompanyCustomer();

            // testing get
            Assert.AreEqual(true, companyCustomer.Active);
            Assert.AreEqual("testAddress", companyCustomer.Address);
            Assert.AreEqual("testAltPhoneNo", companyCustomer.AltPhoneNo);
            Assert.AreEqual("testContactPerson", companyCustomer.ContactPerson);
            Assert.AreEqual(1, companyCustomer.CompanyCustomersNo);
            Assert.AreEqual(2, companyCustomer.CvrNo);
            Assert.AreEqual("testEmail", companyCustomer.Email);
            Assert.AreEqual("testName", companyCustomer.Name);
            Assert.AreEqual("testPhoneNo", companyCustomer.PhoneNo);
            Assert.AreEqual(new TestingCustomers.PostNumberTest().Id, companyCustomer.PostNo.Id);

            // testing set
            companyCustomer.Active = false;
            Assert.AreEqual(false, companyCustomer.Active);
            companyCustomer.Address = "newTestAddress";
            Assert.AreEqual("newTestAddress", companyCustomer.Address);
            companyCustomer.AltPhoneNo = "newTestAltPhoneNo";
            Assert.AreEqual("newTestAltPhoneNo", companyCustomer.AltPhoneNo);
            companyCustomer.ContactPerson = "newTestContactPreson";
            Assert.AreEqual("newTestContactPreson", companyCustomer.ContactPerson);
            companyCustomer.CvrNo = 13;
            Assert.AreEqual(13, companyCustomer.CvrNo);
            companyCustomer.Email = "newTestEmail";
            Assert.AreEqual("newTestEmail", companyCustomer.Email);
            companyCustomer.Name = "newTestName";
            Assert.AreEqual("newTestName", companyCustomer.Name);
            companyCustomer.PhoneNo = "newTestPhoneNo";
            Assert.AreEqual("newTestPhoneNo", companyCustomer.PhoneNo);
            companyCustomer.PostNo = new TestingCustomers.PostNumberTest();
            Assert.AreEqual(new TestingCustomers.PostNumberTest().Id, companyCustomer.PostNo.Id);
        }
    }

    #region  Only for testing
    public class TestingCustomers
    {
        public class TestCompanyCustomer : Customers.CompanyCustomer
        {
            public TestCompanyCustomer()
                : base (true, "testAddress", "testAltPhoneNo", "testContactPerson", 1, 2, "testEmail", 
                "testName", "testPhoneNo", new PostNumberTest())
            {
            }
        }

        public class TestPrivateCustomer : Customers.PrivateCustomer
        {
            public TestPrivateCustomer()
                : base(true, "testAltPhoneNo", "testEmail", "testHomeAddress", "testName", "testPhoneNo",
                new PostNumberTest(), 1, "testSurname")
            {
            }
        }

        public class PostNumberTest : Interface.IpostNo
        {
            public string City { get { return "testCity"; } }
            public int Id { get { return 5; } }
            public int PostNumber { get { return 5000; } }
        }
    }
    #endregion
}
