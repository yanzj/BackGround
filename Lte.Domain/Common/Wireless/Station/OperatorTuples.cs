using System;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Complain;

namespace Lte.Domain.Common.Wireless.Station
{
    [EnumTypeDescription(typeof(Operator), Operator.Others)]
    public enum Operator : byte
    {
        Telecom,
        Mobile,
        Unicom,
        Tower,
        None,
        NoTower,
        Others
    }

    public class OperatorDescriptionTransform : DescriptionTransform<Operator>
    {

    }

    public class OperatorTransform : EnumTransform<Operator>
    {
        public OperatorTransform() : base(Operator.Others)
        {
        }
    }

    internal static class OperatorTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(Operator.Telecom, "电信"),
                new Tuple<object, string>(Operator.Mobile, "移动"),
                new Tuple<object, string>(Operator.Unicom, "联通"),
                new Tuple<object, string>(Operator.Tower, "铁塔公司"),
                new Tuple<object, string>(Operator.None, "无机房"),
                new Tuple<object, string>(Operator.NoTower, "无塔桅"),
                new Tuple<object, string>(Operator.Others, "其他")
            };
        }
    }
}
