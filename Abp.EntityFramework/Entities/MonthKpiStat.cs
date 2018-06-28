using System;
using Abp.Domain.Entities;
using Abp.EntityFramework.Dependency;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities
{
    public class MonthKpiStat : Entity, IStatDate
    {
        [MemberDoc("统计日期")]
        public DateTime StatDate { get; set; }

        public int Year => StatDate.Year;

        public int Month => StatDate.Month;

        [MemberDoc("区域")]
        public string District { get; set; }

        [MemberDoc("无线故障工单接单及时率")]
        public double AlarmWorkItemRate { get; set; }

        [MemberDoc("无线设备故障处理及时率")]
        public double AlarmProcessRate { get; set; }

        [MemberDoc("无线维护作业计划工单完成及时率")]
        public double MaintainProjectRate { get; set; }
        
        [MemberDoc("无线百网元故障工单")]
        public double AverageAlarms { get; set; }

        [MemberDoc("无线月均抱怨率")]
        public int TotalComplains { get; set; }
        
        [MemberDoc("无线公众客户越级投/申诉率")]
        public int YuejiComplains { get; set; }

        public int GongxinYuejiComplains { get; set; }

        [MemberDoc("4G精确覆盖率")]
        public double PreciseRate { get; set; }

        [MemberDoc("2G掉话率")]
        public double Drop2GRate { get; set; }

        [MemberDoc("4G用户3G流量比")]
        public double FlowRate4GTo3G { get; set; }
    }
}