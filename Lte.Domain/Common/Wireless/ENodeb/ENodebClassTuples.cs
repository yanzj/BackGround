using System;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Complain;

namespace Lte.Domain.Common.Wireless.ENodeb
{
    [EnumTypeDescription(typeof(ENodebClass), Others)]
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
