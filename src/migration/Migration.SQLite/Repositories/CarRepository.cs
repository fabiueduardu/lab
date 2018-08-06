using Migration.Core.Entities;
using Migration.Core.Repositories;
using Migration.SQLite.Context;
using System;
using System.Collections.Generic;

namespace Migration.SQLite.Repositories
{
    public class CarRepository : ICarRepository
    {
        private const string ConnectionString = "SQLiteDb";

        public bool Add(Guid carModelId, string name)
        {
            using (var context = new MigrationContext(ConnectionString))
            {
                return true;
            }
        }

        public bool AddModel(string name)
        {
            using (var db = new MigrationContext(ConnectionString))
            {
                db.Set<CarModel>().Add(new CarModel
                {
                    Id = Guid.NewGuid(),
                    Name = name
                });

                return db.SaveChanges() > 0;
            }
        }

        public bool AddTrip(Guid carId, string from, string to)
        {
            throw new NotImplementedException();
        }

        public Car Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Car> GetAll(int take = 1)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CarModel> GetAllModels(int take = 1)
        {
            throw new NotImplementedException();
        }
    }
}
