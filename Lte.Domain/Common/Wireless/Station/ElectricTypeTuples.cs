﻿using System;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Complain;

namespace Lte.Domain.Common.Wireless.Station
{
    [EnumTypeDescription(typeof(ElectricType), Others)]
    public enum ElectricType : byte
    {
        HighVolte,
        LowVolte,
        Others
    }

    public class ElectricTypeDescriptionTransform : DescriptionTransform<ElectricType>
    {

    }

    public class ElectricTypeTransform : EnumTransform<ElectricType>
    {
        public ElectricTypeTransform() : base(ElectricType.Others)
        {
        }
    }

    internal static class ElectricTypeTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(ElectricType.HighVolte, "高压引电"),
                new Tuple<object, string>(ElectricType.LowVolte, "低压引电")
            };
        }
    }
}
