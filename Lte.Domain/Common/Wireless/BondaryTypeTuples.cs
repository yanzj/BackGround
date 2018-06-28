using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common.Types;

namespace Lte.Domain.Common.Wireless
{
    [EnumTypeDescription(typeof(BoundaryType), BoundaryType.Others)]
    public enum BoundaryType : byte
    {
        InterProvince,
        IntraProvince,
        InterAndIntraProvince,
        International,
        None,
        Others
    }

    public class BoundaryTypeDescriptionTransform : DescriptionTransform<BoundaryType>
    {

    }

    public class BoundaryTypeTransform : EnumTransform<BoundaryType>
    {
        public BoundaryTypeTransform() : base(BoundaryType.Others)
        {
        }
    }

    internal static class BoundaryTypeTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(BoundaryType.InterProvince, "省际"),
                new Tuple<object, string>(BoundaryType.IntraProvince, "省内"),
                new Tuple<object, string>(BoundaryType.InterAndIntraProvince, "省内/省际"),
                new Tuple<object, string>(BoundaryType.International, "国际"),
                new Tuple<object, string>(BoundaryType.None, "无")
            };
        }
    }
}
