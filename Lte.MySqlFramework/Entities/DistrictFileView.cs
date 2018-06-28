using System.Collections.Generic;

namespace Lte.MySqlFramework.Entities
{
    public class DistrictFileView
    {
        public string District { get; set; }

        public double Distance { get; set; }

        public double DistanceInMeter => Distance * 1000;

        public int Count { get; set; }

        public int CoverageCount { get; set; }

        public double CoverageRate => Count == 0 ? 0 : 100 * (double)CoverageCount / Count;

        public IEnumerable<TownAreaTestFileView> TownViews { get; set; }
    }
}