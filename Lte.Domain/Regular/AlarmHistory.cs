using Lte.Domain.Regular.Attributes;

namespace Lte.Domain.Regular
{
    [TypeDoc("告警历史记录")]
    public class AlarmHistory
    {
        [MemberDoc("日期字符串")]
        public string DateString { get; set; }

        [MemberDoc("当日告警数")]
        public int Alarms { get; set; }

        public int CoverageStats { get; set; }

        public int TownCoverageStats { get; set; }

        public int ZhangshangyouQualities { get; set; }

        public int ZhangshangyouCoverages { get; set; }
    }
}