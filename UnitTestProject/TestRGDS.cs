using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Linq;
using Interface;
using RDGs;

namespace UnitTestProject
{
    [TestClass]
    public class TestRDGs
    {
        [TestMethod]
        public void SetUpDB()
        {
            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext())
            {
                
            }
        }
    }
}
