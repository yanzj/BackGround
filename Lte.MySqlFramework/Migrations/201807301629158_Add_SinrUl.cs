namespace Lte.MySqlFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_SinrUl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TopMrsSinrUls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StatDate = c.DateTime(nullable: false, precision: 0),
                        ENodebId = c.Int(nullable: false),
                        SectorId = c.Byte(nullable: false),
                        SinrUlBelowM9 = c.Long(nullable: false),
                        SinrUlM9ToM6 = c.Long(nullable: false),
                        SinrUlM6ToM3 = c.Long(nullable: false),
                        SinrUlM3To0 = c.Long(nullable: false),
                        SinrUl0To3 = c.Long(nullable: false),
                        SinrUl3To6 = c.Long(nullable: false),
                        SinrUl6To9 = c.Long(nullable: false),
                        SinrUl9To12 = c.Long(nullable: false),
                        SinrUl12To15 = c.Long(nullable: false),
                        SinrUl15To18 = c.Long(nullable: false),
                        SinrUl18To21 = c.Long(nullable: false),
                        SinrUl21To24 = c.Long(nullable: false),
                        SinrUlAbove24 = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TownMrsSinrUls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StatDate = c.DateTime(nullable: false, precision: 0),
                        SinrUlBelowM9 = c.Long(nullable: false),
                        SinrUlM9ToM6 = c.Long(nullable: false),
                        SinrUlM6ToM3 = c.Long(nullable: false),
                        SinrUlM3To0 = c.Long(nullable: false),
                        SinrUl0To3 = c.Long(nullable: false),
                        SinrUl3To6 = c.Long(nullable: false),
                        SinrUl6To9 = c.Long(nullable: false),
                        SinrUl9To12 = c.Long(nullable: false),
                        SinrUl12To15 = c.Long(nullable: false),
                        SinrUl15To18 = c.Long(nullable: false),
                        SinrUl18To21 = c.Long(nullable: false),
                        SinrUl21To24 = c.Long(nullable: false),
                        SinrUlAbove24 = c.Long(nullable: false),
                        TownId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TownMrsSinrUls");
            DropTable("dbo.TopMrsSinrUls");
        }
    }
}
