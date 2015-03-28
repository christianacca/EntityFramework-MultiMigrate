using System.Data.Entity.Migrations;
using CcAcca.BaseLibrary;
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
            // we need to create seed data for the BaseLibrary in our database 
            // problem is CcAcca.LibraryMigrations.LibraryDbContext can't be used for that...
            // so instead we need to new up the BaseLibraryDbContext using the current connection to our db
            var baseLibraryDbContext = new BaseLibraryDbContext(context.Database.Connection, false);

            var statusLookup = new Lookup { Name = "Order Status" };
            baseLibraryDbContext.Lookups.AddOrUpdate(x => x.Name, statusLookup);

            var inactiveItem = new LookupItem { Code = "P", Description = "Placed", LookupId = statusLookup.Id };
            var activeItem = new LookupItem { Code = "D", Description = "Dispatched", LookupId = statusLookup.Id };
            var items = new[] { inactiveItem, activeItem };
            baseLibraryDbContext.LookupItems.AddOrUpdate(x => new { x.Code, x.LookupId }, items);

            baseLibraryDbContext.Addresses.AddRange(new[]
            {
                new Address {Line1 = "10 Somewhere", Line3 = "Chatham", Line4 = "Kent", Postcode = "xxx xxx"},
                new Address {Line1 = "10 Somewhere else", Line3 = "Gravesend", Line4 = "Kent", Postcode = "yyy yyy"},
            });

            baseLibraryDbContext.SaveChanges();
        }
    }
}