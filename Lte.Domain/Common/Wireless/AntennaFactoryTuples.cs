using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common.Types;

namespace Lte.Domain.Common.Wireless
{
    [EnumTypeDescription(typeof(AntennaFactory), Unkown)]
    public enum AntennaFactory : byte
    {
        Rfs,
        Andrew,
        Anjiexin,
        Guoren,
        Jingxin,
        Indoor,
        Unkown,
        Hengxin,
        Huawei,
        Mobi,
        Rihai,
        Tongyu,
        Zte
    }

    public class AntennaFactoryDescriptionTransform : DescriptionTransform<AntennaFactory>
    {

    }

    public class AntennaFactoryTransform : EnumTransform<AntennaFactory>
    {
        public AntennaFactoryTransform() : base(AntennaFactory.Unkown)
        {
        }
    }

    internal static class AntennaFactoryTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(AntennaFactory.Rfs, "RFS"),
                new Tuple<object, string>(AntennaFactory.Andrew, "安德鲁"),
                new Tuple<object, string>(AntennaFactory.Anjiexin, "安捷信"),
                new Tuple<object, string>(AntennaFactory.Guoren, "国人"),
                new Tuple<object, string>(AntennaFactory.Jingxin, "京信"),
                new Tuple<object, string>(AntennaFactory.Indoor, "室分"),
                new Tuple<object, string>(AntennaFactory.Indoor, "室内"),
                new Tuple<object, string>(AntennaFactory.Hengxin, "亨鑫"),
                new Tuple<object, string>(AntennaFactory.Huawei, "华为"),
                new Tuple<object, string>(AntennaFactory.Mobi, "摩比"),
                new Tuple<object, string>(AntennaFactory.Rihai, "日海"),
                new Tuple<object, string>(AntennaFactory.Tongyu, "通宇"),
                new Tuple<object, string>(AntennaFactory.Zte, "中兴")
            };
        }
    }
}
