using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common.Types;

namespace Lte.Domain.Common.Wireless
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
