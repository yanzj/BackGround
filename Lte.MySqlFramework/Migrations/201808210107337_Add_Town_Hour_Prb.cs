namespace Lte.MySqlFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Town_Hour_Prb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TownHourPrbs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TownId = c.Int(nullable: false),
                        StatDate = c.DateTime(nullable: false, precision: 0),
                        CellSerialNumber = c.String(unicode: false),
                        UplinkPrbCapacity = c.Long(nullable: false),
                        PucchPrbs = c.Double(nullable: false),
                        PdschPrbCapacity = c.Long(nullable: false),
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
                        PdcchCceCapacity = c.Long(nullable: false),
                        PdcchTotalCces = c.Double(nullable: false),
                        PrachNonCompetitivePreambles = c.Long(nullable: false),
                        PrachCompetitivePreambles = c.Long(nullable: false),
                        PrachNonCompetitiveCapacity = c.Long(nullable: false),
                        PrachCompetitiveCapacity = c.Long(nullable: false),
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
                        PagingCapacity = c.Long(nullable: false),
                        PagingCount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TownHourPrbs");
        }
    }
}
