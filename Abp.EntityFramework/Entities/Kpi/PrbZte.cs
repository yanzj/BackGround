using System;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;

namespace Abp.EntityFramework.Entities.Kpi
{
    [AutoMapFrom(typeof(FlowZteCsv))]
    public class PrbZte : Entity, ILteCellQuery, IStatTime
    {
        public int ENodebId { get; set; }

        public byte SectorId { get; set; }

        public DateTime StatTime { get; set; }

        public double PuschPrbs { get; set; }

        public int UplinkPrbSubframe { get; set; }

        public double PdschPrbs { get; set; }
        
        public int DownlinkPrbSubframe { get; set; }

        public double UplinkDtchPrbs { get; set; }

        public double DownlinkDtchPrbs { get; set; }

        public double PuschUsageInterval0Seconds { get; set; }

        public double PuschUsageInterval20Seconds { get; set; }

        public double PuschUsageInterval40Seconds { get; set; }

        public double PuschUsageInterval60Seconds { get; set; }

        public double PuschUsageInterval80Seconds { get; set; }
        
        public double PdschUsageInterval0Seconds { get; set; }
        
        public double PdschUsageInterval20Seconds { get; set; }

        public double PdschUsageInterval40Seconds { get; set; }

        public double PdschUsageInterval60Seconds { get; set; }

        public double PdschUsageInterval80Seconds { get; set; }
    }
}