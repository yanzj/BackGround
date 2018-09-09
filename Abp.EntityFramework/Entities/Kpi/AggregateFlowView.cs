using System;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.RegionKpi;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities.Kpi
{
    [AutoMapFrom(typeof(FlowView), typeof(TownFlowStat))]
    [TypeDoc("聚合流量统计视图")]
    public class AggregateFlowView
    {
        [MemberDoc("小区个数")]
        public int CellCount { get; set; }

        public string Name { get; set; }

        public DateTime StatTime { get; set; }

        [MemberDoc("PDCP层下行流量")]
        public double PdcpDownlinkFlow { get; set; }

        [MemberDoc("PDCP层上行流量")]
        public double PdcpUplinkFlow { get; set; }

        [MemberDoc("平均用户数")]
        public double AverageUsers { get; set; }

        [MemberDoc("最大用户数")]
        public int MaxUsers { get; set; }

        [MemberDoc("平均激活用户数")]
        public double AverageActiveUsers { get; set; }

        [MemberDoc("最大激活用户数")]
        public int MaxActiveUsers { get; set; }

        public double DownlinkFeelingThroughput { get; set; }

        public double DownlinkFeelingDuration { get; set; }

        public double DownlinkFeelingRate
            => DownlinkFeelingDuration == 0 ? 0 : DownlinkFeelingThroughput / DownlinkFeelingDuration;

        public double UplinkFeelingThroughput { get; set; }

        public double UplinkFeelingDuration { get; set; }

        public double UplinkFeelingRate
            => UplinkFeelingDuration == 0 ? 0 : UplinkFeelingThroughput / UplinkFeelingDuration;
    }
}