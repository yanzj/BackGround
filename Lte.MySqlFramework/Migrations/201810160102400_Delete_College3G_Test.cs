namespace Lte.MySqlFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Delete_College3G_Test : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.College3GTestResults");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.College3GTestResults",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CollegeId = c.Int(nullable: false),
                        TestTime = c.DateTime(nullable: false, precision: 0),
                        Place = c.String(unicode: false),
                        Tester = c.String(unicode: false),
                        DownloadRate = c.Double(nullable: false),
                        AccessUsers = c.Int(nullable: false),
                        MinRssi = c.Double(nullable: false),
                        MaxRssi = c.Double(nullable: false),
                        Vswr = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
