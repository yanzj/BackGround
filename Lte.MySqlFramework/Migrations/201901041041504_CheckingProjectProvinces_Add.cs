namespace Lte.MySqlFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CheckingProjectProvinces_Add : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CheckingProjectProvinces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WorkItemNumber = c.String(unicode: false),
                        City = c.String(unicode: false),
                        WorkItemName = c.String(unicode: false),
                        WorkItemState = c.Byte(nullable: false),
                        ProjectName = c.String(unicode: false),
                        Processor = c.String(unicode: false),
                        Receiver = c.String(unicode: false),
                        BeginDate = c.DateTime(nullable: false, precision: 0),
                        FinishDate = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CheckingProjectProvinces");
        }
    }
}
