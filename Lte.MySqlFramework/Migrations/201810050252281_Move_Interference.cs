namespace Lte.MySqlFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Move_Interference : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InterferenceMatrixStats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CellId = c.String(unicode: false),
                        Pci = c.Short(nullable: false),
                        NeighborPci = c.Short(nullable: false),
                        DestENodebId = c.Int(nullable: false),
                        DestSectorId = c.Byte(nullable: false),
                        StatDate = c.DateTime(nullable: false, precision: 0),
                        Diff0 = c.Int(nullable: false),
                        Diff3 = c.Int(nullable: false),
                        Diff6 = c.Int(nullable: false),
                        Diff9 = c.Int(nullable: false),
                        Diff12 = c.Int(nullable: false),
                        DiffLarge = c.Int(nullable: false),
                        SinrUl0to9 = c.Int(nullable: false),
                        SinrUl10to19 = c.Int(nullable: false),
                        SinrUl20to24 = c.Int(nullable: false),
                        SinrUl25to29 = c.Int(nullable: false),
                        SinrUl30to34 = c.Int(nullable: false),
                        SinrUlAbove35 = c.Int(nullable: false),
                        RsrpBelow120 = c.Int(nullable: false),
                        RsrpBetween120110 = c.Int(nullable: false),
                        RsrpBetween110105 = c.Int(nullable: false),
                        RsrpBetween105100 = c.Int(nullable: false),
                        RsrpBetween10090 = c.Int(nullable: false),
                        RsrpAbove90 = c.Int(nullable: false),
                        NeighborRsrpBelow120 = c.Int(),
                        NeighborRsrpBetween120110 = c.Int(),
                        NeighborRsrpBetween110105 = c.Int(),
                        NeighborRsrpBetween105100 = c.Int(),
                        NeighborRsrpBetween10090 = c.Int(),
                        NeighborRsrpAbove90 = c.Int(),
                        Ta0or1 = c.Int(nullable: false),
                        Ta2or3 = c.Int(nullable: false),
                        Ta4or5 = c.Int(nullable: false),
                        Ta6or7 = c.Int(nullable: false),
                        Ta8or9 = c.Int(nullable: false),
                        Ta10to12 = c.Int(nullable: false),
                        Ta13to15 = c.Int(nullable: false),
                        Ta16to19 = c.Int(nullable: false),
                        Ta20to24 = c.Int(nullable: false),
                        Ta25to29 = c.Int(nullable: false),
                        Ta30to39 = c.Int(nullable: false),
                        TaAbove40 = c.Int(nullable: false),
                        Earfcn = c.Int(),
                        NeighborEarfcn = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.InterferenceMatrixStats");
        }
    }
}
