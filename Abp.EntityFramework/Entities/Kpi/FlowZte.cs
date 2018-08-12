using System;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;

namespace Abp.EntityFramework.Entities.Kpi
{
    [AutoMapFrom(typeof(FlowZteCsv))]
    public class FlowZte : Entity, ILteCellQuery, IStatTime
    {
        public DateTime StatTime { get; set; }

        public int ENodebId { get; set; }

        public byte SectorId { get; set; }

        public int MaxRrcUsers { get; set; }

        public double UplinkAverageActiveUsers { get; set; }

        public double DownlinkAverageActiveUsers { get; set; }

        public double AverageRrcUsers { get; set; }

        public double AverageActiveUsers { get; set; }

        public int MaxActiveUsers { get; set; }

        public int PdcpUplinkDuration { get; set; }

        public int PdcpDownlinkDuration { get; set; }

        [AutoMapPropertyResolve("UplindPdcpFlowInMByte", typeof(FlowZteCsv), typeof(ByteTransform))]
        public double UplindPdcpFlow { get; set; }

        [AutoMapPropertyResolve("DownlinkPdcpFlowInMByte", typeof(FlowZteCsv), typeof(ByteTransform))]
        public double DownlinkPdcpFlow { get; set; }

        public double Qci8UplinkIpThroughput { get; set; }

        public double Qci8UplinkIpDuration { get; set; }

        public double Qci9UplinkIpThroughput { get; set; }

        public double Qci9UplinkIpDuration { get; set; }

        public double Qci8DownlinkIpThroughput { get; set; }

        public double Qci8DownlinkIpDuration { get; set; }

        public double Qci9DownlinkIpThroughput { get; set; }

        public double Qci9DownlinkIpDuration { get; set; }

        public int SchedulingTm3 { get; set; }

        public int SchedulingTm3Rank2 { get; set; }

        public int RedirectA2 { get; set; }
        
        public int RedirectB2 { get; set; }

        public int RedirectCdma2000 => RedirectA2 + RedirectB2;

        public double DownlinkFeelingThroughput => Qci8DownlinkIpThroughput + Qci9DownlinkIpThroughput;

        public double DownlinkFeelingDuration => Qci8DownlinkIpDuration + Qci9DownlinkIpDuration;

        public double UplinkFeelingThroughput => Qci8UplinkIpThroughput + Qci9UplinkIpThroughput;

        public double UplinkFeelingDuration => Qci8UplinkIpDuration + Qci9UplinkIpDuration;
    }
}
