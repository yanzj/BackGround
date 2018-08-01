using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Dependency;
using Lte.Domain.Common.Wireless;

namespace Lte.Parameters.Entities.Kpi
{
    [AutoMapFrom(typeof(MrsSinrUlStat))]
    public class TownMrsSinrUlDto : ICityDistrictTown, IStatDate, ITownId
    {
        public string District { get; set; }

        public string Town { get; set; }

        public string City { get; set; }

        public DateTime StatDate { get; set; }

        public long SinrUlBelowM9 { get; set; }

        public long SinrUlM9ToM6 { get; set; }

        public long SinrUlM6ToM3 { get; set; }

        public long SinrUlM3To0 { get; set; }

        public long SinrUl0To3 { get; set; }

        public long SinrUl3To6 { get; set; }

        public long SinrUl6To9 { get; set; }

        public long SinrUl9To12 { get; set; }

        public long SinrUl12To15 { get; set; }

        public long SinrUl15To18 { get; set; }

        public long SinrUl18To21 { get; set; }

        public long SinrUl21To24 { get; set; }

        public long SinrUlAbove24 { get; set; }

        public int TownId { get; set; }

        public long TotalMrs => SinrUlBelowM9 + SinrUlM9ToM6 + SinrUlM6ToM3 +
                                SinrUlM3To0 + SinrUl0To3 + SinrUl3To6 + SinrUl6To9 + SinrUl9To12 + SinrUl12To15 +
                                SinrUl15To18 + SinrUl18To21 + SinrUl21To24 + SinrUlAbove24;

    }
}
