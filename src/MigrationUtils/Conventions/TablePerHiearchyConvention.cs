using System;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CcAcca.EntityFramework.MigrationUtils.Conventions
{
    public class TablePerHiearchyConvention
    {
        public static TablePerHiearchyConvention<TBase> For<TBase>(string tableName = null, string schemaName = null)
            where TBase : class
        {
            Func<Type, string> tableNameSelector = tableName == null
                ? TableNameConvention.DefaultTableNameSelector
                : (type => type.Name);
            return new TablePerHiearchyConvention<TBase>(tableNameSelector, schemaName);
        }
    }

    public class TablePerHiearchyConvention<TBase> : Convention where TBase : class
    {
        public TablePerHiearchyConvention(Func<Type, string> tableNameSeletor, string schemaName = null)
        {
            Type baseType = typeof (TBase);
            string tableName = tableNameSeletor(baseType);
            Types().Where(baseType.IsAssignableFrom).Configure(c => c.ToTable(tableName, schemaName));
        }
    }
}