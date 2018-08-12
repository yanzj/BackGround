using System;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Complain;

namespace Lte.Domain.Common.Wireless.Antenna
{
    [EnumTypeDescription(typeof(AntennaDirection), AntennaDirection.Others)]
    public enum AntennaDirection : byte
    {
        Omini,
        Directional,
        Others
    }

    public class AntennaDirectionDescriptionTransform : DescriptionTransform<AntennaDirection>
    {

    }

    public class AntennaDirectionTransform : EnumTransform<AntennaDirection>
    {
        public AntennaDirectionTransform() : base(AntennaDirection.Others)
        {
        }
    }

    internal static class AntennaDirectionTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(AntennaDirection.Omini, "全向"),
                new Tuple<object, string>(AntennaDirection.Directional, "定向")
            };
        }
    }
}
