using System;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;

namespace Abp.EntityFramework.Entities.Kpi
{
    [AutoMapFrom(typeof(DoubleFlowHuawei), typeof(DoubleFlowZte))]
    public class DoubleFlowView : IStatTime, ILteCellQuery, IENodebName, ICityDistrictTown
    {
        public DateTime StatTime { get; set; }

        public int ENodebId { get; set; }

        public byte SectorId { get; set; }

        public string ENodebName { get; set; }

        public string District { get; set; }

        public string Town { get; set; }

        public string City { get; set; }

        public long CloseLoopRank1Prbs { get; set; }

        public long CloseLoopRank2Prbs { get; set; }

        public long OpenLoopRank1Prbs { get; set; }

        public long OpenLoopRank2Prbs { get; set; }

        public long TotalPrbs => CloseLoopRank1Prbs + CloseLoopRank2Prbs + OpenLoopRank2Prbs + OpenLoopRank1Prbs;

        public long Rank1Prbs => CloseLoopRank1Prbs + OpenLoopRank1Prbs;

        public long CloseLoopPrbs => CloseLoopRank1Prbs + CloseLoopRank2Prbs;

        public double CloseLoopDoubleFlowRate
            => CloseLoopPrbs == 0 ? 0 : (double)CloseLoopRank2Prbs / CloseLoopPrbs;

        public long OpenLoopPrbs => OpenLoopRank1Prbs + OpenLoopRank2Prbs;

        public double OpenLoopDoubleFlowRate
            => OpenLoopPrbs == 0 ? 0 : (double) OpenLoopRank2Prbs / OpenLoopPrbs;

        public double DoubleFlowRate
            => TotalPrbs == 0 ? 0 : (double) (CloseLoopRank2Prbs + OpenLoopRank2Prbs) / TotalPrbs;
    }
}
