using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.Kpi;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Regular.Attributes;

namespace Lte.MySqlFramework.Entities.Kpi
{
    [AutoMapFrom(typeof(FlowHuawei), typeof(FlowZte))]
    public class ENodebFlowView : IENodebId, IGeoPoint<double>
    {
        [ArraySumProtection]
        public int ENodebId { get; set; }

        [ArraySumProtection]
        public double Longtitute { get; set; }

        [ArraySumProtection]
        public double Lattitute { get; set; }

        [AutoMapPropertyResolve("DownlinkPdcpFlow", typeof(FlowZte))]
        [MemberDoc("PDCP����������")]
        public double PdcpDownlinkFlow { get; set; }

        [AutoMapPropertyResolve("UplindPdcpFlow", typeof(FlowZte))]
        [MemberDoc("PDCP����������")]
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

        public double DownlinkFeelingRate
            => DownlinkFeelingDuration == 0 ? 0 : DownlinkFeelingThroughput / DownlinkFeelingDuration;

        public double UplinkFeelingThroughput { get; set; }

        public double UplinkFeelingDuration { get; set; }

        public double UplinkFeelingRate
            => UplinkFeelingDuration == 0 ? 0 : UplinkFeelingThroughput / UplinkFeelingDuration;

    }
}