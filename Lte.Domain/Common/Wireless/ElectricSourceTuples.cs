using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common.Types;

namespace Lte.Domain.Common.Wireless
{
    [EnumTypeDescription(typeof(ElectricSource), ElectricSource.Others)]
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
