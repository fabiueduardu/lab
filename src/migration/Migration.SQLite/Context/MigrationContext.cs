using System.Data.Common;
using System.Data.Entity;

namespace Migration.SQLite.Context
{
    public class MigrationContext : DbContext
    {
        public MigrationContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            Configure();
        }

        public MigrationContext(DbConnection connection, bool contextOwnsConnection)
            : base(connection, contextOwnsConnection)
        {
            Configure();
        }

        private void Configure()
        {
            Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            ModelConfiguration.Configure(modelBuilder);
            var initializer = new DbInitializer(modelBuilder);
            Database.SetInitializer(initializer);
        }
    }
}
