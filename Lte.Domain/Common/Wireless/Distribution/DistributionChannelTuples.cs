using System;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Complain;

namespace Lte.Domain.Common.Wireless.Distribution
{
    [EnumTypeDescription(typeof(DistributionChannel), Others)]
    public enum DistributionChannel : byte
    {
        Single,
        Double,
        Others
    }

    public class DistributionChannelDescriptionTransform : DescriptionTransform<DistributionChannel>
    {

    }

    public class DistributionChannelTransform : EnumTransform<DistributionChannel>
    {
        public DistributionChannelTransform() : base(DistributionChannel.Others)
        {
        }
    }

    internal static class DistributionChannelTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(DistributionChannel.Single, "单通道"),
                new Tuple<object, string>(DistributionChannel.Double, "双通道")
            };
        }
    }
}
