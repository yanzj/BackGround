using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common.Types;

namespace Lte.Domain.Common.Wireless
{
    [EnumTypeDescription(typeof(CellCoverage), CellCoverage.Others)]
    public enum CellCoverage : byte
    {
        Indoor,
        Outdoor,
        Both,
        Others
    }

    public class CellCoverageDescriptionTransform : DescriptionTransform<CellCoverage>
    {

    }

    public class CellCoverageTransform : EnumTransform<CellCoverage>
    {
        public CellCoverageTransform() : base(CellCoverage.Others)
        {
        }
    }

    internal static class CellCoverageTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(CellCoverage.Outdoor, "室外覆盖"),
                new Tuple<object, string>(CellCoverage.Indoor, "室分覆盖"),
                new Tuple<object, string>(CellCoverage.Both, "室外和室分同时覆盖")
            };
        }
    }
}
