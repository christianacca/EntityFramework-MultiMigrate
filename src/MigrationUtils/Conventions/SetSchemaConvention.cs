using System.Data.Entity.ModelConfiguration.Conventions;
using System.Reflection;

namespace CcAcca.EntityFramework.MigrationUtils.Conventions
{
    public class SetSchemaConvention : Convention
    {
        public SetSchemaConvention(Assembly assembly, string schema)
        {
            Types()
                .Where(t => t.Assembly == assembly)
                .Configure(c => c.ToTable(TableNameConvention.DefaultTableNameSelector(c.ClrType), schema));
        }

        public static SetSchemaConvention AllTypesInAssemblyContaining<T>(string schema)
        {
            return new SetSchemaConvention(typeof (T).Assembly, schema);
        }
    }
}