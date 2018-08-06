using Migration.SQLite.Entities;
using SQLite.CodeFirst;
using System.Data.Entity;

namespace Migration.SQLite.Context
{
    public class DbInitializer : SqliteDropCreateDatabaseWhenModelChanges<MigrationContext>
    {
        public DbInitializer(DbModelBuilder modelBuilder)
            : base(modelBuilder, typeof(CustomHistory))
        { }

        protected override void Seed(MigrationContext context)
        {
            // Here you can seed your core data if you have any.
        }
    }

}
