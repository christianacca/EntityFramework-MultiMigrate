namespace CcAcca.BaseLibraryMigrations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeEntityMetadatatablemapping : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "BaseLib.EntityMetadatas", newName: "EntityMetadata");
        }
        
        public override void Down()
        {
            RenameTable(name: "BaseLib.EntityMetadata", newName: "EntityMetadatas");
        }
    }
}
