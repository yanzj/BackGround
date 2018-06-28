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

    public interface IGeoPointList
    {
        List<GeoPoint> BoundaryGeoPoints { get; set; }
    }

    public class GeoGridPoint : IGeoGridPoint<double>
    {
        public int X { get; set; }

        public int Y { get; set; }

        public double Longtitute => 112 + X * 0.00049;

        public double Lattitute => 22 + Y * 0.00045;
    }

    public class IntRangeContainer
    {
        public int West { get; set; }

        public int East { get; set; }

        public int South { get; set; }

        public int North { get; set; }

        public IntRangeContainer(IEnumerable<List<GeoPoint>> boundaries)
        {
            West = (int)((boundaries.Select(x => x.Select(t => t.Longtitute).Min()).Min() - 112) / 0.00049);
            East = (int)((boundaries.Select(x => x.Select(t => t.Longtitute).Max()).Max() - 112) / 0.00049);
            South = (int)((boundaries.Select(x => x.Select(t => t.Lattitute).Min()).Min() - 22) / 0.00045);
            North = (int)((boundaries.Select(x => x.Select(t => t.Lattitute).Max()).Max() - 22) / 0.00045);
        }
    }

    [TypeDoc("指定扇区查询范围条件")]
    public class SectorRangeContainer
    {
        [MemberDoc("西边经度")]
        public double West { get; set; }

        [MemberDoc("东边经度")]
        public double East { get; set; }

        [MemberDoc("南边纬度")]
        public double South { get; set; }

        [MemberDoc(("北边纬度"))]
        public double North { get; set; }

        [MemberDoc("需要排除的小区列表")]
        public IEnumerable<CellIdPair> ExcludedCells { get; set; }
    }

    public class ENodebRangeContainer
    {
        [MemberDoc("西边经度")]
        public double West { get; set; }

        [MemberDoc("东边经度")]
        public double East { get; set; }

        [MemberDoc("南边纬度")]
        public double South { get; set; }

        [MemberDoc(("北边纬度"))]
        public double North { get; set; }

        public IEnumerable<int> ExcludedIds { get; set; }
    }

    [TypeDoc("小区编号和扇区编号定义 ")]
    public class CellIdPair
    {
        [MemberDoc("小区编号")]
        public int CellId { get; set; }

        [MemberDoc("扇区编号")]
        public byte SectorId { get; set; }
    }

    [TypeDoc("CDMA小区编号和扇区编号定义 ")]
    public class CdmaCellIdPair
    {
        [MemberDoc("小区编号")]
        public int CellId { get; set; }

        [MemberDoc("扇区编号")]
        public byte SectorId { get; set; }

        [MemberDoc("小区类型")]
        public string CellType { get; set; }
    }

    [TypeDoc("基站编号容器")]
    public class ENodebIdsContainer
    {
        [MemberDoc("基站编号列表")]
        public IEnumerable<int> ENodebIds { get; set; }
    }

    public class CollegeENodebIdsContainer
    {
        public string CollegeName { get; set; }

        [MemberDoc("基站编号列表")]
        public IEnumerable<int> ENodebIds { get; set; }
    }

    public class CollegeBtsIdsContainer
    {
        public string CollegeName { get; set; }

        [MemberDoc("基站编号列表")]
        public IEnumerable<int> BtsIds { get; set; }
    }

    [TypeDoc("小区编号容器")]
    public class CellIdsContainer
    {
        [MemberDoc("小区编号列表")]
        public IEnumerable<CellIdPair> CellIdPairs { get; set; }
    }

    [TypeDoc("CDMA小区编号容器")]
    public class CdmaCellIdsContainer
    {
        [MemberDoc("小区编号列表")]
        public IEnumerable<CdmaCellIdPair> CellIdPairs { get; set; }
    }

    public class CollegeCellNamesContainer
    {
        public string CollegeName { get; set; }

        public IEnumerable<string> CellNames { get; set; }
    }
}
