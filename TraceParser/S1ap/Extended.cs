﻿using Lte.Domain.Common;
using System;
using Lte.Domain.Common.Types;

namespace TraceParser.S1ap
{
    [Serializable]
    public class ExtendedRepetitionPeriod
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public long Decode(BitArrayInputStream input)
            {
                int num2 = input.ReadBits(2) + 1;
                input.skipUnreadedBits();
                return input.ReadBits(num2 * 8) + 0x1000;
            }
        }
    }

    [Serializable]
    public class ExtendedRNC_ID
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public long Decode(BitArrayInputStream input)
            {
                int num2 = input.ReadBits(1) + 1;
                input.skipUnreadedBits();
                return input.ReadBits(num2 * 8) + 0x1000;
            }
        }
    }

}
