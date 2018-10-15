namespace Lte.MySqlFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Town_Hour_Cqi : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TownHourCqis", "FrequencyBandType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TownHourCqis", "FrequencyBandType");
        }
    }
}
