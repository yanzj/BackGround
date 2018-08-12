using Abp.Domain.Entities;
using Lte.Domain.Common.Geo;

namespace Abp.EntityFramework.Entities.Mr
{
    public class GridCluster : Entity, IGeoGridPoint<double>
    {
        public string Theme { get; set; }

        public int ClusterNumber { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public double Longtitute => 112 + X * 0.00049;

        public double Lattitute => 22 + Y * 0.00045;
    }
}