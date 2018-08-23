namespace Lte.MySqlFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Town_Hour_Users : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TownHourUsers", "AverageCaUsers", c => c.Double(nullable: false));
            AlterColumn("dbo.TownHourUsers", "PCellDownlinkMaxCaUes", c => c.Int(nullable: false));
            AlterColumn("dbo.TownHourUsers", "UplinkCompMaxUsers", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TownHourUsers", "UplinkCompMaxUsers", c => c.Int());
            AlterColumn("dbo.TownHourUsers", "PCellDownlinkMaxCaUes", c => c.Int());
            AlterColumn("dbo.TownHourUsers", "AverageCaUsers", c => c.Double());
        }
    }
}
