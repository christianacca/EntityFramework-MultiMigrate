using System.Data.Entity.Migrations;
using CcAcca.BaseLibrary;

namespace CcAcca.BaseLibraryMigrations.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<BaseLibraryDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BaseLibraryDbContext context)
        {
            var statusLookup = new Lookup { Name = "Status"};
            context.Lookups.AddOrUpdate(x => x.Name, statusLookup);

            var inactiveItem = new LookupItem { Code = "I", Description = "Inactive", LookupId = statusLookup.Id };
            var activeItem = new LookupItem { Code = "A", Description = "Active", LookupId = statusLookup.Id };
            var items = new[] {inactiveItem, activeItem};
            context.LookupItems.AddOrUpdate(x => new { x.Code, x.LookupId }, items);

            var lookupEntity = new EntityMetadata {EntityName = "Lookup", DeveloperNotes = "Use Lookups when enum's don't cut it"};
            context.EntityMetadatas.AddOrUpdate(x => x.EntityName, lookupEntity);
            context.SaveChanges();

            var lookupNameProperty = new EntityPropertyMetadata { EntityId = lookupEntity.Id, PropertyName = "Name" };
            context.EntityPropertyMetadatas.AddOrUpdate(x => x.PropertyName, lookupNameProperty);

            var altLookupEntity = new AlternativeEntityMetadata { EntityName = "AltLookup", Reason = "Lookup in extension library (if any)"};
            context.EntityMetadatas.AddOrUpdate(x => x.EntityName, altLookupEntity);
            context.SaveChanges();

            var altLookupNameProperty = new EntityPropertyMetadata { EntityId = altLookupEntity.Id, PropertyName = "Reason" };
            context.EntityPropertyMetadatas.AddOrUpdate(x => x.PropertyName, altLookupNameProperty);
        }
    }
}