using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations.Design;
using System.IO;
using System.Linq;
using CcAcca.DemoDownstream;
using NUnit.Framework;

namespace CcAcca.EntityFramework.MultiMigrate.Tests.CmdLineTests
{
    /// <summary>
    /// Tests that explore the functionality of the <see cref="ToolingFacade"/> class
    /// which is used as the core implementation in MultiMigrate.exe
    /// </summary>
    [TestFixture]
    public class ToolingFacadeTests
    {
        private readonly List<IDisposable> _trash = new List<IDisposable>();

        [SetUp]
        public void Setup()
        {
            var db = new DemoDbContext("DemoDbContext");
            if (db.Database.Exists())
            {
                db.Database.Delete();
            }
        }

        [TearDown]
        public void Teardown()
        {
            _trash.ForEach(d => d.Dispose());
        }

        [Test]
        public void CanInstantiateEfTooling()
        {
            ToolingFacade facade = CreateToolingFacadeForBaseData();
            Assert.That(facade, Is.Not.Null);
        }

        [Test]
        public void CanListPendingMigrations()
        {
            var facade = CreateToolingFacadeForBaseData();
            List<string> migrations = facade.GetPendingMigrations().ToList();
            Assert.That(migrations, Is.Not.Empty);
            migrations.ForEach(Console.WriteLine);
        }

        [Test]
        public void CanMigrateDb()
        {
            var facade = CreateToolingFacadeForBaseData();
            facade.Update(null, true);
        }

        [Test]
        public void CanMigrateDbPiecemeal()
        {
            var facade = CreateToolingFacadeForBaseData();
            List<string> migrations = facade.GetPendingMigrations().ToList();
            migrations.ForEach(m => facade.Update(m, true));
        }

        
        [Test]
        public void CanMigrateDbFromMultipleConfigurations()
        {
            var bdf = CreateToolingFacadeForBaseData();
            var bdm = new DelegatedMigrator(bdf.GetPendingMigrations, bdf.GetDatabaseMigrations, migration => bdf.Update(migration, true), (s, t) => bdf.ScriptUpdate(s, t, true), CreateDbConnection());
            var mdf = CreateToolingFacadeForMainData();
            var mdm = new DelegatedMigrator(mdf.GetPendingMigrations, mdf.GetDatabaseMigrations, migration => mdf.Update(migration, true), (s, t) => mdf.ScriptUpdate(s, t, true), CreateDbConnection())
            {
                IsAutoMigrationsEnabled = true
            };
            var migrationRunner = new MultiMigrateDbToLastestVsRunner(new[] { bdm, mdm })
            {
                SkippedMigrations = new[] { "201501032326177_Rename LookupItem pk" }
            };
            migrationRunner.Run();
        }

        private DbConnection CreateDbConnection()
        {
            ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["DemoDbContext"];
            DbProviderFactory factory =
                DbProviderFactories.GetFactory(connectionString.ProviderName);
            //create a command of the proper type.
            DbConnection conn = factory.CreateConnection();
            //set the connection string
// ReSharper disable once PossibleNullReferenceException
            conn.ConnectionString = connectionString.ConnectionString;
            return conn;
        }


        private ToolingFacade CreateToolingFacadeForBaseData()
        {
            string workingDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var facade = new ToolingFacade(
                migrationsAssemblyName: "CcAcca.DemoUpstreamMigrations",
                contextAssemblyName: null,
                configurationTypeName: "CcAcca.DemoUpstreamMigrations.Migrations.Configuration",
                workingDirectory: workingDirectory,
                configurationFilePath:
                    Path.Combine(workingDirectory, "CcAcca.EntityFramework.MultiMigrate.Tests.dll.config"),
                dataDirectory: null,
                connectionStringInfo: new DbConnectionInfo("DemoDbContext")
                );
            _trash.Add(facade);
            return facade;
        }
        private ToolingFacade CreateToolingFacadeForMainData()
        {
            string workingDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var facade = new ToolingFacade(
                migrationsAssemblyName: "CcAcca.DemoDownstreamMigrations",
                contextAssemblyName: null,
                configurationTypeName: "CcAcca.DemoDownstreamMigrations.Migrations.Configuration",
                workingDirectory: workingDirectory,
                configurationFilePath:
                    Path.Combine(workingDirectory, "CcAcca.EntityFramework.MultiMigrate.Tests.dll.config"),
                dataDirectory: null,
                connectionStringInfo: new DbConnectionInfo("DemoDbContext")
                );
            _trash.Add(facade);
            return facade;
        }
    }
}