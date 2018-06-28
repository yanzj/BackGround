using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common.Types;

namespace Lte.Domain.Common.Wireless
{
    [EnumTypeDescription(typeof(ENodebFactory), ENodebFactory.Others)]
    public enum ENodebFactory : byte
    {
        PbO3,
        FeAl,
        Others
    }

    public class ENodebFactoryDescriptionTransform : DescriptionTransform<ENodebFactory>
    {

    }

    public class ENodebFactoryTransform : EnumTransform<ENodebFactory>
    {
        public ENodebFactoryTransform() : base(ENodebFactory.Others)
        {
        }
    }

    internal static class ENodebFactoryTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(ENodebFactory.PbO3, "铅酸电池"),
                new Tuple<object, string>(ENodebFactory.FeAl, "铁铝电池")
            };
        }
    }
}
