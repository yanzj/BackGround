using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Infrastructure;

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