﻿using System.Data.Common;
using System.Data.Entity;
using CcAcca.EntityFramework.MigrationUtils.Conventions;

namespace CcAcca.BaseLibrary
{
    public class BaseLibraryDbContext : DbContext
    {
        public const string DbSchemaName = "BaseLib";

        public BaseLibraryDbContext()
        {
        }

        public BaseLibraryDbContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<EntityMetadata> EntityMetadatas { get; set; }
        public DbSet<AlternativeEntityMetadata> AlternativeEntityMetadatas { get; set; }
        public DbSet<EntityPropertyMetadata> EntityPropertyMetadatas { get; set; }
        public DbSet<Lookup> Lookups { get; set; }
        public DbSet<LookupItem> LookupItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<string>().Configure(property => property.HasMaxLength(500));

            // note: we're not using HasDefaultSchema to make it easier on downstream developers who might
            // want to set this themselves
            // instead we're using the DefaultSchemaConvention convention
//            modelBuilder.HasDefaultSchema("BaseLib");
            modelBuilder.Conventions.Add(DefaultSchemaConvention.AllTypesInAssemblyContaining<LookupItem>(DbSchemaName));


            // note: this is a workaround to the standard way of mapping Table-per-hierarchy mapping
            // we're doing this so that a single table get's created in the schema we want
            modelBuilder.Conventions.Add(TablePerHiearchyConvention.For<EntityMetadata>(schemaName: DbSchemaName));
        }
    }
}