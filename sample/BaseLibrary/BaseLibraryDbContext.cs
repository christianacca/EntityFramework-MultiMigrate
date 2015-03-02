using System.Data.Entity;

namespace CcAcca.BaseLibrary
{
    public class BaseLibraryDbContext : DbContext
    {
        public DbSet<EntityMetadata> EntityMetadatas { get; set; }
        public DbSet<EntityPropertyMetadata> EntityPropertyMetadatas { get; set; }
        public DbSet<Lookup> Lookups { get; set; }
        public DbSet<LookupItem> LookupItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<string>().Configure(property => property.HasMaxLength(500));
            base.OnModelCreating(modelBuilder);
        }
    }
}