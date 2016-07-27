using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Web;

namespace JSpank.Test
{
    [TestClass]
    public class WebTest  : BaseTest
    {
        [TestMethod]
        public void HasSession()
        {
            var key = "me";
            Assert.IsNull(HttpContext.Current.Session[key]);
            HttpContext.Current.Session.Add("me", base.Key5);
            Assert.IsNotNull(HttpContext.Current.Session[key]);
        }

        [TestInitialize]
        public void _TestInitialize()
        {
            base.OnWebContext();
            Console.WriteLine("_WebTestTestInitialize");
        }

        [TestCleanup]
        public void _TestCleanup()
        {
            Console.WriteLine("_WebTestTestCleanup");
        }
    }
}
