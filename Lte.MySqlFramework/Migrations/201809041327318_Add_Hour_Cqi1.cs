namespace Lte.MySqlFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Hour_Cqi1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.HourCqis", "TotalPrbs", c => c.Long(nullable: false));
            AlterColumn("dbo.HourCqis", "DoubleFlowPrbs", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.HourCqis", "DoubleFlowPrbs", c => c.Int(nullable: false));
            AlterColumn("dbo.HourCqis", "TotalPrbs", c => c.Int(nullable: false));
        }
    }
}
