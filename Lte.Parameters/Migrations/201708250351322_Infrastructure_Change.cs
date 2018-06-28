namespace Lte.Parameters.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Infrastructure_Change : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.IndoorDistributions");
            DropTable("dbo.InfrastructureInfoes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.InfrastructureInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HotspotType = c.Byte(nullable: false),
                        HotspotName = c.String(),
                        InfrastructureType = c.Byte(nullable: false),
                        InfrastructureId = c.Int(nullable: false),
                        Address = c.String(),
                        SourceName = c.String(),
                        Longtitute = c.Double(nullable: false),
                        Lattitute = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IndoorDistributions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Range = c.String(),
                        SourceName = c.String(),
                        SourceType = c.String(),
                        Longtitute = c.Double(nullable: false),
                        Lattitute = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
