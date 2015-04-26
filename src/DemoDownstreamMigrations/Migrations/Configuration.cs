using System.Data.Entity.Migrations;
using CcAcca.DemoDownstream;

namespace CcAcca.DemoDownstreamMigrations.Migrations
{
    /// <summary>
    /// This configuration will exclude udf's from the model and requires code migrations to
    /// be added by developer
    /// </summary>
    public sealed class Configuration : DbMigrationsConfiguration<DemoDbContext>
    {
        public Configuration()
        {
            MigrationsDirectory = "Migrations";
            AutomaticMigrationsEnabled = false;
            ContextKey = "DemoDbContext";
        }

        protected override void Seed(DemoDbContext context)
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
