namespace Lte.MySqlFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Alarm_Work_Item1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AlarmWorkItems", "State", c => c.Byte(nullable: false));
            AddColumn("dbo.AlarmWorkItems", "IsExpire", c => c.Boolean(nullable: false));
            AddColumn("dbo.AlarmWorkItems", "IsFixExpire", c => c.Boolean(nullable: false));
            AddColumn("dbo.StationDictionaries", "Address", c => c.String(unicode: false));
            DropColumn("dbo.AlarmWorkItems", "StateDescription");
            DropColumn("dbo.AlarmWorkItems", "Expire");
            DropColumn("dbo.AlarmWorkItems", "FixExpire");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AlarmWorkItems", "FixExpire", c => c.String(unicode: false));
            AddColumn("dbo.AlarmWorkItems", "Expire", c => c.String(unicode: false));
            AddColumn("dbo.AlarmWorkItems", "StateDescription", c => c.String(unicode: false));
            DropColumn("dbo.StationDictionaries", "Address");
            DropColumn("dbo.AlarmWorkItems", "IsFixExpire");
            DropColumn("dbo.AlarmWorkItems", "IsExpire");
            DropColumn("dbo.AlarmWorkItems", "State");
        }
    }
}
