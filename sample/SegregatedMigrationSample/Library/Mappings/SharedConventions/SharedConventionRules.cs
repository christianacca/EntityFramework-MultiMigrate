using System.Data.Entity;
using CcAcca.BaseLibrary;
using CcAcca.EntityFramework.MigrationUtils.Conventions;

namespace CcAcca.Library.Mappings.SharedConventions
{
    public static class SharedConventionRules
    {
        public static DbModelBuilder Apply(DbModelBuilder modelBuilder)
        {
            // note: this is a workaround to the standard way of mapping Table-per-hierarchy mapping
            // we're doing this so that a single table get's created in the schema we want
            modelBuilder.Conventions.Add(
                TablePerHiearchyConvention.For<LookupItem>(schemaName: BaseLibraryDbContext.DbSchemaName));
            return modelBuilder;
        }
    }
}