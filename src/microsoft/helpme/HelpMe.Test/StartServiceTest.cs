using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HelpMe.Service;

namespace HelpMe.Test
{
    [TestClass]
    public class StartServiceTest  :BaseTest
    {
        [TestMethod]
        public void Run()
        {
            StartService.Run();
        }
    }
}
