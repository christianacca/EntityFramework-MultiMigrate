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
            var statusLookup = new Lookup { Name = "Order Status" };
            var orderRecommendationLookup = new Lookup { Name = "Order Recommendation" };
            var lookups = new[] { statusLookup, orderRecommendationLookup };
            context.Lookups.AddOrUpdate(x => x.Name, lookups);
            context.SaveChanges();


            var inactiveItem = new LookupItem { Code = "P", Description = "Placed", LookupId = statusLookup.Id };
            var activeItem = new LookupItem { Code = "D", Description = "Dispatched", LookupId = statusLookup.Id };
            var topRated = new CustomLookupItem
            {
                Code = "top",
                Description = "Top Rated",
                Sequence = 0,
                LookupId = orderRecommendationLookup.Id
            };
            var okRated = new CustomLookupItem
            {
                Code = "ok",
                Description = "OK Rated",
                Sequence = 1,
                LookupId = orderRecommendationLookup.Id
            };
            var nokRated = new CustomLookupItem
            {
                Code = "nok",
                Description = "Not OK Rated",
                Sequence = 2,
                LookupId = orderRecommendationLookup.Id
            };

            var items = new[] { inactiveItem, activeItem, topRated, okRated, nokRated };
            context.LookupItems.AddOrUpdate(x => new { x.Code, x.LookupId }, items);
        }
    }
}