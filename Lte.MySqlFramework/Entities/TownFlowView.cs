using System;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Kpi;
using Abp.EntityFramework.Entities.RegionKpi;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Regular.Attributes;

namespace Lte.MySqlFramework.Entities
{
    [AutoMapFrom(typeof(TownFlowStat))]
    public class TownFlowView : ICityDistrictTown, IStatTime
    {
        public string District { get; set; }

        public string Town { get; set; }

        public string City { get; set; }

        public DateTime StatTime { get; set; }

        public string Frequency { get; set; }

        public double PdcpDownlinkFlow { get; set; }

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
            => DownlinkFeelingDuration == 0 ? 0 : (DownlinkFeelingThroughput / DownlinkFeelingDuration);

        public double UplinkFeelingThroughput { get; set; }

        public double UplinkFeelingDuration { get; set; }

        public double UplinkFeelingRate
            => UplinkFeelingDuration == 0 ? 0 : (UplinkFeelingThroughput / UplinkFeelingDuration);

        public double SchedulingRank2 { get; set; }

        public double SchedulingTimes { get; set; }

        public double Rank2Rate => SchedulingTimes == 0 ? 100 : (SchedulingRank2 / SchedulingTimes * 100);

        public int RedirectCdma2000 { get; set; }

        public double DownSwitchRate
            =>
                PdcpUplinkFlow + PdcpDownlinkFlow == 0
                    ? 100
                    : (8 * (double)RedirectCdma2000 / (PdcpUplinkFlow/1024 + PdcpDownlinkFlow/1024));
    }
}