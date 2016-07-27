using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace JSpank.Test
{
    [TestClass]
    public class DbTest : BaseTest
    {
        [TestMethod]
        public void HelloWorld()
        {
            for (var i = 0; i < 1; i++)
            {
                var result = base.OnDbScalar(new string[] { string.Format("select 'HelloWorld on db {0}'", i) }).First();
                Console.WriteLine(result);
            }

            var resultdt = base.OnDbScalar(new string[] { "select getdate()" }).First();
            Console.WriteLine(resultdt);
        }

        [TestInitialize]
        public void _TestInitialize()
        {
            var result = base.OnDbScalar(new string[] { "select '_TestInitialize on db '" }).First();
            Console.WriteLine(result);
        }

        [TestCleanup]
        public void _TestCleanup()
        {
            var result = base.OnDbScalar(new string[] { "select '_TestCleanup on db '" }).First();
            Console.WriteLine(result);
        }
    }
}
