using System;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;

namespace Abp.EntityFramework.Entities.RegionKpi
{
    [AutoMapFrom(typeof(TownDoubleFlow))]
    public class TownDoubleFlowView : ICityDistrictTown, IStatTime
    {
        public string District { get; set; }

        public string Town { get; set; }

        public string City { get; set; }

        public DateTime StatTime { get; set; }

        public long CloseLoopRank1Prbs { get; set; }

        public long CloseLoopRank2Prbs { get; set; }

        public long OpenLoopRank1Prbs { get; set; }

        public long OpenLoopRank2Prbs { get; set; }

        public long TotalPrbs => CloseLoopRank1Prbs + CloseLoopRank2Prbs + OpenLoopRank1Prbs + OpenLoopRank2Prbs;

        public long Rank2Prbs => CloseLoopRank2Prbs + OpenLoopRank2Prbs;

        public double DoubleFlowRate => TotalPrbs == 0 ? 0 : (double) Rank2Prbs / TotalPrbs;
    }
}
