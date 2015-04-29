namespace CcAcca.LibraryMigrations.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Lib.Orders",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    OrderDate = c.DateTimeOffset(nullable: false, precision: 7),
                    CustomerName = c.String(nullable: false, maxLength: 500),
                    OrderStatusId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("BaseLib.LookupItems", t => t.OrderStatusId, cascadeDelete: true)
                .Index(t => t.OrderStatusId);
        }

        public override void Down()
        {
            DropForeignKey("Lib.Orders", "OrderStatusId", "BaseLib.LookupItems");
            DropIndex("Lib.Orders", new[] { "OrderStatusId" });
            DropTable("Lib.Orders");
        }
    }
}