namespace Lte.MySqlFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Station : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ConstructionInformations", "Province", c => c.String(unicode: true));
            AddColumn("dbo.ConstructionInformations", "City", c => c.String(unicode: true));
            AddColumn("dbo.ConstructionInformations", "IsCaCell", c => c.Boolean(nullable: false));
            AddColumn("dbo.ConstructionInformations", "SecondaryCellId", c => c.String(unicode: true));
            AddColumn("dbo.ConstructionInformations", "IsSharedCell", c => c.Boolean(nullable: false));
            AddColumn("dbo.ConstructionInformations", "NetworkType", c => c.Byte(nullable: false));
            AddColumn("dbo.ENodebBases", "Province", c => c.String(unicode: true));
            AddColumn("dbo.ENodebBases", "City", c => c.String(unicode: true));
            AddColumn("dbo.StationAntennas", "TowerPlatform", c => c.Int(nullable: false));
            AddColumn("dbo.StationDictionaries", "Province", c => c.String(unicode: true));
            AddColumn("dbo.StationDictionaries", "City", c => c.String(unicode: true));
            AddColumn("dbo.StationDictionaries", "ENodebClass", c => c.Byte(nullable: false));
            AddColumn("dbo.StationRrus", "Province", c => c.String(unicode: true));
            AddColumn("dbo.StationRrus", "City", c => c.String(unicode: true));
            AddColumn("dbo.StationRrus", "IsSharedRru", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StationRrus", "IsSharedRru");
            DropColumn("dbo.StationRrus", "City");
            DropColumn("dbo.StationRrus", "Province");
            DropColumn("dbo.StationDictionaries", "ENodebClass");
            DropColumn("dbo.StationDictionaries", "City");
            DropColumn("dbo.StationDictionaries", "Province");
            DropColumn("dbo.StationAntennas", "TowerPlatform");
            DropColumn("dbo.ENodebBases", "City");
            DropColumn("dbo.ENodebBases", "Province");
            DropColumn("dbo.ConstructionInformations", "NetworkType");
            DropColumn("dbo.ConstructionInformations", "IsSharedCell");
            DropColumn("dbo.ConstructionInformations", "SecondaryCellId");
            DropColumn("dbo.ConstructionInformations", "IsCaCell");
            DropColumn("dbo.ConstructionInformations", "City");
            DropColumn("dbo.ConstructionInformations", "Province");
        }
    }
}
