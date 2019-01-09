using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.RegionKpi;
using Lte.Domain.Common.Types;
using Lte.Domain.Regular.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abp.EntityFramework.Entities.Kpi
{
    [AutoMapFrom(typeof(DoubleFlowView), typeof(TownDoubleFlow))]
    [TypeDoc("聚合双流比统计视图")]
    public class AggregateDoubleFlowView : IStatTime
    {
        [MemberDoc("小区个数")]
        public int CellCount { get; set; }

        public string Name { get; set; }

        public DateTime StatTime { get; set; }

        public long CloseLoopRank1Prbs { get; set; }

        public long CloseLoopRank2Prbs { get; set; }

        public long OpenLoopRank1Prbs { get; set; }

        public long OpenLoopRank2Prbs { get; set; }

        public long TotalPrbs => CloseLoopRank1Prbs + CloseLoopRank2Prbs + OpenLoopRank1Prbs + OpenLoopRank2Prbs;

        public long Rank2Prbs => CloseLoopRank2Prbs + OpenLoopRank2Prbs;

        public double DoubleFlowRate => TotalPrbs == 0 ? 0 : (double)Rank2Prbs / TotalPrbs;
    }
}
