namespace CcAcca.BaseLibraryMigrations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewpropertyEntityMetadataNotes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EntityMetadatas", "DeveloperNotes", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EntityMetadatas", "DeveloperNotes");
        }
    }
}
