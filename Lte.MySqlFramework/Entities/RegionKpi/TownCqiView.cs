using System;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.RegionKpi;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Regular;
using Lte.Domain.Regular.Attributes;

namespace Lte.MySqlFramework.Entities.RegionKpi
{
    [AutoMapFrom(typeof(TownCqiStat))]
    [IncreaseNumberKpi(KpiPrefix = "Cqi", KpiAffix = "Reports", IndexRange = 16)]
    public class TownCqiView : ICityDistrictTown, IStatTime
    {
        public string District { get; set; }

        public string Town { get; set; }

        public string City { get; set; }

        public DateTime StatTime { get; set; }
        
        public string Frequency { get; set; }

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