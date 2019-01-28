namespace Lte.MySqlFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Change_Complain_Process : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ComplainProcesses", "GroundInfo", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ComplainProcesses", "GroundInfo");
        }
    }
}
