namespace CcAcca.LibraryMigrations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReplacemigrationSubclassLookupItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("BaseLib.LookupItems", "Reason", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("BaseLib.LookupItems", "Reason");
        }
    }
}
