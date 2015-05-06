using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Infrastructure;
using System.Linq;
using CcAcca.DemoDownstream;
using CcAcca.DemoUpstream;
using CcAcca.DemoUpstreamMigrations.Migrations;
using NUnit.Framework;

namespace CcAcca.EntityFramework.MultiMigrate.Tests
{
    [TestFixture]
    public class MultiMigrateDbToLastestVersionTests
    {
        private readonly List<IDisposable> _trash = new List<IDisposable>();

        private DemoDbContext CreateDbContext(string connectionStringName = null)
        {
            var context = new DemoDbContext(connectionStringName);
            _trash.Add(context);
            return context;
        }

        [SetUp]
        public void Setup()
        {
            Database.SetInitializer(new NullDatabaseInitializer<DemoDbContext>());

            using (var db = new DemoDbContext())
            {
                if (db.Database.Exists())
                {
                    db.Database.Delete();
                }
            }
        }

        [TearDown]
        public void Teardown()
        {
            _trash.ForEach(d => d.Dispose());
        }

        [Test]
        public void CanUpgradeToLatestVs()
        {
            // when
            var initializer = new DemoMultiMigrateDbToLastestVersion();
            initializer.InitializeDatabase(CreateDbContext());

            // then...

            var baseModelMigrator = new DbMigrator(new Configuration());
            var baseMigrations = baseModelMigrator.GetDatabaseMigrations().ToList();
            Assert.That(baseMigrations.Count, Is.EqualTo(5));

            var migrator = new DbMigrator(new DemoDownstreamMigrations.Migrations.Configuration());
            var migrations = migrator.GetDatabaseMigrations().ToList();
            Assert.That(migrations.Count, Is.EqualTo(7));

            // test that that the database supports the latest model
            var db = CreateDbContext();
            var userRole = new CustomUserRole { Key = 2, Name = "Whatever", CustomRoleProp = "Whatever"};
            db.UserRoles.Add(userRole);
            db.Assets.Add(new Asset {Reference = "Blah", Title = "Blah", RequiredUserRole = userRole});
            db.SaveChanges();

            List<FakeEntity> fakes = db.FakeEntities.ToList();
            Assert.That(fakes, Is.Not.Empty);
        }

        [Test]
        public void CanUpgradeToLatestVs_SkippedMigrationsAsEmbeddedResource()
        {
            // when
            var initializer = new DemoMultiMigrateDbToLastestVersion
            {
                SkippedMigrations = new[] { "Assembly:CcAcca.DemoDownstreamMigrations" }
            };
            initializer.InitializeDatabase(CreateDbContext());

            // then...

            var baseModelMigrator = new DbMigrator(new Configuration());
            var baseMigrations = baseModelMigrator.GetDatabaseMigrations().ToList();
            Assert.That(baseMigrations.Count, Is.EqualTo(5));

            var migrator = new DbMigrator(new DemoDownstreamMigrations.Migrations.Configuration());
            var migrations = migrator.GetDatabaseMigrations().ToList();
            Assert.That(migrations.Count, Is.EqualTo(7));

            // test that that the database supports the latest model
            var db = CreateDbContext();
            var userRole = new CustomUserRole { Key = 2, Name = "Whatever", CustomRoleProp = "Whatever"};
            db.UserRoles.Add(userRole);
            db.Assets.Add(new Asset {Reference = "Blah", Title = "Blah", RequiredUserRole = userRole});
            db.SaveChanges();

            List<FakeEntity> fakes = db.FakeEntities.ToList();
            Assert.That(fakes, Is.Not.Empty);
        }


        #region EF exploration tests

        [Test]
        public void CanScriptMigration()
        {
            var migrator = new MigratorScriptingDecorator(new DbMigrator(new DemoDownstreamMigrations.Migrations.Configuration()));
            string sql = migrator.ScriptUpdate("201501032325042_Merge BaseModel3", "201501110901388_Add CustomUserRole");
            Console.Out.WriteLine(sql);
        }

        [Test]
        public void CanScriptMigrationFromBaseData()
        {
            // note: notice that there is a bug in the ef that causes schema name for insert into MigrationsHistory table to be incorrect
            // (bug reported here: https://entityframework.codeplex.com/workitem/1871)
            var migrator = new MigratorScriptingDecorator(new DbMigrator(new Configuration()));
            string sql = migrator.ScriptUpdate("201501032315036_Introduce by-directional association", "201501032326177_Rename LookupItem pk");
            Console.Out.WriteLine(sql);
        }

        #endregion
    }
}