namespace Lte.Parameters.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Change_ENodeb : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.CdmaBts");
            DropTable("dbo.ENodebs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ENodebs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ENodebId = c.Int(nullable: false),
                        Name = c.String(maxLength: 50),
                        TownId = c.Int(nullable: false),
                        Longtitute = c.Double(nullable: false),
                        Lattitute = c.Double(nullable: false),
                        Factory = c.String(),
                        IsFdd = c.Boolean(nullable: false),
                        Address = c.String(),
                        Gateway = c.Int(nullable: false),
                        SubIp = c.Byte(nullable: false),
                        PlanNum = c.String(),
                        OpenDate = c.DateTime(nullable: false),
                        IsInUse = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CdmaBts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ENodebId = c.Int(nullable: false),
                        Name = c.String(maxLength: 50),
                        TownId = c.Int(nullable: false),
                        Longtitute = c.Double(nullable: false),
                        Lattitute = c.Double(nullable: false),
                        Address = c.String(),
                        BtsId = c.Int(nullable: false),
                        BscId = c.Short(nullable: false),
                        IsInUse = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
