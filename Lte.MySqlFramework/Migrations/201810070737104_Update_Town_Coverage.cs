namespace Lte.MySqlFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Town_Coverage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TownCoverageStats", "FrequencyBandType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TownCoverageStats", "FrequencyBandType");
        }
    }
}
