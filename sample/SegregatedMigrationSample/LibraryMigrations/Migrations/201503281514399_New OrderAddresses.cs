namespace CcAcca.LibraryMigrations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewOrderAddresses : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderAddresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AddressId = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                        IsDefaultDeliveryAddress = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderAddresses", "OrderId", "dbo.Orders");
            DropIndex("dbo.OrderAddresses", new[] { "OrderId" });
            DropTable("dbo.OrderAddresses");
        }
    }
}
