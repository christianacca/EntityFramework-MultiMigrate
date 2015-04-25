using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Pluralization;
using CcAcca.BaseLibrary;
using CcAcca.Library;

namespace CcAcca.LibraryMigrations
{
    public class LibraryDbContext : BaseLibraryDbContext
    {
        public LibraryDbContext()
        {
        }

        public LibraryDbContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<CustomEntityMetadata> CustomEntityMetadatas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.IgnoreExistingMappedTypes(new BaseLibraryDbContext(Database.Connection, false));

            // note: this is a workaround to the standard way of mapping Table-per-hierarchy mapping for LookupItem
            // we're doing this so that the single table get's created in the schema we want all tables in the inheritance
            // hierarchy
            var baseType = typeof(LookupItem);
            modelBuilder.Types()
                .Where(baseType.IsAssignableFrom)
                .Configure(c => c.ToTable(new EnglishPluralizationService().Pluralize(baseType.Name), "BaseLib"));

            modelBuilder.Entity<CustomEntityMetadata>()
                .Map(m =>
                {
                    m.Properties(t => new { t.EntityName, t.DeveloperNotes });
                    m.ToTable("EntityMetadata", "BaseLib");
                })
                .Map(m =>
                {
                    m.Properties(t => new { t.Description });
                    m.ToTable("CustomEntityMetadatas", "dbo");
                });
        }
    }
}