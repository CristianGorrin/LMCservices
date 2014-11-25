using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using PostNo;
using Interface;

namespace UnitTestProject
{
    [TestClass]
    public class TestPostNo
    {
        [TestMethod]
        public void PostNo()
        {
            var postNo = new PostNo.PostNo("testCity", 1, 6300);

            // testing get
            Assert.AreEqual("testCity", postNo.City);
            Assert.AreEqual(1, postNo.Id);
            Assert.AreEqual(6300, postNo.PostNumber);

            // testing set
            postNo.City = "newTestCity";
            var city = (string)new PrivateObject(postNo).GetField("city");
            Assert.AreEqual("newTestCity", city);
            postNo.PostNumber = 5000;
            var postNumber = (int)new PrivateObject(postNo).GetField("postNo");
            Assert.AreEqual(5000, postNumber);
        }
    }
}