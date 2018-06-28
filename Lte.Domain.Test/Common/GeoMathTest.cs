using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common.Geo;
using NUnit.Framework;

namespace Lte.Domain.Test.Common
{
    [TestFixture]
    public class GeoMathTest
    {
        [TestCase(1.1, 1.1, 1, 1, 2, 1, true)]
        [TestCase(1.1, 1.1, 2, 1, 1, 1, false)]
        [TestCase(2.1, 1.1, 1, 1, 2, 1, true)]
        [TestCase(4.1, 1.1, 1, 1, 2, 1, true)]
        [TestCase(0.1, 1.1, 1, 1, 2, 1, true)]
        [TestCase(0.1, 0.9, 1, 1, 2, 1, false)]
        [TestCase(1.1, 0.9, 1, 1, 2, 1, false)]
        [TestCase(1.1, 0.9, 2, 1, 1, 1, true)]
        [TestCase(1.5, 1.5, 1, 1.1, 2, 1, true)]
        [TestCase(1.5, 1.5, 2, 1, 1, 1.1, false)]
        [TestCase(0, 0, 0, 1, -1, -1, true)]
        [TestCase(0, 0, 1, -1, 0, 1, true)]
        public void PointIsAboveTheRayTest(double cX, double cY, double p1X, double p1Y, double p2X, double p2Y,
            bool result)
        {
            var c = new GeoPoint(cX, cY);
            var p1 = new GeoPoint(p1X, p1Y);
            var p2 = new GeoPoint(p2X, p2Y);
            Assert.AreEqual(GeoMath.PointIsAboveTheRay(c, p1, p2), result);
        }

        [TestCase(0, 0, -1, -1, 0, 1, 1, -1, true)]
        [TestCase(0, 0, 1, -1, 0, 1, -1, -1, true)]
        public void IsInPolygonTest(double cX, double cY,
            double p1X, double p1Y, double p2X, double p2Y, double p3X, double p3Y,
            bool result)
        {
            var c = new GeoPoint(cX, cY);
            var triangle = new List<GeoPoint>
            {
                new GeoPoint(p1X, p1Y),
                new GeoPoint(p2X, p2Y),
                new GeoPoint(p3X, p3Y)
            };
            Assert.AreEqual(GeoMath.IsInPolygon(c, triangle), result);
        }
    }
}
