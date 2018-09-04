using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Entities.Kpi;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities.RegionKpi
{
    [AutoMapFrom(typeof(HourCqi))]
    [TypeDoc("镇区忙时CQI优良率统计")]
    public class TownHourCqi : Entity, ITownId, IStatDate
    {
        [ArraySumProtection]
        public int TownId { get; set; }

        [ArraySumProtection]
        public DateTime StatDate { get; set; }
        
        [MemberDoc("12.2 CQI0上报数量(次)")]
        public long Cqi0Times { get; set; }
        
        [MemberDoc("12.2 CQI1上报数量(次)")]
        public long Cqi1Times { get; set; }

        [MemberDoc("12.2 CQI2上报数量(次)")]
        public long Cqi2Times { get; set; }

        [MemberDoc("12.2 CQI3上报数量(次)")]
        public long Cqi3Times { get; set; }

        [MemberDoc("12.2 CQI4上报数量(次)")]
        public long Cqi4Times { get; set; }

        [MemberDoc("12.2 CQI5上报数量(次)")]
        public long Cqi5Times { get; set; }

        [MemberDoc("12.2 CQI6上报数量(次)")]
        public long Cqi6Times { get; set; }

        [MemberDoc("12.2 CQI7上报数量(次)")]
        public long Cqi7Times { get; set; }

        [MemberDoc("12.2 CQI8上报数量(次)")]
        public long Cqi8Times { get; set; }

        [MemberDoc("12.2 CQI9上报数量(次)")]
        public long Cqi9Times { get; set; }

        [MemberDoc("12.2 CQI10上报数量(次)")]
        public long Cqi10Times { get; set; }

        [MemberDoc("12.2 CQI11上报数量(次)")]
        public long Cqi11Times { get; set; }

        [MemberDoc("12.2 CQI12上报数量(次)")]
        public long Cqi12Times { get; set; }

        [MemberDoc("12.2 CQI13上报数量(次)")]
        public long Cqi13Times { get; set; }

        [MemberDoc("12.2 CQI14上报数量(次)")]
        public long Cqi14Times { get; set; }

        [MemberDoc("12.2 CQI15上报数量(次)")]
        public long Cqi15Times { get; set; }
        
        [MemberDoc("12.1 下行实际占用的PRB总数(个)")]
        public double TotalPrbs { get; set; }

        [MemberDoc("12.1 下行双流模式调度的PRB总数(个)")]
        public long DoubleFlowPrbs { get; set; }
    }
}
