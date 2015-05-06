namespace CcAcca.LibraryMigrations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewCustomLookupItemclass : DbMigration
    {
        public override void Up()
        {
            AddColumn("BaseLib.LookupItems", "Sequence", c => c.Int());
            // note: the default value has been added manually to ensure existing records are fixed up
            // and any new instance created via BaseLibrary will be assigned the correct Discriminator
            AddColumn("BaseLib.LookupItems", "Discriminator", c => c.String(nullable: false, maxLength: 128, defaultValue: "LookupItem"));
            AddColumn("Lib.Orders", "OrderRecommendationId", c => c.Int(nullable: false));
            CreateIndex("Lib.Orders", "OrderRecommendationId");
            AddForeignKey("Lib.Orders", "OrderRecommendationId", "BaseLib.LookupItems", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("Lib.Orders", "OrderRecommendationId", "BaseLib.LookupItems");
            DropIndex("Lib.Orders", new[] { "OrderRecommendationId" });
            DropColumn("Lib.Orders", "OrderRecommendationId");
            DropColumn("BaseLib.LookupItems", "Discriminator");
            DropColumn("BaseLib.LookupItems", "Sequence");
        }
    }
}
