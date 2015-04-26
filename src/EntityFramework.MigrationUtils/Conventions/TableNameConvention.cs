using System;
using System.Data.Entity.Infrastructure.Pluralization;

namespace CcAcca.EntityFramework.MigrationUtils.Conventions
{
    public static class TableNameConvention
    {
         static TableNameConvention()
        {
            DefaultTableNameSelector = AsEnglishPlural;
        }

        public static Func<Type, string> DefaultTableNameSelector { get; set; }

        public static Func<Type, string> AsEnglishPlural
        {
            get { return type => new EnglishPluralizationService().Pluralize(type.Name); }
        }

        public static Func<Type, string> AsTypeName
        {
            get { return type => new EnglishPluralizationService().Pluralize(type.Name); }
        }
    }
}