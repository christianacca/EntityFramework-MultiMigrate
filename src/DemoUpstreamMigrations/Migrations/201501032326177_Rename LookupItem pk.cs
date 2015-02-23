using System.Data.Entity.Migrations;

namespace CcAcca.DemoUpstreamMigrations.Migrations
{
    public partial class RenameLookupItempk : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("Base.LookupItems");
            RenameColumn(table: "Base.LookupItems", name: "Id", newName: "Key");
            AddPrimaryKey("Base.LookupItems", "Key");
        }
        
        public override void Down()
        {
            DropPrimaryKey("Base.LookupItems");
            RenameColumn(table: "Base.LookupItems", name: "Key", newName: "Id");
            AddPrimaryKey("Base.LookupItems", "Id");
        }
    }
}
