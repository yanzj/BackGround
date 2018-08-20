namespace Lte.MySqlFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_HourPrb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HourPrbs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StatTime = c.DateTime(nullable: false, precision: 0),
                        ENodebId = c.Int(nullable: false),
                        SectorId = c.Byte(nullable: false),
                        CellSerialNumber = c.String(unicode: false),
                        UplinkPrbCapacity = c.Int(nullable: false),
                        PucchPrbs = c.Double(nullable: false),
                        PdschPrbCapacity = c.Int(nullable: false),
                        PdschTotalPrbs = c.Double(nullable: false),
                        PdschQci1Prbs = c.Double(nullable: false),
                        PdschQci2Prbs = c.Double(nullable: false),
                        PdschQci3Prbs = c.Double(nullable: false),
                        PdschQci4Prbs = c.Double(nullable: false),
                        PdschQci5Prbs = c.Double(nullable: false),
                        PdschQci6Prbs = c.Double(nullable: false),
                        PdschQci7Prbs = c.Double(nullable: false),
                        PdschQci8Prbs = c.Double(nullable: false),
                        PdschQci9Prbs = c.Double(nullable: false),
                        DownlinkControlPrbs = c.Double(nullable: false),
                        UplinkTotalPrbs = c.Double(nullable: false),
                        DownlinkTotalPrbs = c.Double(nullable: false),
                        PdcchCceCapacity = c.Int(nullable: false),
                        PdcchTotalCces = c.Double(nullable: false),
                        PrachNonCompetitivePreambles = c.Int(nullable: false),
                        PrachCompetitivePreambles = c.Int(nullable: false),
                        PrachNonCompetitiveCapacity = c.Int(nullable: false),
                        PrachCompetitiveCapacity = c.Int(nullable: false),
                        PuschTotalPrbs = c.Double(nullable: false),
                        PuschQci1Prbs = c.Double(nullable: false),
                        PuschQci2Prbs = c.Double(nullable: false),
                        PuschQci3Prbs = c.Double(nullable: false),
                        PuschQci4Prbs = c.Double(nullable: false),
                        PuschQci5Prbs = c.Double(nullable: false),
                        PuschQci6Prbs = c.Double(nullable: false),
                        PuschQci7Prbs = c.Double(nullable: false),
                        PuschQci8Prbs = c.Double(nullable: false),
                        PuschQci9Prbs = c.Double(nullable: false),
                        PagingCapacity = c.Int(nullable: false),
                        PagingCount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.HourPrbs");
        }
    }
}
