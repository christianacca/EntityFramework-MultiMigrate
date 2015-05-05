using System.Data.Common;
using System.Data.Entity;
using CcAcca.BaseLibrary.DemoModel;
using CcAcca.EntityFramework.MigrationUtils.Conventions;

namespace CcAcca.BaseLibrary.DemoMigrations
{
    public class DemoDbContext: BaseLibraryDbContext
    {
        public DemoDbContext()
        {
        }

        public DemoDbContext(DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection)
        {
        }

        public DbSet<CostCentre> CostCentres { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.IgnoreExistingMappedTypes<BaseLibraryDbContext>();

            // we can't set a default schema to 'Demo' due to a limitation of using automatic migrations
            modelBuilder.HasDefaultSchema("dbo");
        }
    }
}