using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Regular.Attributes;

namespace Lte.Domain.Regular
{
    [TypeDoc("MRO覆盖历史记录")]
    public class CoverageHistory
    {
        [MemberDoc("日期字符串")]
        public string DateString { get; set; }
        
        public int CoverageStats { get; set; }

        public int TownCoverageStats { get; set; }
        
        public int TownCoverage800 { get; set; }

        public int TownCoverage1800 { get; set; }

        public int TownCoverage2100 { get; set; }

        public int CollegeCoverageStats { get; set; }
        
        public int MarketCoverageStats { get; set; }
        
        public int TransportationCoverageStats { get; set; }

    }
}
