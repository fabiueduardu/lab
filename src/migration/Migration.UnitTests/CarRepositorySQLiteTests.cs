using Microsoft.VisualStudio.TestTools.UnitTesting;
using Migration.Core.Repositories;
using Migration.SQLite.Repositories;

namespace Migration.UnitTests.Migration.SQLServer
{
    [TestClass]
    public class CarRepositorySQLiteTests : CarRepositoryBaseTests
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
