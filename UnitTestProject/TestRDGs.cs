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
    // The tests is base on the values from 

    [TestClass]
    public class TestRDGs
    {
        [TestClass]
        public class TestRDGtblPostNo
        {
            [TestMethod]
            public void Get()
            {
                var tblPostNo = new RDGs.RDGtblPostNo();
                var list = tblPostNo.Get();

                Assert.AreEqual(591, list.Count);
            }

            [TestMethod]
            public void Find()
            {
                var tblPostNo = new RDGs.RDGtblPostNo();
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
                var tblPostNo = new RDGs.RDGtblPostNo();

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
                var tblPostNo = new RDGs.RDGtblPostNo();

                var newPostNo = new PostNum() { City = "addNewCity", PostNumber = 50 };
                tblPostNo.Add(newPostNo);

                var result = tblPostNo.Find(tblPostNo.NextId - 1);

                Assert.AreEqual("addNewCity", result.City);
                Assert.AreEqual(50, result.PostNumber);
            }

            [TestMethod]
            public void Delete()
            {
                var tblPostNo = new RDGs.RDGtblPostNo();

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
                var bankAccounts = new RDGtblBankAccounts();

                var list = bankAccounts.Get();

                if (list.Count != 100)
                {
                    throw new AssertFailedException("RDGtblBankAccounts doesn't return expect amount");
                }
            }

            [TestMethod]
            public void Find()
            {
                var bankAccounts = new RDGtblBankAccounts();

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
                var bankAccounts = new RDGtblBankAccounts();

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
                var bankAccounts = new RDGtblBankAccounts();

                bankAccounts.Add(new BankAcc() { AccountName = "newAccName", AccountNo = "newAccNo-486532",
                    RegNo = 4856, Balance = 4184.4685D, Bank = "newBank" });

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
                var bankAccounts = new RDGtblBankAccounts();

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
        
        
    }
}
