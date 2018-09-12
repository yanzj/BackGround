using System;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Entities.Channel;
using Lte.Domain.Common.Geo;

namespace Lte.Parameters.Entities.Channel
{
    [AutoMapFrom(typeof(AgpsMongo))]
    public class AgpsCoverageView : IStatDate, IGeoPoint<double>
    {
        public DateTime StatDate { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public double Longtitute { get; set; }

        public double Lattitute { get; set; }

        public int Rsrp { get; set; }

        public double AverageRsrp => (double) Rsrp/Count;

        public int Count { get; set; }

        public int GoodCount { get; set; }

        public double CoverageRate110 => (double) GoodCount/Count;

        public double CoverageRate105 => (double) GoodCount105/Count;

        public double CoverageRate100 => (double) GoodCount100/Count;

        public int GoodCount105 { get; set; }

        public int GoodCount100 { get; set; }
    }
}