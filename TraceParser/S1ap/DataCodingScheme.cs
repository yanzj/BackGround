using Lte.Domain.Common;
using System;
using Lte.Domain.Common.Types;

namespace TraceParser.S1ap
{
    [Serializable]
    public class DataCodingScheme
    {
        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public string Decode(BitArrayInputStream input)
            {
                return input.ReadBitString(8);
            }
        }
    }
}
