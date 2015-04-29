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

            // by calling into BaseLibrary at the end here ensures by default that conventions 
            // of BaseLibrary take precendence
            base.OnModelCreating(modelBuilder);

            // we can't set a default schema to 'Lib' due to a limitation of using automatic migrations
            modelBuilder.HasDefaultSchema("dbo");
        }
    }
}