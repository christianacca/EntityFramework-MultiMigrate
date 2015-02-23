using System.Data.Entity.Migrations;

namespace CcAcca.DemoUpstreamMigrations.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<DemoBaseDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "DemoBaseDbContext";
        }

        protected override void Seed(DemoBaseDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
