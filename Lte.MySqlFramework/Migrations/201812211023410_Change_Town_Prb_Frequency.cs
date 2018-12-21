namespace Lte.MySqlFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Change_Town_Prb_Frequency : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TownPrbStats", "FrequencyBandType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TownPrbStats", "FrequencyBandType");
        }
    }
}
