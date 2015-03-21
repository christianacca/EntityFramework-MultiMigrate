namespace CcAcca.BaseLibraryMigrations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EntityMetadatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EntityName = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EntityPropertyMetadatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PropertyName = c.String(nullable: false, maxLength: 500),
                        EntityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EntityMetadatas", t => t.EntityId, cascadeDelete: true)
                .Index(t => t.EntityId);
            
            CreateTable(
                "dbo.LookupItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 50),
                        Description = c.String(maxLength: 500),
                        LookupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lookups", t => t.LookupId, cascadeDelete: true)
                .Index(t => t.LookupId);
            
            CreateTable(
                "dbo.Lookups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);

            // Static data
            SqlResource("CcAcca.BaseLibraryMigrations.Migrations.201503011903273_InitialCreate_Up.sql");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LookupItems", "LookupId", "dbo.Lookups");
            DropForeignKey("dbo.EntityPropertyMetadatas", "EntityId", "dbo.EntityMetadatas");
            DropIndex("dbo.LookupItems", new[] { "LookupId" });
            DropIndex("dbo.EntityPropertyMetadatas", new[] { "EntityId" });
            DropTable("dbo.Lookups");
            DropTable("dbo.LookupItems");
            DropTable("dbo.EntityPropertyMetadatas");
            DropTable("dbo.EntityMetadatas");
        }
    }
}
