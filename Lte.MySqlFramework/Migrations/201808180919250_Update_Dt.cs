namespace Lte.MySqlFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Dt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CsvFilesInfoes", "FileType", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CsvFilesInfoes", "FileType");
        }
    }
}
