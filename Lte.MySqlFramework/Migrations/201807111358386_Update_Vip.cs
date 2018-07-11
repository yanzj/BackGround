namespace Lte.MySqlFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Vip : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VipDemands", "WorkItemNumber", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.VipDemands", "WorkItemNumber");
        }
    }
}
