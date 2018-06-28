namespace Lte.MySqlFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Indoor_Distribution : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IndoorDistributions", "DistributionChannel", c => c.Byte(nullable: false));
            AddColumn("dbo.IndoorDistributions", "TotalFloors", c => c.Int(nullable: false));
            AddColumn("dbo.IndoorDistributions", "IsHasUnderGroundParker", c => c.Boolean(nullable: false));
            AddColumn("dbo.IndoorDistributions", "CoverageBuildingArea", c => c.Double());
            AddColumn("dbo.IndoorDistributions", "CoverageFloors", c => c.Int());
            DropColumn("dbo.IndoorDistributions", "DistributionChannelDescrition");
        }
        
        public override void Down()
        {
            AddColumn("dbo.IndoorDistributions", "DistributionChannelDescrition", c => c.String(unicode: false));
            DropColumn("dbo.IndoorDistributions", "CoverageFloors");
            DropColumn("dbo.IndoorDistributions", "CoverageBuildingArea");
            DropColumn("dbo.IndoorDistributions", "IsHasUnderGroundParker");
            DropColumn("dbo.IndoorDistributions", "TotalFloors");
            DropColumn("dbo.IndoorDistributions", "DistributionChannel");
        }
    }
}
