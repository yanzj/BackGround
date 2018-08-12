using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.Maintainence;
using Lte.Domain.Common.Wireless;

namespace Abp.EntityFramework.Entities.Kpi
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