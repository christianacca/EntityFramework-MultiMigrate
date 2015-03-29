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
            // we need to create seed data for the BaseLibrary in our database 
            // problem is CcAcca.LibraryMigrations.LibraryDbContext can't be used for that...
            // so instead we need to new up the CcAcca.Library.LibraryDbContext using the current connection to our db
            var libraryDbContext = new Library.LibraryDbContext(context.Database.Connection, false);

            var statusLookup = new Lookup { Name = "Order Status" };
            var orderRecommendationLookup = new Lookup { Name = "Order Recommendation" };
            var lookups = new[] {statusLookup, orderRecommendationLookup};
            libraryDbContext.Lookups.AddOrUpdate(x => x.Name, lookups);
            libraryDbContext.SaveChanges();

            var inactiveItem = new LookupItem { Code = "P", Description = "Placed", LookupId = statusLookup.Id };
            var activeItem = new LookupItem { Code = "D", Description = "Dispatched", LookupId = statusLookup.Id };

            var topRated = new CustomLookupItem
            {
                Code = "top", Description = "Top Rated", Sequence = 0, LookupId = orderRecommendationLookup.Id
            };
            var okRated = new CustomLookupItem
            {
                Code = "ok", Description = "OK Rated", Sequence = 1, LookupId = orderRecommendationLookup.Id
            };
            var nokRated = new CustomLookupItem
            {
                Code = "nok", Description = "Not OK Rated", Sequence = 2, LookupId = orderRecommendationLookup.Id
            };

            var items = new[] { inactiveItem, activeItem, topRated, okRated, nokRated };
            libraryDbContext.LookupItems.AddOrUpdate(x => new { x.Code, x.LookupId }, items);

            libraryDbContext.Addresses
                .AddOrUpdate(a => new { a.Line1, a.Postcode }, 
                new Address { Line1 = "10 Somewhere", Line3 = "Chatham", Line4 = "Kent", Postcode = "xxx xxx" },
                new Address { Line1 = "10 Somewhere else", Line3 = "Gravesend", Line4 = "Kent", Postcode = "yyy yyy" });

            libraryDbContext.SaveChanges();
        }
    }
}