using System.Data.Entity;
using CcAcca.BaseLibrary;
using CcAcca.EntityFramework.MigrationUtils.Conventions;

namespace CcAcca.Library
{
    public class LibraryDbContext : BaseLibraryDbContext
    {
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add(DefaultSchemaConvention.AllTypesInAssemblyContaining<Order>("Lib"));

            // note: this is a workaround to the standard way of mapping Table-per-hierarchy mapping
            // we're doing this so that a single table get's created in the schema we want
            modelBuilder.Conventions.Add(TablePerHiearchyConvention.For<LookupItem>(schemaName: BaseSchemaName));

            // we need to turn off cascade delete as this is not allowed due to causing cascade cycles
            modelBuilder.Entity<Order>()
                .HasRequired(o => o.OrderRecommendation).WithMany().WillCascadeOnDelete(false);

            // by calling into BaseLibrary at the end here ensures by default that conventions 
            // of BaseLibrary take precendence
            base.OnModelCreating(modelBuilder);

            // we can't set a default schema to 'Lib' due to a limitation of using automatic migrations
            modelBuilder.HasDefaultSchema("dbo");
        }
    }
}