namespace Lte.MySqlFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Staion_InUse : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ConstructionInformations", "IsInUse", c => c.Boolean(nullable: false));
            AddColumn("dbo.ENodebBases", "IsInUse", c => c.Boolean(nullable: false));
            AddColumn("dbo.IndoorDistributions", "IsInUse", c => c.Boolean(nullable: false));
            AddColumn("dbo.StationAntennas", "IsInUse", c => c.Boolean(nullable: false));
            AddColumn("dbo.StationDictionaries", "IsInUse", c => c.Boolean(nullable: false));
            AddColumn("dbo.StationRrus", "IsInUse", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StationRrus", "IsInUse");
            DropColumn("dbo.StationDictionaries", "IsInUse");
            DropColumn("dbo.StationAntennas", "IsInUse");
            DropColumn("dbo.IndoorDistributions", "IsInUse");
            DropColumn("dbo.ENodebBases", "IsInUse");
            DropColumn("dbo.ConstructionInformations", "IsInUse");
        }
    }
}
