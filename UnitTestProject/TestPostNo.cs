using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using PostNo;

namespace UnitTestProject
{
    [TestClass]
    public class TestPostNo
    {
        [TestMethod]
        public void PostNumber()
        {
            PostNo.PostNo postNumber = new TestingPostNo.PostNumber();
 
            // testing get
            Assert.AreEqual("testCity", postNumber.City);
            Assert.AreEqual(1, postNumber.Id);
            Assert.AreEqual(6300, postNumber.PostNumber);

            // testing set
            postNumber.City = "newTestCity";
            // TODO fix
            string test = (string)new PrivateObject(postNumber).GetField("city");
            Assert.AreEqual("newTestCity", test);

            postNumber.PostNumber = 5000;
        }
    }

    public class TestingPostNo 
    {
        public class PostNumber : PostNo.PostNo
        {
            public PostNumber()
                : base("testCity", 1, 6300)
            {
            }
        }
    }
}
