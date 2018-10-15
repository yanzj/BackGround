using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.RegionKpi;
using Lte.Domain.Common.Types;
using Lte.Domain.Regular;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities.Kpi
{
    [AutoMapFrom(typeof(CqiView), typeof(TownCqiStat))]
    [IncreaseNumberKpi(KpiPrefix = "Cqi", KpiAffix = "Reports", IndexRange = 16)]
    [TypeDoc("聚合CQI优良比统计视图")]
    public class AggregateCqiView : IStatTime
    {
        [MemberDoc("小区个数")]
        public int CellCount { get; set; }

        public string Name { get; set; }

        public DateTime StatTime { get; set; }
        
        public long Cqi0Reports { get; set; }

        public long Cqi1Reports { get; set; }

        public long Cqi2Reports { get; set; }

        public long Cqi3Reports { get; set; }

        public long Cqi4Reports { get; set; }

        public long Cqi5Reports { get; set; }

        public long Cqi6Reports { get; set; }

        public long Cqi7Reports { get; set; }

        public long Cqi8Reports { get; set; }

        public long Cqi9Reports { get; set; }

        public long Cqi10Reports { get; set; }

        public long Cqi11Reports { get; set; }

        public long Cqi12Reports { get; set; }

        public long Cqi13Reports { get; set; }

        public long Cqi14Reports { get; set; }

        public long Cqi15Reports { get; set; }
        
        public Tuple<long, long> CqiCounts => this.GetDivsionLongTuple(7);

        public double CqiRate => (double)CqiCounts.Item2 * 100 / (CqiCounts.Item1 + CqiCounts.Item2);
    }
}
