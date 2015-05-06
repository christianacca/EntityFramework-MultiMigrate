namespace CcAcca.BaseLibraryMigrations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SubclassLookupItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("BaseLib.LookupItems", "Reason", c => c.String(maxLength: 500));
            AddColumn("BaseLib.LookupItems", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("BaseLib.LookupItems", "Discriminator");
            DropColumn("BaseLib.LookupItems", "Reason");
        }
    }
}
