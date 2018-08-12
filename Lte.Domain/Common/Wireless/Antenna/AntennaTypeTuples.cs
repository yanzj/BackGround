using System;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Complain;

namespace Lte.Domain.Common.Wireless.Antenna
{
    [EnumTypeDescription(typeof(AntennaType), AntennaType.Others)]
    public enum AntennaType : byte
    {
        None,
        Single,
        PowerSplit,
        Cascade,
        PowerSplitAndCaascade,
        Others
    }

    public class AntennaTypeDescriptionTransform : DescriptionTransform<AntennaType>
    {

    }

    public class AntennaTypeTransform : EnumTransform<AntennaType>
    {
        public AntennaTypeTransform() : base(AntennaType.Others)
        {
        }
    }

    internal static class AntennaTypeTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(AntennaType.None, "无天线"),
                new Tuple<object, string>(AntennaType.Single, "单天线"),
                new Tuple<object, string>(AntennaType.PowerSplit, "功分天线"),
                new Tuple<object, string>(AntennaType.Cascade, "RRU级联天线"),
                new Tuple<object, string>(AntennaType.PowerSplitAndCaascade, "含功分和级联天线")
            };
        }
    }
}
