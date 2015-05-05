using System.Data.Common;
using System.Data.Entity;

namespace CcAcca.BaseLibrary.DemoModel
{
    public class DemoDbContext : BaseLibraryDbContext
    {
        public DemoDbContext()
        {
        }

        public DemoDbContext(DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection)
        {
        }

        public DbSet<CostCentre> CostCentres { get; set; }
    }
}