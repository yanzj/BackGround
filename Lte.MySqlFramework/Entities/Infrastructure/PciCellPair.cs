using Abp.EntityFramework.AutoMapper;

namespace Lte.MySqlFramework.Entities.Infrastructure
{
    [AutoMapFrom(typeof(PciCell))]
    public class PciCellPair
    {
        public int ENodebId { get; set; }

        public short Pci { get; set; }
    }
}