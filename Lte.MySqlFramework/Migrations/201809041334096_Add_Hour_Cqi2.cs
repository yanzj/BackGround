namespace Lte.MySqlFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Hour_Cqi2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.HourCqis", "TotalPrbs", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.HourCqis", "TotalPrbs", c => c.Long(nullable: false));
        }
    }
}
