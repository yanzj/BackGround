using System;
using Lte.Domain.Common.Types;

namespace Lte.Domain.Common.Wireless.Complain
{
    [EnumTypeDescription(typeof(ComplainScene), Others)]
    public enum ComplainScene : byte
    {
        BetweenCityAndVillage,
        VillageInCity,
        SubRailway,
        TransportationRoutine,
        College,
        CenterOfCity,
        ImportantRegion,
        Residential,
        Others,
        DenseUrban,
        Urban,
        Suburban,
        Rural,
        IndustrialArea
    }

    public class ComplainSceneDescriptionTransform : DescriptionTransform<ComplainScene>
    {

    }

    public class ComplainSceneTransform : EnumTransform<ComplainScene>
    {
        public ComplainSceneTransform() : base(ComplainScene.Others)
        {
        }
    }

    internal static class ComplainSceneTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(ComplainScene.BetweenCityAndVillage, "城乡结合部"),
                new Tuple<object, string>(ComplainScene.VillageInCity, "城中村"),
                new Tuple<object, string>(ComplainScene.SubRailway, "地铁"),
                new Tuple<object, string>(ComplainScene.TransportationRoutine, "交通要道"),
                new Tuple<object, string>(ComplainScene.College, "校园"),
                new Tuple<object, string>(ComplainScene.CenterOfCity, "中心市区"),
                new Tuple<object, string>(ComplainScene.ImportantRegion, "重要区域"),
                new Tuple<object, string>(ComplainScene.Residential, "住宅小区"),
                new Tuple<object, string>(ComplainScene.Residential, "住宅区"),
                new Tuple<object, string>(ComplainScene.Residential, "居民住宅"),
                new Tuple<object, string>(ComplainScene.Others, "其他"),
                new Tuple<object, string>(ComplainScene.DenseUrban, "密集城区"),
                new Tuple<object, string>(ComplainScene.DenseUrban, "商务办公"),
                new Tuple<object, string>(ComplainScene.Urban, "普通城区"),
                new Tuple<object, string>(ComplainScene.Urban, "一般城区"),
                new Tuple<object, string>(ComplainScene.Suburban, "郊区"),
                new Tuple<object, string>(ComplainScene.Suburban, "一般城镇"),
                new Tuple<object, string>(ComplainScene.Rural, "农村"),
                new Tuple<object, string>(ComplainScene.IndustrialArea, "工业区"),
            };
        }
    }
}
