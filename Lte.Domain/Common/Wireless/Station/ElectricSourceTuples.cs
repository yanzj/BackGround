using System;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Complain;

namespace Lte.Domain.Common.Wireless.Station
{
    [EnumTypeDescription(typeof(ElectricSource), Others)]
    public enum ElectricSource
    {
        Dc,
        Ac,
        Others
    }

    public class ElectricSourceDescriptionTransform : DescriptionTransform<ElectricSource>
    {

    }

    public class ElectricSourceTransform : EnumTransform<ElectricSource>
    {
        public ElectricSourceTransform() : base(ElectricSource.Others)
        {
        }
    }

    internal static class ElectricSourceTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(ElectricSource.Dc, "直流"),
                new Tuple<object, string>(ElectricSource.Ac, "交流")
            };
        }
    }
}
