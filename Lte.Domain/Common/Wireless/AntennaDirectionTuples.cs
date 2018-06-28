using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common.Types;

namespace Lte.Domain.Common.Wireless
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
