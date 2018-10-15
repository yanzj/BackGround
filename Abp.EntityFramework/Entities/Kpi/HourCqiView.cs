using System;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Regular;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities.Kpi
{
    [AutoMapFrom(typeof(HourCqi))]
    [IncreaseNumberKpi(KpiPrefix = "Cqi", KpiAffix = "Times", IndexRange = 16)]
    [TypeDoc("集团网管定义忙时CQI优良率统计")]
    public class HourCqiView : IStatTime, ILteCellQuery, IENodebName, ICityDistrictTown
    {
        public DateTime StatTime { get; set; }

        public int ENodebId { get; set; }

        public byte SectorId { get; set; }

        public string ENodebName { get; set; }

        public string District { get; set; }

        public string Town { get; set; }

        public string City { get; set; }

        [MemberDoc("12.2 CQI0上报数量(次)")]
        public int Cqi0Times { get; set; }
        
        [MemberDoc("12.2 CQI1上报数量(次)")]
        public int Cqi1Times { get; set; }

        [MemberDoc("12.2 CQI2上报数量(次)")]
        public int Cqi2Times { get; set; }

        [MemberDoc("12.2 CQI3上报数量(次)")]
        public int Cqi3Times { get; set; }

        [MemberDoc("12.2 CQI4上报数量(次)")]
        public int Cqi4Times { get; set; }

        [MemberDoc("12.2 CQI5上报数量(次)")]
        public int Cqi5Times { get; set; }

        [MemberDoc("12.2 CQI6上报数量(次)")]
        public int Cqi6Times { get; set; }

        [MemberDoc("12.2 CQI7上报数量(次)")]
        public int Cqi7Times { get; set; }

        [MemberDoc("12.2 CQI8上报数量(次)")]
        public int Cqi8Times { get; set; }

        [MemberDoc("12.2 CQI9上报数量(次)")]
        public int Cqi9Times { get; set; }

        [MemberDoc("12.2 CQI10上报数量(次)")]
        public int Cqi10Times { get; set; }

        [MemberDoc("12.2 CQI11上报数量(次)")]
        public int Cqi11Times { get; set; }

        [MemberDoc("12.2 CQI12上报数量(次)")]
        public int Cqi12Times { get; set; }

        [MemberDoc("12.2 CQI13上报数量(次)")]
        public int Cqi13Times { get; set; }

        [MemberDoc("12.2 CQI14上报数量(次)")]
        public int Cqi14Times { get; set; }

        [MemberDoc("12.2 CQI15上报数量(次)")]
        public int Cqi15Times { get; set; }
        
        public Tuple<int, int> CqiCounts => this.GetDivsionIntTuple(7);

        public double CqiRate => (double)CqiCounts.Item2 * 100 / (CqiCounts.Item1 + CqiCounts.Item2);

        [MemberDoc("12.1 下行实际占用的PRB总数(个)")]
        public long TotalPrbs { get; set; }

        [MemberDoc("12.1 下行双流模式调度的PRB总数(个)")]
        public int DoubleFlowPrbs { get; set; }

        public double DoubleFlowRate => TotalPrbs == 0 ? 0 : (double) DoubleFlowPrbs / TotalPrbs * 100;
    }
}
