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
                .ForeignKey("dbo.EntityMetadatas", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomEntityMetadatas", "Id", "dbo.EntityMetadatas");
            DropIndex("dbo.CustomEntityMetadatas", new[] { "Id" });
            DropTable("dbo.CustomEntityMetadatas");
        }
    }
}
