using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common.Types;

namespace Lte.Domain.Common.Wireless
{
    [EnumTypeDescription(typeof(AntennaBeauty), AntennaBeauty.Others)]
    public enum AntennaBeauty : byte
    {
        Single,
        Bianselong,
        Shuixiang,
        Fangsheng,
        Fangzhu,
        Ludeng,
        Guanggaopai,
        Jishu,
        Caoping,
        Kongtiao,
        Shedeng,
        Paiqiguan,
        Bigua,
        Xidingdeng,
        Yuanqiu,
        Weiqiang,
        Reshuiqi,
        Huajia,
        Others
    }

    public class AntennaBeautyDescriptionTransform : DescriptionTransform<AntennaBeauty>
    {

    }

    public class AntennaBeautyTransform : EnumTransform<AntennaBeauty>
    {
        public AntennaBeautyTransform() : base(AntennaBeauty.Others)
        {
        }
    }

    internal static class AntennaBeautyTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(AntennaBeauty.Single, "常规"),
                new Tuple<object, string>(AntennaBeauty.Bianselong, "变色龙"),
                new Tuple<object, string>(AntennaBeauty.Shuixiang, "水箱/水塔"),
                new Tuple<object, string>(AntennaBeauty.Fangsheng, "仿生植物"),
                new Tuple<object, string>(AntennaBeauty.Fangzhu, "方柱/圆柱/烟囱"),
                new Tuple<object, string>(AntennaBeauty.Ludeng, "灯塔/路灯"),
                new Tuple<object, string>(AntennaBeauty.Guanggaopai, "广告牌/标志牌"),
                new Tuple<object, string>(AntennaBeauty.Jishu, "集束天线"),
                new Tuple<object, string>(AntennaBeauty.Caoping, "草坪灯"),
                new Tuple<object, string>(AntennaBeauty.Kongtiao, "空调室外机"),
                new Tuple<object, string>(AntennaBeauty.Shedeng, "室外射灯"),
                new Tuple<object, string>(AntennaBeauty.Paiqiguan, "排气管/排水管"),
                new Tuple<object, string>(AntennaBeauty.Bigua, "壁挂"),
                new Tuple<object, string>(AntennaBeauty.Xidingdeng, "吸顶灯"),
                new Tuple<object, string>(AntennaBeauty.Yuanqiu, "圆球"),
                new Tuple<object, string>(AntennaBeauty.Weiqiang, "围墙/栅栏/围栏"),
                new Tuple<object, string>(AntennaBeauty.Reshuiqi, "太阳能热水器"),
                new Tuple<object, string>(AntennaBeauty.Huajia, "花架")
            };
        }
    }
}
