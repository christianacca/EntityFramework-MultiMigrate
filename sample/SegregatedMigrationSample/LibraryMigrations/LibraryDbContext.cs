using System.Data.Common;
using System.Data.Entity;
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

            modelBuilder.IgnoreExistingMappedTypes<BaseLibraryDbContext>();

            modelBuilder.Entity<CustomEntityMetadata>()
                .Map(m =>
                {
                    m.Properties(t => new { t.EntityName, t.DeveloperNotes });
                    m.ToTable("EntityMetadatas");
                })
                .Map(m =>
                {
                    m.Properties(t => new { t.Description });
                    m.ToTable("CustomEntityMetadatas");
                });
        }
    }
}
