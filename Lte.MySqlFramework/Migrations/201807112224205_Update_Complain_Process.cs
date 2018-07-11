namespace Lte.MySqlFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Complain_Process : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ComplainProcesses", "ContactInfo", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ComplainProcesses", "ContactInfo");
        }
    }
}
