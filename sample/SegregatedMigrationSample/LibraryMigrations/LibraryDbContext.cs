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

        public LibraryDbContext(DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection)
        {
        }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Ignore<EntityMetadata>();
            modelBuilder.Ignore<EntityPropertyMetadata>();
            modelBuilder.Ignore<Lookup>();
            modelBuilder.Ignore<LookupItem>();
        }
    }
}