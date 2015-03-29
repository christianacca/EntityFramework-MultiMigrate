using System.Data.Common;
using System.Data.Entity;
using CcAcca.BaseLibrary;

namespace CcAcca.Library
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

            modelBuilder.Entity<CustomEntityMetadata>().ToTable("CustomEntityMetadatas");

            // normally it would be neccessary to disable cascade delete to avoid cascade delete cycles...
            // we don't have to as we've hand coded our migrations which do NOT create the FK constraint
            // I'm leaving it here to make it easy to switch to building the database with a 
            // DbContext that combines the Persistent Model of both BaseLibrary and Library components
            modelBuilder.Entity<Order>()
                .HasRequired(o => o.OrderRecommendation).WithMany().WillCascadeOnDelete(false);
        }
    }
}