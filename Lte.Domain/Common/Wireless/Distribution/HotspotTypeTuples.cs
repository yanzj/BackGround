using System;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Complain;

namespace Lte.Domain.Common.Wireless.Distribution
{
    [EnumTypeDescription(typeof(HotspotType), Others)]
    public enum HotspotType : byte
    {
        College,
        Hospital,
        ShoppingMall,
        Building,
        Transportation,
        TopPrecise,
        Others,
        Highway,
        Railway,
        Subway,
        Downtown,
        AreaDef,
        Tourist,
        Entertainment,
        Stadium,
        Marketing,
        VegetableMarket
    }

    public class HotspotTypeDescriptionTransform : DescriptionTransform<HotspotType>
    {

    }

    public class HotspotTypeTransform : EnumTransform<HotspotType>
    {
        public HotspotTypeTransform() : base(HotspotType.Others)
        {
        }
    }

    internal static class HotspotTypeTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(HotspotType.Building, "楼宇"),
                new Tuple<object, string>(HotspotType.College, "校园网"),
                new Tuple<object, string>(HotspotType.Hospital, "医院"),
                new Tuple<object, string>(HotspotType.ShoppingMall, "高流量商务区"),
                new Tuple<object, string>(HotspotType.TopPrecise, "TOP小区"),
                new Tuple<object, string>(HotspotType.Transportation, "交通枢纽"),
                new Tuple<object, string>(HotspotType.Others, "其他"),
                new Tuple<object, string>(HotspotType.Highway, "高速公路"),
                new Tuple<object, string>(HotspotType.Railway, "高速铁路"),
                new Tuple<object, string>(HotspotType.Downtown, "高密度住宅区"),
                new Tuple<object, string>(HotspotType.Subway, "地铁"),
                new Tuple<object, string>(HotspotType.AreaDef, "区域定义"), //11
                new Tuple<object, string>(HotspotType.Tourist, "旅游景点"), 
                new Tuple<object, string>(HotspotType.Entertainment, "餐饮娱乐"), 
                new Tuple<object, string>(HotspotType.Stadium, "场馆"), 
                new Tuple<object, string>(HotspotType.Marketing, "专业市场"), 
                new Tuple<object, string>(HotspotType.VegetableMarket, "农贸市场"), 
            };
        }
    }
}
