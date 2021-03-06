﻿using System;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Complain;

namespace Lte.Domain.Common.Wireless.Region
{
    [EnumTypeDescription(typeof(TestType), TestType.Others)]
    public enum TestType : byte
    {
        Cqt,
        FeelingTest,
        VoLte,
        WebPage,
        Stream,
        Others,
        Silence,
        Explosion
    }

    public class TestTypeDescriptionTransform : DescriptionTransform<TestType>
    {

    }

    public class TestTypeTransform : EnumTransform<TestType>
    {
        public TestTypeTransform() : base(TestType.Others)
        {
        }
    }

    internal static class TestTypeTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(TestType.Cqt, "CQT"),
                new Tuple<object, string>(TestType.FeelingTest, "大众版感知测试"),
                new Tuple<object, string>(TestType.VoLte, "VoLte"),
                new Tuple<object, string>(TestType.WebPage, "网页测试（大众版）"),
                new Tuple<object, string>(TestType.Stream, "视频测试（大众版）"),
                new Tuple<object, string>(TestType.Others, "其他"),
                new Tuple<object, string>(TestType.Silence, "静默测试"), 
                new Tuple<object, string>(TestType.Explosion, "定点引爆"), 
            };
        }
    }
}
