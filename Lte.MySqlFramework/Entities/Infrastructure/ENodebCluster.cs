using System.Collections.Generic;

namespace Lte.MySqlFramework.Entities.Infrastructure
{
    public class ENodebCluster
    {
        public int LongtituteGrid { get; set; }

        public double Longtitute => (double) LongtituteGrid / 100000;

        public int LattituteGrid { get; set; }

        public double Lattitute => (double) LattituteGrid / 100000;

        public IEnumerable<ENodebView> ENodebViews { get; set; }
    }
}