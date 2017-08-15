using Migration.Core.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;
using Migration.Core.Entities;
using Migration.SQLServer.Context;

namespace Migration.SQLServer.Repositories
{
    public class CarRepository : ICarRepository
    {
        public bool Add(Guid carModelId, string name)
        {
            using (var db = new MigrationContext())
            {
                db.Cars.Add(new Car
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    CarModelId = carModelId
                });

                return db.SaveChanges() > 0;
            }
        }

        public bool AddModel(string name)
        {
            using (var db = new MigrationContext())
            {
                db.CarModels.Add(new CarModel
                {
                    Id = Guid.NewGuid(),
                    Name = name
                });

                return db.SaveChanges() > 0;
            }
        }

        public bool AddTrip(Guid carId, string from, string to)
        {
            using (var db = new MigrationContext())
            {
                db.CarTrips.Add(new CarTrip
                {
                    Id = Guid.NewGuid(),
                    CarId = carId,
                    From = from,
                    To = to
                });

                return db.SaveChanges() > 0;
            }
        }

        public Car Get(Guid id)
        {
            using (var db = new MigrationContext())
            {
                return db.Cars.FirstOrDefault(a => a.Id.Equals(id));
            }
        }

        public IEnumerable<Car> GetAll(int take = 1)
        {
            using (var db = new MigrationContext())
            {
                return db.Cars.Take(take).ToList();
            }
        }

        public IEnumerable<CarModel> GetAllModels(int take = 1)
        {
            using (var db = new MigrationContext())
            {
                return db.CarModels.Take(take).ToList();
            }
        }
    }
}
