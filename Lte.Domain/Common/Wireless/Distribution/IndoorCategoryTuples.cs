using System;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Complain;

namespace Lte.Domain.Common.Wireless.Distribution
{
    [EnumTypeDescription(typeof(IndoorCategory), IndoorCategory.Others)]
    public enum IndoorCategory : byte
    {
        Government,
        Shiye,
        Hotel,
        Xiuxianyule,
        Daxinggouwu,
        Square,
        JuleiMarket,
        ComercialBuilding,
        Hospital,
        Telecom,
        College,
        Exihibition,
        XiuxianDujia,
        ChangtuStation,
        RailwayStation,
        AirPort,
        Harbour,
        Stadium,
        Factory,
        Subway,
        SpecialZone,
        Residence,
        GaotieTunnel,
        GaosuTunnel,
        OtherTunnel,
        Others
    }

    public class IndoorCategoryDescriptionTransform : DescriptionTransform<IndoorCategory>
    {

    }

    public class IndoorCategoryTransform : EnumTransform<IndoorCategory>
    {
        public IndoorCategoryTransform() : base(IndoorCategory.Others)
        {
        }
    }

    internal static class IndoorCategoryTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(IndoorCategory.Government, "政府机关"),
                new Tuple<object, string>(IndoorCategory.Shiye, "事业单位"),
                new Tuple<object, string>(IndoorCategory.Hotel, "宾馆酒店"),
                new Tuple<object, string>(IndoorCategory.Xiuxianyule, "休闲娱乐"),
                new Tuple<object, string>(IndoorCategory.Daxinggouwu, "大型购物"),
                new Tuple<object, string>(IndoorCategory.Square, "广场"),
                new Tuple<object, string>(IndoorCategory.JuleiMarket, "聚类市场"),
                new Tuple<object, string>(IndoorCategory.ComercialBuilding, "商务楼"),
                new Tuple<object, string>(IndoorCategory.Hospital, "医院"),
                new Tuple<object, string>(IndoorCategory.Telecom, "电信自有"),
                new Tuple<object, string>(IndoorCategory.College, "校园"),
                new Tuple<object, string>(IndoorCategory.Exihibition, "会展中心"),
                new Tuple<object, string>(IndoorCategory.XiuxianDujia, "休闲度假区"),
                new Tuple<object, string>(IndoorCategory.ChangtuStation, "长途汽车站"),
                new Tuple<object, string>(IndoorCategory.RailwayStation, "火车站"),
                new Tuple<object, string>(IndoorCategory.AirPort, "机场"),
                new Tuple<object, string>(IndoorCategory.Harbour, "码头"),
                new Tuple<object, string>(IndoorCategory.Stadium, "体育场馆"),
                new Tuple<object, string>(IndoorCategory.Factory, "厂房"),
                new Tuple<object, string>(IndoorCategory.Subway, "地铁"),
                new Tuple<object, string>(IndoorCategory.SpecialZone, "特殊区域：包括电梯、停车场、地下室其他"),
                new Tuple<object, string>(IndoorCategory.Residence, "住宅小区"),
                new Tuple<object, string>(IndoorCategory.GaotieTunnel, "高铁隧道"),
                new Tuple<object, string>(IndoorCategory.GaosuTunnel, "高速隧道"),
                new Tuple<object, string>(IndoorCategory.OtherTunnel, "其他隧道")
            };
        }
    }
}
