namespace Lte.Parameters.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Change_InterferenceMatrix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InterferenceMatrixStats", "NeighborRsrpBelow120", c => c.Int());
            AddColumn("dbo.InterferenceMatrixStats", "NeighborRsrpBetween120110", c => c.Int());
            AddColumn("dbo.InterferenceMatrixStats", "NeighborRsrpBetween110105", c => c.Int());
            AddColumn("dbo.InterferenceMatrixStats", "NeighborRsrpBetween105100", c => c.Int());
            AddColumn("dbo.InterferenceMatrixStats", "NeighborRsrpBetween10090", c => c.Int());
            AddColumn("dbo.InterferenceMatrixStats", "NeighborRsrpAbove90", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.InterferenceMatrixStats", "NeighborRsrpAbove90");
            DropColumn("dbo.InterferenceMatrixStats", "NeighborRsrpBetween10090");
            DropColumn("dbo.InterferenceMatrixStats", "NeighborRsrpBetween105100");
            DropColumn("dbo.InterferenceMatrixStats", "NeighborRsrpBetween110105");
            DropColumn("dbo.InterferenceMatrixStats", "NeighborRsrpBetween120110");
            DropColumn("dbo.InterferenceMatrixStats", "NeighborRsrpBelow120");
        }
    }
}
