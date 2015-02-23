using System.Data.Entity.Migrations;

namespace CcAcca.DemoDownstreamMigrations.Migrations
{
    public partial class AddCustomUserRole : DbMigration
    {
        public override void Up()
        {
            AddColumn("Base.UserRoles", "CustomRoleProp", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            DropColumn("Base.UserRoles", "CustomRoleProp");
        }
    }
}
