namespace Lte.MySqlFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_ComplainItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ComplainItems", "FinishContents", c => c.String(unicode: false));
            AddColumn("dbo.ComplainItems", "IsBaiduOffset", c => c.Boolean(nullable: false));
            DropColumn("dbo.ComplainItems", "IndoorDescription");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ComplainItems", "IndoorDescription", c => c.String(unicode: false));
            DropColumn("dbo.ComplainItems", "IsBaiduOffset");
            DropColumn("dbo.ComplainItems", "FinishContents");
        }
    }
}
