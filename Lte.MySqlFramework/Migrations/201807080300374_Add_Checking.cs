namespace Lte.MySqlFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Checking : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CheckingBasics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CheckingFlowNumber = c.String(unicode: false),
                        BeginDate = c.DateTime(nullable: false, precision: 0),
                        EndDate = c.DateTime(precision: 0),
                        Longtitute = c.Double(nullable: false),
                        Lattitute = c.Double(nullable: false),
                        Distance = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CheckingDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CheckingFlowNumber = c.String(unicode: false),
                        CheckingTheme = c.String(unicode: false),
                        Results = c.String(unicode: false),
                        IsNeedFixing = c.Boolean(nullable: false),
                        IsContainPicture = c.Boolean(nullable: false),
                        Directory = c.String(unicode: false),
                        ComplainState = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CheckingProjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        District = c.String(unicode: false),
                        StationSerialNumber = c.String(unicode: false),
                        Company = c.String(unicode: false),
                        Maintainer = c.String(unicode: false),
                        CheckingFlowNumber = c.String(unicode: false),
                        BeginDate = c.DateTime(nullable: false, precision: 0),
                        EndDate = c.DateTime(nullable: false, precision: 0),
                        ProjectName = c.String(unicode: false),
                        FinalDate = c.DateTime(precision: 0),
                        WorkItemState = c.Byte(nullable: false),
                        StationName = c.String(unicode: false),
                        Comments = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CheckingProjects");
            DropTable("dbo.CheckingDetails");
            DropTable("dbo.CheckingBasics");
        }
    }
}
