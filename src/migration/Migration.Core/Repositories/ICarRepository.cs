using Migration.Core.Entities;
using System;
using System.Collections.Generic;

namespace Migration.Core.Repositories
{
    public interface ICarRepository
    {
        bool Add(Guid carModelId, string name);

        bool AddModel(string name);

        bool AddTrip(Guid carId, string from, string to);

        Car Get(Guid id);

        IEnumerable<CarModel> GetAllModels(int take = 1);

        IEnumerable<Car> GetAll(int take = 1);
    }
}
