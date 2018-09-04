using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Common.Wireless.Kpi;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities.Kpi
{
    [TypeDoc("集团网管定义忙时CQI优良率统计")]
    [AutoMapFrom(typeof(HourCqiCsv))]
    public class HourCqi: Entity, IStatTime, ILteCellQuery
    {
        [MemberDoc("时间")]
        public DateTime StatTime { get; set; }

        [MemberDoc("ENBID")]
        public int ENodebId { get; set; }

        [MemberDoc("小区ID")]
        public byte SectorId { get; set; }

        [MemberDoc("小区标识")]
        public string CellSerialNumber { get; set; }
        
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
        
        [MemberDoc("12.1 下行实际占用的PRB总数(个)")]
        public int TotalPrbs { get; set; }

        [MemberDoc("12.1 下行双流模式调度的PRB总数(个)")]
        public int DoubleFlowPrbs { get; set; }
    }
}
