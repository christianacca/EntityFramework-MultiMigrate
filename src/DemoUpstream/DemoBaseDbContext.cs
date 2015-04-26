using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Reflection;
using CcAcca.EntityFramework.MigrationUtils.Conventions;

namespace CcAcca.DemoUpstream
{
    public class DemoBaseDbContext : DbContext
    {
        public const string BaseSchemaName = "Base";

        public DemoBaseDbContext()
        {
        }

        public DemoBaseDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }

        public DemoBaseDbContext(DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection)
        {
        }

        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<FakeEntity> FakeEntities { get; set; }
        public DbSet<Lookup> Lookups { get; set; }
        public DbSet<LookupItem> LookupItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // set default schema
            modelBuilder.Conventions.Add(DefaultSchemaConvention.AllTypesInAssemblyContaining<LookupItem>(BaseSchemaName));
            modelBuilder.HasDefaultSchema(BaseSchemaName);

            // note: this is a workaround to the standard way of mapping Table-per-hierarchy mapping
            // we're doing this so that a single table get's created in the schema we want
            modelBuilder.Conventions.Add(TablePerHiearchyConvention.For<UserRole>(schemaName: BaseSchemaName));

            modelBuilder.Types()
                .Where(t => t.GetCustomAttribute<ReferenceDataAttribute>() != null)
                .Configure(c => c.HasTableAnnotation("CustomIdentitySeed", true));

            modelBuilder.Entity<UserRole>()
                .Property(p => p.Key).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Key") { IsUnique = true }));
        }
    }
}