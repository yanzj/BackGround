using System;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Complain;

namespace Lte.Domain.Common.Wireless.Antenna
{
    [EnumTypeDescription(typeof(BatteryType), BatteryType.Others)]
    public enum BatteryType : byte
    {
        PbO3,
        FeAl,
        Others
    }

    public class BatteryTypeDescriptionTransform : DescriptionTransform<BatteryType>
    {

    }

    public class BatteryTypeTransform : EnumTransform<BatteryType>
    {
        public BatteryTypeTransform() : base(BatteryType.Others)
        {
        }
    }

    internal static class BatteryTypeTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(BatteryType.PbO3, "铅酸电池"),
                new Tuple<object, string>(BatteryType.FeAl, "铁铝电池")
            };
        }
    }
}
