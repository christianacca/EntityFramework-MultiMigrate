﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Infrastructure;
using System.Linq;

namespace CcAcca.EntityFramework.MultiMigrate
{
    /// <summary>
    ///     A database initialiser that will upgrade the database to the latest version as determined
    ///     by one or more <see cref="DbMigrationsConfiguration" />
    /// </summary>
    public class MultiMigrateDbToLastestVersion : IDatabaseInitializer<DbContext>
    {
        private static readonly Action<IEnumerable<DbMigrationsConfiguration>, DbContext> NoOpConfigurator = (configurations, context) => { };

        private readonly string _connectionStringName;
        private Action<IEnumerable<DbMigrationsConfiguration>, DbContext> _configurator = NoOpConfigurator;
        private readonly IEnumerable<DbMigrationsConfiguration> _configurations;

        /// <remarks>
        ///     The order of <paramref name="configurations" /> should be least dependent model
        ///     first
        /// </remarks>
        /// <param name="configurations">
        ///     Defines the migrations to run. The order of <paramref name="configurations" />
        ///     should be least dependent model first
        /// </param>
        /// <param name="connectionStringName">
        ///     The name of a connection string that specifies the target database to migrate
        /// </param>
        public MultiMigrateDbToLastestVersion(IEnumerable<DbMigrationsConfiguration> configurations,
            string connectionStringName)
        {
            _connectionStringName = connectionStringName;
            _configurations = configurations ?? Enumerable.Empty<DbMigrationsConfiguration>();
            Logger = NullMigrationsLogger.Instance;
        }

        /// <summary>
        /// Registers a delegate which will be executed just before migrations are run. This delegate can
        /// be used to apply final migration configuration settings
        /// </summary>
        /// <param name="configurator">
        /// A delegate instance that will be supplied the migration configuration(s) along with the
        /// database context that triggered the migration
        /// </param>
        public void SetConfigOverride(Action<IEnumerable<DbMigrationsConfiguration>, DbContext> configurator)
        {
            _configurator = configurator ?? NoOpConfigurator;
        }

        /// <summary>
        ///     Names of migrations that should be skipped
        /// </summary>
        /// <remarks>
        ///     This is useful when migrations from one configuration should be used in place of one or more migrations
        ///     defined in another configuration
        /// </remarks>
        public IEnumerable<string> SkippedMigrations { get; set; }

        /// <summary>
        ///     Assign the logger that should output generated log messages
        /// </summary>
        /// <remarks>
        ///     By default this will be assigned a <see cref="NullMigrationsLogger" />
        /// </remarks>
        public MigrationsLogger Logger { get; set; }

        public virtual void InitializeDatabase(DbContext context)
        {
            AssertDbContextConnection(context.Database.Connection);
            ApplyFinalConfigurations(context);
            bool migrationsRun = UpgradeDb();
            AdditionalSeed(context, migrationsRun);
            context.SaveChanges();
        }

        private void ApplyFinalConfigurations(DbContext context)
        {
            foreach (var c in _configurations)
            {
                c.TargetDatabase = new DbConnectionInfo(_connectionStringName);
            }
            _configurator(_configurations, context);
        }

        private void AssertDbContextConnection(DbConnection ctxCnn)
        {
            using (DbConnection migrationsCnn = CreateDbConnection())
            {
                const string msgSummary =
                    "The database connection for the DbContext that triggered database initialization is not equivalent to the connection supplied for running migrations";
                if (!String.Equals(ctxCnn.DataSource, migrationsCnn.DataSource,
                    StringComparison.InvariantCultureIgnoreCase))
                {
                    const string msgDetail = "Server names are different. DbContext uses: '{0}'; Migrations uses: {1}";
                    string msg = msgSummary + Environment.NewLine +
                                 string.Format(msgDetail, ctxCnn.DataSource, migrationsCnn.DataSource);
                    throw new InvalidOperationException(msg);
                }
                if (!String.Equals(ctxCnn.Database, migrationsCnn.Database, StringComparison.InvariantCultureIgnoreCase))
                {
                    const string msgDetail = "Database names are different. DbContext uses: '{0}'; Migrations uses: {1}";
                    string msg = msgSummary + Environment.NewLine +
                                 string.Format(msgDetail, ctxCnn.Database, migrationsCnn.Database);
                    throw new InvalidOperationException(msg);
                }
            }
        }

        private bool UpgradeDb()
        {
            using (DbConnection cnn = CreateDbConnection())
            {
                List<DelegatedMigrator> migrators
                    = _configurations.Select((c, migratorPriority) => CreateMigrator(c, migratorPriority, cnn)).ToList();
                using (var runner = new MultiMigrateDbToLastestVsRunner(migrators)
                {
                    SkippedMigrations = SkippedMigrations,
                    Logger = Logger
                })
                {
                    return runner.Run();
                }
            }
        }

        private DbConnection CreateDbConnection()
        {
            ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings[_connectionStringName];
            return DelegatedMigrator.CreateDbConnection(connectionString);
        }

        private DelegatedMigrator CreateMigrator(DbMigrationsConfiguration c, int migratorPriority, DbConnection cnn)
        {
            var migrator = DelegatedMigrator.CreateFromMigrationConfig(c, cnn);
            migrator.Priority = migratorPriority;
            return migrator;
        }

        /// <summary>
        ///     Override in subclass to provide additional seed data
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Seed data created by this method will be in addition to the data created by
        ///         <see cref="DbMigrationsConfiguration" />.<see cref="DbMigrationsConfiguration{T}.Seed" />
        ///         methods supplied to this instance.
        ///     </para>
        ///     <para>
        ///         <see cref="DbContext.SaveChanges" /> will be run after this method has finshed.
        ///     </para>
        ///     <para>
        ///         ** WARNING ** Seed data created by this method will not be run when upgrading the database
        ///         using Update-Database command in nuget package console or in an automated deploy scenarios
        ///         using migrate.exe
        ///     </para>
        /// </remarks>
        /// <param name="context">The database context that will used to save seed data</param>
        /// <param name="migrationsRun"><c>true</c> whether migrations were actually run</param>
        public virtual void AdditionalSeed(DbContext context, bool migrationsRun)
        {
            // override in subclass
        }
    }
}