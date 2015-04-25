namespace CcAcca.LibraryMigrations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    /// <remarks>
    /// For a discussion of why this migration needed to be scaffolded see:
    /// http://codingmonster.co.uk/blog/1296/entity-framework-segregated-migration#ImplementingInheritance
    /// </remarks>>
    public partial class MergeChangeEntityMetadatatablemapping : DbMigration
    {
        public override void Up()
        {
            // these migrations will have already been included in BaseLibraryMigrations
/*
            RenameTable(name: "BaseLib.EntityMetadatas", newName: "EntityMetadata");
*/
        }
        
        public override void Down()
        {
            // these migrations will have already been included in BaseLibraryMigrations
/*
            RenameTable(name: "BaseLib.EntityMetadata", newName: "EntityMetadatas");
*/
        }
    }
}
