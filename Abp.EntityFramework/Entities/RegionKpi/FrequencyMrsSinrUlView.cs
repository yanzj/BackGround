using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Entities.Mr;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Cell;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities.RegionKpi
{
    [AutoMap(typeof(TownMrsSinrUl))]
    public class FrequencyMrsSinrUlView : IStatTime, IFrequencyBand
    {
        [ArraySumProtection]
        public DateTime StatTime { get; set; }
        
        [ArraySumProtection]
        public FrequencyBandType FrequencyBandType { get; set; }
        
        public string FrequencyBand => FrequencyBandType.GetBandDescription();
        
        public long SinrUlBelowM9 { get; set; }

        public long SinrUlM9ToM6 { get; set; }

        public long SinrUlM6ToM3 { get; set; }

        public long SinrUlM3To0 { get; set; }

        public long SinrUl0To3 { get; set; }

        public long SinrUl3To6 { get; set; }

        public long SinrUl6To9 { get; set; }

        public long SinrUl9To12 { get; set; }

        public long SinrUl12To15 { get; set; }

        public long SinrUl15To18 { get; set; }

        public long SinrUl18To21 { get; set; }

        public long SinrUl21To24 { get; set; }

        public long SinrUlAbove24 { get; set; }
        
        public long SinrUlAbove15 => SinrUlAbove24 + SinrUl21To24 + SinrUl18To21 + SinrUl15To18;

        public long SinrUlAbove9 => SinrUlAbove15 + SinrUl12To15 + SinrUl9To12;

        public long SinrUlAbove3 => SinrUlAbove9 + SinrUl6To9 + SinrUl3To6;

        public long SinrUlAbove0 => SinrUlAbove3 + SinrUl0To3;

        public long SinrUlAboveM3 => SinrUlAbove0 + SinrUlM3To0;

        public long TotalMrs => SinrUlAboveM3 + SinrUlM6ToM3 + SinrUlM9ToM6 + SinrUlBelowM9;

        public double CoverageAbove15 => TotalMrs == 0 ? 0 : (double) SinrUlAbove15 / TotalMrs;

        public double CoverageAbove9 => TotalMrs == 0 ? 0 : (double)SinrUlAbove9 / TotalMrs;

        public double CoverageAbove3 => TotalMrs == 0 ? 0 : (double)SinrUlAbove3 / TotalMrs;

        public double CoverageAbove0 => TotalMrs == 0 ? 0 : (double)SinrUlAbove0 / TotalMrs;

        public double CoverageAboveM3 => TotalMrs == 0 ? 0 : (double)SinrUlAboveM3 / TotalMrs;

    }
}
