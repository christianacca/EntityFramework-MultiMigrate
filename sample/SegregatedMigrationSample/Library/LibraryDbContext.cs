using System.Data.Entity;
using CcAcca.BaseLibrary;

namespace CcAcca.Library
{
    public class LibraryDbContext : BaseLibraryDbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<CustomEntityMetadata> CustomEntityMetadatas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CustomEntityMetadata>().ToTable("CustomEntityMetadatas");
        }
    }
}