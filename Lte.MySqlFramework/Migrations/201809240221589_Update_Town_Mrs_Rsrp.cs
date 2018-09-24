namespace Lte.MySqlFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Town_Mrs_Rsrp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TownMrsRsrps", "FrequencyBandType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TownMrsRsrps", "FrequencyBandType");
        }
    }
}
