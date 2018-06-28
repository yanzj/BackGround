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
        Huawei,
        Zte,
        Bell,
        Erisson,
        Nokia,
        Datang,
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
                new Tuple<object, string>(BatteryType.Huawei, "华为"),
                new Tuple<object, string>(BatteryType.Zte, "中兴"),
                new Tuple<object, string>(BatteryType.Bell, "贝尔"),
                new Tuple<object, string>(BatteryType.Erisson, "爱立信"),
                new Tuple<object, string>(BatteryType.Nokia, "诺基亚"),
                new Tuple<object, string>(BatteryType.Datang, "大唐")
            };
        }
    }
}
