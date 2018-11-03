namespace Lte.MySqlFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_SinrUl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TownMrsSinrUls", "FrequencyBandType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TownMrsSinrUls", "FrequencyBandType");
        }
    }
}
