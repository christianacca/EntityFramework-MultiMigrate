using System.Collections.Generic;
using System.Data.Entity.Migrations;
using CcAcca.DemoSharedDataMigrations.Migrations;

namespace CcAcca.EntityFramework.MultiMigrate.Tests
{
    public class DemoMultiMigrateDbToLastestVersion : MultiMigrateDbToLastestVersion
    {
        public DemoMultiMigrateDbToLastestVersion(IEnumerable<DbMigrationsConfiguration> configurations = null, string connectionStringName = null)
            : base(configurations ?? new DbMigrationsConfiguration[]
            {
                new Configuration(),
                new DemoDataMigrations.Migrations.Configuration()
            }, connectionStringName ?? "DemoDbContext")
        {
            SkippedMigrations = new[] { "201501032326177_Rename LookupItem pk" };
        }
    }
}