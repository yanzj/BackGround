using System.ComponentModel.DataAnnotations.Schema;

namespace Abp.EntityFramework.Entities.Infrastructure
{
    [Table("dbo.LteNeighborCells")]
    public class NearestPciCell : LteNeighborCell
    {
        public short Pci { get; set; }

        public int TotalTimes { get; set; }

    }
}