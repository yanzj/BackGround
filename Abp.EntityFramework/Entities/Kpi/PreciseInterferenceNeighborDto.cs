using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.Maintainence;
using Lte.Domain.Common.Wireless;

namespace Abp.EntityFramework.Entities.Kpi
{
    [AutoMapTo(typeof(PreciseWorkItemCell))]
    public class PreciseInterferenceNeighborDto : ILteCellQuery
    {
        public int ENodebId { get; set; }

        public byte SectorId { get; set; }

        public double Db6Share { get; set; }

        public double Db10Share { get; set; }

        public double Mod3Share { get; set; }

        public double Mod6Share { get; set; }
    }
}