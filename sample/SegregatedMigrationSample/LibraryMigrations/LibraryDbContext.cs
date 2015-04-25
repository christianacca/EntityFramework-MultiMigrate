using System.Data.Common;
using System.Data.Entity;
using CcAcca.BaseLibrary;
using CcAcca.Library;
using CcAcca.Library.Mappings.SharedConventions;

namespace CcAcca.LibraryMigrations
{
    public class LibraryDbContext : BaseLibraryDbContext
    {
        public const string OurDbSchemaName = "dbo";

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
            // note that our component's conventions will be overridden by BaseLibrary convetions.
            // on balance that's usually what you want so that our component follows the same
            // conventions as the BaseLibrary component
            SharedConventionRules.Apply(modelBuilder);

            // Table-Per-Type inheritance mapping
            modelBuilder.Entity<CustomEntityMetadata>()
                .Map(m =>
                {
                    m.Properties(t => new { t.EntityName, t.DeveloperNotes });
                    m.ToTable("EntityMetadata", DbSchemaName);
                })
                .Map(m =>
                {
                    m.Properties(t => new { t.Description });
                    m.ToTable("CustomEntityMetadatas", OurDbSchemaName);
                });

            // by calling into BaseLibrary at the end here ensures by default that conventions 
            // of BaseLibrary take precendence
            base.OnModelCreating(modelBuilder);

            // by convention remove BaseLibrary component classes from our component's persistent model
            modelBuilder.IgnoreExistingMappedTypes(new BaseLibraryDbContext(Database.Connection, false));
        }
    }
}