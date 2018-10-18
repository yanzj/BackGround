namespace Lte.MySqlFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Special_Alarm : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SpecialAlarmWorkItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AlarmNumber = c.String(unicode: false),
                        CircuitName = c.String(unicode: false),
                        CircuitNumber = c.String(unicode: false),
                        WorkItemNumber = c.String(unicode: false),
                        WorkItemState = c.Boolean(nullable: false),
                        AlarmInfo = c.String(unicode: false),
                        AlarmPosition = c.String(unicode: false),
                        BeginDate = c.DateTime(nullable: false, precision: 0),
                        RecoverTime = c.DateTime(precision: 0),
                        StationName = c.String(unicode: false),
                        AdditionalInfo = c.String(unicode: false),
                        ApplianceType = c.String(unicode: false),
                        AlarmLevel = c.String(unicode: false),
                        ApplianceName = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SpecialAlarmWorkItems");
        }
    }
}
