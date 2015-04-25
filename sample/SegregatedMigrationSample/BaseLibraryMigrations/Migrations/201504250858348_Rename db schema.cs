namespace CcAcca.BaseLibraryMigrations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Renamedbschema : DbMigration
    {
        public override void Up()
        {
            MoveTable(name: "dbo.Addresses", newSchema: "BaseLib");
            MoveTable(name: "dbo.EntityMetadatas", newSchema: "BaseLib");
            MoveTable(name: "dbo.EntityPropertyMetadatas", newSchema: "BaseLib");
            MoveTable(name: "dbo.LookupItems", newSchema: "BaseLib");
            MoveTable(name: "dbo.Lookups", newSchema: "BaseLib");
        }
        
        public override void Down()
        {
            MoveTable(name: "BaseLib.Lookups", newSchema: "dbo");
            MoveTable(name: "BaseLib.LookupItems", newSchema: "dbo");
            MoveTable(name: "BaseLib.EntityPropertyMetadatas", newSchema: "dbo");
            MoveTable(name: "BaseLib.EntityMetadatas", newSchema: "dbo");
            MoveTable(name: "BaseLib.Addresses", newSchema: "dbo");
        }
    }
}
