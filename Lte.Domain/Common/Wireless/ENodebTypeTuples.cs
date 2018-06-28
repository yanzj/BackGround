﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common.Types;

namespace Lte.Domain.Common.Wireless
{
    [EnumTypeDescription(typeof(ENodebType), ENodebType.Others)]
    public enum ENodebType : byte
    {
        Macro,
        Micro,
        Pico,
        Femto,
        Others
    }

    public class ENodebTypeDescriptionTransform : DescriptionTransform<ENodebType>
    {

    }

    public class ENodebTypeTransform : EnumTransform<ENodebType>
    {
        public ENodebTypeTransform() : base(ENodebType.Others)
        {
        }
    }

    internal static class ENodebTypeTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(ENodebType.Macro, "BBU+RRU"),
                new Tuple<object, string>(ENodebType.Micro, "微基站"),
                new Tuple<object, string>(ENodebType.Pico, "微微基站"),
                new Tuple<object, string>(ENodebType.Femto, "家庭基站")
            };
        }
    }
}
