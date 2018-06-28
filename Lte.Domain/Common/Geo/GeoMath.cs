using System;
using System.Collections.Generic;

namespace Lte.Domain.Common.Geo
{
    public static class GeoMath
    {
        private const double EarthRadius = 6371;

        private const double Eps = 1E-6;
        
        public static double Distance(this IGeoPoint<double> p1, IGeoPoint<double> p2)
        {
            return (EarthRadius * Math.Acos(Math.Sin(p1.Lattitute * (Math.PI / 180)) * Math.Sin(p2.Lattitute * (Math.PI / 180))
                + Math.Cos(p1.Lattitute * (Math.PI / 180)) * Math.Cos(p2.Lattitute * (Math.PI / 180))
                * Math.Cos((p1.Longtitute - p2.Longtitute) * (Math.PI / 180))));
        }

        public static double SimpleDistance(this IGeoPoint<double> p1, IGeoPoint<double> p2)
        {
            return EarthRadius * Math.PI / 180 * Math.Sqrt((p1.Lattitute - p2.Lattitute) * (p1.Lattitute - p2.Lattitute)
                + (p1.Longtitute - p2.Longtitute) * (p1.Longtitute - p2.Longtitute));
        }

        /// <summary>
        /// 将距离（米）转化为经纬度差
        /// </summary>
        /// <param name="distanceInMeter"></param>
        /// <returns></returns>
        public static double GetDegreeInterval(this double distanceInMeter)
        {
            return distanceInMeter * 180 / (EarthRadius * Math.PI * 1000);
        }

        public static double GetDistanceInMeter(this double degreeInterval)
        {
            return degreeInterval * EarthRadius * Math.PI * 1000 / 180;
        }

        public static double PositionAzimuth(this IGeoPoint<double> p, IGeoPoint<double> c)
        {
            return (Math.Abs(p.Lattitute - c.Lattitute) < Eps) ? ((p.Longtitute >= c.Longtitute) ? 90 : 270) :
                180 / Math.PI * Math.Atan2(p.Longtitute - c.Longtitute, p.Lattitute - c.Lattitute);
        }

        public static double AngleBetweenAzimuths(double a1, double a2)
        {
            var diff = Math.Abs(a1 - a2) % 360;
            return (diff <= 180) ? diff : 360 - diff;
        }

        public static double AllAngleBetweenAzimuths(double a1, double a2)
        {
            var diff = (a1 - a2) % 360;
            if (diff <= 180 && diff >= -180) { return diff; }
            if (diff > 180) { return diff - 360; }
            return diff + 360;
        }

        public static IGeoPoint<double> Move(this IGeoPoint<double> origin, double radiusInMeter, double azimuth)
        {
            var degree = radiusInMeter * 180 / 1000 / EarthRadius / Math.Cos(origin.Lattitute * Math.PI / 180);
            return new GeoPoint(origin.Longtitute + degree * Math.Sin(azimuth * Math.PI / 180),
                origin.Lattitute + degree * Math.Cos(azimuth * Math.PI / 180));
        }

        /// <summary>  
        /// 判断点是否在多边形内.  
        /// ----------原理----------  
        /// 注意到如果从P作水平向左的射线的话，如果P在多边形内部，那么这条射线与多边形的交点必为奇数，  
        /// 如果P在多边形外部，则交点个数必为偶数(0也在内)。  
        /// </summary>  
        /// <param name="checkPoint">要判断的点</param>  
        /// <param name="polygonPoints">多边形的顶点</param>  
        /// <returns></returns>  
        public static bool IsInPolygon(this IGeoPoint<double> checkPoint, List<GeoPoint> polygonPoints)
        {
            var pointCount = polygonPoints.Count;
            var transferList = new List<bool>();
            for (int i = 0, j = pointCount - 1; i < pointCount; j = i, i++)
                //第一个点和最后一个点作为第一条线，之后是第一个点和第二个点作为第二条线，之后是第二个点与第三个点，第三个点与第四个点...  
            {
                var p1 = polygonPoints[i];
                var p2 = polygonPoints[j];
                if ((checkPoint.Lattitute > p2.Lattitute && checkPoint.Lattitute > p1.Lattitute) ||
                    (checkPoint.Lattitute < p1.Lattitute && checkPoint.Lattitute < p2.Lattitute)) continue;
                transferList.Add(PointIsAboveTheRay(checkPoint, p1, p2));
                    //射线与多边形交点为奇数时则在多边形之内，若为偶数个交点时则在多边形之外。  
                    //由于inside初始值为false，即交点数为零。所以当有第一个交点时，则必为奇数，则在内部，此时为inside=(!inside)  
                    //所以当有第二个交点时，则必为偶数，则在外部，此时为inside=(!inside)  
            }
            return CalculateTransfer(transferList);
        }

        private static bool CalculateTransfer(List<bool> transferList)
        {
            var trueTransfer = 0;
            var falseTransfer = 0;
            for (var i = 0; i < transferList.Count; i++)
            {
                if (transferList[i] && transferList[(i + 1)%transferList.Count]) trueTransfer++;
                if (!transferList[i] && !transferList[(i + 1)%transferList.Count]) falseTransfer++;
            }
            if (trueTransfer > 0 && trueTransfer%2 == 0) return true;
            if (falseTransfer > 0 && falseTransfer%2 == 0) return true;
            return false;
        }

        public static bool PointIsAboveTheRay(IGeoPoint<double> checkPoint, GeoPoint p1, GeoPoint p2)
        {
            return (checkPoint.Lattitute - p1.Lattitute)*(p2.Longtitute - p1.Longtitute) >
                   (checkPoint.Longtitute - p1.Longtitute)*(p2.Lattitute - p1.Lattitute);
        }
    }
}
