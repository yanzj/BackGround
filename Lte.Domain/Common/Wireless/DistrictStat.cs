using Lte.Domain.Common.Geo;
using Lte.Domain.Regular.Attributes;

namespace Lte.Domain.Common.Wireless
{
    [TypeDoc("区域内基站小区统计")]
    public class DistrictStat
    {
        [MemberDoc("区")]
        public string District { get; set; }

        [MemberDoc("LTE基站总数")]
        public int TotalLteENodebs { get; set; }

        [MemberDoc("LTE小区总数")]
        public int TotalLteCells { get; set; }

        [MemberDoc("800M 小区数（不含NB-IoT）")]
        public int Lte800Cells { get; set; }

        public int Lte1800Cells {get; set; }

        public int Lte2100Cells { get; set; }

        public int Lte2600Cells { get; set; }

        [MemberDoc("NB-IoT小区总数")]
        public int TotalNbIotCells { get; set; }

        [MemberDoc("CDMA基站总数")]
        public int TotalCdmaBts { get; set; }

        [MemberDoc("CDMA小区总数")]
        public int TotalCdmaCells { get; set; }
    }
}
