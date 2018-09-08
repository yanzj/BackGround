using Lte.Domain.Regular.Attributes;

namespace Lte.Domain.Common.Wireless
{
    [TypeDoc("区域内LTE小区频段统计")]
    public class DistrictBandClassStat
    {
        [MemberDoc("区")]
        public string District { get; set; }

        [MemberDoc("2.1G频段小区总数")]
        public int Band1Cells { get; set; }

        [MemberDoc("1.8G频段小区总数")]
        public int Band3Cells { get; set; }

        [MemberDoc("800M频段（非NB-IoT）小区总数")]
        public int Band5Cells { get; set; }

        [MemberDoc("800M频段（NB-IoT）小区总数")]
        public int NbIotCells { get; set; }

        [MemberDoc("TDD频段小区总数")]
        public int Band41Cells { get; set; }
    }
}