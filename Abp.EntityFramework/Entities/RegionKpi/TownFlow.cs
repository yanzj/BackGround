using System;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.Kpi;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Common.Wireless.Cell;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities.RegionKpi
{
    [AutoMapFrom(typeof(FlowHuawei), typeof(FlowZte), typeof(FlowView))]
    public class TownFlowStat : Entity, ITownId, IStatTime
    {
        [ArraySumProtection]
        public int TownId { get; set; }

        [ArraySumProtection]
        public DateTime StatTime { get; set; }

        [ArraySumProtection]
        public FrequencyBandType FrequencyBandType { get; set; } = FrequencyBandType.All;

        public string Frequency => FrequencyBandType.ToString();

        [AutoMapPropertyResolve("DownlinkPdcpFlow", typeof(FlowZte))]
        public double PdcpDownlinkFlow { get; set; }

        [AutoMapPropertyResolve("UplindPdcpFlow", typeof(FlowZte))]
        public double PdcpUplinkFlow { get; set; }

        [AutoMapPropertyResolve("AverageRrcUsers", typeof(FlowZte))]
        [MemberDoc("ƽ���û���")]
        public double AverageUsers { get; set; }

        [AutoMapPropertyResolve("MaxRrcUsers", typeof(FlowZte))]
        [MemberDoc("����û���")]
        public int MaxUsers { get; set; }

        [MemberDoc("ƽ�������û���")]
        public double AverageActiveUsers { get; set; }

        [MemberDoc("��󼤻��û���")]
        public int MaxActiveUsers { get; set; }

        public double DownlinkFeelingThroughput { get; set; }

        public double DownlinkFeelingDuration { get; set; }
        
        public double UplinkFeelingThroughput { get; set; }

        public double UplinkFeelingDuration { get; set; }

        [AutoMapPropertyResolve("SchedulingTm3Rank2", typeof(FlowZte))]
        public double SchedulingRank2 { get; set; }

        [AutoMapPropertyResolve("SchedulingTm3", typeof(FlowZte))]
        public double SchedulingTimes { get; set; }
        
        public int RedirectCdma2000 { get; set; }
    }
}