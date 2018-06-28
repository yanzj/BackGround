using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Wireless;

namespace Abp.EntityFramework.Entities
{
    [AutoMapTo(typeof(PreciseWorkItemCell))]
    public class PreciseInterferenceVictimDto : ILteCellQuery
    {
        public int ENodebId { get; set; }

        public byte SectorId { get; set; }

        public double BackwardDb6Share { get; set; }

        public double BackwardDb10Share { get; set; }

        public double BackwardMod3Share { get; set; }

        public double BackwardMod6Share { get; set; }
    }
}