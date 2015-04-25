namespace CcAcca.BaseLibraryMigrations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SubclassEntityMetadata : DbMigration
    {
        public override void Up()
        {
            AddColumn("BaseLib.EntityMetadata", "Reason", c => c.String(maxLength: 500));
            AddColumn("BaseLib.EntityMetadata", "Discriminator", c => c.String(nullable: false, maxLength: 128));

            // Static data
            SqlResource("CcAcca.BaseLibraryMigrations.Migrations.201504251239590_Subclass EntityMetadata_Up.sql");
        }
        
        public override void Down()
        {
            DropColumn("BaseLib.EntityMetadata", "Discriminator");
            DropColumn("BaseLib.EntityMetadata", "Reason");
        }
    }
}
