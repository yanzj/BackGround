namespace Lte.MySqlFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Town_Cqi : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.TownCqiStats", "FrequencyBandType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            //DropColumn("dbo.TownCqiStats", "FrequencyBandType");
        }
    }
}
