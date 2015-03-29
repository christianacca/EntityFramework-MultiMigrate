namespace CcAcca.LibraryMigrations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    /// <remarks>
    /// For a discussion of why this migration needed to be scaffolded see:
    /// http://codingmonster.co.uk/blog/1296/entity-framework-segregated-migration#ImplementingInheritance
    /// </remarks>>
    public partial class MergeEntityMetadataNotes : DbMigration
    {
        public override void Up()
        {
            // these migrations will have already been included in BaseLibraryMigrations
//            AddColumn("dbo.EntityMetadatas", "DeveloperNotes", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            // these migrations will have already been included in BaseLibraryMigrations
//            DropColumn("dbo.EntityMetadatas", "DeveloperNotes");
        }
    }
}
