using System;
using Lte.Domain.Common.Wireless.Alarm;
using Lte.Domain.LinqToCsv;

namespace Lte.Domain.Common.Wireless.Work
{
    public class AlarmStatHuawei
    {
        [CsvColumn(Name = "级别")]
        public string AlarmLevelDescription { get; set; }

        public AlarmCategory AlarmCategory => AlarmCategory.Huawei;

        [CsvColumn(Name = "告警源")]
        public string NetworkItem { get; set; }

        [CsvColumn(Name = "MO对象")]
        public string Position { get; set; }

        [CsvColumn(Name = "名称")]
        public string AlarmCodeDescription { get; set; }

        [CsvColumn(Name = "最近发生时间(NT)")]
        public DateTime RecentHappenTime { get; set; }

        [CsvColumn(Name = "发生时间(NT)")]
        public DateTime NewHappenTime { get; set; }

        public DateTime HappenTime => RecentHappenTime > NewHappenTime ? RecentHappenTime : NewHappenTime;

        [CsvColumn(Name = "用户自定义标识")]
        public string Cause { get; set; }

        [CsvColumn(Name = "定位信息")]
        public string Details { get; set; }

        [CsvColumn(Name = "清除时间(NT)")]
        public string RecoverTime { get; set; }

        [CsvColumn(Name = "eNodeB ID", CanBeNull = true)]
        public string ENodebIdString { get; set; }

        [CsvColumn(Name = "告警ID", CanBeNull = true)]
        public int AlarmId { get; set; }
    }
}