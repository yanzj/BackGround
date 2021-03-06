using System;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Entities.Kpi;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Common.Wireless.Cell;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities.RegionKpi
{
    [AutoMapFrom(typeof(PrbHuawei), typeof(PrbZte), typeof(PrbView))]
    public class TownPrbStat : Entity, ITownId, IStatTime, IFrequency
    {
        public int TownId { get; set; }

        public DateTime StatTime { get; set; }

        [ArraySumProtection]
        public FrequencyBandType FrequencyBandType { get; set; } = FrequencyBandType.All;

        public string Frequency => FrequencyBandType.ToString();

        public double PdschPrbs { get; set; }

        [AutoMapPropertyResolve("DownlinkDtchPrbs", typeof(PrbZte))]
        public double DownlinkDtchPrbNumber { get; set; }

        public double PuschPrbs { get; set; }

        [AutoMapPropertyResolve("UplinkDtchPrbs", typeof(PrbZte))]
        public double UplinkDtchPrbNumber { get; set; }

        public int DownlinkPrbSubframe { get; set; }

        public int UplinkPrbSubframe { get; set; }

        public double PdschUsageInterval0Seconds { get; set; }

        public double PdschUsageInterval10Seconds { get; set; }

        public double PdschUsageInterval20Seconds { get; set; }

        public double PdschUsageInterval30Seconds { get; set; }

        public double PdschUsageInterval40Seconds { get; set; }

        public double PdschUsageInterval50Seconds { get; set; }

        public double PdschUsageInterval60Seconds { get; set; }

        public double PdschUsageInterval70Seconds { get; set; }

        public double PdschUsageInterval80Seconds { get; set; }

        public double PdschUsageInterval90Seconds { get; set; }

        public double PuschUsageInterval0Seconds { get; set; }

        public double PuschUsageInterval10Seconds { get; set; }

        public double PuschUsageInterval20Seconds { get; set; }

        public double PuschUsageInterval30Seconds { get; set; }

        public double PuschUsageInterval40Seconds { get; set; }

        public double PuschUsageInterval50Seconds { get; set; }

        public double PuschUsageInterval60Seconds { get; set; }

        public double PuschUsageInterval70Seconds { get; set; }

        public double PuschUsageInterval80Seconds { get; set; }

        public double PuschUsageInterval90Seconds { get; set; }
    }
}