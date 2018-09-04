namespace Lte.MySqlFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Town_Hour_Cqi : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TownHourCqis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TownId = c.Int(nullable: false),
                        StatDate = c.DateTime(nullable: false, precision: 0),
                        Cqi0Times = c.Long(nullable: false),
                        Cqi1Times = c.Long(nullable: false),
                        Cqi2Times = c.Long(nullable: false),
                        Cqi3Times = c.Long(nullable: false),
                        Cqi4Times = c.Long(nullable: false),
                        Cqi5Times = c.Long(nullable: false),
                        Cqi6Times = c.Long(nullable: false),
                        Cqi7Times = c.Long(nullable: false),
                        Cqi8Times = c.Long(nullable: false),
                        Cqi9Times = c.Long(nullable: false),
                        Cqi10Times = c.Long(nullable: false),
                        Cqi11Times = c.Long(nullable: false),
                        Cqi12Times = c.Long(nullable: false),
                        Cqi13Times = c.Long(nullable: false),
                        Cqi14Times = c.Long(nullable: false),
                        Cqi15Times = c.Long(nullable: false),
                        TotalPrbs = c.Double(nullable: false),
                        DoubleFlowPrbs = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TownHourCqis");
        }
    }
}
