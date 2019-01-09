namespace Lte.MySqlFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Town_DoubleFlow : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TownDoubleFlows", "FrequencyBandType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TownDoubleFlows", "FrequencyBandType");
        }
    }
}
