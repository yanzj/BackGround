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
    [AutoMapFrom(typeof(TownPrbStat))]
    [TypeDoc("频段PRB利用率统计")]
    public class FrequencyPrbView : IStatDate, IFrequencyBand
    {
        [AutoMapPropertyResolve("StatTime", typeof(TownPrbStat))]
        public DateTime StatDate { get; set; }

        [ArraySumProtection]
        public FrequencyBandType FrequencyBandType { get; set; }

        public string FrequencyBand => FrequencyBandType.GetBandDescription();
        
        public string Frequency { get; set; }

        public double PdschPrbs { get; set; }

        public double PdschPrbRate => PdschPrbs / DownlinkPrbSubframe * 100;

        public double DownlinkDtchPrbNumber { get; set; }

        public double PdschDtchPrbRate => DownlinkDtchPrbNumber / DownlinkPrbSubframe * 100;

        public double PuschPrbs { get; set; }

        public double PuschPrbRate => PuschPrbs / UplinkPrbSubframe * 100;

        public double UplinkDtchPrbNumber { get; set; }

        public double PuschDtchPrbRate => UplinkDtchPrbNumber / UplinkPrbSubframe * 100;

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
