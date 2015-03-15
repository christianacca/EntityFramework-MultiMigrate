using System.Data.Entity;
using CcAcca.BaseLibrary;

namespace CcAcca.Library
{
    public class LibraryDbContext : BaseLibraryDbContext
    {
        public DbSet<Order> Orders { get; set; }
    }
}