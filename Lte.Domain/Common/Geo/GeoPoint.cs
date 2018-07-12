using System.Collections.Generic;
using System.Linq;
using Lte.Domain.Regular.Attributes;

namespace Lte.Domain.Common.Geo
{
    [TypeDoc("经纬度地理点")]
    public class GeoPoint : IGeoPoint<double>, IGeoPointReadonly<double>
    {
        public GeoPoint(double x, double y)
        {
            Longtitute = x;
            Lattitute = y;
        }

        [MemberDoc("经度")]
        public double Longtitute { get; set; }

        [MemberDoc("纬度")]
        public double Lattitute { get; set; }

        public GeoPoint() { }

        public GeoPoint(IEnumerable<IGeoPoint<double>> pointList)
        {
            var geoPoints = pointList as IGeoPoint<double>[] ?? pointList.ToArray();
            Longtitute = geoPoints.Select(x => x.Longtitute).Average();
            Lattitute = geoPoints.Select(x => x.Lattitute).Average();
        }

        public GeoPoint(IGeoPoint<double> center, double longtituteOffset, double lattituteOffset)
        {
            Longtitute = center.Longtitute + longtituteOffset;
            Lattitute = center.Lattitute + lattituteOffset;
        }
    }
}
