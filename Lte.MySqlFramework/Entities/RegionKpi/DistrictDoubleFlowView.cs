using System;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.RegionKpi;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;

namespace Lte.MySqlFramework.Entities.RegionKpi
{
    [AutoMapFrom(typeof(TownDoubleFlowView))]
    public class DistrictDoubleFlowView : ICityDistrict, IStatTime
    {
        public string City { get; set; }

        public string District { get; set; }

        public DateTime StatTime { get; set; }

        public long CloseLoopRank1Prbs { get; set; }

        public long CloseLoopRank2Prbs { get; set; }

        public long OpenLoopRank1Prbs { get; set; }

        public long OpenLoopRank2Prbs { get; set; }

        public long TotalPrbs => CloseLoopRank1Prbs + CloseLoopRank2Prbs + OpenLoopRank1Prbs + OpenLoopRank2Prbs;

        public long Rank2Prbs => CloseLoopRank2Prbs + OpenLoopRank2Prbs;

        public double DoubleFlowRate => TotalPrbs == 0 ? 0 : (double)Rank2Prbs / TotalPrbs;

        public static DistrictDoubleFlowView ConstructView(TownDoubleFlowView townView)
        {
            return townView.MapTo<DistrictDoubleFlowView>();
        }
    }
}
