namespace Lte.MySqlFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Town_Hour_Prb1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.TownHourPrbs", "CellSerialNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TownHourPrbs", "CellSerialNumber", c => c.String(unicode: false));
        }
    }
}
