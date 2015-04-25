using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Pluralization;
using System.Reflection;

namespace CcAcca.BaseLibrary
{
    public class BaseLibraryDbContext : DbContext
    {
        public BaseLibraryDbContext()
        {
        }

        public BaseLibraryDbContext(DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection)
        {

        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<EntityMetadata> EntityMetadatas { get; set; }
        public DbSet<AlternativeEntityMetadata> AlternativeEntityMetadatas { get; set; }
        public DbSet<EntityPropertyMetadata> EntityPropertyMetadatas { get; set; }
        public DbSet<Lookup> Lookups { get; set; }
        public DbSet<LookupItem> LookupItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<string>().Configure(property => property.HasMaxLength(500));

            // note: we're not using HasDefaultSchema to make it easier on downstream developers who might
            // want to set this themselves
            // instead we're using the modelBuilder.Types().Configure() method to define the db schema for
            // the classes in this assembly
//            modelBuilder.HasDefaultSchema("BaseLib");
            Assembly thisAssembly = typeof(LookupItem).Assembly;
            modelBuilder.Types().Where(t => t.Assembly == thisAssembly)
                .Configure(c => c.ToTable(new EnglishPluralizationService().Pluralize(c.ClrType.Name), "BaseLib"));


            // note: this is a workaround to the standard way of mapping Table-per-hierarchy mapping
            // we're doing this so that the single table get's created in the schema we want for our tables
            var baseType = typeof(EntityMetadata);
            modelBuilder.Types()
                .Where(baseType.IsAssignableFrom)
                .Configure(c => c.ToTable("EntityMetadata", "BaseLib"));
        }
    }
}