namespace Lte.Parameters.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Change_Town_Precise : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.TownPreciseCoverage4GStat");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TownPreciseCoverage4GStat",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StatTime = c.DateTime(nullable: false),
                        TownId = c.Int(nullable: false),
                        TotalMrs = c.Int(nullable: false),
                        ThirdNeighbors = c.Int(nullable: false),
                        SecondNeighbors = c.Int(nullable: false),
                        FirstNeighbors = c.Int(nullable: false),
                        NeighborsMore = c.Int(nullable: false),
                        InterFirstNeighbors = c.Int(nullable: false),
                        InterSecondNeighbors = c.Int(nullable: false),
                        InterThirdNeighbors = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
