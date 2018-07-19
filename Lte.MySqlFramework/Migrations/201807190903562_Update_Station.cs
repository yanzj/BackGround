namespace Lte.MySqlFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Station : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ENodebBases", "TowerStationNum", c => c.String(unicode: false));
            AddColumn("dbo.ENodebBases", "TowerStationName", c => c.String(unicode: false));
            AddColumn("dbo.ENodebBases", "BandClass", c => c.String(unicode: false));
            AddColumn("dbo.ENodebBases", "ServiceType", c => c.String(unicode: false));
            AddColumn("dbo.StationAntennas", "TowerStationNum", c => c.String(unicode: false));
            AddColumn("dbo.StationAntennas", "TowerStationName", c => c.String(unicode: false));
            AddColumn("dbo.StationRrus", "TowerStationNum", c => c.String(unicode: false));
            AddColumn("dbo.StationRrus", "TowerStationName", c => c.String(unicode: false));
            AddColumn("dbo.StationRrus", "RruName", c => c.String(unicode: false));
            AddColumn("dbo.StationRrus", "FrequencyBandType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StationRrus", "FrequencyBandType");
            DropColumn("dbo.StationRrus", "RruName");
            DropColumn("dbo.StationRrus", "TowerStationName");
            DropColumn("dbo.StationRrus", "TowerStationNum");
            DropColumn("dbo.StationAntennas", "TowerStationName");
            DropColumn("dbo.StationAntennas", "TowerStationNum");
            DropColumn("dbo.ENodebBases", "ServiceType");
            DropColumn("dbo.ENodebBases", "BandClass");
            DropColumn("dbo.ENodebBases", "TowerStationName");
            DropColumn("dbo.ENodebBases", "TowerStationNum");
        }
    }
}
