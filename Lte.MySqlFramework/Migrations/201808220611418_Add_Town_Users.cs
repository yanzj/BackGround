namespace Lte.MySqlFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Town_Users : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TownHourUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TownId = c.Int(nullable: false),
                        StatDate = c.DateTime(nullable: false, precision: 0),
                        MaxRrcUsers = c.Int(nullable: false),
                        AverageRrcUsers = c.Double(nullable: false),
                        UplinkAverageActiveUsers = c.Double(nullable: false),
                        UplinkAverageActiveUsersQci1 = c.Double(nullable: false),
                        UplinkAverageActiveUsersQci2 = c.Double(nullable: false),
                        UplinkAverageActiveUsersQci3 = c.Double(nullable: false),
                        UplinkAverageActiveUsersQci4 = c.Double(nullable: false),
                        UplinkAverageActiveUsersQci5 = c.Double(nullable: false),
                        UplinkAverageActiveUsersQci6 = c.Double(nullable: false),
                        UplinkAverageActiveUsersQci7 = c.Double(nullable: false),
                        UplinkAverageActiveUsersQci8 = c.Double(nullable: false),
                        UplinkAverageActiveUsersQci9 = c.Double(nullable: false),
                        DownlinkAverageActiveUsers = c.Double(nullable: false),
                        DownlinkAverageActiveUsersQci1 = c.Double(nullable: false),
                        DownlinkAverageActiveUsersQci2 = c.Double(nullable: false),
                        DownlinkAverageActiveUsersQci3 = c.Double(nullable: false),
                        DownlinkAverageActiveUsersQci4 = c.Double(nullable: false),
                        DownlinkAverageActiveUsersQci5 = c.Double(nullable: false),
                        DownlinkAverageActiveUsersQci6 = c.Double(nullable: false),
                        DownlinkAverageActiveUsersQci7 = c.Double(nullable: false),
                        DownlinkAverageActiveUsersQci8 = c.Double(nullable: false),
                        DownlinkAverageActiveUsersQci9 = c.Double(nullable: false),
                        MaxActiveUsers = c.Int(nullable: false),
                        AverageCaUsers = c.Double(),
                        MaxCaUsers = c.Int(nullable: false),
                        PCellDownlinkAverageCaUes = c.Double(nullable: false),
                        PCellDownlinkMaxCaUes = c.Int(),
                        PCellPdschCaPrbs = c.Long(nullable: false),
                        SCellPdschCaPrbs = c.Long(nullable: false),
                        UplinkCompAverageUsers = c.Double(nullable: false),
                        UplinkCompMaxUsers = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TownHourUsers");
        }
    }
}
