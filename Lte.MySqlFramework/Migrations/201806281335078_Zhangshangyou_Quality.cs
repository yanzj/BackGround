namespace Lte.MySqlFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Zhangshangyou_Quality : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ZhangshangyouQualities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SerialNumber = c.String(unicode: false),
                        StatTime = c.DateTime(nullable: false, precision: 0),
                        BackhaulNetwork = c.Byte(nullable: false),
                        Operator = c.Byte(nullable: false),
                        TestType = c.Byte(nullable: false),
                        BuildingName = c.String(unicode: false),
                        Floor = c.String(unicode: false),
                        UserName = c.String(unicode: false),
                        Terminal = c.String(unicode: false),
                        Imei = c.String(unicode: false),
                        Imsi = c.String(unicode: false),
                        FirstPacketDelay = c.Double(nullable: false),
                        PageOpenDelay = c.Double(nullable: false),
                        PageDnsDelay = c.Int(nullable: false),
                        PageConnectionSetupDelay = c.Int(nullable: false),
                        PageRequestDelay = c.Int(nullable: false),
                        PageResponseDelayString = c.Int(nullable: false),
                        PageRate = c.Double(nullable: false),
                        PageTestTimes = c.Int(nullable: false),
                        DownloadPeakRate = c.Double(nullable: false),
                        DownloadAverageRate = c.Double(nullable: false),
                        UploadPeakRate = c.Double(nullable: false),
                        UploadAverageRate = c.Double(nullable: false),
                        PingDelay = c.Int(nullable: false),
                        PingTimes = c.Int(nullable: false),
                        PingLossPackets = c.Int(nullable: false),
                        StreamConnectionDelay = c.Int(nullable: false),
                        StreamPlayDelay = c.Int(nullable: false),
                        StreamAverageRate = c.Double(nullable: false),
                        StreamPeakRate = c.Double(nullable: false),
                        StreamSuccessTimes = c.Int(nullable: false),
                        StreamTestTimes = c.Int(nullable: false),
                        LteCity = c.String(unicode: false),
                        ENodebId = c.Int(nullable: false),
                        SectorId = c.Byte(nullable: false),
                        ENodebName = c.String(unicode: false),
                        Ci = c.String(unicode: false),
                        Pci = c.Int(nullable: false),
                        CdmaCity = c.String(unicode: false),
                        CdmaBsc = c.Byte(nullable: false),
                        BtsId = c.Int(nullable: false),
                        BtsName = c.String(unicode: false),
                        CdmaSectorId = c.Byte(nullable: false),
                        Cid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.BluePrints");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.BluePrints",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FslNumber = c.String(unicode: false),
                        FileName = c.String(unicode: false),
                        Folder = c.String(unicode: false),
                        DesignBrief = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.ZhangshangyouQualities");
        }
    }
}
