using System;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Complain;

namespace Lte.Domain.Common.Wireless.ENodeb
{
    [EnumTypeDescription(typeof(Duplexing), Duplexing.Others)]
    public enum Duplexing : byte
    {
        Fdd,
        Tdd,
        DoubleMode,
        Others
    }

    public class DuplexingDescriptionTransform : DescriptionTransform<Duplexing>
    {

    }

    public class DuplexingTransform : EnumTransform<Duplexing>
    {
        public DuplexingTransform() : base(Duplexing.Others)
        {
        }
    }

    internal static class DuplexingTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(Duplexing.Fdd, "FDD"),
                new Tuple<object, string>(Duplexing.Tdd, "TDD"),
                new Tuple<object, string>(Duplexing.DoubleMode, "FT双模")
            };
        }
    }
}
