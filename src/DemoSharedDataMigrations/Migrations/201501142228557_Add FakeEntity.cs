using System.Data.Entity.Migrations;

namespace CcAcca.DemoSharedDataMigrations.Migrations
{
    public partial class AddFakeEntity : DbMigration
    {
        public override void Up()
        {
            Sql(@"CREATE VIEW [Base].[FakeEntities] AS SELECT CAST(1 AS INT) AS Id, CAST('Christian' AS NVARCHAR(150)) AS Name UNION ALL SELECT CAST(1 AS INT) AS Id, CAST('Crowhurst' AS NVARCHAR(150)) AS Name");
            
        }
        
        public override void Down()
        {
            Sql("DROP VIEW [Base].[FakeEntities]");
        }
    }
}
