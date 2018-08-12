using System;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Complain;

namespace Lte.Domain.Common.Wireless.Cell
{
    [EnumTypeDescription(typeof(RemoteType), RemoteType.Others)]
    public enum RemoteType : byte
    {
        None,
        Remote,
        Partial,
        Others
    }

    public class RemoteTypeDescriptionTransform : DescriptionTransform<RemoteType>
    {

    }

    public class RemoteTypeTransform : EnumTransform<RemoteType>
    {
        public RemoteTypeTransform() : base(RemoteType.Others)
        {
        }
    }

    internal static class RemoteTypeTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(RemoteType.None, "不拉远"),
                new Tuple<object, string>(RemoteType.Remote, "拉远"),
                new Tuple<object, string>(RemoteType.Partial, "部分天线拉远")
            };
        }
    }
}
