namespace Migration.MySQL.Migrations
{
    using Core.Entities;
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Migration.MySQL.Context.MigrationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Migration.MySQL.Context.MigrationContext context)
        {
            var carModelId = Guid.NewGuid();
            context.CarModels.AddOrUpdate(
              p => p.Id,
              new CarModel { Id = carModelId, Name = "Model 1" }
            );

            var carId = Guid.NewGuid();
            context.Cars.AddOrUpdate(
              p => p.Id,
              new Car { Id = carId, Name = "Car 1", CarModelId = carModelId }
            );
        }
    }
}
