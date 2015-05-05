using System.Data.Common;
using System.Data.Entity;
using CcAcca.BaseLibrary;
using CcAcca.BaseLibrary.DemoModel;

namespace BaseLibrary.Web
{
    public class MultiMigrateDbToLatestVs : IDatabaseInitializer<DemoDbContext>
    {
        public void InitializeDatabase(DemoDbContext context)
        {
            InitializeBaseLibrarySchema(context.Database.Connection);
            InitializeDemoSchema(context.Database.Connection);
        }

        private void InitializeDemoSchema(DbConnection connection)
        {
            using (var db = new CcAcca.BaseLibrary.DemoMigrations.DemoDbContext(connection, false))
            {
                var initializer =
                    new MigrateDatabaseToLatestVersion
                        <CcAcca.BaseLibrary.DemoMigrations.DemoDbContext, CcAcca.BaseLibrary.DemoMigrations.Migrations.Configuration>(true);
                initializer.InitializeDatabase(db);
            }
        }

        private static void InitializeBaseLibrarySchema(DbConnection connection)
        {
            using (var db = new BaseLibraryDbContext(connection, false))
            {
                var initializer =
                    new MigrateDatabaseToLatestVersion
                        <BaseLibraryDbContext, CcAcca.BaseLibraryMigrations.Migrations.Configuration>(true);
                initializer.InitializeDatabase(db);
            }
        }
    }
}