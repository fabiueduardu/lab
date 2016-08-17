using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace JSpank.Test
{
    [TestClass]
    public class DbTest : BaseTest
    {
        [TestMethod]
        public void SelectGetDate()
        {
            var result = base.OnDbScalar(new string[] { "select getdate()" }).First();
            Console.WriteLine(result);
        }

        [TestMethod]
        public void SelectTables()
        {
            var result = base.OnReader("select * from sys.tables");
            foreach (var item in result)
                Console.WriteLine("db name: {0}", item["name"]);
        }

        [TestInitialize]
        public void _TestInitialize()
        {
            if (!base.OnReader("SELECT * FROM sys.tables WHERE name = 'DbTest'").Any())
                base.OnDb(new string[] { "CREATE TABLE DbTest(Id INT IDENTITY(1,1) PRIMARY KEY, Name VARCHAR(100))" });
        }

        [TestCleanup]
        public void _TestCleanup()
        {
        }
    }
}
