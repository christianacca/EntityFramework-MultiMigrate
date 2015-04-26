using System.Data.Entity.ModelConfiguration.Conventions;
using System.Reflection;

namespace CcAcca.EntityFramework.MigrationUtils.Conventions
{
    public class DefaultSchemaConvention : Convention
    {
        public DefaultSchemaConvention(Assembly assembly, string schema)
        {
            Types()
                .Where(t => t.Assembly == assembly)
                .Configure(c => c.ToTable(TableNameConvention.DefaultTableNameSelector(c.ClrType), schema));
        }

        public static DefaultSchemaConvention AllTypesInAssemblyContaining<T>(string schema)
        {
            return new DefaultSchemaConvention(typeof (T).Assembly, schema);
        }
    }
}