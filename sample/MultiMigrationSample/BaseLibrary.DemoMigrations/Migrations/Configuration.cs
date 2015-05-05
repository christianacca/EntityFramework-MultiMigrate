using System.Data.Entity.Migrations;

namespace CcAcca.BaseLibrary.DemoMigrations.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<DemoDbContext>
    {
        public Configuration()
        {
            // we don't want to have to scafold migrations for the classes that we're using
            // as a developer aid to develop the production BaseLibrary component
            // therefore let auto migrations keep our db up to date
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(DemoDbContext context)
        {
            // we need to create seed data for the BaseLibrary in our database 
            // problem is CcAcca.BaseLibrary.DemoMigrations.DemoDbContext can't be used for that...
            // so instead we need to new up the CcAcca.BaseLibrary.DemoModel.DemoDbContext 
            // using the current connection to our db
            var context2 = new DemoModel.DemoDbContext(context.Database.Connection, false);

            var costCentreType = new Lookup { Name = "Cost Centre Type" };
            context2.Lookups.AddOrUpdate(x => x.Name, costCentreType);
            // make sure that costCentreType get's an id before we use it below
            context2.SaveChanges();

            var items = new[]
            {
                new LookupItem { Code = "P", Description = "Profit Centre", LookupId = costCentreType.Id }, 
                new LookupItem { Code = "C", Description = "Cost Centre", LookupId = costCentreType.Id }
            };
            context2.LookupItems.AddOrUpdate(x => new { x.Code, x.LookupId }, items);
        }
    }
}