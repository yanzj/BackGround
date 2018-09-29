namespace Lte.MySqlFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Rssi_Stat : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RssiHuaweis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ENodebId = c.Int(nullable: false),
                        SectorId = c.Byte(nullable: false),
                        StatTime = c.DateTime(nullable: false, precision: 0),
                        AverageRssiBelow120 = c.Long(nullable: false),
                        AverageRssi120To100 = c.Long(nullable: false),
                        AverageRssi100To92 = c.Long(nullable: false),
                        AverageRssi92To80 = c.Long(nullable: false),
                        AverageRssiAbove80 = c.Long(nullable: false),
                        PucchRssiIndex0 = c.Long(nullable: false),
                        PucchRssiIndex1 = c.Long(nullable: false),
                        PucchRssiIndex2 = c.Long(nullable: false),
                        PucchRssiIndex3 = c.Long(nullable: false),
                        PucchRssiIndex4 = c.Long(nullable: false),
                        PucchRssiIndex5 = c.Long(nullable: false),
                        PucchRssiIndex6 = c.Long(nullable: false),
                        PucchRssiIndex7 = c.Long(nullable: false),
                        PucchRssiIndex8 = c.Long(nullable: false),
                        PucchRssiIndex9 = c.Long(nullable: false),
                        PucchRssiIndex10 = c.Long(nullable: false),
                        PucchRssiIndex11 = c.Long(nullable: false),
                        PucchRssiIndex12 = c.Long(nullable: false),
                        PucchRssiIndex13 = c.Long(nullable: false),
                        PucchRssiIndex14 = c.Long(nullable: false),
                        PucchRssiIndex15 = c.Long(nullable: false),
                        PucchRssiIndex16 = c.Long(nullable: false),
                        PucchRssiIndex17 = c.Long(nullable: false),
                        PucchRssiIndex18 = c.Long(nullable: false),
                        PucchRssiIndex19 = c.Long(nullable: false),
                        PucchRssiIndex20 = c.Long(nullable: false),
                        PucchRssiIndex21 = c.Long(nullable: false),
                        PuschRssiIndex0 = c.Long(nullable: false),
                        PuschRssiIndex1 = c.Long(nullable: false),
                        PuschRssiIndex2 = c.Long(nullable: false),
                        PuschRssiIndex3 = c.Long(nullable: false),
                        PuschRssiIndex4 = c.Long(nullable: false),
                        PuschRssiIndex5 = c.Long(nullable: false),
                        PuschRssiIndex6 = c.Long(nullable: false),
                        PuschRssiIndex7 = c.Long(nullable: false),
                        PuschRssiIndex8 = c.Long(nullable: false),
                        PuschRssiIndex9 = c.Long(nullable: false),
                        PuschRssiIndex10 = c.Long(nullable: false),
                        PuschRssiIndex11 = c.Long(nullable: false),
                        PuschRssiIndex12 = c.Long(nullable: false),
                        PuschRssiIndex13 = c.Long(nullable: false),
                        PuschRssiIndex14 = c.Long(nullable: false),
                        PuschRssiIndex15 = c.Long(nullable: false),
                        PuschRssiIndex16 = c.Long(nullable: false),
                        PuschRssiIndex17 = c.Long(nullable: false),
                        PuschRssiIndex18 = c.Long(nullable: false),
                        PuschRssiIndex19 = c.Long(nullable: false),
                        PuschRssiIndex20 = c.Long(nullable: false),
                        PuschRssiIndex21 = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RssiZtes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ENodebId = c.Int(nullable: false),
                        SectorId = c.Byte(nullable: false),
                        StatTime = c.DateTime(nullable: false, precision: 0),
                        MaxRssi = c.Double(nullable: false),
                        MinRssi = c.Double(nullable: false),
                        AverageRssi = c.Double(nullable: false),
                        PuschRssiBelow120 = c.Int(nullable: false),
                        PuschRssi120To116 = c.Int(nullable: false),
                        PuschRssi116To112 = c.Int(nullable: false),
                        PuschRssi112To108 = c.Int(nullable: false),
                        PuschRssi108To104 = c.Int(nullable: false),
                        PuschRssi104To100 = c.Int(nullable: false),
                        PuschRssi100To96 = c.Int(nullable: false),
                        PuschRssi96To92 = c.Int(nullable: false),
                        PuschRssiAbove92 = c.Int(nullable: false),
                        PucchRssiBelow120 = c.Int(nullable: false),
                        PucchRssi120To116 = c.Int(nullable: false),
                        PucchRssi116To112 = c.Int(nullable: false),
                        PucchRssi112To108 = c.Int(nullable: false),
                        PucchRssi108To104 = c.Int(nullable: false),
                        PucchRssi104To100 = c.Int(nullable: false),
                        PucchRssi100To96 = c.Int(nullable: false),
                        PucchRssi96To92 = c.Int(nullable: false),
                        PucchRssiAbove92 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RssiZtes");
            DropTable("dbo.RssiHuaweis");
        }
    }
}
