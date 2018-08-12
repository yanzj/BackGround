using System;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Complain;

namespace Lte.Domain.Common.Wireless.Distribution
{
    [EnumTypeDescription(typeof(IndoorNetwork), Others)]
    public enum IndoorNetwork : byte
    {
        LteOnly,
        CdmaOnly,
        LteAndCdma,
        Others
    }

    public class IndoorNetworkDescriptionTransform : DescriptionTransform<IndoorNetwork>
    {

    }

    public class IndoorNetworkTransform : EnumTransform<IndoorNetwork>
    {
        public IndoorNetworkTransform() : base(IndoorNetwork.Others)
        {
        }
    }

    internal static class IndoorNetworkTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(IndoorNetwork.LteOnly, "L网专用"),
                new Tuple<object, string>(IndoorNetwork.CdmaOnly, "C网专用"),
                new Tuple<object, string>(IndoorNetwork.LteAndCdma, "C+L")
            };
        }
    }
}
