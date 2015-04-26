using System.Data.Common;
using System.Data.Entity;
using CcAcca.DemoUpstream;
using CcAcca.EntityFramework.MigrationUtils.Conventions;

namespace CcAcca.DemoDownstream
{
    public class DemoDbContext : DemoBaseDbContext
    {
        public DemoDbContext()
        {
        }

        public DemoDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString ?? "DemoDbContext")
        {
        }

        public DemoDbContext(DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection)
        {
        }

        public DbSet<Asset> Assets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // set default schema...
            // we can't set a default schema that all tables should have because of a limitation of using automatic migrations
            modelBuilder.HasDefaultSchema(null);
            modelBuilder.Conventions.Add(SetSchemaConvention.AllTypesInAssemblyContaining<Asset>("Main"));

            // by calling into DemoBaseDbContext at the end here ensures by default that conventions 
            // of DemoUpstream take precendence
            base.OnModelCreating(modelBuilder);
        }
    }
}