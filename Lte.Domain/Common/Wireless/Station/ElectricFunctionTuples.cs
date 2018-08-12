using System;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Complain;

namespace Lte.Domain.Common.Wireless.Station
{
    [EnumTypeDescription(typeof(ElectricFunction), ElectricFunction.Others)]
    public enum ElectricFunction
    {
        Gongdian,
        Yezhu,
        Others
    }

    public class ElectricFunctionDescriptionTransform : DescriptionTransform<ElectricFunction>
    {
        
    }

    public class ElectricFunctionTransform : EnumTransform<ElectricFunction>
    {
        public ElectricFunctionTransform() : base(ElectricFunction.Others)
        {
        }
    }

    internal static class ElectricFunctionTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(ElectricFunction.Gongdian, "供电引电"),
                new Tuple<object, string>(ElectricFunction.Yezhu, "业主引电")
            };
        }
    }
}
