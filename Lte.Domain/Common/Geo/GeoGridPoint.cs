namespace Lte.Domain.Common.Geo
{
    public class GeoGridPoint : IGeoGridPoint<double>
    {
        public int X { get; set; }

        public int Y { get; set; }

        public double Longtitute => 112 + X * 0.00049;

        public double Lattitute => 22 + Y * 0.00045;
    }
}