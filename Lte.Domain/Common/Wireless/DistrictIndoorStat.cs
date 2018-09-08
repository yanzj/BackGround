using Lte.Domain.Regular.Attributes;

namespace Lte.Domain.Common.Wireless
{
    [TypeDoc("区域内LTE室内外小区统计")]
    public class DistrictIndoorStat
    {
        [MemberDoc("区")]
        public string District { get; set; }

        [MemberDoc("室内小区总数")]
        public int TotalIndoorCells { get; set; }

        [MemberDoc("室外小区总数")]
        public int TotalOutdoorCells { get; set; }
    }
}