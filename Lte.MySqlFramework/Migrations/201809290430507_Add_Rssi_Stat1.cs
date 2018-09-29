namespace Lte.MySqlFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Rssi_Stat1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RssiHuaweis", "MaxRssiRb", c => c.Double(nullable: false));
            AddColumn("dbo.RssiHuaweis", "MinRssiRb", c => c.Double(nullable: false));
            AddColumn("dbo.RssiHuaweis", "AverageRssiRb", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RssiHuaweis", "AverageRssiRb");
            DropColumn("dbo.RssiHuaweis", "MinRssiRb");
            DropColumn("dbo.RssiHuaweis", "MaxRssiRb");
        }
    }
}
