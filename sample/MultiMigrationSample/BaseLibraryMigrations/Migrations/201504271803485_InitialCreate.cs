namespace CcAcca.BaseLibraryMigrations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BaseLib.EntityMetadatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EntityName = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "BaseLib.EntityPropertyMetadatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PropertyName = c.String(nullable: false, maxLength: 500),
                        EntityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("BaseLib.EntityMetadatas", t => t.EntityId, cascadeDelete: true)
                .Index(t => t.EntityId);
            
            CreateTable(
                "BaseLib.LookupItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 50),
                        Description = c.String(maxLength: 500),
                        LookupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("BaseLib.Lookups", t => t.LookupId, cascadeDelete: true)
                .Index(t => t.LookupId);
            
            CreateTable(
                "BaseLib.Lookups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("BaseLib.LookupItems", "LookupId", "BaseLib.Lookups");
            DropForeignKey("BaseLib.EntityPropertyMetadatas", "EntityId", "BaseLib.EntityMetadatas");
            DropIndex("BaseLib.LookupItems", new[] { "LookupId" });
            DropIndex("BaseLib.EntityPropertyMetadatas", new[] { "EntityId" });
            DropTable("BaseLib.Lookups");
            DropTable("BaseLib.LookupItems");
            DropTable("BaseLib.EntityPropertyMetadatas");
            DropTable("BaseLib.EntityMetadatas");
        }
    }
}
