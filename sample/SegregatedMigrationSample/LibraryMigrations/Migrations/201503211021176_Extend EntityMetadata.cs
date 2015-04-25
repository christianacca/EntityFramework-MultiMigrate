namespace CcAcca.LibraryMigrations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExtendEntityMetadata : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomEntityMetadatas",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Description = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Id);
            AddForeignKey("dbo.CustomEntityMetadatas", "Id", "BaseLib.EntityMetadata", "Id");

        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomEntityMetadatas", "Id", "BaseLib.EntityMetadata");
            DropIndex("dbo.CustomEntityMetadatas", new[] { "Id" });
            DropTable("dbo.CustomEntityMetadatas");
        }
    }
}