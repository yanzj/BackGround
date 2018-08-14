using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.RegionKpi;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities.Kpi
{
    [AutoMapFrom(typeof(FlowView), typeof(TownFlowStat))]
    [TypeDoc("�ۺ�����ͳ����ͼ")]
    public class AggregateFlowView
    {
        [MemberDoc("С������")]
        public int CellCount { get; set; }

        [MemberDoc("PDCP����������")]
        public double PdcpDownlinkFlow { get; set; }

        [MemberDoc("PDCP����������")]
        public double PdcpUplinkFlow { get; set; }

        [MemberDoc("ƽ���û���")]
        public double AverageUsers { get; set; }

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