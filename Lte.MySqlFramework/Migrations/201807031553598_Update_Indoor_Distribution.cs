namespace Lte.MySqlFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Indoor_Distribution : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IndoorDistributions", "CombinerIntegrator", c => c.String(unicode: false));
            AddColumn("dbo.IndoorDistributions", "DistributionClass", c => c.Byte(nullable: false));
            AddColumn("dbo.IndoorDistributions", "CheckingAddress", c => c.String(unicode: false));
            AddColumn("dbo.IndoorDistributions", "IsCombinedWithOtherOperator", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.IndoorDistributions", "IsCombinedWithOtherOperator");
            DropColumn("dbo.IndoorDistributions", "CheckingAddress");
            DropColumn("dbo.IndoorDistributions", "DistributionClass");
            DropColumn("dbo.IndoorDistributions", "CombinerIntegrator");
        }
    }
}
