using System.Data.Entity.Migrations;
using CcAcca.EntityFramework.MultiMigrate;
using CcAcca.Library;
using CcAcca.LibraryMigrations.Migrations;

namespace CcAcca.LibraryMigrations
{
    public class MyMultiMigrateDbToLastestVersion : MultiMigrateDbToLastestVersion
    {
        public MyMultiMigrateDbToLastestVersion(string connectionStringName = null) : 
            base(new DbMigrationsConfiguration[] { new Configuration(), new BaseLibraryMigrations.Migrations.Configuration()}, 
            connectionStringName ?? new LibraryDbContext().Database.Connection.ConnectionString)
        {
            SkippedMigrations = new[] { "Assembly:CcAcca.LibraryMigrations" };
        }
    }
}