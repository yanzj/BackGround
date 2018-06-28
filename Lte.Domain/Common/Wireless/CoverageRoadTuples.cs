using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common.Types;

namespace Lte.Domain.Common.Wireless
{
    [EnumTypeDescription(typeof(CoverageRoad), CoverageRoad.Others)]
    public enum CoverageRoad : byte
    {
        City,
        County,
        HighspeedRailway,
        HighspeedRoad,
        Railway,
        Subway,
        SeaRoute,
        StateRoad,
        ProvinceRoad,
        CountyRoad,
        VillageRoad,
        None,
        Water,
        Others
    }

    public class CoverageRoadDescriptionTransform : DescriptionTransform<CoverageRoad>
    {

    }

    public class CoverageRoadTransform : EnumTransform<CoverageRoad>
    {
        public CoverageRoadTransform() : base(CoverageRoad.Others)
        {
        }
    }

    internal static class CoverageRoadTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(CoverageRoad.City, "市区"),
                new Tuple<object, string>(CoverageRoad.County, "县城"),
                new Tuple<object, string>(CoverageRoad.HighspeedRailway, "高铁"),
                new Tuple<object, string>(CoverageRoad.HighspeedRoad, "高速"),
                new Tuple<object, string>(CoverageRoad.Railway, "铁路"),
                new Tuple<object, string>(CoverageRoad.Subway, "地铁"),
                new Tuple<object, string>(CoverageRoad.SeaRoute, "航道"),
                new Tuple<object, string>(CoverageRoad.StateRoad, "国道"),
                new Tuple<object, string>(CoverageRoad.ProvinceRoad, "省道"),
                new Tuple<object, string>(CoverageRoad.CountyRoad, "县道"),
                new Tuple<object, string>(CoverageRoad.VillageRoad, "乡道"),
                new Tuple<object, string>(CoverageRoad.None, "无"),
                new Tuple<object, string>(CoverageRoad.Water, "水运")
            };
        }
    }
}
