using Microsoft.VisualStudio.TestTools.UnitTesting;
using Migration.Core.Repositories;
using Migration.SQLServer.Repositories;

namespace Migration.UnitTests.Migration.SQLServer
{
    [TestClass]
    public class CarRepositorySQLServerTests : CarRepositoryBaseTests
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
