using System.Data.Common;
using System.Data.Entity;
using CcAcca.BaseLibrary;
using CcAcca.Library;

namespace Library.Web
{
    public class MultiMigrateDbToLatestVs : IDatabaseInitializer<LibraryDbContext>
    {
        public void InitializeDatabase(LibraryDbContext context)
        {
            InitializeBaseLibrarySchema(context.Database.Connection);
            InitializeLibrarySchema(context.Database.Connection);
        }

        private void InitializeLibrarySchema(DbConnection connection)
        {

            using (var db = new CcAcca.LibraryMigrations.LibraryDbContext(connection, false))
            {
                var initializer =
                    new MigrateDatabaseToLatestVersion
                        <CcAcca.LibraryMigrations.LibraryDbContext, CcAcca.LibraryMigrations.Migrations.Configuration>(true);
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