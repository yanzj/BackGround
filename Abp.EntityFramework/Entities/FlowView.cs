using System;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities
{
    [AutoMapFrom(typeof(FlowHuawei), typeof(FlowZte))]
    [TypeDoc("小区单日流量统计视图")]
    public class FlowView : IStatTime, ILteCellQuery, IENodebName, ICityDistrictTown
    {
        [MemberDoc("统计时间")]
        [ArraySumProtection]
        public DateTime StatTime { get; set; }

        [MemberDoc("基站编号")]
        [ArraySumProtection]
        public int ENodebId { get; set; }

        [MemberDoc("扇区编号")]
        [ArraySumProtection]
        public byte SectorId { get; set; }

        [ArraySumProtection]
        public string ENodebName { get; set; }

        [AutoMapPropertyResolve("DownlinkPdcpFlow", typeof(FlowZte))]
        [MemberDoc("PDCP层下行流量")]
        public double PdcpDownlinkFlow { get; set; }

        [AutoMapPropertyResolve("UplindPdcpFlow", typeof(FlowZte))]
        [MemberDoc("PDCP层上行流量")]
        public double PdcpUplinkFlow { get; set; }

        [AutoMapPropertyResolve("AverageRrcUsers", typeof(FlowZte))]
        [MemberDoc("平均用户数")]
        public double AverageUsers { get; set; }

        [AutoMapPropertyResolve("MaxRrcUsers", typeof(FlowZte))]
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

        public double RedirectCdma2000 { get; set; }

        public double DownSwitchRate
            =>
                PdcpUplinkFlow + PdcpDownlinkFlow == 0
                    ? 100
                    : (8 * (double)RedirectCdma2000 / (PdcpUplinkFlow / 1024 + PdcpDownlinkFlow / 1024));

        [AutoMapPropertyResolve("SchedulingTm3", typeof(FlowZte))]
        public double SchedulingTimes { get; set; }

        [AutoMapPropertyResolve("SchedulingTm3Rank2", typeof(FlowZte))]
        public double SchedulingRank2 { get; set; }

        public double Rank2Rate => SchedulingTimes == 0 ? 100 : SchedulingRank2 / SchedulingTimes * 100;

        public string City { get; set; }

        public string District { get; set; }

        public string Town { get; set; }
    }
}
