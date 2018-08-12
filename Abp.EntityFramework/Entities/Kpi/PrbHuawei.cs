using System;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;

namespace Abp.EntityFramework.Entities.Kpi
{
    [AutoMapFrom(typeof(FlowHuaweiCsv))]
    public class PrbHuawei : Entity, ILocalCellQuery, IStatTime, ILteCellQuery
    {
        public int ENodebId { get; set; }

        public byte LocalCellId { get; set; }

        public byte SectorId { get; set; }

        public DateTime StatTime { get; set; }

        public double PdschPrbs { get; set; }//

        public double DownlinkDtchPrbNumber { get; set; }//

        public double PuschPrbs { get; set; }

        public double UplinkDtchPrbNumber { get; set; }

        public int DownlinkPrbSubframe { get; set; }//

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