using Microsoft.VisualStudio.TestTools.UnitTesting;
using Migration.Core.Repositories;
using Migration.MySQL.Repositories;

namespace Migration.UnitTests.Migration.SQLServer
{
    [TestClass]
    public class CarRepositoryMySQLTests : CarRepositoryBaseTests
    {
        public override ICarRepository CarRepository
        {
            get
            {

                return new CarRepository();
            }
        }
    }
}
