using Lte.Domain.Regular.Attributes;

namespace Lte.Domain.Common.Wireless
{
    public class LteCellStat
    {
        [MemberDoc("1.8G小区数")]
        public int Lte1800Cells { get; set; }

        [MemberDoc("2.1G小区数")]
        public int Lte2100Cells { get; set; }

        [MemberDoc("800M 小区数（不含NB-IoT）")]
        public int Lte800Cells { get; set; }

        public int Lte2600Cells { get; set; }

        [MemberDoc("NB-IoT小区总数")]
        public int TotalNbIotCells { get; set; }
    }
}