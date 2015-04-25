using System;
using System.Data.Entity.Infrastructure.Pluralization;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CcAcca.BaseLibrary.ReusableConventions
{
    public class TablePerHiearchyConvention
    {
        static TablePerHiearchyConvention()
        {
            DefaultTableNameSelector = type => new EnglishPluralizationService().Pluralize(type.Name);
        }

        public static Func<Type, string> DefaultTableNameSelector { get; set; }

        public static TablePerHiearchyConvention<TBase> For<TBase>(string tableName = null, string schemaName = null)
            where TBase : class
        {
            Func<Type, string> tableNameSelector = tableName == null ? DefaultTableNameSelector : (type => type.Name);
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