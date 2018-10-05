namespace Lte.Parameters.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Construct_Dt : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FileRecord2G",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RasterNum = c.Short(nullable: false),
                        StatTime = c.DateTime(nullable: false, precision: 0),
                        Longtitute = c.Double(),
                        Lattitute = c.Double(),
                        Pn = c.Short(),
                        Ecio = c.Double(),
                        RxAgc = c.Double(),
                        TxAgc = c.Double(),
                        TxPower = c.Double(),
                        TxGain = c.Double(),
                        GridNumber = c.Int(),
                        FileId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FileRecord3G",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RasterNum = c.Short(nullable: false),
                        StatTime = c.DateTime(nullable: false, precision: 0),
                        Longtitute = c.Double(),
                        Lattitute = c.Double(),
                        Pn = c.Short(),
                        Sinr = c.Double(),
                        RxAgc0 = c.Double(),
                        RxAgc1 = c.Double(),
                        TxAgc = c.Double(),
                        TotalCi = c.Double(),
                        DrcValue = c.Int(),
                        RlpThroughput = c.Int(),
                        FileId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FileRecord4G",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ind = c.Int(),
                        RasterNum = c.Short(nullable: false),
                        StatTime = c.DateTime(nullable: false, precision: 0),
                        Longtitute = c.Double(),
                        Lattitute = c.Double(),
                        ENodebId = c.Int(),
                        SectorId = c.Byte(),
                        Frequency = c.Double(),
                        Pci = c.Short(),
                        Rsrp = c.Double(),
                        Sinr = c.Double(),
                        DlBler = c.Byte(),
                        CqiAverage = c.Double(),
                        UlMcs = c.Byte(),
                        DlMcs = c.Byte(),
                        PdcpThroughputUl = c.Double(),
                        PdcpThroughputDl = c.Double(),
                        PhyThroughputDl = c.Double(),
                        MacThroughputDl = c.Double(),
                        PuschRbNum = c.Int(),
                        PdschRbNum = c.Int(),
                        PuschRbSizeAverage = c.Int(),
                        PdschRbSizeAverage = c.Int(),
                        N1Pci = c.Short(),
                        N1Rsrp = c.Double(),
                        N2Pci = c.Short(),
                        N2Rsrp = c.Double(),
                        N3Pci = c.Short(),
                        N3Rsrp = c.Double(),
                        FileId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FileRecordVoltes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Longtitute = c.Double(),
                        Lattitute = c.Double(),
                        StatTime = c.DateTime(nullable: false, precision: 0),
                        ENodebId = c.Int(),
                        SectorId = c.Byte(),
                        Rsrp = c.Double(),
                        Sinr = c.Double(),
                        PdcchPathloss = c.Double(),
                        PdschPathloss = c.Double(),
                        PucchTxPower = c.Double(),
                        PucchPathloss = c.Double(),
                        PuschPathloss = c.Double(),
                        FirstEarfcn = c.Int(),
                        FirstPci = c.Short(),
                        FirstRsrp = c.Double(),
                        SecondEarfcn = c.Int(),
                        SecondPci = c.Short(),
                        SecondRsrp = c.Double(),
                        ThirdEarfcn = c.Int(),
                        ThirdPci = c.Short(),
                        ThirdRsrp = c.Double(),
                        FourthEarfcn = c.Int(),
                        FourthPci = c.Short(),
                        FourthRsrp = c.Double(),
                        FifthEarfcn = c.Int(),
                        FifthPci = c.Short(),
                        FifthRsrp = c.Double(),
                        SixthEarfcn = c.Int(),
                        SixthPci = c.Short(),
                        SixthRsrp = c.Double(),
                        RtpFrameType = c.Byte(),
                        RtpLoggedPayloadSize = c.Double(),
                        RtpPayloadSize = c.Double(),
                        PolqaMos = c.Double(),
                        PacketLossCount = c.Double(),
                        RxRtpPacketNum = c.Double(),
                        VoicePacketDelay = c.Double(),
                        VoicePacketLossRate = c.Double(),
                        VoiceJitter = c.Double(),
                        Rank2Cqi = c.Double(),
                        Rank1Cqi = c.Double(),
                        RasterNum = c.Short(nullable: false),
                        FileId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FileRecordVoltes");
            DropTable("dbo.FileRecord4G");
            DropTable("dbo.FileRecord3G");
            DropTable("dbo.FileRecord2G");
        }
    }
}
