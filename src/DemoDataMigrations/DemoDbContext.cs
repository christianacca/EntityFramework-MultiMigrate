using System.Data.Entity;
using System.Linq;
using System.Reflection;
using CcAcca.DemoSharedModel;

namespace CcAcca.DemoDataMigrations
{
    public class DemoDbContext : CcAcca.DemoData.DemoDbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Types()
                .Configure(c =>
                {
                    var udfProperties =
                        c.ClrType.GetProperties().Where(p => p.GetCustomAttribute<UdfAttribute>() != null).ToList();
                    udfProperties.ForEach(p => c.Ignore(p));
                });
        }
    }
}