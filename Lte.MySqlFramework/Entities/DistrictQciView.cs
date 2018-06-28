using System;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Regular;
using Lte.Domain.Regular.Attributes;

namespace Lte.MySqlFramework.Entities
{
    [AutoMapFrom(typeof(TownQciView))]
    [IncreaseNumberKpi(KpiPrefix = "Cqi", KpiAffix = "Times", IndexRange = 16)]
    public class DistrictQciView : ICityDistrict, IStatTime
    {
        public string City { get; set; }

        public string District { get; set; }

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

        public double CqiRate => (double) CqiCounts.Item2 * 100 / (CqiCounts.Item1 + CqiCounts.Item2);

        public static DistrictQciView ConstructView(TownQciView townView)
        {
            return townView.MapTo<DistrictQciView>();
        }
    }

    [AutoMapFrom(typeof(TownCqiView))]
    [IncreaseNumberKpi(KpiPrefix = "Cqi", KpiAffix = "Reports", IndexRange = 16)]
    public class DistrictCqiView : ICityDistrict, IStatTime
    {
        public string City { get; set; }

        public string District { get; set; }

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

        public static DistrictCqiView ConstructView(TownCqiView townView)
        {
            return townView.MapTo<DistrictCqiView>();
        }
    }
}