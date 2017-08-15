using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Migration.Core.Repositories;

namespace Migration.UnitTests
{
    public abstract class CarRepositoryBaseTests : BaseTests
    {
        public abstract ICarRepository CarRepository { get; }

        [TestMethod]
        public void Add()
        {
            this.Add_To();
        }

        [TestMethod]
        public void Add_100()
        {
            int total = 100;
            this.Add_To(total);
        }

        //[TestMethod]
        public void Add_1000()
        {
            int total = 1000;
            this.Add_To(total);
        }

        [TestMethod]
        public void AddModel()
        {
            this.Add_ModelTo();

            var collection = CarRepository.GetAllModels();
            Assert.AreEqual(collection.Count(), 1);
        }

        [TestMethod]
        public void AddTrip()
        {

            int total = 1000;
            this.Add_To(total);

            var collection = CarRepository.GetAll();
            Assert.AreEqual(collection.Count(), 1);

            var item = collection.FirstOrDefault();
            var result = CarRepository.AddTrip(item.Id, string.Concat("from - ", base.Key), string.Concat("to - ", base.Key));
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Get()
        {
            int total = 1000;
            this.Add_To(total);

            var collection = CarRepository.GetAll();
            Assert.AreEqual(collection.Count(), 1);

            var item = collection.FirstOrDefault();
            var item_db = CarRepository.Get(item.Id);
            Assert.IsNotNull(item_db);
        }

        [TestMethod]
        public void GetAll()
        {
            int total = 1000;
            this.Add_To(total);

            var collection = CarRepository.GetAll();
            Assert.AreEqual(collection.Count(), 1);
        }

        void Add_To(int total = 1)
        {
            this.Add_ModelTo();

            var collection = CarRepository.GetAllModels();
            Assert.AreEqual(collection.Count(), 1);

            var item_model = collection.First();

            for (var i = 0; i < total; i++)
            {
                var result = CarRepository.Add(item_model.Id, base.Key);
                Assert.IsTrue(result);
            }
        }

        void Add_ModelTo(int total = 1)
        {
            for (var i = 0; i < total; i++)
            {
                var result = CarRepository.AddModel(base.Key);
                Assert.IsTrue(result);
            }
        }
    }
}
