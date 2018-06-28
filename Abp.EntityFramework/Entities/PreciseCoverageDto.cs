using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Wireless;

namespace Abp.EntityFramework.Entities
{
    [AutoMapTo(typeof(PreciseWorkItemCell))]
    public class PreciseCoverageDto : ILteCellQuery
    {
        public int ENodebId { get; set; }

        public byte SectorId { get; set; }

        public double WeakCoverageRate { get; set; }

        public double OverCoverageRate { get; set; }
    }
}