using System.Data.Entity;
using CcAcca.EntityFramework.MigrationUtils.Conventions;

namespace CcAcca.BaseLibrary
{
    public class BaseLibraryDbContext : DbContext
    {
        private const string BaseSchemaName = "BaseLib";

        public DbSet<EntityMetadata> EntityMetadatas { get; set; }
        public DbSet<EntityPropertyMetadata> EntityPropertyMetadatas { get; set; }
        public DbSet<Lookup> Lookups { get; set; }
        public DbSet<LookupItem> LookupItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<string>().Configure(property => property.HasMaxLength(500));

            modelBuilder.Conventions.Add(DefaultSchemaConvention.AllTypesInAssemblyContaining<LookupItem>(BaseSchemaName));
            modelBuilder.HasDefaultSchema(BaseSchemaName);
        }

    }
}