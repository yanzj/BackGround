using System;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Complain;

namespace Lte.Domain.Common.Wireless.Station
{
    [EnumTypeDescription(typeof(ShareFunction), ShareFunction.Others)]
    public enum ShareFunction : byte
    {
        IndependCarriage,
        ShareCarriage,
        Others
    }

    public class ShareFunctionDescriptionTransform : DescriptionTransform<ShareFunction>
    {

    }

    public class ShareFunctionTransform : EnumTransform<ShareFunction>
    {
        public ShareFunctionTransform() : base(ShareFunction.Others)
        {
        }
    }

    internal static class ShareFunctionTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(ShareFunction.IndependCarriage, "独立载波"),
                new Tuple<object, string>(ShareFunction.ShareCarriage, "共享载波")
            };
        }
    }
}
