namespace LtePlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Expand_User : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "LoginTimes", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "LastLoginTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "LastLoginTime");
            DropColumn("dbo.AspNetUsers", "LoginTimes");
        }
    }
}
