namespace Lte.MySqlFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Move_Precise : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PreciseCoverage4G",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StatTime = c.DateTime(nullable: false, precision: 0),
                        CellId = c.Int(nullable: false),
                        SectorId = c.Byte(nullable: false),
                        Neighbors0 = c.Int(nullable: false),
                        Neighbors1 = c.Int(nullable: false),
                        Neighbors2 = c.Int(nullable: false),
                        Neighbors3 = c.Int(nullable: false),
                        NeighborsMore = c.Int(nullable: false),
                        TotalMrs = c.Int(nullable: false),
                        ThirdNeighbors = c.Int(nullable: false),
                        SecondNeighbors = c.Int(nullable: false),
                        FirstNeighbors = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PreciseCoverage4G");
        }
    }
}
