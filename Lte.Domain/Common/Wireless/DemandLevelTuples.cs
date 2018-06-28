using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common.Types;

namespace Lte.Domain.Common.Wireless
{
    [EnumTypeDescription(typeof(DemandLevel), LevelB)]
    public enum DemandLevel : byte
    {
        LevelA,
        LevelB,
        LevelC
    }

    public class DemandLevelDescriptionTransform : DescriptionTransform<DemandLevel>
    {
    }

    public class DemandLevelTransform : EnumTransform<DemandLevel>
    {
        public DemandLevelTransform() : base(DemandLevel.LevelB)
        {
        }
    }

    internal static class DemandLevelTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(DemandLevel.LevelA, "A级"),
                new Tuple<object, string>(DemandLevel.LevelB, "B级"),
                new Tuple<object, string>(DemandLevel.LevelC, "C级"),
            };
        }
    }
}
