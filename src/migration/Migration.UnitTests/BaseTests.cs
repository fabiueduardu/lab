using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Migration.UnitTests
{
    public class BaseTests
    {

        protected string Key
        {
            get
            {

                return Guid.NewGuid().ToString();
            }
        }

        [TestInitialize]
        public void _TestInitialize()
        {
        }
    }
}
