using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities;

namespace Lte.MySqlFramework.Entities
{
    [AutoMapFrom(typeof(Cell))]
    public class PciCell
    {
        public int ENodebId { get; set; }

        public byte SectorId { get; set; }

        public short Pci { get; set; }

        public int Frequency { get; set; }
    }
}