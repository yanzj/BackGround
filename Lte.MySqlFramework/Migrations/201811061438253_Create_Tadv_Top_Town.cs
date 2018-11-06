namespace Lte.MySqlFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_Tadv_Top_Town : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TopMrsTadvs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StatDate = c.DateTime(nullable: false, precision: 0),
                        ENodebId = c.Int(nullable: false),
                        SectorId = c.Byte(nullable: false),
                        TadvBelow1 = c.Int(nullable: false),
                        Tadv1To2 = c.Int(nullable: false),
                        Tadv2To3 = c.Int(nullable: false),
                        Tadv3To4 = c.Int(nullable: false),
                        Tadv4To6 = c.Int(nullable: false),
                        Tadv6To8 = c.Int(nullable: false),
                        Tadv8To10 = c.Int(nullable: false),
                        Tadv10To12 = c.Int(nullable: false),
                        Tadv12To14 = c.Int(nullable: false),
                        Tadv14To16 = c.Int(nullable: false),
                        Tadv16To18 = c.Int(nullable: false),
                        Tadv18To20 = c.Int(nullable: false),
                        Tadv20To24 = c.Int(nullable: false),
                        Tadv24To28 = c.Int(nullable: false),
                        Tadv28To32 = c.Int(nullable: false),
                        Tadv32To36 = c.Int(nullable: false),
                        Tadv36To42 = c.Int(nullable: false),
                        Tadv42To48 = c.Int(nullable: false),
                        Tadv48To54 = c.Int(nullable: false),
                        Tadv54To60 = c.Int(nullable: false),
                        Tadv60To80 = c.Int(nullable: false),
                        Tadv80To112 = c.Int(nullable: false),
                        Tadv112To192 = c.Int(nullable: false),
                        TadvAbove192 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TownMrsTadvs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StatDate = c.DateTime(nullable: false, precision: 0),
                        FrequencyBandType = c.Int(nullable: false),
                        TownId = c.Int(nullable: false),
                        TadvBelow1 = c.Long(nullable: false),
                        Tadv1To2 = c.Long(nullable: false),
                        Tadv2To3 = c.Long(nullable: false),
                        Tadv3To4 = c.Long(nullable: false),
                        Tadv4To6 = c.Long(nullable: false),
                        Tadv6To8 = c.Long(nullable: false),
                        Tadv8To10 = c.Long(nullable: false),
                        Tadv10To12 = c.Long(nullable: false),
                        Tadv12To14 = c.Long(nullable: false),
                        Tadv14To16 = c.Long(nullable: false),
                        Tadv16To18 = c.Long(nullable: false),
                        Tadv18To20 = c.Long(nullable: false),
                        Tadv20To24 = c.Long(nullable: false),
                        Tadv24To28 = c.Long(nullable: false),
                        Tadv28To32 = c.Long(nullable: false),
                        Tadv32To36 = c.Long(nullable: false),
                        Tadv36To42 = c.Long(nullable: false),
                        Tadv42To48 = c.Long(nullable: false),
                        Tadv48To54 = c.Long(nullable: false),
                        Tadv54To60 = c.Long(nullable: false),
                        Tadv60To80 = c.Long(nullable: false),
                        Tadv80To112 = c.Long(nullable: false),
                        Tadv112To192 = c.Long(nullable: false),
                        TadvAbove192 = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TownMrsTadvs");
            DropTable("dbo.TopMrsTadvs");
        }
    }
}
