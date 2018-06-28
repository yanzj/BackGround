using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common.Types;

namespace Lte.Domain.Common.Wireless
{
    [EnumTypeDescription(typeof(CoverageArea),CoverageArea.Others)]
    public enum CoverageArea : byte
    {
        DenseCity,
        City,
        County,
        Suburb,
        PlainVillage,
        WaterVillage,
        HillVillage,
        MountainVillage,
        VillageInCity,
        Others
    }

    public class CoverageAreaDescriptionTransform : DescriptionTransform<CoverageArea>
    {

    }

    public class CoverageAreaTransform : EnumTransform<CoverageArea>
    {
        public CoverageAreaTransform() : base(CoverageArea.Others)
        {
        }
    }

    internal static class CoverageAreaTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(CoverageArea.DenseCity, "密集市区"),
                new Tuple<object, string>(CoverageArea.City, "市区"),
                new Tuple<object, string>(CoverageArea.County, "县城"),
                new Tuple<object, string>(CoverageArea.Suburb, "郊区"),
                new Tuple<object, string>(CoverageArea.PlainVillage, "平原农村"),
                new Tuple<object, string>(CoverageArea.WaterVillage, "水域农村"),
                new Tuple<object, string>(CoverageArea.HillVillage, "丘陵农村"),
                new Tuple<object, string>(CoverageArea.MountainVillage, "山区农村"),
                new Tuple<object, string>(CoverageArea.VillageInCity, "城中村")
            };
        }
    }
}
