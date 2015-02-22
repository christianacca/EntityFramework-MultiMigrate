using System.Data.Entity.Migrations;

namespace CcAcca.DemoDataMigrations.Migrations
{
    public partial class AddAssetCreatedBy : DbMigration
    {
        public override void Up()
        {
            AddColumn("Main.Assets", "CreatedBy", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("Main.Assets", "CreatedBy");
        }
    }
}
