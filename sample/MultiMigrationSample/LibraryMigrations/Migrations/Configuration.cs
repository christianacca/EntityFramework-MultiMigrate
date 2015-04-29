using System.Data.Entity.Migrations;
using CcAcca.BaseLibrary;
using CcAcca.Library;

namespace CcAcca.LibraryMigrations.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<LibraryDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "LibraryDbContext";
        }

        protected override void Seed(LibraryDbContext context)
        {
            var statusLookup = new Lookup {Name = "Order Status"};
            context.Lookups.AddOrUpdate(x => x.Name, statusLookup);

            var inactiveItem = new LookupItem {Code = "P", Description = "Placed", LookupId = statusLookup.Id};
            var activeItem = new LookupItem {Code = "D", Description = "Dispatched", LookupId = statusLookup.Id};
            var items = new[] {inactiveItem, activeItem};
            context.LookupItems.AddOrUpdate(x => new {x.Code, x.LookupId}, items);
        }
    }
}