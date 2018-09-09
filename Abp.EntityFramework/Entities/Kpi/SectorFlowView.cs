using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.Infrastructure;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities.Kpi
{
    [TypeDoc("扇区流量视图，用于地理化显示")]
    [AutoMapFrom(typeof(SectorView), typeof(FlowView))]
    public class SectorFlowView : ILteCellQuery, IGeoPoint<double>, IStatTime
    {
        [MemberDoc("小区名称，用于辨析小区")]
        public string CellName { get; set; }

        [MemberDoc("基站编号")]
        public int ENodebId { get; set; }

        [MemberDoc("扇区编号")]
        public byte SectorId { get; set; }

        [MemberDoc("是否为室内小区")]
        public string Indoor { get; set; }

        [MemberDoc("方位角")]
        public double Azimuth { get; set; }

        [MemberDoc("经度")]
        public double Longtitute { get; set; }

        [MemberDoc("纬度")]
        public double Lattitute { get; set; }

        [MemberDoc("天线挂高")]
        public double Height { get; set; }

        [MemberDoc("下倾角")]
        public double DownTilt { get; set; }

        [MemberDoc("天线增益")]
        public double AntennaGain { get; set; }

        [MemberDoc("频点")]
        public int Frequency { get; set; }

        [MemberDoc("频带编号，如1表示2.1G，3表示1.8G，5表示800M")]
        public byte BandClass { get; set; }
        
        [MemberDoc("统计时间")]
        [ArraySumProtection]
        public DateTime StatTime { get; set; }
        
        [ArraySumProtection]
        public string ENodebName { get; set; }
        
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

    }
}
