using Lte.Domain.Common;
using System;
using Lte.Domain.Common.Types;

namespace TraceParser.Eutra
{
    [Serializable]
    public class S_TMSI
    {
        public void InitDefaults()
        {
        }

        public string m_TMSI { get; set; }

        public string mmec { get; set; }

        public class PerDecoder
        {
            public static readonly PerDecoder Instance = new PerDecoder();

            public S_TMSI Decode(BitArrayInputStream input)
            {
                S_TMSI s_tmsi = new S_TMSI();
                s_tmsi.InitDefaults();
                s_tmsi.mmec = input.ReadBitString(8);
                s_tmsi.m_TMSI = input.ReadBitString(0x20);
                return s_tmsi;
            }
        }
    }
}
