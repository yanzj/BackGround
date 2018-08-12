using System;
using Abp.EntityFramework.AutoMapper;

namespace Abp.EntityFramework.Entities.Test
{
    [AutoMapFrom(typeof(AreaTestInfo))]
    public class AreaTestFileView
    {
        public DateTime TestDate { get; set; }

        public string CsvFileName { get; set; }

        public double Distance { get; set; }

        public double DistanceInMeter => Distance * 1000;

        public int Count { get; set; }

        public int CoverageCount { get; set; }

        public double CoverageRate => Count == 0 ? 0 : 100 * (double)CoverageCount / Count;

        public string AreaName { get; set; }
    }
}