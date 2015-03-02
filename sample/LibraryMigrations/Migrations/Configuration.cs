using System.Data.Entity.Migrations;
using CcAcca.BaseLibrary;
using CcAcca.Library;
using BaseLibraryConfiguration = CcAcca.BaseLibraryMigrations.Migrations.Configuration;

namespace CcAcca.LibraryMigrations.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<LibraryDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LibraryDbContext context)
        {
            new BaseLibraryConfiguration().InvokeMethod("Seed", (BaseLibraryDbContext) context);
            context.SaveChanges();

            var statusLookup = new Lookup { Name = "Order Status" };
            context.Lookups.AddOrUpdate(x => x.Name, statusLookup);

            var inactiveItem = new LookupItem { Code = "P", Description = "Placed", LookupId = statusLookup.Id };
            var activeItem = new LookupItem { Code = "D", Description = "Dispatched", LookupId = statusLookup.Id };
            var items = new[] { inactiveItem, activeItem };
            context.LookupItems.AddOrUpdate(x => new { x.Code, x.LookupId }, items);
        }
    }
}