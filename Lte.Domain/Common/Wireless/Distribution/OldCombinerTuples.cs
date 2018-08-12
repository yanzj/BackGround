using System;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Complain;

namespace Lte.Domain.Common.Wireless.Distribution
{
    [EnumTypeDescription(typeof(OldCombiner), Others)]
    public enum OldCombiner : byte
    {
        CombineP,
        CombineC,
        CombineW,
        CombinePW,
        CombineCW,
        CombinePCW,
        Others
    }

    public class OldCombinerDescriptionTransform : DescriptionTransform<OldCombiner>
    {

    }

    public class OldCombinerTransform : EnumTransform<OldCombiner>
    {
        public OldCombinerTransform() : base(OldCombiner.Others)
        {
        }
    }

    internal static class OldCombinerTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(OldCombiner.CombineP, "合路P"),
                new Tuple<object, string>(OldCombiner.CombineC, "合路C"),
                new Tuple<object, string>(OldCombiner.CombineW, "合路W"),
                new Tuple<object, string>(OldCombiner.CombinePW, "合路P+W"),
                new Tuple<object, string>(OldCombiner.CombineCW, "合路C+W"),
                new Tuple<object, string>(OldCombiner.CombinePCW, "合路P+C+W")
            };
        }
    }
}
