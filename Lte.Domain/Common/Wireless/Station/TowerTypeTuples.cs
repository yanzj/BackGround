using System;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Complain;

namespace Lte.Domain.Common.Wireless.Station
{
    [EnumTypeDescription(typeof(TowerType), TowerType.Others)]
    public enum TowerType : byte
    {
        LuodiJiaogang,
        LuodiJingguan,
        LuodiLaxian,
        LuodiDanguan,
        LuodiSanguan,
        LuodiSiguan,
        LuodiSijiao,
        LoudingBaogan,
        LoudingJiaogang,
        LoudingMeihua,
        LoudingLaxian,
        LoudingZuhe,
        LoudingJingguan,
        LoudingDanguan,
        HGan,
        ShuiniGan,
        LudengGan,
        JiankongGan,
        None,
        Others
    }

    public class TowerTypeDescriptionTransform : DescriptionTransform<TowerType>
    {

    }

    public class TowerTypeTransform : EnumTransform<TowerType>
    {
        public TowerTypeTransform() : base(TowerType.Others)
        {
        }
    }

    internal static class TowerTypeTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(TowerType.LuodiJiaogang, "落地角钢塔"),
                new Tuple<object, string>(TowerType.LuodiJingguan, "落地景观塔"),
                new Tuple<object, string>(TowerType.LuodiLaxian, "落地拉线塔"),
                new Tuple<object, string>(TowerType.LuodiDanguan, "落地单管塔"),
                new Tuple<object, string>(TowerType.LuodiSanguan, "落地三管塔"),
                new Tuple<object, string>(TowerType.LuodiSiguan, "落地四管塔"),
                new Tuple<object, string>(TowerType.LuodiSijiao, "落地四角塔"),
                new Tuple<object, string>(TowerType.LoudingBaogan, "楼顶抱杆"),
                new Tuple<object, string>(TowerType.LoudingJiaogang, "楼顶角钢塔"),
                new Tuple<object, string>(TowerType.LoudingMeihua, "楼顶美化天线"),
                new Tuple<object, string>(TowerType.LoudingLaxian, "楼顶拉线塔"),
                new Tuple<object, string>(TowerType.LoudingZuhe, "楼顶组合抱杆"),
                new Tuple<object, string>(TowerType.LoudingJingguan, "楼顶景观塔"),
                new Tuple<object, string>(TowerType.LoudingDanguan, "楼顶单管塔"),
                new Tuple<object, string>(TowerType.HGan, "H杆"),
                new Tuple<object, string>(TowerType.ShuiniGan, "水泥杆"),
                new Tuple<object, string>(TowerType.LudengGan, "路灯杆"),
                new Tuple<object, string>(TowerType.JiankongGan, "监控立杆"),
                new Tuple<object, string>(TowerType.None, "无塔桅"),
                new Tuple<object, string>(TowerType.Others, "其他")
            };
        }
    }
}
