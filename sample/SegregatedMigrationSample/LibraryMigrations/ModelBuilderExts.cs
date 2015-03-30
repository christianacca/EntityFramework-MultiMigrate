using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace CcAcca.LibraryMigrations
{
    public static class ModelBuilderExts
    {
        public static void IgnoreExistingMappedTypes<T>(this DbModelBuilder modelBuilder) where T : DbContext, new()
        {
            modelBuilder.IgnoreExistingMappedTypes(new T());
        }

        public static void IgnoreExistingMappedTypes<T>(this DbModelBuilder modelBuilder, T dbContext) where T : DbContext
        {
            IEnumerable<Type> mappedClrTypes = GetMappedClrEntityTypes(dbContext);
            foreach (var type in mappedClrTypes)
            {
                modelBuilder.Ignore(new[] { type });
            }
        }

        private static IEnumerable<Type> GetMappedClrEntityTypes<T>(T dbContext) where T : DbContext
        {
            ObjectContext octx = (dbContext as IObjectContextAdapter).ObjectContext;
            // Get the mapping between CLR types and metadata OSpace
            var objectItemCollection = ((ObjectItemCollection) octx.MetadataWorkspace.GetItemCollection(DataSpace.OSpace));

            var entityTypes = octx.MetadataWorkspace.GetItems(DataSpace.OSpace)
                .Where(x => x.BuiltInTypeKind == BuiltInTypeKind.EntityType)
                .OfType<EntityType>();

            return entityTypes.Select(objectItemCollection.GetClrType);
        }
    }
}