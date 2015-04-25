namespace CcAcca.LibraryMigrations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewCustomLookupItemclass : DbMigration
    {
        public override void Up()
        {
            // note: this is hand written migrations:
            // we're forcing the alteration of the dbo.LookupItems table created by BaseLibraryMigrations 
            // to support table-per-hierachy inheritance mapping

            // we're setting a defaultValue in case BaseLibraryDbContext is used to insert LookupItem instances
            AddColumn("BaseLib.LookupItems", "Discriminator", c => c.String(maxLength: 128, nullable: false, defaultValue: "LookupItem"));
            AddColumn("BaseLib.LookupItems", "Sequence", c => c.Int(nullable: true));
            AddColumn("dbo.Orders", "OrderRecommendationId", c => c.Int(nullable: true));
            CreateIndex("dbo.Orders", "OrderRecommendationId");
        }
        
        public override void Down()
        {
            // note: this is hand written migrations - see above
            DropIndex("dbo.Orders", new[] { "OrderRecommendationId" });
            DropColumn("dbo.Orders", "OrderRecommendationId");
            DropColumn("BaseLib.LookupItems", "Sequence");
            DropColumn("BaseLib.LookupItems", "Discriminator");
        }
    }
}