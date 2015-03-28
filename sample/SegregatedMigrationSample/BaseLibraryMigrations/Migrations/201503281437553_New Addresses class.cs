namespace CcAcca.BaseLibraryMigrations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewAddressesclass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Line1 = c.String(maxLength: 500),
                        Line2 = c.String(maxLength: 500),
                        Line3 = c.String(maxLength: 500),
                        Line4 = c.String(maxLength: 500),
                        Postcode = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Addresses");
        }
    }
}
