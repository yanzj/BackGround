namespace Lte.MySqlFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Zhangshangyou_Coverage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ZhangshangyouCoverages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StatTime = c.DateTime(nullable: false, precision: 0),
                        BuildingName = c.String(unicode: false),
                        UserName = c.String(unicode: false),
                        SerialNumber = c.String(unicode: false),
                        Imei = c.String(unicode: false),
                        Imsi = c.String(unicode: false),
                        Terminal = c.String(unicode: false),
                        BackhaulNetwork = c.Byte(nullable: false),
                        Longtitute = c.Double(nullable: false),
                        Lattitute = c.Double(nullable: false),
                        District = c.String(unicode: false),
                        Road = c.String(unicode: false),
                        Address = c.String(unicode: false),
                        LteCity = c.String(unicode: false),
                        ENodebId = c.Int(nullable: false),
                        SectorId = c.Byte(nullable: false),
                        ENodebName = c.String(unicode: false),
                        Ci = c.String(unicode: false),
                        Pci = c.Int(nullable: false),
                        Tac = c.Int(nullable: false),
                        Rsrp = c.Double(nullable: false),
                        Rsrq = c.Double(nullable: false),
                        Rssi = c.Double(nullable: false),
                        Sinr = c.Double(nullable: false),
                        CdmaCity = c.String(unicode: false),
                        CdmaBsc = c.Byte(nullable: false),
                        BtsId = c.Int(nullable: false),
                        BtsName = c.String(unicode: false),
                        CdmaSectorId = c.Byte(nullable: false),
                        Cid = c.Int(nullable: false),
                        Rx3G = c.Double(nullable: false),
                        EcIo3G = c.Double(nullable: false),
                        Sinr3G = c.Double(nullable: false),
                        Rx2G = c.Double(nullable: false),
                        EcIo = c.Double(nullable: false),
                        GsmCellId = c.String(unicode: false),
                        GsmLac = c.String(unicode: false),
                        GsmRx = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ZhangshangyouCoverages");
        }
    }
}
