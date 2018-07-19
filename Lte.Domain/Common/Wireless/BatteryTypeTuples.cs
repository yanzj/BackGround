using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common.Types;

namespace Lte.Domain.Common.Wireless
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
