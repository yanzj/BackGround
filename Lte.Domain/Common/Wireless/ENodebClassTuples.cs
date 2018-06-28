using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common.Types;

namespace Lte.Domain.Common.Wireless
{
    [EnumTypeDescription(typeof(ENodebClass), ENodebClass.Others)]
    public enum ENodebClass : byte
    {
        ClassA,
        ClassB,
        ClassC,
        ClassD,
        Others
    }

    public class ENodebClassDescriptionTransform : DescriptionTransform<ENodebClass>
    {

    }

    public class ENodebClassTransform : EnumTransform<ENodebClass>
    {
        public ENodebClassTransform() : base(ENodebClass.Others)
        {
        }
    }

    internal static class ENodebClassTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(ENodebClass.ClassA, "A"),
                new Tuple<object, string>(ENodebClass.ClassB, "B"),
                new Tuple<object, string>(ENodebClass.ClassC, "C"),
                new Tuple<object, string>(ENodebClass.ClassD, "D")
            };
        }
    }
}
