using System;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.RegionKpi;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Regular;
using Lte.Domain.Regular.Attributes;

namespace Lte.MySqlFramework.Entities.RegionKpi
{
    [AutoMapFrom(typeof(TownQciStat))]
    [IncreaseNumberKpi(KpiPrefix = "Cqi", KpiAffix = "Times", IndexRange = 16)]
    public class TownQciView : ICityDistrictTown, IStatTime
    {
        public string District { get; set; }

        public string Town { get; set; }

        public string City { get; set; }

        public DateTime StatTime { get; set; }

        public long Cqi0Times { get; set; }

        public long Cqi1Times { get; set; }

        public long Cqi2Times { get; set; }

        public long Cqi3Times { get; set; }

        public long Cqi4Times { get; set; }

        public long Cqi5Times { get; set; }

        public long Cqi6Times { get; set; }

        public long Cqi7Times { get; set; }

        public long Cqi8Times { get; set; }

        public long Cqi9Times { get; set; }

        public long Cqi10Times { get; set; }

        public long Cqi11Times { get; set; }

        public long Cqi12Times { get; set; }

        public long Cqi13Times { get; set; }

        public long Cqi14Times { get; set; }

        public long Cqi15Times { get; set; }

        public Tuple<long, long> CqiCounts => this.GetDivsionLongTuple(7);

        public double CqiRate => (double)CqiCounts.Item2 * 100 / (CqiCounts.Item1 + CqiCounts.Item2);
    }
}