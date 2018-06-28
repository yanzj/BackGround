using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common.Types;

namespace Lte.Domain.Common.Wireless
{
    [EnumTypeDescription(typeof(CombinerFunction), Others)]
    public enum CombinerFunction : byte
    {
        IndependentDistribution,
        SystematicIndependent,
        TrunkSource,
        Combiner,
        Others
    }

    public class CombinerFunctionDescriptionTransform : DescriptionTransform<CombinerFunction>
    {

    }

    public class CombinerFunctionTransform : EnumTransform<CombinerFunction>
    {
        public CombinerFunctionTransform() : base(CombinerFunction.Others)
        {
        }
    }

    internal static class CombinerFunctionTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(CombinerFunction.IndependentDistribution, "独立分布"),
                new Tuple<object, string>(CombinerFunction.SystematicIndependent, "系统独立"),
                new Tuple<object, string>(CombinerFunction.TrunkSource, "主干信源"),
                new Tuple<object, string>(CombinerFunction.Combiner, "合路")
            };
        }
    }
}
