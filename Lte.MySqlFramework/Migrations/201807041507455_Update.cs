namespace Lte.MySqlFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ZhangshangyouCoverages", "XOffset", c => c.Double(nullable: false));
            AddColumn("dbo.ZhangshangyouCoverages", "YOffset", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ZhangshangyouCoverages", "YOffset");
            DropColumn("dbo.ZhangshangyouCoverages", "XOffset");
        }
    }
}
