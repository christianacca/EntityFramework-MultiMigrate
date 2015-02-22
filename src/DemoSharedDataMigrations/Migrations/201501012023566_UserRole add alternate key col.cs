using System.Data.Entity.Migrations;

namespace CcAcca.DemoSharedDataMigrations.Migrations
{
    public partial class UserRoleaddalternatekeycol : DbMigration
    {
        public override void Up()
        {
            AddColumn("Base.UserRoles", "Key", c => c.Int(nullable: false));
            CreateIndex("Base.UserRoles", "Key", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("Base.UserRoles", new[] { "Key" });
            DropColumn("Base.UserRoles", "Key");
        }
    }
}
