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

        }
    }
}
