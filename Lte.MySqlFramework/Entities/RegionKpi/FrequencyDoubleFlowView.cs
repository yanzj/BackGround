using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Entities.RegionKpi;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Cell;
using Lte.Domain.Regular.Attributes;

namespace Lte.MySqlFramework.Entities.RegionKpi
{
    [AutoMapFrom(typeof(TownDoubleFlow))]
    [TypeDoc("频段PRB利用率统计")]
    public class FrequencyDoubleFlowView : IStatDate, IFrequencyBand
    {
        [AutoMapPropertyResolve("StatTime", typeof(TownPrbStat))]
        public DateTime StatDate { get; set; }

        [ArraySumProtection]
        public FrequencyBandType FrequencyBandType { get; set; }

        public string FrequencyBand => FrequencyBandType.GetBandDescription();
        
        public string Frequency { get; set; }
        
        public long CloseLoopRank1Prbs { get; set; }

        public long CloseLoopRank2Prbs { get; set; }

        public long OpenLoopRank1Prbs { get; set; }

        public long OpenLoopRank2Prbs { get; set; }

        public long TotalPrbs => CloseLoopRank1Prbs + CloseLoopRank2Prbs + OpenLoopRank1Prbs + OpenLoopRank2Prbs;

        public long Rank2Prbs => CloseLoopRank2Prbs + OpenLoopRank2Prbs;

        public double DoubleFlowRate => TotalPrbs == 0 ? 0 : (double) Rank2Prbs / TotalPrbs;
    }
}
